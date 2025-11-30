import { writable } from "svelte/store";

export interface Notification {
    id: number;
    title: string;
    message: string;
    type: 'error' | 'neutral' | 'warn' | 'success';
    manualClose: boolean;
}

export const notificationArray = writable<Notification[]>([]);

let nextId = 1;

export function pushNotification(
    title: string,
    message: string,
    type: 'error' | 'neutral' | 'warn' | 'success' = 'neutral',
    durationSeconds: number = 4,
    manualClose: boolean = false
) {
    const id = nextId++;

    const newNotification: Notification = {
        id,
        title,
        message,
        type,
        manualClose
    };

    // add to store
    notificationArray.update((arr) => [...arr, newNotification]);

    // auto-remove if not manual-close
    if (!manualClose) {
        setTimeout(() => {
            notificationArray.update((arr) =>
                arr.filter((n) => n.id !== id)
            );
        }, durationSeconds * 1000);
    }
}

export function removeNotification(id: number) {
    notificationArray.update((arr) => arr.filter((n) => n.id !== id));
}
