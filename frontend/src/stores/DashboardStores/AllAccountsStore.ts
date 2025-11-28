import type { User } from "$lib/types/User";
import { get, writable } from "svelte/store";
import { adminFetch } from "../../fetchers/AdminFetch";

export const hasFetchedAllAccounts = writable<boolean>(false)
export const isFetchingAllAccounts = writable<boolean>(false)
export const allUserAccounts = writable<User[]>([])
export const allUserFetchError = writable<string | null>(null)

export async function fetchAllAccounts() {

    // if we have fetched accounts, then just return...

    try {
        if (get(hasFetchedAllAccounts)) {
            return;
        }

        isFetchingAllAccounts.set(true)

        const response = await adminFetch('/Admin/Get-Users', {
            method: 'GET'
        })

        console.log(response)

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
            const idA = a.userId ?? 0;
            const idB = b.userId ?? 0;
            return idB - idA;
        });

        return updated;
    });
}
