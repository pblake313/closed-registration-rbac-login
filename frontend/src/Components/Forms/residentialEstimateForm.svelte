<script lang="ts">
    import Button from "../UI/Button.svelte";
    import TextInput from "../UI/TextInput.svelte";
    import Loader from "../UI/Loader.svelte"
    import Checkbox from "../UI/Checkbox.svelte";
    import TextArea from "../UI/TextArea.svelte";
    import SelectButton from "../UI/SelectButton.svelte";
    import './estimateform.css'
    import { stateOptions, urgentOptions } from "$lib/SelectOptions";
    import PhoneInput from "../UI/PhoneInput.svelte";
    import { isValidEmail, isValidPhoneNumber } from "../../utils/validators";
    import EmailInput from "../UI/EmailInput.svelte";
    import { get } from "svelte/store";
    import { adIdUsed } from "../../stores/AdIdStore";
    import Angiesvg from "../SVG/angiesvg.svelte";

    let submitted:boolean = false
    let name: string | null = null
    let phone:string = ''
    let email: string | null = null
    let isUrgent: boolean = false
    let streetAddress: string | null = null
    let city: string | null = null
    let state: string = 'Michigan'
    let message: string | null = null

    let roofingServices: boolean = false
    let sidingServices: boolean = false
    let trimServices: boolean = false
    let gutterServices: boolean = false

    let formError: string | null = null

    let isLoading: boolean = false
    let messageSent: boolean = false

    
    const backendUrl = import.meta.env.VITE_BACKEND_URL

    let formErrorTimeout: ReturnType<typeof setTimeout> | null = null;

    // This function will be called when the button is clicked
    async function submitContactForm() {

        submitted = true

        scrollTo(0, 0);


        if (!isValidContactForm()){
            setFormError('Whoops! You are missing required fields, or the required inputs are invalid.')
            return
        }

        const postObj = {
            name,
            phone,
            email,
            isUrgent,
            streetAddress,
            city,
            state,
            message,
            roofingServices,
            sidingServices,
            trimServices,
            gutterServices,
            adId: get(adIdUsed)
        }

        isLoading = true

        try {
            const response = await fetch(`${backendUrl}/public/contact`, {
                method: 'POST', 
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(postObj)
            });

            // Assuming the response is JSON
            const responseData = await response.json();
            // Do something with the response data
            if (responseData.error) {
                setFormError(responseData.error);
            }
            messageSent = true

        } catch (error) {
            // console.log(error)
            setFormError('Whoops! An unkown error has occured. Please try again later, or give us a call at (248) 388-8771.')
        } finally {
            isLoading = false
        }
    }


    function setFormError(errorMessage: string, duration = 7000) {
        formError = errorMessage;
        scrollTo(0, 0);

        if (formErrorTimeout) {
            clearTimeout(formErrorTimeout);
        }

        formErrorTimeout = setTimeout(() => {
            formError = null;
            formErrorTimeout = null;
        }, duration);
    }


    function isValidContactForm(): boolean{

        if (!name || !streetAddress || !city){
            return false
        }
        if (!isValidPhoneNumber(phone)) {
            return false
        }

        if (!isValidEmail(email)){
            return false
        }

        if (!sidingServices && !trimServices && !roofingServices && !gutterServices){
            console.log('no service selected')
            return false
        }

        return true
    }

    function handleUrgent(event: CustomEvent){
        console.log(event.detail)
        if (event.detail === 'Yes, this is urgent.') {
            isUrgent = true
        } else {
            isUrgent = false
        }
    }
</script>


<div class="wrapForm" style="transition: .3s;">
    {#if !messageSent}
        {#if !isLoading}


            <div class="estimateHeader">
                <div class="headSide">
                    <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">Quick Response Time</p>
                    <h2>Free Estimate</h2>
                    <p>Get a free estimate for roofing, siding, trim, or gutter work — whether it's a new installation or routine maintenance.</p>
                </div>
        
            </div>
            {#if formError}
                <br>
                <div class="invalidService">
                    <p style="color: red;">{formError}</p>
                </div>
                <br>
            {/if}
            <br>
            <div class="flexInputs">
                <div class="wrapIn">
                    <TextInput isInvalid={!name && submitted} id='name' label='Name' value={name} on:inputUpdated={(e) => name = e.detail} />
                </div>

                <div class="wrapIn">
                    <PhoneInput isInvalid={!isValidPhoneNumber(phone) && submitted} on:inputUpdated={(e) => phone = e.detail} id='phone' label='Phone' value={phone}></PhoneInput>
                </div>
                
                <div class="wrapIn">
                    <EmailInput isInvalid={!isValidEmail(email) && submitted} id='email' label='Email' value={email} on:inputUpdated={(e) => email = e.detail}></EmailInput>
                </div>

                <div class="wrapIn">
                    <SelectButton on:select={(e) => handleUrgent(e)} selectedOption={isUrgent ? 'Yes, this is urgent.' : 'This is not urgent.'} options={urgentOptions} label='Is this an Emergency?' id='emergencySelect'></SelectButton>
                </div>
            </div>

            <div class="flexInputs">
                <div class="addyIn">
                    <TextInput isInvalid={!streetAddress && submitted} id='address' label='Address' value={streetAddress} on:inputUpdated={(e) => streetAddress = e.detail} />
                </div>
                <div class="wrapIn">
                    <TextInput isInvalid={!city && submitted} on:inputUpdated={(e) => city = e.detail}  id='city' label='City' value={city} />
                </div>
                <div class="wrapIn">
                    <SelectButton selectedOption={state} on:select={(e) => state = e.detail} options={stateOptions} label='State' id='state'></SelectButton>
                </div>
            </div>

            
            <TextArea value={message} on:inputUpdated={(e) => message = e.detail} label='Message' placeholder='Message' ></TextArea>
            <br>
            <h3>Services Requested</h3>

            {#if submitted && !roofingServices && !sidingServices && !trimServices && !gutterServices}
                <div class="invalidService">
                    <p style="color:  red;">Please select a service below.</p>
                </div>
                <br>
            
            {/if}
            <div class="checkFlex">
                <div class="checkWrap">
                    <Checkbox value={roofingServices} on:radioChanged={(e) => roofingServices = e.detail} label="Roofing" checkboxId="roofing"/>
                </div>
                <div class="checkWrap">
                    <Checkbox value={sidingServices} on:radioChanged={(e) => sidingServices = e.detail} label="Siding"  checkboxId="siding"/>
                </div>
                <div class="checkWrap">
                    <Checkbox value={trimServices} on:radioChanged={(e) => trimServices = e.detail}  label="Trim"  checkboxId="trim"/>
                </div>
                <div class="checkWrap">
                    <Checkbox value={gutterServices} on:radioChanged={(e) => gutterServices = e.detail}  label="Gutters" checkboxId="gutters"/>
                </div>
            </div>
            <br>
            <div class="requestDesk">
                <Button on:click={submitContactForm}>Request Quote</Button>
            </div>
            <div class="requestMobile">
                <Button fullWidth={true} on:click={submitContactForm}>Request Quote</Button>
            </div>
      
        {:else}
            <div class="loadWrap">
                <Loader></Loader>
            </div>
        {/if}

    {:else}
        <div class="loadWrap">
            <div style="margin: auto; text-align: center;">
                <h4>Success!</h4>
                <p>Your message was sent successfully! We will reach out to you shortly!</p>
            </div>

        </div>
    {/if}

</div>
