<script>
    import { goto } from '$app/navigation';
    import Button from '../../UI/Button.svelte';
    import './Hero.css'

    
</script>



<!-- replaced slide two of both the mobile version and desktop version -->

<div class="heroWrap">
    <img class="heroImage" src="/Images/Roofing/roofers.jpg" alt="">
    <div class="heroOverlay"></div>
    <div class="heroGradient"></div>
    <div class="mobileGradient"></div>
    <div class="heroInfo">
        <h1 class="heroText">Sustainable roofing, siding, trim and gutter services.</h1>
        <p class="heroSubText">Repeat Five Star Reviews</p>
        <div class="wrapHeroButton">
            <Button on:click={(e)=> {goto('/estimate')}}>Free Estimate</Button>
        </div>
    </div>
</div>