<script lang="ts">
    import { notificationArray } from '../stores/NotificationStore';
    import NotificationItem from './NotificationItem.svelte';
    import './Notifications.css';

    let showWide = false;
    let timeoutId: any = null;

    $: {
        const count = $notificationArray.length;

        if (count > 0) {
            // Immediately go wide
            showWide = true;

            // Clear any pending shrink timeout
            clearTimeout(timeoutId);
        } else {
            // Delay shrink by 1 second
            clearTimeout(timeoutId);
            timeoutId = setTimeout(() => {
                showWide = false;
            }, 300); 
        }
    }
</script>

<div class="notificationContainer" class:wideContain={showWide}>
    {#each $notificationArray as notification (notification.id)}
        <NotificationItem {notification} />
    {/each}
</div>
