<script lang="ts">
    import { publicFetch } from "../../../fetchers/PublicFetch";
    import { isValidEmail } from "../../../utils/validators";
    import Button from "../../UI/Button.svelte";
    import EmailInput from "../../UI/EmailInput.svelte";
    import FormError from "../../UI/FormError.svelte";
    import Loader from "../../UI/Loaders/Loader.svelte";

    let email: string | null = null
    let formSubmitted: boolean = false
    let isSendingResetLink: boolean = false
    let resetError: string | null = null
    let successEmail:  string | null = null

    async function sendResetLink() {

        formSubmitted = true
        resetError = null

        if (!email || !isValidEmail(email)){
            return
        }

        try {
            isSendingResetLink = true

            const response = await publicFetch('/Forgot/Send-Reset-Link', {
                method: "POST",
                body: JSON.stringify({ Email: email })
            })

            console.log(response)

            successEmail = email

        } catch (error: any) {

            if (
                error?.message === "No account exists with that email address." || 
                error?.message ===  "Recent Mail Sent"
            ){
                successEmail = email
                return
            }

            resetError = error?.message || error?.data?.message || 'An unknown error has occurred.';
        } finally {
            isSendingResetLink = false
        }

        
    }


</script>


<h3 style="font-size: 20pt; text-align: center;">Forgot Password</h3>
<br>


{#if isSendingResetLink}
    <Loader></Loader>
{:else}

    {#if successEmail}
        <div style="text-align: center;">
            <p>If an account exists for <em>{successEmail}</em>, a password reset link has been emailed to you.</p>
        </div>
    {:else}
        <form on:submit={sendResetLink}>
        
            <div style="margin-bottom: 20px;">
                <EmailInput 
                    onEmailChange={(v) => {
                        email = v
                        resetError = null

                    }} 
                    value={email}
                    label={'Email'}
                    inputErrorText={!isValidEmail(email) && formSubmitted ? "Please enter a valid email address." : null}
                ></EmailInput> 
            </div>

            <Button fullWidth={true}>Send Reset Link</Button>

        </form>
    {/if}


{/if}


{#if resetError}
    <br>
    <FormError centerAlign={true} errorMessage={resetError} errorTitle={'Send Reset Link Error'}></FormError>
{/if}

