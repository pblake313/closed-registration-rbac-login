<script lang="ts">
	import { onMount } from 'svelte';
	import 'swiper/css';
	import 'swiper/css/navigation';
	import { roofingServicesData, type TextGridItem } from "$lib/textGridData";
    import SwiperButton from '../../UI/SwiperButton.svelte';
    import './TextGrid.css'

	export let heading: string = `Enter 'heading'`;
	export let subHeading: string = `Enter 'subHeading'`;
	export let paragraphText: string = `Enter 'paragraphText'`;
	export let gridData: TextGridItem[] = roofingServicesData;

	let finnaContaina: HTMLDivElement;
	let Swiper;
	let swiperInstance: any;

	let isBeginning = true;
	let isEnd = false;

	onMount(async () => {
		const swiperModule = await import('swiper');
		Swiper = swiperModule.default;

		swiperInstance = new Swiper(finnaContaina, {
			slidesPerView: 1.1,
			spaceBetween: 15,
			breakpoints: {
                750: {
                    slidesPerView: 2,
					spaceBetween: 15
                },
				1250: {
					slidesPerView: 3,
					spaceBetween: 25
				}
			},
			on: {
				reachBeginning: () => { isBeginning = true; isEnd = false; },
				reachEnd: () => { isEnd = true; isBeginning = false; },
				fromEdge: () => { isBeginning = false; isEnd = false; }
			}
		});
	});

	function prevSlide() {
		swiperInstance?.slidePrev();
	}

	function nextSlide() {
		swiperInstance?.slideNext();
	}
</script>

<div class="megaTextGridWrap">
	<div class="wrapAllTextGrid">
		<p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">{subHeading}</p>
        <div class="gridItemFlez">
            <div class="wrapGridTit">
                <h2>{heading}</h2>
            </div>
            <div class="swiper-controls conrolJ deskcontrollas">
                <SwiperButton on:click={prevSlide} direction="left" isDisabled={isBeginning} />
                <div style="margin-left: 15px;">
                    <SwiperButton on:click={nextSlide} direction="right" isDisabled={isEnd} />
                </div>
            </div>
        </div>
		<p>{paragraphText}</p>
		<br>


		<div bind:this={finnaContaina} class="swiper holdfin">
			<div class="swiper-wrapper cardsrap">
				{#each gridData as gridItem}
					<div class="swiper-slide textGridItem">
                        <!-- <div class="textgriditemOverlay"></div> -->
                        <!-- <img class="textgriditemimage" src="/Images/Gutters/guttercorner.jpg" alt="{gridItem.header}"> -->
                        <div class="flexoutInner">
                            <h3 >{gridItem.header}</h3>
                            <p style="margin-top: 20px;">{gridItem.paragraphText}</p>
                        </div>
					</div>
				{/each}
			</div>
		</div>

        <div class="gridswipecontrolrap">
            <div></div>
            <div class="swiper-controls conrolJ">
                <SwiperButton on:click={prevSlide} direction="left" isDisabled={isBeginning} />
                <div style="margin-left: 15px;">
                    <SwiperButton on:click={nextSlide} direction="right" isDisabled={isEnd} />
                </div>
            </div>
    
        </div>
	
	</div>
</div>