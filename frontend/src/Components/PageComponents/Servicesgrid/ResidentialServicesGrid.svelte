<script lang="ts">
    import { onMount } from 'svelte';
    import 'swiper/css';
    import 'swiper/css/navigation';
    import './ResidentialServicesGrid.css'
    import SwiperButton from '../../UI/SwiperButton.svelte';
    import BasicText from '../BasicText/BasicText.svelte';
    import Button from '../../UI/Button.svelte';
    import { goto } from '$app/navigation';
  
    let swiperContainer: HTMLDivElement;
    let Swiper;
    let swiperInstance: any;
  
    let isBeginning = true;
    let isEnd = false;
  
    onMount(async () => {
    const swiperModule = await import('swiper');
    Swiper = swiperModule.default;

    swiperInstance = new Swiper(swiperContainer, {
        loop: false,
        slidesPerView: 3,
        breakpoints: {
                0: {
                    slidesPerView: 1.2,
                    spaceBetween: 10,

                },
                600: {
                    slidesPerView: 2,
                    spaceBetween: 10,

                },
                1100: {
                    slidesPerView: 3,
                    spaceBetween: 25,

                }
        },
        on: {
            reachBeginning: () => {
            isBeginning = true;
            isEnd = false;
            },
            reachEnd: () => {
            isEnd = true;
            isBeginning = false;
            },
            fromEdge: () => {
            isBeginning = false;
            isEnd = false;
            }
        },

        });
    });
    function prevslide() {
        swiperInstance?.slidePrev();
        updateEdgeStates();
    }

    function nextslide() {
        swiperInstance?.slideNext();
        updateEdgeStates();
    }

    function updateEdgeStates() {
        setTimeout(() => {
            isBeginning = swiperInstance.isBeginning;
            isEnd = swiperInstance.isEnd;
        }, 0); // Use `tick()` if you want, but this is usually fine
    }

</script>

<div class="wrapGrid">
    <div class="wrapreshead">
        <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: 0.15em;">residential services</p>
        <h2>Residential Construction Services</h2>
    </div>
 
    <div class="deskbwr">
        <SwiperButton on:click={prevslide} direction={'left'} isDisabled={isBeginning} />
        <div style="margin-left: 15px;">
            <SwiperButton on:click={nextslide} direction={'right'} isDisabled={isEnd} />
        </div>
    </div>
</div>

<div class="megaSwiperWrapper">
    <div class="wrapLinkSwiper">
        <div bind:this={swiperContainer} class="swiper holdupswipe">
            <div class="swiper-wrapper linkswiper" >
                <div class="swiper-slide">
                    <a class="wrapServiceLink" href="/roofing">
                        <div class="cardHead">

                            <div class="cardHeadFlex">
                                <h3 style="color: white;">Roofing</h3>
                                <img src="/Images/Roofing/rooficonwhite.png" alt="gutter Icon">
                            </div>
                            <br>
                            <p class="cardTex">Our team possesses expert skills in roof installation and repair, ensuring your home's durability.</p>
                        </div>
                        <div class="cardPointerFlex">
                            <div class="wrapMegaButton">
                                <Button on:click={(e) => {goto('/roofing')}} buttonClass={'stayWhite'} fullWidth={true}>Roofing Services</Button>
                            </div>
                            <div class="wrapPbutton">
                                <SwiperButton></SwiperButton>
                            </div>
                        </div>
                        <div class="cardGradient"></div>
                        <div class="serviceOverlay"></div>
                        <img  class="serviceLinkBack" src="/Images/Roofing/roofrim.jpg" alt="roof">
                    </a>
                </div>
                <div class="swiper-slide">
                    <a class="wrapServiceLink" href="/siding-trim">
                        <div class="cardHead">

                            <div class="cardHeadFlex">
                                <h3 style="color: white;">Siding & Trim</h3>
                                <img src="/Images/Siding/sidingiconwhite.png"  alt="gutter Icon">
                            </div>
                            <br>
                            <p class="cardTex">We specialize in precise siding and trim installation to enhance both the aesthetic and functionality of your home.</p>
                        </div>
                        <div class="cardPointerFlex">
                            <div class="wrapMegaButton">
                                <Button  on:click={(e) => {goto('/siding-trim')}} buttonClass={'stayWhite'} fullWidth={true}>Siding & Trim Services</Button>
                            </div>
                            <div class="wrapPbutton">
                                <SwiperButton></SwiperButton>

                            </div>
                        </div>
                        <div class="cardGradient"></div>
                        <div class="serviceOverlay"></div>
                        <img class="serviceLinkBack" src="/Images/Siding/siding.jpg" alt="roof">
                    </a>
                </div>
                <div class="swiper-slide">
                    <a class="wrapServiceLink" href="/gutters">
                        <div class="cardHead">

                            <div class="cardHeadFlex">
                                <h3 style="color: white;">Gutters</h3>
                                <img src="/Images/Gutters/guttericonwhite.png" alt="gutter Icon">
                            </div>
                            <br>
                            <p class="cardTex">Our skilled professionals provide effective gutter solutions for optimal water management and protection.</p>
                        </div>
                        <div class="cardPointerFlex">
                            <div class="wrapMegaButton">
                                <Button on:click={(e) => {goto('/gutters')}} buttonClass={'stayWhite'} fullWidth={true}>Gutter Services</Button>
                            </div>
                            <div class="wrapPbutton">
                                <SwiperButton></SwiperButton>

                            </div>
                        </div>
                        <div class="cardGradient"></div>
                        <div class="serviceOverlay"></div>
                        <img class="serviceLinkBack" src="/Images/Gutters/guttercorner.jpg" alt="roof">
                    </a>
                </div>
            </div>
        
            <div class="slideNavButtonsWrap">
                <div>

                </div>
                <div class="bfla">
                    <SwiperButton on:click={prevslide} direction={'left'} isDisabled={isBeginning} />
                    <div style="margin-left: 15px;">
                        <SwiperButton on:click={nextslide} direction={'right'} isDisabled={isEnd} />
                    </div>
                </div>
               
            </div>
            
        </div>
    </div>
</div>