<script lang="ts">
    import BreadCrumbs from '../../Components/DashboardComponents/BreadCrumbs.svelte';
import DashboardNav from '../../Components/DashboardNav.svelte';
    import './DashboardPage.css'
</script>


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
                        <p>Logout etc...</p>
                    </div>
                </div>
            </div>

            <slot />

        </div>
    </div>
</div>

