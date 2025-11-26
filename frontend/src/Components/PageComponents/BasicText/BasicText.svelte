<script lang="ts">
    export let heading: string = `Enter 'heading'`
    export let subHeading: string = `Enter 'subHeading'`
    export let paragraphText: string | null = null
</script>


<div class="wrapBasic">
    <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">{subHeading}</p>
    <h2>{heading}</h2>
    {#if paragraphText}
        <p>{paragraphText}</p>
    {/if}
    
    
</div>


