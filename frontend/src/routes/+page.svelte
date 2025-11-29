<script>
    import { onMount } from "svelte";
    import ContactPointer from "../Components/PageComponents/ContactPointer/ContactPointer.svelte";
    import Hero from "../Components/PageComponents/Hero/Hero.svelte";
    import Outsidelinks from "../Components/PageComponents/OutsideLinks/Outsidelinks.svelte";
    import ResidentialServicesGrid from "../Components/PageComponents/Servicesgrid/ResidentialServicesGrid.svelte";
    import Testimonials from "../Components/PageComponents/Testimonials/Testimonials.svelte";
    import { navStyle } from "../stores/NavStore";
    import ImageLeftSection from "../Components/PageComponents/AboutPointer/ImageLeftSection.svelte";
    import { homePageSlider } from "$lib/ImageSwiperData";
    import ImageSwiper from "../Components/PageComponents/Swipers/ImageSwiper.svelte";
    import Navigation from "../Components/Navigation.svelte";
    import Footer from "../Components/Footer.svelte";

    
    const frontendUrl = import.meta.env.VITE_FRONTEND_URL
    const metaImage = `${frontendUrl}/Images/MetaData/homemeta.png`;

    onMount(()=>{
        navStyle.set({class: 'transparent'})
    })


</script>

<svelte:head>
    <title>S.A. Construction | Roofing, Siding, Gutters & Trim in Troy, MI</title>
    <meta name="description" content="Troy, MI's trusted roofing, siding, gutters & trim contractor with 30+ years of experience. We deliver quality craftsmanship to protect & enhance your home." />
    <meta name="keywords" content="roofing, roof repair, gutters, gutter installation, siding, trim, residential roofing, Troy MI, roofing contractor, S.A. Construction" />
    <meta name="author" content="S.A. Construction" />

    <!-- Open Graph -->
    <meta property="og:title" content="Roofing, Siding, Gutters & Trim in Troy, MI | S.A. Construction" />
    <meta property="og:description" content="Troy, MI's trusted roofing, siding, gutters & trim contractor with 30+ years of experience. We deliver quality craftsmanship to protect & enhance your home." />
    <meta property="og:image" content={metaImage} />
    <meta property="og:url" content={frontendUrl} />
    <meta property="og:type" content="website" />

    <!-- Twitter Card -->
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="Roofing, Siding, Gutters & Trim in Troy, MI | S.A. Construction" />
    <meta name="twitter:description" content="Troy, MI's trusted roofing, siding, gutters & trim contractor with 30+ years of experience. We deliver quality craftsmanship to protect & enhance your home." />
    <meta name="twitter:image" content={metaImage} />
</svelte:head>


<Navigation></Navigation>

<Hero></Hero>

<Outsidelinks></Outsidelinks>

<Testimonials></Testimonials>

<ImageSwiper subText="Past Projects" slideTitle="A look at what we've built." slideImages={homePageSlider}></ImageSwiper>

<ResidentialServicesGrid></ResidentialServicesGrid>

<ImageLeftSection></ImageLeftSection>

<ContactPointer></ContactPointer>

<Footer></Footer>
