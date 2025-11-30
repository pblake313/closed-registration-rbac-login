<script lang="ts">
    import './Navigation.css'
    import Button from './UI/Button.svelte';
    import { goto } from '$app/navigation';
    import { onMount } from 'svelte';
    import MainLogo from './SVG/MainLogo.svelte'
    import { navStyle } from '../stores/NavStore';
    import { page } from '$app/stores';
    import Menusvg from './SVG/menusvg.svelte';
    import { authenticatedUser } from '../stores/UserStore';


    let scrolled: boolean = false
    let mobileOpen = false


    onMount(() => {
        function handleScroll() {
            scrolled = window.scrollY > 0;
        }

        window.addEventListener('scroll', handleScroll);
        handleScroll(); // call once on load

        return () => {
            window.removeEventListener('scroll', handleScroll);
        };
    });


    function toggleMobile (){
        mobileOpen = !mobileOpen
    }


    function goHome(){
        mobileOpen = false
        goto('/'); 
    }


</script>


<!-- nav classes are "bright" and "transparent" -->
<div class="fullWidth {$navStyle.class}" class:scrolled={scrolled && !mobileOpen}>
    <div class="wrapNav ">
        <div class="navLeft">
            <button class="homeButton" on:click={(e)=> {goto('/'), mobileOpen = false}}>
          
                <div class="deskML">
                    <MainLogo height={'35px'}></MainLogo>
                </div>
                <div class="mobileML">
                    <MainLogo height={'35px'}></MainLogo>
                </div>
    
            </button>

        </div>
    
        <div class="navRight">
            <div class="links">
                <a href="/" class:activeNavLink={$page.url.pathname === '/'}>Home</a>

                <div style="margin-left: 15px;">
                    {#if $authenticatedUser}
                        <Button usePadding={false} on:click={(e)=> goto('/dashboard')}>Dashboard</Button>
                    {:else}
                        <Button usePadding={false} on:click={(e)=> goto('/login')}>Login</Button>
                    {/if}
                </div>
                
      
            </div>
            {#if !mobileOpen}
                <button id="MobileMenuBtn" on:click={toggleMobile}>
                    <Menusvg color={scrolled || $navStyle.class == 'bright' ? '#000000' : '#ffffff'}></Menusvg>
                </button>
            {:else}
                <button id="wrapCloser" on:click={toggleMobile}>
                    x
                </button>
            {/if}

        </div>
    
        
    </div>

</div>



<button class="mobileMenu" class:openMobile={mobileOpen} on:click={toggleMobile}>

    <div class="mobileHead">

    </div>
    <div class="mobileBody">

        <a class="mobileMenuLink" class:mobileActiveLink={$page.url.pathname === '/'}  href="/">Home</a>

    </div>
</button>

<div class="ghostNav"></div>
