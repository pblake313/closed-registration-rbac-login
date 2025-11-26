import type { BreadCrumb } from "$lib/types/breadcrumb";
import { writable } from "svelte/store";

export const breadcrumbs = writable<BreadCrumb[]>([])

export function setCrumbArray(crumbArray: BreadCrumb[]){
    breadcrumbs.set(crumbArray)
}