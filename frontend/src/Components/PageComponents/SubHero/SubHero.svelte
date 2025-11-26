<script lang="ts">

    import './SubHero.css'

    export let heroImage: string = '/Images/Roofing/roofsit.jpg'
    export let heroText: string = 'Enter Hero Text'


</script>

<div class="subHeroArea">

    <img class="subHeroImage" src="{heroImage}" alt="{heroText}">
    <div class="subOverlay"></div>
    <div class="subHeroGradient"></div>
    <div class="subHeroInfo">
        <h2 class="subHeroText">{heroText}</h2>
    </div>
</div>
