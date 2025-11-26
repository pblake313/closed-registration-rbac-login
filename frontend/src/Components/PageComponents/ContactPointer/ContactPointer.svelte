<script>
    import Button from '../../UI/Button.svelte';
    import './ContactPointer.css'
</script>


<!-- REPLACEMENT BELOW -->
<div class="pointWrap">
    <div class="contactLeft">
        <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">Contact</p>
        <h2>Get a free estimate.</h2>
        <p>Looking to upgrade your roofing, siding, gutters, or trim? Reach out today for a free, no-obligation estimate tailored to your project needs.</p>
        <br><br>
        <a href="/estimate">
            <div class="mobileContactLink">
                <Button fullWidth={true}>Get Free Estimate</Button>
            </div>
            <div class="deskContactLink">
                <Button>Get Free Estimate</Button>
            </div>
        </a>
    </div>
    <div class="contactRight">
    </div>
</div>