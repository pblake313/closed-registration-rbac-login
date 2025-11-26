<script lang="ts">
    import LoginAnimation from "../../Components/Animations/LoginAnimation.svelte";
    import Footer from "../../Components/Footer.svelte";
    import LoginForm from "../../Components/Forms/Auth/LoginForm.svelte";
    import Navigation from "../../Components/Navigation.svelte";
    import { navStyle } from "../../stores/NavStore";
    import './LoginPageStyles.css'


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
        <LoginForm></LoginForm>
    </div>
    <div class="containMoreLinks">
        <p>Enter more options here.</p>
    </div>
    <div class="formSpacer"></div>
    <div class="backLines rightb"></div>

    
</div>

<Footer></Footer>