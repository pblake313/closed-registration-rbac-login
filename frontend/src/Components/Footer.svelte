<script>
    import './Footer.css'
  import NewsLetterForm from './Forms/newsLetterForm.svelte';
  import Herosvg from './SVG/herosvg.svelte';
    import MainLogo from './SVG/MainLogo.svelte';
    import OrangeLogo from './SVG/OrangeLogo.svelte';
</script>


<!-- remove engineering stuff -->

<div class="footWrap">
    <div class="finna">
        <Herosvg uniqueId='footsvjizzle'></Herosvg>
    </div>

    <div class="insideFoot">
        
        <div class="innerFlex">


            <div class="logoAndLinks">
                <div class="deskFootLogo">
                    <OrangeLogo height={'160px'}></OrangeLogo>
                </div>
                <div class="mobileFootLogo">
                    <OrangeLogo height={'60px'}></OrangeLogo>
                </div>
            </div>

            <div class="newsLetterAndLinks">

                <div class="newsLetterForm">
                    <NewsLetterForm></NewsLetterForm>
                </div>

                <div class="footLinkers">
                    <div class="menu">
                        <h4 class="menuHead">Menu</h4>
                        <a class="footLink" href="/estimate">Free Estimate</a>
                        <a class="footLink" href="/">Home</a>
                        <a class="footLink" href="/roofing">Roofing</a>
                        <a class="footLink" href="/gutters">Gutters</a>
                        <a class="footLink" href="/siding-trim">Siding & Trim</a>
                    </div>
                    <div class="menu">
                        <h4 class="menuHead">Portal</h4>
                        <a class="footLink" href="/login">Login</a>
                        <a class="footLink" href="/forgot-password">Forgot Password</a>
                    </div>
                    <div class="menu">
                        <h4 class="menuHead">Legal</h4>
                        <a class="footLink" href="/terms">Terms & Conditions</a>
                        <a class="footLink" href="/privacy">Privacy Policy</a>
                    </div>
                </div>
            </div>
         
        </div>
      
    </div>

</div>