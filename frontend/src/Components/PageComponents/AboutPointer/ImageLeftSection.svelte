<script lang="ts">
    import Button from '../../UI/Button.svelte';
    import './ImageLeftSection.css'

    export let imageUrl: string = '/Images/Roofing/roofstaple.jpg'
    export let subText: string = 'Feature'
    export let title: string = 'Providing solutions that meet your needs.'
    export let paragraphText: string = `Whether you're embarking on a years-long major project or in need of a quick repair for your gutters, roofing, or siding, we're here to assist. No job is too big or small for us!`
    export let buttonText: string = 'Get in touch'
    export let butttonHref: string = '/estimate'
</script>

<div class="aboutWrap">
    <div class="surpriseHead">
        <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">{subText}</p>
        <h2>{title}</h2>
    </div>
    <div class="aboutLeft">
        <img src="{imageUrl}" alt="{title}"> 
    </div>
    <div class="aboutRight">
        <div class="hidemobheading">
            <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">{subText}</p>
            <h2>{title}</h2>
        </div>
        <p>{paragraphText}</p>
        <br>
        <a href="{butttonHref}">
            <div class="deskLii">
                <Button>{buttonText}</Button>
            </div>
            <div class="mobileLii">
                <Button fullWidth={true}>{buttonText}</Button>
            </div>
        </a>

    </div>
</div>