<script lang="ts">
    import HomeIcon from "../Icons/HomeIcon.svelte";
    import './BreadCrumbs.css'
    import FolderNextIcon from "../Icons/FolderNextIcon.svelte";
    import { page } from '$app/stores';
    import { breadcrumbs } from "../../stores/BreadCrumbStore";
    $page.url.pathname
</script>


<div class="breadCrumbsWrapper">
    <div class="crumbStart"></div>
    <a href="/dashboard" class="homeIconWrap">
        <HomeIcon height={'25px'}></HomeIcon>
    </a>
    {#each $breadcrumbs as crumb, i}
        <div class="holdFolder">
            <FolderNextIcon></FolderNextIcon>
        </div>
        <a 
            class="crumbLink" 
            class:isLast={i === $breadcrumbs.length - 1} 
            href="{crumb.route}"
            class:activeCrumb={$page.url.pathname === crumb.route}
        >{crumb.crumbText}</a>
    {/each}

</div>