<script lang="ts">
    import { onMount } from "svelte";
    import { setCrumbArray } from "../../stores/DashboardStores/BreadCrumbStore";
    import { dashCrumbs } from "$lib/BreadCrumbData";
    import { authenticatedUser } from "../../stores/UserStore";
    import { goto } from "$app/navigation";

    onMount(()=> {
        setCrumbArray(dashCrumbs)

        if (!$authenticatedUser){
            goto('/login')
        }
    })
</script>

<!-- required for layout to work. -->

<p>If you see this, you have general access. Any account that is logged in can see this page.</p>
