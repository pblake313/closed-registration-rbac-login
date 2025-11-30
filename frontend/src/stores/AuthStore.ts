import { get, writable } from "svelte/store";
import { accessToken } from "./TokenStore";
import { authenticatedUser } from "./UserStore";
import { goto } from "$app/navigation";
import {
    allUserAccounts,
    allUserFetchError,
    hasFetchedAllAccounts,
    isFetchingAllAccounts
} from "./DashboardStores/AllAccountsStore";
import { protectedFetch } from "../fetchers/protectedFetch";
import type { User } from "$lib/types/User";
import { publicFetch } from "../fetchers/PublicFetch";

export const autoLoginAttempted = writable<boolean>(false);
export const isAutoLoggingIn = writable<boolean>(false);

export async function autoLogin(): Promise<User | null> {
    // 🔒 Hard guard: if we've already tried, don't do it again
    if (get(autoLoginAttempted)) {
        return get(authenticatedUser) ?? null;
    }

    autoLoginAttempted.set(true);
    isAutoLoggingIn.set(true);

    try {
        const user = await getAuthenticatedUser();
        return user;
    } catch (err) {
        console.error("autoLogin error:", err);
        return null;
    } finally {
        isAutoLoggingIn.set(false);
    }
}

export async function getAuthenticatedUser(): Promise<User | null> {
    try {
        const response = await protectedFetch("/Account/Get-Authenticated-User", {
            method: "GET"
        });

        const authUser: User = {
            UserId: response.userId,
            Email: response.email,
            DateCreated: response.dateCreated,
            FirstName: response.firstName,
            LastName: response.lastName,
            LastPasswordResetEmailSentAt: response.lastPasswordResetEmailSentAt,
            UpdatedAt: response.updatedAt,
            LastAutoLogin: response.lastAutoLogin,
            LastLogin: response.lastLogin,
            permissions: {
                ViewCandidates: response.viewCandidates,
                AccountManagement: response.accountManagement,
                JobPostings: response.jobPostings
            }
        };

        authenticatedUser.set(authUser);
        return authUser;
    } catch (err) {
        console.error("getAuthenticatedUser failed:", err);
        authenticatedUser.set(null);
        return null;
    }
}

export async function logout() {
    try {
        accessToken.set(null);
        authenticatedUser.set(null);

        // reset all stores...
        allUserAccounts.set([]);
        allUserFetchError.set(null);
        isFetchingAllAccounts.set(false);
        hasFetchedAllAccounts.set(false);

        // reset auto-login state for next time
        autoLoginAttempted.set(false);
        isAutoLoggingIn.set(false);

        const response = await publicFetch("/Auth/Logout", { method: "GET" });
        console.log(response);
    } catch (err) {
        console.log(err);
    } finally {
        goto("/login");
    }
}
