<script lang="ts">
    import AddIcon from '../Icons/AddIcon.svelte';
    import LogoutIcon from '../Icons/LogoutIcon.svelte';
    import './Button.css';

    export let icon: 'add' | 'logout' | null = null;
    export let iconColor: string = '#ffffff';
    export let buttonClass: 'primary' | 'secondary' | 'darkback' | 'stayWhite' | 'stayDark' = 'primary';
    export let fullWidth: boolean = false;
    export let usePadding: boolean = true;
    export let shaking: boolean = false; // 🔥 new
</script>

<button
    class="saButton {buttonClass}"
    class:fuller={fullWidth}
    class:noPad={!usePadding}
    class:shake={shaking}  
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
                        <LogoutIcon color={'#fff'} height={'15px'}></LogoutIcon>
                    </div>
                {/if}
            </div>
        {/if}

        <slot></slot>
    </div>
</button>
