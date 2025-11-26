<script lang="ts">
    import { onMount } from 'svelte';
    import 'swiper/css';
    import 'swiper/css/navigation';
    import 'swiper/css/pagination';

    import Angiesvg from '../../SVG/angiesvg.svelte';
    import Gafsvg from '../../SVG/gafsvg.svelte';
    import Button from '../../UI/Button.svelte';
    import BasicText from '../BasicText/BasicText.svelte';
    import './Outsidelinks.css';
    import SwiperButton from '../../UI/SwiperButton.svelte';

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
            spaceBetween: 25,
            breakpoints: {
                0: {
                    slidesPerView: 1.2,
                    spaceBetween: 10,

                },
                1051: {
                    slidesPerView: 2
                }
            },
            on: {
                slideChange: updateSwiperState,
                afterInit: updateSwiperState
            }
        });

        updateSwiperState();
    });

    function updateSwiperState() {
        isBeginning = swiperInstance?.isBeginning;
        isEnd = swiperInstance?.isEnd;
    }

    function previousSlide() {
        swiperInstance?.slidePrev();
    }

    function nextSlide() {
        swiperInstance?.slideNext();
    }
</script>

<div class="wrapOutsideAll">
    <!-- Desktop Layout -->
    <div class="desktopDevices">
        <BasicText 
            subHeading="Certified Professionals"
            heading="Proven and Reliable"
            paragraphText="We’re proud to be recognized by top industry platforms like GAF and Angi. These trusted certifications reflect our ongoing commitment to quality, safety, and customer satisfaction." 
        />
        <br>
        <div class="wrapOut">
            <div class="linksFlex">
                <a class="outerLink" href="https://www.gaf.com/en-us/roofing-contractors/residential/usa/mi/troy/sa-construction-1141701" target="_blank">
                    <img src="/Images/Roofing/roofer2.jpg" alt="Roofer">
                    <div class="linkOverlay"></div>
                    <div class="iconOuterFlex">
                        <div class="iconSide">
                            <p>Our GAF certification means you’re hiring a team that meets the highest standards in roofing installation, product knowledge, and customer service.</p>
                        </div>
                        <div class="holdOuterB">
                            <Button buttonClass="stayWhite">View Profile</Button>
                        </div>
                    </div>
                    <div class="linkInfo">
                        <Gafsvg />
                    </div>
                    <div class="linkGradient"></div>
                </a>

                <a class="outerLink bottomL" href="https://www.angi.com/companylist/us/mi/troy/sa-construction-reviews-2612138.htm" target="_blank">
                    <img src="/Images/Gutters/gutterup.jpg" alt="Roofer">
                    <div class="linkOverlay"></div>
                    <div class="iconOuterFlex">
                        <div class="iconSide">
                            <p>We’re proud to be top-rated on Angi — a trusted platform where real customers recognize our professionalism, reliability, and workmanship.</p>
                        </div>
                        <div class="holdOuterB">
                            <Button buttonClass="stayWhite">View Profile</Button>
                        </div>
                    </div>
                    <div class="linkInfo">
                        <Angiesvg />
                    </div>
                    <div class="linkGradient"></div>
                </a>
            </div>
        </div>
    </div>

    <!-- Touchscreen Swiper Layout -->
    <div class="wrapToucher">
        <div class="touchScreenDevices">

            <div class="touchSwiperTitleFlex">
                <div class="touchHeadSide">
                    <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">Certified Professionals</p>
                    <h2>Proven and Reliable</h2>
                </div>
                <div class="touchButtonsSide desktappers">
                    <SwiperButton direction="left" isDisabled={isBeginning} on:click={previousSlide} />
                    <div style="margin-left: 15px;">
                        <SwiperButton direction="right" isDisabled={isEnd} on:click={nextSlide} />
                    </div>
                </div>
            </div>

            <div class="swiper finnatouchswiper" bind:this={swiperContainer}>
                <div class="swiper-wrapper outswiperap">
                    <div class="swiper-slide outslide">
                        <a class="touchOuter" href="https://www.gaf.com/en-us/roofing-contractors/residential/usa/mi/troy/sa-construction-1141701" target="_blank">
                            <div class="wrapOuterImageLinker">
                                <img src="/Images/Roofing/roofer2.jpg" alt="Roofer">
                                <div class="touchOverlay"></div>
                                <div class="linkInfo">
                                    <Gafsvg height="50px" />
                                </div>
                                <div class="touchInnerInfo">
                                    <p style="margin-bottom: 10px; color: white;">Our GAF certification means you’re hiring a team that meets the highest standards in roofing installation, product knowledge, and customer service.</p>
                                    <div class="shortButtoLink">
                                        <Button buttonClass="stayWhite">View Profile</Button>
                                    </div>
                                    <div class="fullButtoLink">
                                        <Button fullWidth={true} buttonClass="stayWhite">View Profile</Button>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>

                    <div class="swiper-slide outslide">
                        <a class="touchOuter" href="https://www.angi.com/companylist/us/mi/troy/sa-construction-reviews-2612138.htm" target="_blank">
                            <div class="wrapOuterImageLinker">
                                <img src="/Images/Gutters/gutterup.jpg" alt="Roofer">
                                <div class="touchOverlay"></div>
                                <div class="linkInfo">
                                    <Angiesvg height="50px" />
                                </div>
                                <div class="touchInnerInfo">
                                    <p style="margin-bottom: 10px; color: white;">We’re proud to be top-rated on Angi — a trusted platform where real customers recognize our professionalism, reliability, and workmanship.</p>
                                    <div class="shortButtoLink">
                                        <Button buttonClass="stayWhite">View Profile</Button>
                                    </div>
                                    <div class="fullButtoLink">
                                        <Button fullWidth={true} buttonClass="stayWhite">View Profile</Button>
                                    </div>
                                </div>
                            </div>
                        </a>
                    </div>
                </div>
            </div>

            <div class="mobileTappers">
                <div></div>
                <div class="touchButtonsSide">
                    <SwiperButton direction="left" isDisabled={isBeginning} on:click={previousSlide} />
                    <div style="margin-left: 15px;">
                        <SwiperButton direction="right" isDisabled={isEnd} on:click={nextSlide} />
                    </div>
                </div>
            </div>
       

        </div>
    </div>
</div>
