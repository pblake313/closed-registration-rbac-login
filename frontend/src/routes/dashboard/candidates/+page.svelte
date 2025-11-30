<script lang="ts">
    import { onMount } from "svelte";
    import { candidateCrumbs } from "$lib/BreadCrumbData";
    import DashTop from "../../../Components/DashboardComponents/DashTop.svelte";
    import Button from "../../../Components/UI/Button.svelte";
    import Loader from "../../../Components/UI/Loaders/Loader.svelte";
    import { protectedFetch } from "../../../fetchers/protectedFetch";
    import { authenticatedUser } from "../../../stores/UserStore";
    import { goto } from "$app/navigation";
    import { setCrumbArray } from "../../../stores/BreadCrumbStore";


    let loading: boolean = false
    let testingCandidateMessage: string | null = null


    onMount(() => {
        setCrumbArray(candidateCrumbs)
        if (!$authenticatedUser?.permissions.ViewCandidates){
            goto('/dashboard')
        }
    })

    async function testCandidates() {
        try {
            loading = true
            const response = await protectedFetch('/Candidates/Test', {
                method: 'GET'
            })

            testingCandidateMessage = 'Success! You have access to view anything on the /Candidates route in the backend.'

        } catch (err: any) {

            testingCandidateMessage = err?.data?.message || 'An unknown error has occurred.'
        } finally {
            loading = false
        }
        
    }

</script>

<DashTop useButton={false} dashButtonClicked={() => {}} dashHeading={'Candidates'}></DashTop>


{#if $authenticatedUser?.permissions.ViewCandidates}
    <p>Candidates Works</p>

    {#if loading}
        <Loader></Loader>
    {:else}
        <br>
        <Button usePadding={false} on:click={testCandidates}>Test Candidates</Button>
    {/if}

    <br><br>

    {#if testingCandidateMessage}
        <p>{testingCandidateMessage}</p> 
    {/if}

{/if}

