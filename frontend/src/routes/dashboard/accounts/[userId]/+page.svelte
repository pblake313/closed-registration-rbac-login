<script lang="ts">
    import { page } from '$app/stores';
    import { onMount } from 'svelte';
    import { setCrumbArray } from '../../../../stores/DashboardStores/BreadCrumbStore';
    import BreadCrumbs from '../../../../Components/DashboardComponents/BreadCrumbs.svelte';
    import { protectedFetch } from '../../../../fetchers/protectedFetch';
    import type { User } from '$lib/types/User';
    import Loader from '../../../../Components/UI/Loaders/Loader.svelte';
    import FormError from '../../../../Components/UI/FormError.svelte';
    import EditUserForm from '../../../../Components/Forms/DashboadForms/EditUserForm.svelte';
    import DashTop from '../../../../Components/DashboardComponents/DashTop.svelte';

    // define the shape of a single crumb
    type Crumb = {
        route: string;
        crumbText: string;
    };

    $: userId = $page.params.userId;

    let isFetchingUser: boolean = false
    let fetchErrorMessage: string | null = null
    let userToEdit: User | null = null

    onMount( async () => {

        await getUserToEdit()

        setCrumbs();
    });


    async function getUserToEdit() {
        try {
            isFetchingUser = true

            const res = await protectedFetch(`/Admin/Get-User/${userId}`)

            console.log(res)

            userToEdit = {
                UserId: res.userId,
                Email: res.email,
                FirstName: res.firstName,
                LastName: res.lastName,
                DateCreated: res.dateCreated,
                UpdatedAt: res.updatedAt,
                LastPasswordResetEmailSentAt: res.lastPasswordResetEmailSentAt,
                LastLogin: res.LastLogin,
                permissions : {
                    JobPostings: res.jobPostings,
                    AccountManagement: res.accountManagement,
                    ViewCandidates: res.viewCandidates
                }
            }
        } catch {
            
        } finally{
            isFetchingUser = false
        }
    }

    function setCrumbs() {
        const crumbArray: Crumb[] = [
            {
                route: '/dashboard/accounts',
                crumbText: 'Accounts'
            },
            {
                route: `/dashboard/accounts/${userId}`,
                crumbText: `${ userToEdit?.FirstName + ' ' + userToEdit?.LastName || userId}`
            }
        ];

        setCrumbArray(crumbArray);
    }
</script>


{#if isFetchingUser}
    <Loader></Loader>

{:else}


    {#if fetchErrorMessage}
        <FormError
            errorMessage={fetchErrorMessage}
            errorTitle={'Fetch User Error'}
        ></FormError>
    {:else}
        <EditUserForm {userToEdit}></EditUserForm>
    {/if}
    
    
{/if}
