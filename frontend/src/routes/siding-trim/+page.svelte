<script lang="ts">
    import { sidingPageSlider } from "$lib/ImageSwiperData";
    import { sidingInstallData, sidingMaintenanceData } from "$lib/textGridData";
    import { onMount } from "svelte";
    import ContactPointer from "../../Components/PageComponents/ContactPointer/ContactPointer.svelte";
    import SubHero from "../../Components/PageComponents/SubHero/SubHero.svelte";
    import ImageSwiper from "../../Components/PageComponents/Swipers/ImageSwiper.svelte";
    import TextGrid from "../../Components/PageComponents/TextGrid/TextGrid.svelte";
    import { navStyle } from "../../stores/NavStore";
    import CertifiedPro from "../../Components/PageComponents/CertifiedPro/CertifiedPro.svelte";
    import Navigation from "../../Components/Navigation.svelte";
    import Footer from "../../Components/Footer.svelte";

    const frontendUrl = import.meta.env.VITE_FRONTEND_URL
    const metaImage = `${frontendUrl}/Images/MetaData/sidingmeta.png`;
    const metaUrl = `${frontendUrl}/siding-trim`


    onMount(()=>{
        navStyle.set({class: 'transparent'})
    })
    
</script>

<svelte:head>
    <title>Siding & Trim Installation and Repair in Troy, MI | S.A. Construction</title>
    <meta name="description" content="S.A. Construction provides vinyl & fiber cement siding installation and exterior trim work and repairs in Troy, MI – enhancing your home's curb appeal and protection." />
    <meta name="keywords" content="vinyl siding, fiber cement siding, siding installation, siding repair, exterior trim, trim installation, trim repair, Troy MI, siding contractor, S.A. Construction" />
    <meta name="author" content="S.A. Construction" />
    <meta property="og:title" content="Siding & Trim Installation & Repair in Troy, MI | S.A. Construction" />
    <meta property="og:description" content="S.A. Construction provides vinyl & fiber cement siding installation and exterior trim work and repairs in Troy, MI – enhancing your home's curb appeal and protection." />
    <meta property="og:image" content="{metaImage}" />
    <meta property="og:url" content="{metaUrl}" />
    <meta property="og:type" content="website" />
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="Siding & Trim Installation & Repair in Troy, MI | S.A. Construction" />
    <meta name="twitter:description" content="S.A. Construction provides vinyl & fiber cement siding installation and exterior trim work and repairs in Troy, MI – enhancing your home's curb appeal and protection." />
    <meta name="twitter:image" content="{metaImage}" />
</svelte:head>

<Navigation></Navigation>

<SubHero heroText={'Siding & Trim Services'} heroImage={'/Images/Siding/siding.jpg'}></SubHero>

<TextGrid subHeading={'New Systems'} heading={'Siding Installation'} paragraphText={'At S.A. Construction, we provide professional siding solutions designed to protect your home while enhancing its appearance. We specialize in both siding installation and repair for residential properties across Troy, MI and surrounding areas. Our focus on premium materials like vinyl and fiber cement siding, combined with expert craftsmanship, ensures long-lasting results built for Michigan’s weather.'} gridData={sidingInstallData}></TextGrid>

<ImageSwiper subText={'Precise Installations'} slideTitle={'Project Work'} slideImages={sidingPageSlider}></ImageSwiper>


<TextGrid subHeading={'Top Tier Maintenance'} heading={"Maintenance & Repair"} paragraphText={'S.A. Construction provides dependable siding repair and upkeep to protect your home from the elements and extend the life of your exterior.'} gridData={sidingMaintenanceData}></TextGrid>

<CertifiedPro></CertifiedPro>

<ContactPointer></ContactPointer>

<Footer></Footer>