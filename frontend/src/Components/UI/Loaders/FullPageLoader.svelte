<script lang="ts">
    import { fade } from 'svelte/transition';
    import './FullPageLoader.css'
    import MainLogo from '../../SVG/MainLogo.svelte';
    import Loader from './Loader.svelte';

    export let loadingText: string | null = null


</script>

<div class="fullpageLoaderWrapper" out:fade={{duration: 500, delay: 250}}>
    <div class="insideLoad">
        <MainLogo height={'180px'}></MainLogo>
        <br><br>
        <Loader></Loader>
        {#if loadingText}
            <p style="font-size: 10pt; text-align: center;">{loadingText}</p>
        {/if}
        
    </div>
</div>