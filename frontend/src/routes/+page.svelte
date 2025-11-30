<script>
    import { onMount } from "svelte";
    import { navStyle } from "../stores/NavStore";
    import Navigation from "../Components/Navigation.svelte";
    
    const frontendUrl = import.meta.env.VITE_FRONTEND_URL
    const metaImage = `${frontendUrl}/Images/MetaData/homemeta.png`;

    onMount(()=>{
        navStyle.set({class: 'bright'})
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

<p>Home page</p>


