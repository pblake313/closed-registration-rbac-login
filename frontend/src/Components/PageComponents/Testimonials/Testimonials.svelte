<script lang="ts">
	import { onMount } from 'svelte';
	import 'swiper/css';
	import 'swiper/css/navigation';
	import 'swiper/css/pagination';
	import './Testimonials.css';
	import Button from '../../UI/Button.svelte';
    import SwiperButton from '../../UI/SwiperButton.svelte';
	import { tick } from 'svelte';
    import Starsvg from '../../SVG/Starsvg.svelte';

	let testSwiperContainer: HTMLDivElement;
	let swiperInstance: any;

	let isBeginning = true;
	let isEnd = false;


	const testimonialArray = [
		{
			name: 'Bob Count',
			review: 'Excellent, timely.',
			services: 'Roof damage repair'
		},
		{
			name: 'Kevin Tamer',
			review:
				'Very professional and prompt group. Pete is a stand up guy and went out of his way to help get our repair taken care of. Quality work and group.',
			services: 'Gutter repairs'
		},
		{
			name: 'Kristina Haralampopoulos',
			services: 'Gutter installation, Remodeling, Roof installation',
			review:
				'They turned our vision into reality. Our neighbors have been complimenting us too. We’re glad to have found such a great contractor and grateful that we worked with such nice people, Peter, Jay, and their Team. We highly recommend SA Construction!'
		},
		{
			name: 'Lin Riley',
			review:
				'Peter, Jay and their team are very organized and professional. They arrived on time a few days in a roll and got everything done perfectly. I am hiring them back in spring for other projects and would highly recommend S.A. Constructions to others who are looking for a professional & reliable contractor.'
		},
		{
			name: 'Genevieve Fox',
			review:
				'These guys are very professional and do great quality work. Pete is the brains, Jay is the bronze. With this dynamic duo, nothing can go wrong.'
		},
		{
			name: 'Rosalind Gerth',
			review:
				'These guys truly are professional. The clean up was awesome and all my bushes and flowers were still intact. Would definitely recommend to anyone.'
		},
		{
			name: 'Gerald Kent',
			review: 'New garage roof. Would recommend.',
			services: 'Roof repair, Roof installation, Roof damage repair'
		}
	];

	onMount(async () => {
		const swiperModule = await import('swiper');
		const Swiper = swiperModule.default;

		swiperInstance = new Swiper(testSwiperContainer, {
			loop: false,
			initialSlide: 1, 
			slidesPerView: 1,
			centeredSlides: false,
			spaceBetween: 15,
			pagination: {
				el: '.swiper-pagination',
				clickable: true
			},
			breakpoints: {
                0: {
                    slidesPerView: 1.2,
                    spaceBetween: 15,
					centeredSlides: true,

                },
                600: {
                    slidesPerView: 1
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


<div class="megaTest">
	<div class="wrapTests">
		<div class="testsLeft">
			<p style="text-transform: uppercase; font-size: 9pt; letter-spacing: 0.15em; text-align: center">
				Testimonials
			</p>
			<h2 style="text-align: center;">What our clients have to say about us.</h2>
		</div>
	
		<div class="testsRight">
			<div class="reviewButtonFlex deskbhold">
				<SwiperButton isDisabled={isBeginning} direction="left" on:click={previousSlide} />
				<div style="margin-left: 15px;">
					<SwiperButton isDisabled={isEnd} direction="right" on:click={nextSlide} />
				</div>
			</div>
	
			<div class="testSwiper" bind:this={testSwiperContainer}>
				<div class="swiper-wrapper swrap">
					{#each testimonialArray as testimonial}
						<div class="swiper-slide testSingleWrap">
							<div class="testimonialCard">
								<div class="testimonalInside">
									<div class="starFlex">
										<Starsvg></Starsvg>
										<Starsvg></Starsvg>
										<Starsvg></Starsvg>
										<Starsvg></Starsvg>
										<Starsvg></Starsvg>
									</div>
									<h3>{testimonial.name}</h3>
									{#if testimonial.services}
										<p id="services"><em>{testimonial.services}</em></p>
									{/if}
									<p>{testimonial.review}</p>
								</div>
							</div>
						</div>
					{/each}
				</div>
			</div>
		</div>
		<div class="bottomerFlex">
			<div></div>
			<div class="mobileTestFlex">
				<SwiperButton isDisabled={isBeginning} direction="left" on:click={previousSlide} />
				<div style="margin-left: 15px;">
					<SwiperButton isDisabled={isEnd} direction="right" on:click={nextSlide} />
				</div>
			</div>
		</div>
	
	</div>
	
</div>
