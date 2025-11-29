<script lang="ts">
    import { goto } from "$app/navigation";
    import { onMount } from "svelte";
    import DashTop from "../../../Components/DashboardComponents/DashTop.svelte";
    import { setCrumbArray } from "../../../stores/DashboardStores/BreadCrumbStore";
    import { accountCrumbs } from "$lib/BreadCrumbData";
    import { allUserAccounts, allUserFetchError, fetchAllAccounts, isFetchingAllAccounts } from "../../../stores/DashboardStores/AllAccountsStore";
    import Loader from "../../../Components/UI/Loaders/Loader.svelte";
    import FormError from "../../../Components/UI/FormError.svelte";
    import AccountItem from "../../../Components/DashboardComponents/ListItems/AccountItem.svelte";
    import AccountItemsHeader from "../../../Components/DashboardComponents/ListHeaders/AccountItemsHeader.svelte";
    import { authenticatedUser } from "../../../stores/UserStore";

    onMount(async () => {
        setCrumbArray(accountCrumbs)

        try {
            await fetchAllAccounts()
        } catch {

        }
   
    })

</script>

<DashTop  
    dashHeading={'Accounts'}
    buttonText={'Create User'}
    dashButtonClicked={() => goto('/dashboard/accounts/create-user')}
></DashTop>


{#if $authenticatedUser?.permissions.AccountManagement}

    {#if $allUserFetchError}
        <FormError centerAlign={true} errorTitle={'Fetch Accounts Error'} errorMessage={$allUserFetchError}></FormError>
    {/if}


    {#if !$isFetchingAllAccounts}

            {#if $allUserAccounts.length <= 0}
                <p>No Accounts</p>
            {:else}
                <AccountItemsHeader></AccountItemsHeader>
                {#each $allUserAccounts as user}
                    <AccountItem {user}></AccountItem>
                {/each}
                <div style="height: 50px;"></div>

            {/if}

    {:else}
        <Loader></Loader>
    {/if}

{/if}
