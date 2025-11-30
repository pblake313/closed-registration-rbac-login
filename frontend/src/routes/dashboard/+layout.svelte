<script lang="ts">
    import { goto } from '$app/navigation';
    import { browser } from '$app/environment';
    import { onMount } from 'svelte';
    import { get } from 'svelte/store';

    import BreadCrumbs from '../../Components/DashboardComponents/BreadCrumbs.svelte';
    import DashboardNav from '../../Components/DashboardNav.svelte';
    import MainLogo from '../../Components/SVG/MainLogo.svelte';

    import { authenticatedUser } from '../../stores/UserStore';
    import { autoLogin } from '../../stores/AuthStore';

    import './DashboardPage.css';

    onMount(async () => {
        if (!browser) return;

        // already logged in? stay here
        if (get(authenticatedUser)) return;

        // try auto-login (once)
        const user = await autoLogin();

        // if still no user, bounce
        if (!user) {
            goto('/login');
        }
    });
</script>
{#if $authenticatedUser}
    <div class="dashboardFlex">

        <div class="dashNavigation">
            <DashboardNav></DashboardNav>
        </div>
        <div></div>

        <div class="dashMainContent">
    

            <div class="containMainJoint">
                <div class="mainJointBar">
                    <div class="insideJointBar">
                        
                        <div class="breadCrumbSide">
                            <BreadCrumbs></BreadCrumbs>
                        </div>
                        
                        <div class="moreActionSide">
                            <a href="/" class="containHomeNav">
                                <div class="loginNavFlex">
                                    <div class="wrapDashNavLogo">
                                        <MainLogo height={'100%'} />
                                    </div>
                                </div>
                            </a>
                        </div>
                    </div>
                </div>

                <slot />
                    
            </div>
        </div>
    </div>

{/if}


