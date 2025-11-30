import { writable } from "svelte/store";
import { accessToken } from "./TokenStore";
import { authenticatedUser } from "./UserStore";
import { goto } from "$app/navigation";
import { allUserAccounts, allUserFetchError, hasFetchedAllAccounts, isFetchingAllAccounts } from "./DashboardStores/AllAccountsStore";
import { protectedFetch } from "../fetchers/protectedFetch";
import type { User } from "$lib/types/User";
import { publicFetch } from "../fetchers/PublicFetch";

export const autoLoginAttempted = writable<boolean>(false)


export const isAutoLoggingIn = writable<boolean>(false)


export async function autoLogin(){
    try {
        isAutoLoggingIn.set(true)

        const user = await getAuthenticatedUser()


    } catch (err) {
        console.log(err)

    } finally {
        isAutoLoggingIn.set(false)
    }
}


export async function getAuthenticatedUser(){
    try {
        const response = await protectedFetch('/Account/Get-Authenticated-User', {
            method: 'GET'
        })

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
        }

        authenticatedUser.set(authUser)

    } catch (err) {
        return null
    }
}

export async function logout() {
    //still need to clear the cookie


    try {


        accessToken.set(null)
        authenticatedUser.set(null)


        // reset all stores...
        allUserAccounts.set([])
        allUserFetchError.set(null)
        isFetchingAllAccounts.set(false)
        hasFetchedAllAccounts.set(false)

        const response = await publicFetch('/Auth/Logout', {method: 'GET'})

        console.log(response)

    } catch (err) {
        console.log(err)

    } finally {
        goto('/login')

    }


    
}