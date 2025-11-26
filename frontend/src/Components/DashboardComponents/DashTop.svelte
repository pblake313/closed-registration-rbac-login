<script lang="ts">
    import Button from '../UI/Button.svelte';
    import './DashTop.css';

    export let buttonText: string = 'Enter `buttonText`';
    export let useButton: boolean = true;
    export let dashHeading: string = 'Enter `dashHeading`'

    // Svelte 5 event-callback-style prop
    export let dashButtonClicked: (() => void) | undefined;

    function dashTopClicked() {
        // Call it if passed from parent
        dashButtonClicked?.();
    }
</script>




<div class="containDashTop">
    <div class="dashTopTitle">
        <h3>{dashHeading}</h3>
    </div>
    {#if useButton}
        <div class="containDashTopButton">
            <Button buttonClass={'stayDark'} usePadding={false} icon={'add'} on:click={dashTopClicked}>{buttonText}</Button>
        </div>
    {/if}


</div>
