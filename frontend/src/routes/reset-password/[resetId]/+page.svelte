<script lang="ts">
    import { onMount } from "svelte";
    import LoginAnimation from "../../../Components/Animations/LoginAnimation.svelte";
    import Footer from "../../../Components/Footer.svelte";
    import Navigation from "../../../Components/Navigation.svelte";
    import "./ResetStyles.css";
    import { navStyle } from "../../../stores/NavStore";
    import ResetPasswordForm from "../../../Components/Forms/Auth/ResetPasswordForm.svelte";

    onMount(() => {
        navStyle.set({class: 'bright'})
    })
</script>

<Navigation></Navigation>
<div class="wrapTheLoginPage">
    <div class="loginFormBack">
        <LoginAnimation></LoginAnimation>
    </div>

    <div class="backLines"></div>

    <div class="formSpacer"></div>
    <div class="containLoginForm">

        <h3 style="font-size: 20pt; text-align: center;">Reset Password</h3>
        <br>
        <ResetPasswordForm></ResetPasswordForm>
    </div>

    <div class="formSpacer"></div>
    <div class="backLines rightb"></div>
</div>

<Footer></Footer>
