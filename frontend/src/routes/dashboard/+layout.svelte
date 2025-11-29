<script lang="ts">
    import { goto } from '$app/navigation';
    import BreadCrumbs from '../../Components/DashboardComponents/BreadCrumbs.svelte';
import DashboardNav from '../../Components/DashboardNav.svelte';
    import { authenticatedUser } from '../../stores/UserStore';
    import './DashboardPage.css'
        import { browser } from '$app/environment';
    import Button from '../../Components/UI/Button.svelte';
    import { logout } from '../../stores/AuthStore';
    import MainLogo from '../../Components/SVG/MainLogo.svelte';

    $: if (browser && $authenticatedUser === null) {
        goto('/login');
    }
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


