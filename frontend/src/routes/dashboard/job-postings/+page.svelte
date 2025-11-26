<script lang="ts">
    import { onMount } from "svelte";
    import { setCrumbArray } from "../../../stores/DashboardStores/BreadCrumbStore";
    import { jobPostCrumbs } from "$lib/BreadCrumbData";

    onMount(()=> {
        setCrumbArray(jobPostCrumbs)
    })
</script>

<p>Job Postings Works</p>