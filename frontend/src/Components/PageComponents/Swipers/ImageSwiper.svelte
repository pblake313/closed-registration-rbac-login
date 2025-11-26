<script lang="ts">
	import { onMount, tick } from 'svelte';
	import 'swiper/css';
	import 'swiper/css/navigation';
	import 'swiper/css/pagination';
	import './ImageSwiper.css';
	import SwiperButton from '../../UI/SwiperButton.svelte';

	let swiperContainer: HTMLDivElement;
	let Swiper;
	let swiperInstance: any;

	let isBeginning = true;
	let isEnd = false;

	export let slideImages: string[] = [
		'/Images/Roofing/roofer2.jpg',
		'/Images/Siding/siding.jpg',
		'/Images/Roofing/roofers.jpg',
		'/Images/Siding/threeladders.JPG'
	];

	export let slideTitle: string = 'Enter Slide Title';
	export let subText: string = 'Enter subText';

	onMount(async () => {
		const swiperModule = await import('swiper');
		Swiper = swiperModule.default;

		swiperInstance = new Swiper(swiperContainer, {
			loop: false,
			slidesPerView: 1.1,
			centeredSlides: false,
			slidesPerGroup: 1,
			spaceBetween: 15,
			pagination: {
				el: '.swiper-pagination',
				clickable: true
			},
			breakpoints: {
				1250: {
					slidesPerView: 2,
					spaceBetween: 25
				}
			},
			on: {
				init: async () => {
					await tick();
					isBeginning = swiperInstance.isBeginning;
					isEnd = swiperInstance.isEnd;
				},
				slideChange: async () => {
					await tick();
					isBeginning = swiperInstance.isBeginning;
					isEnd = swiperInstance.isEnd;
				}
			}
		});
	});

	function nextSlide() {
		if (swiperInstance && !isEnd) swiperInstance.slideNext();
	}

	function previousSlide() {
		if (swiperInstance && !isBeginning) swiperInstance.slidePrev();
	}
</script>

<div class="megaSwipeWrap">
	<div class="wrapImageSwiper">
		<p style="text-transform: uppercase; font-size: 9pt; letter-spacing: 0.15em;">
			{subText}
		</p>
		<div class="wrapTitle">
			<div class="wrapSliderTitle">
				<h2>{slideTitle}</h2>
			</div>
			<div class="buttonsSwipeRap">
				<SwiperButton direction="left" isDisabled={isBeginning} on:click={previousSlide} />
				<div style="margin-left: 15px;">
					<SwiperButton direction="right" isDisabled={isEnd} on:click={nextSlide} />
				</div>
			</div>
		</div>

		<div bind:this={swiperContainer} class="swiper finnaswipe">
			<div class="swiper-wrapper">
				{#each slideImages as slidePic}
					<div class="swiper-slide finjoint">
						<div class="imageContainer">
							<img
								alt="Roofing, siding, trim, and gutter updates"
								src={slidePic}
							/>
						</div>
					</div>
				{/each}
			</div>
		</div>

        <div class="mobileSwipNav">
            <div></div>
            <div class="mobileButtonsSwiperWRap">
                <SwiperButton direction="left" isDisabled={isBeginning} on:click={previousSlide} />
                <div style="margin-left: 15px;">
                    <SwiperButton direction="right" isDisabled={isEnd} on:click={nextSlide} />
                </div>
            </div>
        </div>



	</div>


</div>
