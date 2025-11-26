<script lang="ts">
    import './Navigation.css'
    import Button from './UI/Button.svelte';
    import { goto } from '$app/navigation';
    import { onMount } from 'svelte';
    import Herosvg from './SVG/herosvg.svelte';
    import Whiteclosesvg from './SVG/whiteclosesvg.svelte';
    import MainLogo from './SVG/MainLogo.svelte'
    import { navStyle } from '../stores/NavStore';
    import OrangeLogo from './SVG/OrangeLogo.svelte';
    import { page } from '$app/stores';
    import Menusvg from './SVG/menusvg.svelte';
    import { tick } from 'svelte';


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
                {#if !scrolled && $navStyle.class == 'transparent'}
                    <div class="deskML">
                        <OrangeLogo height={'45px'}></OrangeLogo>
                    </div>
                    <div class="mobileML">
                        <OrangeLogo height={'35px'}></OrangeLogo>
                    </div>
                {:else}

                    {#if mobileOpen}
                        <div class="deskML">
                            <OrangeLogo height={'45px'}></OrangeLogo>
                        </div>
                        <div class="mobileML">
                            <OrangeLogo height={'35px'}></OrangeLogo>
                        </div>
                    {:else}
                        <div class="deskML">
                            <MainLogo height={'45px'}></MainLogo>
                        </div>
                        <div class="mobileML">
                            <MainLogo height={'35px'}></MainLogo>
                        </div>
                    {/if}
      
                {/if}
            </button>
            <div class="sacText" class:mobileOpenText={mobileOpen}>
                <p class="st SA" >S.A. Construction</p>
                <p class="st numby"><span>(248) 388-8771</span></p> 
            </div>
            

        </div>
    
        <div class="navRight">
            <div class="links">
                <a href="/" class:activeNavLink={$page.url.pathname === '/'}>Home</a>
                <a href="/roofing" class:activeNavLink={$page.url.pathname === '/roofing'}>Roofing</a>
                <a href="/gutters" class:activeNavLink={$page.url.pathname === '/gutters'}>Gutters</a>
                <a href="/siding-trim" class:activeNavLink={$page.url.pathname === '/siding-trim'}>Siding & Trim</a>
                
                <div style="margin-left: 25px;">
                    <Button on:click={(e)=> goto('/estimate')}>Free Estimate</Button>
                </div>
            </div>
            {#if !mobileOpen}
                <button id="MobileMenuBtn" on:click={toggleMobile}>
                    <Menusvg color={scrolled || $navStyle.class == 'bright' ? '#000000' : '#ffffff'}></Menusvg>
                </button>
            {:else}
                <button id="wrapCloser" on:click={toggleMobile}>
                    <Whiteclosesvg></Whiteclosesvg>
                </button>
            {/if}

        </div>
    
        
    </div>

</div>



<button class="mobileMenu" class:openMobile={mobileOpen} on:click={toggleMobile}>
    <div class="holdHeroBack">
        <Herosvg uniqueId='mobilenavver'></Herosvg>
    </div>
    <div class="mobileHead">

    </div>
    <div class="mobileBody">

        <a class="mobileMenuLink" class:mobileActiveLink={$page.url.pathname === '/'}  href="/">Home</a>
        <a class="mobileMenuLink" class:mobileActiveLink={$page.url.pathname === '/roofing'} href="/roofing">Roofing</a>
        <a class="mobileMenuLink" class:mobileActiveLink={$page.url.pathname === '/gutters'}  href="/gutters">Gutters</a>
        <a class="mobileMenuLink" class:mobileActiveLink={$page.url.pathname === '/siding-trim'}  href="/siding-trim">Siding & Trim</a>
        <a class="mobileMenuLink" href="/estimate" class:mobileActiveLink={$page.url.pathname === '/estimate'} >Get Free Estimate</a>

    </div>
</button>

<div class="ghostNav"></div>
