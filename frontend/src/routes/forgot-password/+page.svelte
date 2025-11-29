<script lang="ts">
    import { goto } from "$app/navigation";
    import LoginAnimation from "../../Components/Animations/LoginAnimation.svelte";
    import Footer from "../../Components/Footer.svelte";
    import ForgotPasswordForm from "../../Components/Forms/Auth/ForgotPasswordForm.svelte";
    import Navigation from "../../Components/Navigation.svelte";
    import { navStyle } from "../../stores/NavStore";
    import { authenticatedUser } from "../../stores/UserStore";
    import './ForgotPageStyles.css'

    navStyle.set({class: 'bright'})

    $: if ($authenticatedUser) {
        goto('/dashboard')
    }

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

    <br>
    <div style="margin: auto; width: fit-content;">
        <a style="text-align: center; margin:auto;" href="/login">Login</a>
    </div>

    <div class="formSpacer"></div>
    <div class="backLines rightb"></div>

</div>

<Footer></Footer>