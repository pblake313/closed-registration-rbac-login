import { writable } from "svelte/store";
import { accessToken } from "./TokenStore";
import { authenticatedUser } from "./UserStore";
import { goto } from "$app/navigation";
import { allUserAccounts, allUserFetchError, hasFetchedAllAccounts, isFetchingAllAccounts } from "./DashboardStores/AllAccountsStore";

export const autoLoginAttempted = writable<boolean>(false)

export async function autoLogin() {

    
}


export async function logout() {
    //still need to clear the cookie


    accessToken.set(null)
    authenticatedUser.set(null)
    goto('/login')


    // reset all stores...
    allUserAccounts.set([])
    allUserFetchError.set(null)
    isFetchingAllAccounts.set(false)
    hasFetchedAllAccounts.set(false)
    
}