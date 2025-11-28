<script lang="ts">
    import LoginAnimation from "../../Components/Animations/LoginAnimation.svelte";
    import Footer from "../../Components/Footer.svelte";
    import ForgotPasswordForm from "../../Components/Forms/Auth/ForgotPasswordForm.svelte";
    import Navigation from "../../Components/Navigation.svelte";
    import { navStyle } from "../../stores/NavStore";
    import './ForgotPageStyles.css'

    navStyle.set({class: 'bright'})

</script>

<Navigation></Navigation>

<div class="wrapTheLoginPage">


    <div class="loginFormBack">
        <LoginAnimation></LoginAnimation>
    </div>

    <div class="backLines"></div>

    <div class="formSpacer"></div>
    <div class="containLoginForm">
        <ForgotPasswordForm></ForgotPasswordForm>
    </div>
    
    <div class="formSpacer"></div>
    <div class="backLines rightb"></div>

</div>

<Footer></Footer>