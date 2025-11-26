<script lang="ts">
    import { page } from '$app/stores';
    import { onMount } from 'svelte';
    import { setCrumbArray } from '../../../../stores/DashboardStores/BreadCrumbStore';
    import BreadCrumbs from '../../../../Components/DashboardComponents/BreadCrumbs.svelte';

    // define the shape of a single crumb
    type Crumb = {
        route: string;
        crumbText: string;
    };

    $: userId = $page.params.userId;

    onMount(() => {
        setCrumbs();
    });

    function setCrumbs() {
        const crumbArray: Crumb[] = [
            {
                route: '/dashboard/accounts',
                crumbText: 'Accounts'
            },
            {
                route: `/dashboard/accounts/${userId}`,
                crumbText: `User ID : ${userId}`
            }
        ];

        console.log(crumbArray);
        setCrumbArray(crumbArray);
    }
</script>

<p>User ID: {userId}</p>
