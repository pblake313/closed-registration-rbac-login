<script lang="ts">
    import { gutterPageSlider } from "$lib/ImageSwiperData";
    import { gutterInstallData, gutterMaintenanceData } from "$lib/textGridData";
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
    const metaImage = `${frontendUrl}/Images/MetaData/guttermeta.png`;
    const metaUrl = `${frontendUrl}/gutters`

    onMount(()=>{
        navStyle.set({class: 'transparent'})
    })

</script>


<svelte:head>
    <title>Seamless Gutter Installation & Repair in Troy, MI | S.A. Construction</title>
    <meta name="description" content="Prevent water damage with seamless gutter installation & repair in Troy, MI by S.A. Construction. Custom-fit gutters and downspouts ensure proper rainwater drainage." />
    <meta name="keywords" content="seamless gutters, gutter installation, gutter repair, gutter replacement, downspouts, rainwater drainage, Troy MI, gutter contractor, S.A. Construction" />
    <meta name="author" content="S.A. Construction" />
    <meta property="og:title" content="Seamless Gutter Installation & Repair in Troy, MI | S.A. Construction" />
    <meta property="og:description" content="Prevent water damage with seamless gutter installation & repair in Troy, MI by S.A. Construction. Custom-fit gutters and downspouts ensure proper rainwater drainage." />
    <meta property="og:image" content="{metaImage}" />
    <meta property="og:url" content="{metaUrl}" />
    <meta property="og:type" content="website" />
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="Seamless Gutter Installation & Repair in Troy, MI | S.A. Construction" />
    <meta name="twitter:description" content="Prevent water damage with seamless gutter installation & repair in Troy, MI by S.A. Construction. Custom-fit gutters and downspouts ensure proper rainwater drainage." />
    <meta name="twitter:image" content="{metaImage}" />
</svelte:head>


<Navigation></Navigation>

<SubHero heroText={'Gutter Services'} heroImage={'Images/Gutters/gutterup.jpg'}></SubHero>


<TextGrid subHeading={'New Systems'} heading={'Gutter Installation'} paragraphText={'We specialize in seamless aluminum gutter systems that are custom-fit to your home. These gutters are fabricated on-site for a perfect fit, reducing the risk of leaks and improving water flow. Our expert installers ensure a clean, professional finish that complements your home’s exterior and protects it year-round from water damage.'} gridData={gutterInstallData}></TextGrid>

<ImageSwiper subText={'Top Notch Installations'} slideTitle={'Past Work'} slideImages={gutterPageSlider}></ImageSwiper>

<TextGrid subHeading={'Maintained Systems'} heading={'Gutter Maintenance & Repair'} paragraphText={'Michigan’s weather can take a toll on your gutter system. We offer repair and maintenance services to keep your gutters performing like new, prevent water damage, and extend the system’s lifespan.'} gridData={gutterMaintenanceData}></TextGrid>

<CertifiedPro></CertifiedPro>

<ContactPointer></ContactPointer>

<Footer></Footer>