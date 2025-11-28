<script lang="ts">
    import { goto } from "$app/navigation";
    import LoginAnimation from "../../Components/Animations/LoginAnimation.svelte";
    import Footer from "../../Components/Footer.svelte";
    import LoginForm from "../../Components/Forms/Auth/LoginForm.svelte";
    import Navigation from "../../Components/Navigation.svelte";
    import { navStyle } from "../../stores/NavStore";
    import { authenticatedUser } from "../../stores/UserStore";
    import './LoginPageStyles.css'


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
        <LoginForm></LoginForm>
    </div>
    <div class="containMoreLinks">
        <a href="/forgot-password">Forgot Password?</a>
    </div>
    <div class="formSpacer"></div>
    <div class="backLines rightb"></div>

    
</div>

<Footer></Footer>