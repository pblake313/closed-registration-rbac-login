<script>

    import TextInput from "../UI/TextInput.svelte";
    import {isValidPhone, isValidEmail} from '../../utils/validators'
    import {formatPhoneNumber} from '../../utils/formatters'
    import TextArea from "../UI/TextArea.svelte";
    import SelectButton from "../UI/SelectButton.svelte";
    import Button from "../UI/Button.svelte";
    import Loader from "../UI/Loader.svelte"


    const backendUrl = import.meta.env.VITE_BACKEND_URL


    let submitted = false

    let name = ''
    let phone = ''
    let email = ''      
    let projectDetails = ''
    let startDate = ''
    let jobLocation = ''
    let position = ''


    let isLoading = false
    let emailSent = false

    let formError = null

    const positionOptions = [
        'Other',
        'AutoCAD Engineer',
        'Controls Designer',
        'Controls Engineer',
        'Conveyor Engineer',
        'Electrical Engineer',
        'Industrial Engineer',
        'Manufacturing Engineer',
        'Mechanical Engineer',
        'PLC Programmer',
        'Plant Layout Designer',
        'Project Manager',
        'Project Scheduler',
        'Quality Engineer',
        'Robotics Engineer',
        'Safety Engineer',
        'Software Engineer',
        'Tooling Engineer'
    ]
    const startDateOptions = [
        'Less than 1 week',
        '1-2 weeks',
        '2-4 weeks',
        'Greater than 1 month'
    ]

    function handlePhoneInput(event) {
        const inputVal = event.target.value;
        const formattedPhone = formatPhoneNumber(inputVal);
        // Update the input field's value to only display the formatted phone number
        event.target.value = formattedPhone;
        phone = formattedPhone; // Make sure to update the phone variable with the formatted number
    }

    function selectPosition(event){
        position = event.detail
    }
    function selectStartDate(event){
        startDate = event.detail
    }

    async function requestEstimate(){
        submitted = true
        isLoading = true


        if (!isValidEmail(email)){
            isLoading = false
            return
        }
        if (!isValidPhone(phone)){
            isLoading = false
            return
        }
        if (name === '' || jobLocation === '' || position === '' || startDate === ''){
            isLoading = false
            return
        }

        const postObj = {
            name,
            phone,
            email,
            position,
            startDate,
            jobLocation,
            projectDetails
        }

        try {

            const response = await fetch(`${backendUrl}/public/engineering-estimate`, {
                method: 'POST', 
                headers: {
                    'Content-Type': 'application/json'
                },
                body: JSON.stringify(postObj)
            });


            const contentType = response.headers.get("content-type");
            if (!response.ok) {
                // Check if the response is JSON before attempting to parse it
                if (contentType && contentType.indexOf("application/json") !== -1) {
                    const errorResponse = await response.json(); // Parse the JSON error response
                    formError = errorResponse.message
                    throw new Error(`${errorResponse.message}`);
                } else {
                    // Non-JSON response or no response body
                    throw new Error(`An unknown error occurred.`);
                }
            }
            // Assuming the response is JSON
            const responseData = await response.json();
            // console.log(responseData);

            isLoading = false
            emailSent = true

        } catch (error) {
            console.log(error)
            isLoading = false   

        }
    }

</script>

<div class="wrapAll">
    {#if !isLoading && !emailSent}
        <div>
            <div class="flexInputs">
                <div class="wrapIn">
                    <TextInput extraClass={submitted && name === '' ? 'invalid' : ''} id='name' label='Name' value={name} on:input={event => (name = event.target.value)}/>
                </div>
        
                <div class="wrapIn">
                    <TextInput extraClass={submitted && !isValidPhone(phone) ? 'invalid' : ''} id='phone' label='Phone' value={phone} on:input={handlePhoneInput}/>
                </div>
                
                <div class="wrapIn">
                    <TextInput extraClass={submitted && !isValidEmail(email) ? 'invalid' : ''}  id='email' label='Email' value={email} on:input={event => (email = event.target.value)}/>
                </div>
            </div>
        
            <div class="flexInputs">
                <div class="wrapIn okya">
                    <SelectButton extraClass={submitted && position === '' ? 'invalid' : ''} options={positionOptions} label='Position Needed' id='posSelect' on:select={selectPosition}></SelectButton>
                </div>
                <div class="wrapIn okya">
                    <SelectButton extraClass={submitted && startDate === '' ? 'invalid' : ''} options={startDateOptions} label='Job Start Date' id='startDate' on:select={selectStartDate}></SelectButton>
                </div>
                <div class="wrapIn">
                    <TextInput extraClass={submitted && jobLocation === '' ? 'invalid' : ''} id='jobLocation' label='Job Location' value={jobLocation} on:input={event => (jobLocation = event.target.value)}/>
                </div>
            </div>
        
            <TextArea label='Project Details' placeholder='Tell us about your project.' on:input={event => (projectDetails = event.target.value)}></TextArea>
            <br>
            <Button on:click={requestEstimate}>Request Quote</Button>
        
            {#if formError}
                <br><br>
                <p style="color: red;">{formError}</p>
                
            {/if}
        </div>

    {:else if isLoading && !emailSent}
        <Loader></Loader>

    {:else if emailSent}
        <div class="loadWrap">
            <div style="margin: auto; text-align: center;">
                <h4>Succeess!</h4>
                <p>Your message was sent successfully! We will reach out to you shortly!</p>
            </div>

        </div>
    {/if}


</div>


<style>
    .flexInputs {
        display: flex;
        justify-content: space-between;
    }
    .wrapIn {
        width: calc(33% - 20px);
    }

    .wrapAll {
        min-height: calc(100vh - 790px);
    }
    .loadWrap{
        min-height: calc(100vh - 814px);
        display: flex;
        flex-direction: column;
        justify-content: space-evenly;
    }

    @media (max-width: 775px){
        .flexInputs {
            flex-direction: column;
        }
        .wrapIn {
            width: 100%;
        }
        .okya {
            margin-bottom: 20px;
        }
    }
</style>