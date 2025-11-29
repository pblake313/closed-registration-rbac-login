<script lang="ts">
    import AddIcon from '../Icons/AddIcon.svelte';
    import LogoutIcon from '../Icons/LogoutIcon.svelte';
    import TrashIcon from '../Icons/TrashIcon.svelte';
    import './Button.css';

    export let icon: 'add' | 'logout' | 'trash' | null = null;
    export let iconColor: string = '#ffffff';
    export let buttonClass: 'primary' | 'secondary' | 'darkback' | 'stayWhite' | 'stayDark' | 'danger' = 'primary';
    export let fullWidth: boolean = false;
    export let usePadding: boolean = true;
    export let shaking: boolean = false; // 🔥 new
    export let disabled: boolean = false
</script>

<button
    class="saButton {buttonClass}"
    class:fuller={fullWidth}
    class:noPad={!usePadding}
    class:shake={shaking}  
    disabled={disabled}
    on:click
>
    <div class="flexInsideButton">

        {#if icon}
            <div class="containIcon">
                {#if icon === 'add'}
                    <div class="singleIconWrapper" style="margin-top: 2px;">
                        <AddIcon height={'15px'} color={iconColor} />
                    </div>
                {:else if icon === 'logout'}
                    <div class="singleIconWrapper"  style="margin-top: 2px;">
                        <LogoutIcon color={iconColor} height={'15px'}></LogoutIcon>
                    </div>
                {:else if icon === 'trash'}
                    <div class="singleIconWrapper"  style="margin-top: 2px;">
                        <TrashIcon color={iconColor} height={'15px'}></TrashIcon>
                    </div>
                {/if}
            </div>
        {/if}

        <slot></slot>
    </div>
</button>
