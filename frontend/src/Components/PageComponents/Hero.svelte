<script lang="ts">
    import { goto } from '$app/navigation';
    import MainLogo from '../SVG/MainLogo.svelte';
    import Button from '../UI/Button.svelte';
    import './Hero.css';


</script>

<div class="hero">
    <div class="hero-content">
        <MainLogo height="150px" />

        <h1 class="hero-title">Build. Deploy. Dominate.</h1>
        <p class="hero-subtitle">
            Firepower for whatever you’re building next.
        </p>

        <div class="hero-actions">
            <Button usePadding={false} on:click={()=> {goto('/login')}}><span style="padding-left: 5px;"></span>Login</Button>

            <Button 
                buttonClass={'stayWhite'} 
                usePadding={false} 
                on:click={() => {
                    const el = document.getElementById('pstart');
                    if (el) el.scrollIntoView({ behavior: 'smooth' });
                }}
            >
                <span style="padding-left: 5px;"></span>Read More
            </Button>

        </div>
    </div>
</div>
<div id="pstart"></div>
<div style="height: 15px;"></div>