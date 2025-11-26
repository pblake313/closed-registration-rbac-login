<script lang="ts">
    import CloseIcon from "../Icons/CloseIcon.svelte";
    import './CloseButton.css'
    // callback prop from parent
    export let onClose: (() => void) | undefined;
</script>

<button class="closeButton" on:click={() => onClose?.()}>
    <div class="wrapCloseIcon">
        <CloseIcon height={'20px'} color={'#323232'}/>
    </div>
</button>
