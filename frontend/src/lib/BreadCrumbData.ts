import type { BreadCrumb } from "./types/breadcrumb";

export let dashCrumbs: BreadCrumb[] = [
    {
        route: '/dashboard',
        crumbText: 'Dashboard'
    }
]
export let accountCrumbs: BreadCrumb[] = [
    {
        route: '/dashboard/accounts',
        crumbText: 'Accounts'
    }
]
export let createAccountCrumbs: BreadCrumb[] = [
    {
        route: '/dashboard',
        crumbText: 'Dashboard'
    }
]

export let candidateCrumbs: BreadCrumb[] = [
    {
        route: '/dashboard/candidates',
        crumbText: 'Candidates'
    }
]
export let jobPostCrumbs: BreadCrumb[] = [
    {
        route: '/dashboard/job-postings',
        crumbText: 'Job Postings'
    }
]