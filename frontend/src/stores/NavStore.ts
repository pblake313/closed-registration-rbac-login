import { writable } from "svelte/store";

export const navStyle = writable<{
    class: 'transparent' | 'bright', 
}>({
    class: 'transparent',
});
