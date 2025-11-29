<script lang="ts">
    import { onMount } from "svelte";
    import { setCrumbArray } from "../../stores/DashboardStores/BreadCrumbStore";
    import { dashCrumbs } from "$lib/BreadCrumbData";
    import Button from "../../Components/UI/Button.svelte";
    import { protectedFetch } from "../../fetchers/protectedFetch";

    onMount(()=> {
        setCrumbArray(dashCrumbs)
    })

    async function tryOut() {
        try {
            const response = await protectedFetch('/Account/Get-Authenticated-User', {
                method: 'GET'
            })

            console.log(response)
        } catch (err: any){
            console.log(err)
        }
    }

</script>

<!-- required for layout to work. -->

<p>Dashboard Page Works</p>

<Button on:click={tryOut}>Test</Button>