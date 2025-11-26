<script>
    import Loader from "../UI/Loaders/Loader.svelte";

    const backendUrl = import.meta.env.VITE_BACKEND_URL

    let email = ''
    let isLoading = false
    let joinedList = false

    let submitted = false
    let emailIsInvalid = true

    let formError = null

    async function joinNewsLetter(){
        submitted = true
        isLoading = true


        if (emailIsInvalid) {
            isLoading = false
            return
        }

        const postData = {email}

        try {

            const response = await fetch(`${backendUrl}/public/join-mailing-list`, {
                method: 'POST', 
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(postData)
            });


            const contentType = response.headers.get("content-type");
            if (!response.ok) {
                isLoading = false;
                // Check if the response is JSON before attempting to parse it
                if (contentType && contentType.indexOf("application/json") !== -1) {
                    const errorResponse = await response.json(); // Parse the JSON error response
                    formError = errorResponse.message
                    if (formError === 'Email already exists in the mailing list') {
                        isLoading = false
                        joinedList = true
                        return
                    }
                    console.error(errorResponse);

                    throw new Error(`${errorResponse.message}`);
                } else {
                    // Non-JSON response or no response body
                    throw new Error(`An unknown error occurred.`);
                }
            }
            const responseData = await response.json();
            // console.log(responseData)

            isLoading = false
            joinedList = true

        } catch (error) {
            isLoading = false
            console.log(error)
        }
    }

</script>

<style>
    .newsLetterInputWrap {
        display: flex;
        justify-content: space-between;
        position: relative;
    }
    input {
        background-color: transparent;
        width: calc(100%);
        padding: 15px;
        border: 1px solid rgb(142, 142, 142);
        outline: none;
        color: white;
	    font-family: "Plus Jakarta Sans", sans-serif;
        font-weight: 300;
        background-color: #1a1a1a;
    }
    input:focus {
        border: 1px solid rgb(213, 213, 213);

    }

    button {
        position: absolute;
        top: 50%;
        right: 0px;
        transform: translate(0%, -50%);
        border-color: white;
        color: black;
    }
    button:hover {
        background-color: #FF7B00;
        border-color: #FF7B00;
    }
    p {
        color: rgb(168, 168, 168);
    }

    input:-webkit-autofill,
    input:-webkit-autofill:hover, 
    input:-webkit-autofill:focus, 
    input:-webkit-autofill:active{
        -webkit-box-shadow: 0 0 0 40px #1a1a1a inset !important;
        -webkit-text-fill-color: rgb(231, 231, 231) !important;
        border-color: white;
    }
</style>


{#if !isLoading && !joinedList}
    
    <div>
        <h3 style="color: white;">Join Newsletter</h3>
        <p>Get updates about promotions, new products, and more! </p>
        <br>
        <div class="newsLetterInputWrap">
            <input type="text" placeholder="Email" id='emailCity' bind:value={email}/>
            <button on:click={joinNewsLetter}>Join </button>
        </div>
        {#if submitted && emailIsInvalid}
            <br>
            <p style="color: red;"> Please enter a valid email!</p>
        {/if}
        <!-- {#if submitted && formError}
            <br>
            <p style="color: red;">{formError}</p>
        {/if} -->
    </div>

{:else if isLoading && !joinedList}
    <Loader></Loader>

{:else if joinedList}
    <h3 style="color: white;">Success!</h3>
    <p>You have successfully joined our mailing list!</p>
{/if}


