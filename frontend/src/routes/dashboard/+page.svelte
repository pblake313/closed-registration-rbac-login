<script lang="ts">
    import { onMount } from "svelte";
    import { setCrumbArray } from "../../stores/DashboardStores/BreadCrumbStore";
    import { dashCrumbs } from "$lib/BreadCrumbData";

    onMount(()=> {
        setCrumbArray(dashCrumbs)
    })
</script>

<!-- required for layout to work. -->

<p>If you see this, you have general access. Any account that is logged in can see this page.</p>
