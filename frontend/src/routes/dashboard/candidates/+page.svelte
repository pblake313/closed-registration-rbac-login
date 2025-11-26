<script lang="ts">
    import { onMount } from "svelte";
    import { setCrumbArray } from "../../../stores/DashboardStores/BreadCrumbStore";
    import { candidateCrumbs } from "$lib/BreadCrumbData";
    import DashTop from "../../../Components/DashboardComponents/DashTop.svelte";
    import { pushNotification } from "../../../stores/DashboardStores/NotificationStore";


    onMount(() => {
        setCrumbArray(candidateCrumbs)
    })

</script>

<DashTop useButton={false} dashButtonClicked={() => {}} dashHeading={'Candidates'}></DashTop>

<p>Candidates Works</p>
