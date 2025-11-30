<script>
    import { onMount } from 'svelte';
    import Notifications from '../Components/Notifications.svelte';
    import FullPageLoader from '../Components/UI/Loaders/FullPageLoader.svelte';
    import { autoLogin, isAutoLoggingIn } from '../stores/AuthStore';
	import './styles.css';
    import { fade } from 'svelte/transition';

    onMount(async ()=> {
        await autoLogin();

    })

</script>




<Notifications></Notifications>

{#if $isAutoLoggingIn}
    <FullPageLoader></FullPageLoader>
{:else}
    <div in:fade={{duration: 500, delay: 250}}>
        <slot />
    </div>
{/if}