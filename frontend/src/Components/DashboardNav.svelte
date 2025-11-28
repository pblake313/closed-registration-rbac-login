<script lang="ts">
    import './DashboardNav.css';
    import MainLogo from './SVG/MainLogo.svelte';
    import { page } from '$app/stores';

</script>

<a href="/" class="containHomeNav">
    <div class="loginNavFlex">
        <div class="wrapDashNavLogo">
            <MainLogo height={'100%'} />
        </div>
        <div class="empText">
            <h1 style="font-size: 12pt;">SA Portal</h1>
        </div>
    </div>
</a>

<br>

<div class="containNavLink">
        <a href="/dashboard" class="dashNavLink" class:active={$page.url.pathname === '/dashboard'}> Dashboard</a>
</div>

<div class="containNavLink">
    <!-- any route starting with /dashboard/accounts -->
    <a href="/dashboard/accounts" class="dashNavLink" class:active={$page.url.pathname.startsWith('/dashboard/accounts')}>Accounts</a>
</div>

<div class="containNavLink">
    <!-- any route starting with /dashboard/candidates -->
    <a href="/dashboard/candidates" class="dashNavLink" class:active={$page.url.pathname.startsWith('/dashboard/candidates')}> Candidates</a>
</div>

<div class="containNavLink">
    <!-- any route starting with /dashboard/job-postings -->
    <a href="/dashboard/job-postings" class="dashNavLink" class:active={$page.url.pathname.startsWith('/dashboard/job-postings')}>Job Postings</a>
</div>
