<script lang="ts">
    import type { User } from "$lib/types/User";
    import { publicFetch } from "../../../fetchers/PublicFetch";
    import { pushNotification } from "../../../stores/DashboardStores/NotificationStore";
    import { accessToken } from "../../../stores/TokenStore";
    import { authenticatedUser } from "../../../stores/UserStore";
    import { isValidEmail } from "../../../utils/validators";
    import Button from "../../UI/Button.svelte";
    import EmailInput from "../../UI/EmailInput.svelte";
    import FormError from "../../UI/FormError.svelte";
    import Loader from "../../UI/Loaders/Loader.svelte";
    import PasswordInput from "../../UI/PasswordInput.svelte";

    let errorMessage: string | null = null;
    let isLoggingIn: boolean = false;

    let email: string | null = null;
    let password: string | null = null;
    let formSubmitted: boolean = false;

    // keep track of the active timeout
    let errorTimeout: ReturnType<typeof setTimeout> | null = null;

    function clearError() {
        errorMessage = null;
        if (errorTimeout) {
            clearTimeout(errorTimeout);
            errorTimeout = null;
        }
    }

    async function login(event: SubmitEvent) {
        event.preventDefault(); // optional but recommended

        formSubmitted = true;

        // reset error on every submit
        clearError();

        if (!password || !isValidEmail(email)) {
            return;
        }

        try {
            isLoggingIn = true;

            let postObj = {
                Email: email,
                Password: password
            };

            const response = await publicFetch('/Auth/Login', {
                method: "POST",
                body: JSON.stringify(postObj)
            });

            if (response.accessToken) {
                accessToken.set(response.accessToken);

                const authUser = response.authenticatedUser;

                if (authUser) {
                    const userToset: User = {
                        UserId: authUser.userId,
                        Email: authUser.email,
                        FirstName: authUser.firstName,
                        LastName: authUser.lastName,
                        DateCreated: authUser.dateCreated,
                        UpdatedAt: authUser.updatedeAt,
                        LastPasswordResetEmailSentAt: authUser.lastPasswordResetEmailSentAt,
                        permissions: {
                            JobPostings: authUser.jobPostings,
                            AccountManagement: authUser.accountManagement,
                            ViewCandidates: authUser.viewCandidates
                        }
                    };

                    authenticatedUser.set(userToset);
                }
            } else {
                pushNotification(
                    'Login Error',
                    "A successful request was made, but no access token was returned. Please try again later.",
                    'error',
                    5.5,
                    false
                );
            }
        } catch (error: any) {
            // clear any existing timeout before setting a new error
            if (errorTimeout) {
                clearTimeout(errorTimeout);
                errorTimeout = null;
            }

            errorMessage =
                error?.message ||
                error?.data?.message ||
                'An unknown error has occurred.';

            // start a fresh timer for this specific message
            errorTimeout = setTimeout(() => {
                errorMessage = null;
                errorTimeout = null;
            }, 6000);
        } finally {
            isLoggingIn = false;
        }
    }
</script>



<form on:submit={login}>
    <h3 style="font-size: 20pt; text-align: center;">Login</h3>
    <br>


    {#if !isLoggingIn}
    
        <div style="margin-bottom: 20px;">
            <EmailInput 
                onEmailChange={(v) => {
                    email = v
                }} 
                label={'Email'}
                inputErrorText={!isValidEmail(email) && formSubmitted ? "Enter a valid email address." : null}
                value={email}
            ></EmailInput>
        </div>
        <div style="margin-bottom: 20px;">
            <PasswordInput 
                showPasswordRequirements={false} 
                onPasswordChange={(v) => {
                    password = v
                }} 
                inputErrorText={!password && formSubmitted ? "Enter a password." : null}
                value={password}
                label={'Password'}
            ></PasswordInput>
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


