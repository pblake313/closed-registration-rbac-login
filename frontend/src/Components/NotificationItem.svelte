<script lang="ts">
    import { fade } from 'svelte/transition';
    import './NotificationItem.css'
    import CloseIcon from './Icons/CloseIcon.svelte';
    import { removeNotification, type Notification } from '../stores/NotificationStore';

    export let notification: Notification;

    function handleClose() {
        removeNotification(notification.id);
    }
</script>

<div class={`notification notification--${notification.type}`} in:fade={{duration: 300}} out:fade={{duration: 300}}>
    <div class="insideNotificationFlex">

        <div class={`notification-content note-${notification.type}`}>
            <h4 class="notification-title">{notification.title}</h4>
            <p class="notification-message">{notification.message}</p>
        </div>

        <button class="closeJoint" on:click={handleClose}>
            <div class="wrapCloseIcc">
                <CloseIcon color={'#323232'} height={'100%'}></CloseIcon>
            </div>
        </button>

    </div>

</div>
