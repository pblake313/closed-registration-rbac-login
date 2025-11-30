import type { User } from "$lib/types/User";
import { get, writable } from "svelte/store";
import { protectedFetch } from "../../fetchers/protectedFetch";

export const hasFetchedAllAccounts = writable<boolean>(false)
export const isFetchingAllAccounts = writable<boolean>(false)
export const allUserAccounts = writable<User[]>([])
export const allUserFetchError = writable<string | null>(null)

export async function fetchAllAccounts() {

    // if we have fetched accounts, then just return...

    try {

        if (get(hasFetchedAllAccounts) === true) {
            return;
        }


        isFetchingAllAccounts.set(true)

        const response = await protectedFetch('/Admin/Get-Users', {
            method: 'GET'
        })


        for (let index = 0; index < response.length; index++) {
            const user = response[index];
            let userObj: User = {
                UserId: user.userId,
                Email: user.email,
                FirstName: user.firstName,
                LastName: user.lastName,
                UpdatedAt: user.updatedAt,
                DateCreated: user.dateCreated,
                LastLogin: user.lastLogin,
                LastAutoLogin: user.lastAutoLogin,
                LastPasswordResetEmailSentAt: user.lastPasswordResetEmailSentAt,
                permissions: {
                    JobPostings: user.jobPostings,
                    AccountManagement: user.accountManagement,
                    ViewCandidates: user.viewCandidates
                }
            }

            upsertUserIntoAllUserAccounts(userObj)

        }
        hasFetchedAllAccounts.set(true) // only mark on success.

    } catch (err: any) {
        // if backend told us to force logout, protectedFetch already did it.
        // 🚫 protectedFetch already logged out + navigated
        if (err?.data?.forceLogout){
            return
        }


        allUserFetchError.set(
            err.errorMessage || err.message || 'An unknown error has occurred.'
        );
    } finally {
        isFetchingAllAccounts.set(false)
    }
    
}

export function upsertUserIntoAllUserAccounts(user: User) {
    allUserAccounts.update((current) => {
        const index = current.findIndex((u) => u.UserId === user.UserId);

        let updated: User[];

        if (index === -1) {
            // insert
            updated = [...current, user];
        } else {
            // update
            updated = [...current];
            updated[index] = { ...current[index], ...user };
        }

        // 🔥 Sort by userId (newest first)
        updated.sort((a, b) => {
            const idA = a.UserId ?? 0;
            const idB = b.UserId ?? 0;
            return idB - idA;
        });

        return updated;
    });
}

export function removeUserFromAccountsArray(userId: number) {
    if (userId == null) return;

    allUserAccounts.update((current) =>
        current.filter((u) => u.UserId !== userId)
    );
}
