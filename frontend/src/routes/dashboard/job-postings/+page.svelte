<script lang="ts">
    import { onMount } from "svelte";
    import { setCrumbArray } from "../../../stores/DashboardStores/BreadCrumbStore";
    import { jobPostCrumbs } from "$lib/BreadCrumbData";
    import { authenticatedUser } from "../../../stores/UserStore";
    import Button from "../../../Components/UI/Button.svelte";
    import Loader from "../../../Components/UI/Loaders/Loader.svelte";
    import { protectedFetch } from "../../../fetchers/protectedFetch";
    import { goto } from "$app/navigation";

    onMount(()=> {
        setCrumbArray(jobPostCrumbs)

        if (!$authenticatedUser?.permissions.JobPostings){
            goto('/dashboard')
        }

    })

    let loading: boolean = false
    let jobPostTestMessage: string | null = null


    async function testCandidates() {
        try {
            loading = true
            const response = await protectedFetch('/JobPostings/Test', {
                method: 'GET'
            })

            jobPostTestMessage = 'Success! You have access to view anything on the /JobPostings route in the backend.'

        } catch (err: any) {

            jobPostTestMessage = err?.data?.message || 'An unknown error has occurred.'
        } finally {
            loading = false
        }
        
    }

</script>
{#if $authenticatedUser?.permissions.JobPostings}
    <h3>Job Postings</h3>
    <p>Job Postings Works</p>

    {#if loading}
        <Loader></Loader>
    {:else}

        <Button on:click={testCandidates}>Test Job Postings</Button>
    {/if}

    <br><br>

    {#if jobPostTestMessage}
        <p>{jobPostTestMessage}</p> 
    {/if}

{/if}
