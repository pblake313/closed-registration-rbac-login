import type { User } from "$lib/types/User";
import { writable } from "svelte/store";
import { adminFetch } from "../../fetchers/AdminFetch";

export const hasFetchedAllAccounts = writable<boolean>(false)
export const isFetchingAllAccounts = writable<boolean>(false)
export const allUserAccounts = writable<User[]>([])
export const allUserFetchError = writable<string | null>(null)

export async function fetchAllAccounts() {

    // if we have fetched accounts, then just return...

    try {
        isFetchingAllAccounts.set(true)

        const response = await adminFetch('/Admin/Get-Users', {
            method: 'GET'
        })

        for (let index = 0; index < response.length; index++) {
            const user = response[index];
            let userObj: User = {
                userId: user.userId,
                Email: user.email,
                FirstName: user.firstName,
                LastName: user.lastName,
                UpdatedAt: user.updatedAt,
                DateCreated: user.dateCreated,
                permissions: {
                    JobPostings: user.jobPostings,
                    AccountManagement: user.accountManagement,
                    ViewCandidates: user.viewCandidates
                }
            }

            upsertUserIntoAllUserAccounts(userObj)

        }

    } catch (err: any) {
        console.log(err)
        allUserFetchError.set(err.errorMessage || err.message || 'An unknown error has occurred.')
    } finally {
        isFetchingAllAccounts.set(false)
        hasFetchedAllAccounts.set(true)
    }
    
}


export function upsertUserIntoAllUserAccounts(user: User) {
    allUserAccounts.update((current) => {
        const index = current.findIndex((u) => u.userId === user.userId);

        if (index === -1) {
            // user not found → insert
            return [...current, user];
        }

        // user found → update
        const updated = [...current];
        updated[index] = {
            ...current[index],
            ...user
        };

        return updated;
    });
}
