import { writable } from "svelte/store";

export const adIdUsed = writable<string | null>(null);
