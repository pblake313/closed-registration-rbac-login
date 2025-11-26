
<script lang="ts">
    import { onMount } from "svelte";

    export let height = '25px';
    export let color: string = '#ffffff'
    
    let uniqueId = '';
    onMount(()=>{
      uniqueId = `logo-${Math.random().toString(36).substring(2, 9)}`;

    })
</script>

<svg {height} viewBox="0 0 20 20" xmlns="http://www.w3.org/2000/svg" fill="none">
    <path fill="{color}" fill-rule="evenodd" d="M19 4a1 1 0 01-1 1H2a1 1 0 010-2h16a1 1 0 011 1zm0 6a1 1 0 01-1 1H2a1 1 0 110-2h16a1 1 0 011 1zm-1 7a1 1 0 100-2H2a1 1 0 100 2h16z"/>
</svg>