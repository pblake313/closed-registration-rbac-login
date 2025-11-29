<script lang="ts">
    import './DashboardNav.css';
    import MainLogo from './SVG/MainLogo.svelte';
    import { page } from '$app/stores';
    import { authenticatedUser } from '../stores/UserStore';
    import Button from './UI/Button.svelte';
    import { logout } from '../stores/AuthStore';
    import { toOneLetter } from '../utils/textFormatter';

</script>




<div class="navTop">

    {#if $authenticatedUser}
        <a href="/dashboard" class="authUserButton">

            <div class="circleJoint">
                <h4 class="initialsText">{toOneLetter($authenticatedUser.FirstName)}{toOneLetter($authenticatedUser.LastName)}</h4>
            </div>
            <div class="containUserEmail">

                <p>{$authenticatedUser.Email}</p>
            </div>
        </a>
    {/if}


    <div class="containNavLink">
            <a href="/dashboard" class="dashNavLink" class:active={$page.url.pathname === '/dashboard'}> Dashboard</a>
    </div>

    {#if $authenticatedUser?.permissions.AccountManagement}
        <div class="containNavLink">
            <!-- any route starting with /dashboard/accounts -->
            <a href="/dashboard/accounts" class="dashNavLink" class:active={$page.url.pathname.startsWith('/dashboard/accounts')}>Accounts</a>
        </div>
    {/if}


    {#if $authenticatedUser?.permissions.ViewCandidates}
        <div class="containNavLink">
            <!-- any route starting with /dashboard/candidates -->
            <a href="/dashboard/candidates" class="dashNavLink" class:active={$page.url.pathname.startsWith('/dashboard/candidates')}> Candidates</a>
        </div>
    {/if}

    {#if $authenticatedUser?.permissions.JobPostings}
        <div class="containNavLink">
            <!-- any route starting with /dashboard/job-postings -->
            <a href="/dashboard/job-postings" class="dashNavLink" class:active={$page.url.pathname.startsWith('/dashboard/job-postings')}>Job Postings</a>
        </div>
    {/if}
</div>


<Button fullWidth={true} iconColor={'#000'} on:click={logout} icon={'logout'} usePadding={false} buttonClass={'stayWhite'}>Logout</Button>
 