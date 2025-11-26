<script lang="ts">
    import { publicFetch } from "../../../fetchers/PublicFetch";
    import Button from "../../UI/Button.svelte";
    import EmailInput from "../../UI/EmailInput.svelte";
    import FormError from "../../UI/FormError.svelte";
    import Loader from "../../UI/Loaders/Loader.svelte";
    import TextInput from "../../UI/TextInput.svelte";

    let errorMessage: string | null = null

    let isLoggingIn: boolean = false

    async function test() {
        try {
            if (isLoggingIn){
                // return if we are already attempting to login.
                return
            }

            isLoggingIn = true

            const response = await publicFetch('/test/finna', {
                method: "GET"
            })

            console.log(response)
        } catch (err:any) {
            errorMessage = err.message || 'An unknown error has occurred.'
            setTimeout(() => {
                errorMessage = null
            }, 6000);
        } finally {
            isLoggingIn = false
        }
        
        
    }
</script>



  <form on:submit={test}>
        <h3 style="font-size: 20pt; text-align: center;">Login</h3>
        <br>


        {#if !isLoggingIn}
        
            <div style="margin-bottom: 20px;">
                <EmailInput onEmailChange={(v) => {console.log(v)}} label={'Email'}></EmailInput>
            </div>
            <div style="margin-bottom: 20px;">
                <TextInput onTextChange={(v) => {console.log(v)}} label={'Password'}></TextInput>
            </div>
            <Button fullWidth={true}>Login</Button>

      
        {:else}
            <Loader></Loader>
        {/if}

      {#if errorMessage}
            <br><br>
            <FormError centerAlign={true} errorTitle={'Login Error'} errorMessage={errorMessage}></FormError>
        {/if}
    </form>


