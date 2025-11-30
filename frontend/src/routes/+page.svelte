<script>
    import { onMount } from "svelte";
    import { navStyle } from "../stores/NavStore";
    import Navigation from "../Components/Navigation.svelte";
    import Hero from "../Components/PageComponents/Hero.svelte";
    import ProjectDetails from "../Components/PageComponents/ProjectDetails.svelte";
    
    onMount(()=>{
        navStyle.set({class: 'transparent'})
    })


</script>

<svelte:head>
    <title>RBAC Project</title>
</svelte:head>


<Navigation></Navigation>

<Hero></Hero>

<ProjectDetails></ProjectDetails>
