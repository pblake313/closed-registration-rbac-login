<script lang="ts">
	import { page } from '$app/stores';
	import { onMount } from 'svelte';
    import { publicFetch } from '../../../fetchers/PublicFetch';
    import Loader from '../../UI/Loaders/Loader.svelte';
    import Button from '../../UI/Button.svelte';
    import { goto } from '$app/navigation';
    import PasswordInput from '../../UI/PasswordInput.svelte';
    import { isValidPassword } from '../../../utils/validators';
    import FormError from '../../UI/FormError.svelte';

	// reactive param
	$: resetId = $page.params.resetId;

    let loading: boolean = false
    let formErrorMessage: string | null = null
    let linkExpired: boolean = false

    let newPass: string | null = null
    let confirmPass: string | null = null

    let formSubmitted: boolean = false

    let passwordUpdated: boolean = false


	onMount(() => {
		if (!resetId) return;

		validateResetId(resetId);
	});

	async function validateResetId(id: string) {
        try {
            loading = true
            const response = await publicFetch(`/Forgot/Validate/${resetId}`, {
                method: "GET"
            })

            console.log(response)
        } catch (error: any) {
            formErrorMessage = error?.message || error?.data?.message || 'An unknown error has occurred.';

            if (error?.message === 'Reset Doc Non Existant' || error?.message === 'Reset Doc Expired') {
                linkExpired = true
                return
            }

        } finally {
            loading = false
        }
	}

    async function resetPassword() {
        formSubmitted = true

        if (newPass !== confirmPass || !isValidPassword(newPass) || !isValidPassword(confirmPass)){
            return
        }

        try {
            loading = true

            let postObj = {
                NewPassword: newPass,
                ConfirmPassword: confirmPass,
                ReferenceId: resetId
            }

            const response = await publicFetch(`/Forgot/Reset-Password`, {
                method: 'POST',
                body: JSON.stringify(postObj)
            })

            console.log(response)

            passwordUpdated = true
        } catch (error:any) {

            if (error?.message === 'Reset Doc Non Existant' || error?.message === 'Reset Doc Expired') {
                linkExpired = true
                return
            }

            formErrorMessage = error?.message || error?.data?.message || 'An unknown error has occurred.';

        } finally {
            loading = false
        }
    }
</script>


{#if loading}
    <Loader></Loader>
{:else}


    {#if passwordUpdated}
        <h3 style="text-align: center; color: green;">Success!</h3>
        <p style="text-align: center;">Your password has been updated!</p>
        <br>
        <Button fullWidth={true} on:click={() => {goto('/login')}}>Login</Button>
    {:else}

        {#if !linkExpired}
                <form on:submit={resetPassword}>
                    <PasswordInput 
                        label={'Password'} 
                        value={newPass} 
                        onPasswordChange={(v)=> {
                            newPass = v
                            formErrorMessage = null;
                        }}
                        inputErrorText={!isValidPassword(newPass) && formSubmitted ? "Please enter a valid password." : null}
                    ></PasswordInput>

                    <br>
                    <PasswordInput 
                        showPasswordRequirements={false} 
                        label={'Confirm Password'} 
                        value={confirmPass}
                        onPasswordChange={(v)=> {
                            confirmPass = v
                            formErrorMessage = null;
                        }}
                        inputErrorText={
                            formSubmitted && !isValidPassword(confirmPass)
                                ? 'Please enter a valid password'
                                : confirmPass !== newPass
                                    ? 'Passwords do not match.'
                                    : null
                        }
                    ></PasswordInput>

                    <br>
                    <Button fullWidth={true}>Update Password</Button>
                </form>

            {/if}


            {#if linkExpired}
                <p style="text-align: center;">This link has either expired or is non-existant.</p>
                <br>
                <Button fullWidth={true} on:click={() => {goto('/forgot-password')}}>Request New Link</Button>
            {/if}

            {#if formErrorMessage && !linkExpired}
                <br>
                <FormError centerAlign={true} errorMessage={formErrorMessage} errorTitle={"Reset Password Error"}></FormError>
            {/if}




    {/if}
   

{/if}
