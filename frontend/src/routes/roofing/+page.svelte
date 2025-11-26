<script lang="ts">
    import { roofingPageSlider } from "$lib/ImageSwiperData";
    import { asphaltShingleData } from "$lib/textGridData";
    import { onMount } from "svelte";
    import ContactPointer from "../../Components/PageComponents/ContactPointer/ContactPointer.svelte";
    import SubHero from "../../Components/PageComponents/SubHero/SubHero.svelte";
    import ImageSwiper from "../../Components/PageComponents/Swipers/ImageSwiper.svelte";
    import TextGrid from "../../Components/PageComponents/TextGrid/TextGrid.svelte";
    import { navStyle } from "../../stores/NavStore";
    import CertifiedPro from "../../Components/PageComponents/CertifiedPro/CertifiedPro.svelte";
    const frontendUrl = import.meta.env.VITE_FRONTEND_URL
    const metaImage = `${frontendUrl}/Images/MetaData/roofingmeta.png`;
    const metaUrl = `${frontendUrl}/roofing`



    onMount(()=>{
        navStyle.set({class: 'transparent'})
    })
</script>

<svelte:head>
    <title>Roof Installation & Repair in Troy, MI | S.A. Construction</title>
    <meta name="description" content="Asphalt shingle roof installation & repair in Troy, MI. S.A. Construction ensures durable, leak-free results with quality materials and expert craftsmanship." />
    <meta name="keywords" content="roofing, roof installation, roof repair, asphalt shingles, roof replacement, residential roofing, Troy MI, roofing contractor, S.A. Construction" />
    <meta name="author" content="S.A. Construction" />
    <meta property="og:title" content="Asphalt Shingle Roof Installation & Repair in Troy, MI | S.A. Construction" />
    <meta property="og:description" content="Asphalt shingle roof installation & repair in Troy, MI. S.A. Construction ensures durable, leak-free results with quality materials and expert craftsmanship." />
    <meta property="og:image" content="{metaImage}" />
    <meta property="og:url" content="{metaUrl}" />
    <meta property="og:type" content="website" />
    <meta name="twitter:card" content="summary_large_image" />
    <meta name="twitter:title" content="Asphalt Shingle Roof Installation & Repair in Troy, MI | S.A. Construction" />
    <meta name="twitter:description" content="Asphalt shingle roof installation & repair in Troy, MI. S.A. Construction ensures durable, leak-free results with quality materials and expert craftsmanship." />
    <meta name="twitter:image" content="{metaImage}" />
</svelte:head>

<SubHero heroText={'Roofing Services'}></SubHero>

<TextGrid subHeading={'Full Roof Replacement'} paragraphText={'We specialize in asphalt shingle roofing for new installations and full roof replacements. We use only high-quality shingles that hold up against Michigan’s seasonal extremes — from heavy snow to summer heat. Our team follows precise installation techniques to ensure a secure, long-lasting roof that not only protects your home but also enhances its appearance.'} heading={'Roofing Services'} gridData={asphaltShingleData}></TextGrid>

<ImageSwiper slideImages={roofingPageSlider} subText={'Past Work'} slideTitle={'Recent Roofing Projects'}></ImageSwiper>

<TextGrid subHeading={'Staying Covered'} heading={'Repair & Maintenance'} paragraphText={'From minor leaks and missing shingles to storm damage repairs, our team provides prompt, reliable roof repair services to extend the life of your roof.'}></TextGrid>

<CertifiedPro></CertifiedPro>

<ContactPointer></ContactPointer>


