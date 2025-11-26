<script lang="ts">
    import ResidentialEstimateForm from "../../Components/Forms/residentialEstimateForm.svelte";
    import Testimonials from "../../Components/PageComponents/Testimonials/Testimonials.svelte";
    import Angiesvg from "../../Components/SVG/angiesvg.svelte";
    import { page } from '$app/stores';
    import { browser } from '$app/environment';
    import { adIdUsed } from "../../stores/AdIdStore";
    import { get } from "svelte/store";
    import CertifiedPro from "../../Components/PageComponents/CertifiedPro/CertifiedPro.svelte";
    import Navigation from "../../Components/Navigation.svelte";
    import Footer from "../../Components/Footer.svelte";

    const frontendUrl = import.meta.env.VITE_FRONTEND_URL
    const metaImage = `${frontendUrl}/Images/MetaData/estimatemeta.png`;
    const metaUrl = `${frontendUrl}/estimate`

    $: if (browser) {
        const paramId = $page.params.advertisementId;
        const current = get(adIdUsed);

        if (paramId && !current) {
            // console.log('Setting adIdUsed from param:', paramId);
            adIdUsed.set(paramId);
        } else if (current) {
            // console.log('Ad ID already set:', current);
        }
    }

</script>
  
<svelte:head>
  <title>Request a Roofing, Siding, Trim or Gutter Estimate | S.A. Construction Troy, MI</title>
  <meta name="description" content="Need a quote for roofing, siding, gutters, or trim work? Request a free estimate from S.A. Construction, your trusted residential contractor in Troy, MI." />
  <meta name="keywords" content="free estimate, roofing quote, siding quote, gutter estimate, trim work, Troy MI contractor, S.A. Construction estimate, roofing pricing" />
  <meta name="author" content="S.A. Construction" />

  <!-- Open Graph -->
  <meta property="og:title" content="Request a Free Estimate | Roofing & Exterior Work in Troy, MI" />
  <meta property="og:description" content="Get a quick, professional quote for your next roofing, siding, gutter, or trim project from S.A. Construction in Troy, MI." />
  <meta property="og:image" content="{metaImage}" />
  <meta property="og:url" content="{metaUrl}" />
  <meta property="og:type" content="website" />

  <!-- Twitter Card -->
  <meta name="twitter:card" content="summary_large_image" />
  <meta name="twitter:title" content="Get an Estimate | S.A. Construction - Troy, MI" />
  <meta name="twitter:description" content="Request a professional estimate for your home exterior project — roofing, gutters, siding, or trim — from S.A. Construction in Troy, MI." />
  <meta name="twitter:image" content="{metaImage}" />
</svelte:head>


<slot />


<Navigation></Navigation>
<CertifiedPro estimateHeading={true}></CertifiedPro>
<ResidentialEstimateForm></ResidentialEstimateForm>
<Testimonials></Testimonials>
<div style="height: 150px;"></div>

<Footer></Footer>