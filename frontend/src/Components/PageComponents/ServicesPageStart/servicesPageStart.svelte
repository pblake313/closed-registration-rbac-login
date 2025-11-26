<script>
    import Button from '../../UI/Button.svelte';
    import './servicesPageStart.css';

    export let preText = ''
    export let headingText = ''
    export let subHeader = ''
    export let buttonText = ''
    export let goesTo = '/'
    export let imageUrl = 'https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FImages%2FIMG_2360.JPG?alt=media&token=ca574909-c365-4e76-a8ba-11c459ac7852'
</script>


<div class="wrapStart">
    <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">{preText}</p>
    <h2>{headingText}</h2>
    <p class="finnaShort">{subHeader}</p>
    <br>
    <a href="{goesTo}">
        <Button>{buttonText}</Button>
    </a>
    <br><br>
    <div class="imageSplit">
        <div class="imageWrap">
            <img src="{imageUrl}" alt="{headingText}">
        </div>
    </div>
    
</div>


