<script lang="ts">
    import './PasswordInput.css';

    export let label: string | null = null;
    export let value: string | null = null;
    export let id: string | null = null;
    export let placeholder: string | null = null;
    export let showInputError: boolean = true;
    export let inputErrorText: string | null = null;
    export let maxChar: number = 150;

    export let onPasswordChange: ((value: string | null) => void) | undefined;

    // NEW PROP — lets YOU control if requirements appear
    export let showPasswordRequirements: boolean = true;

    let show = false;
    let focused = false;

    function handleTextInput(event: Event) {
        const target = event.target as HTMLInputElement;

        // prevent spaces
        const raw = target.value.replace(/\s+/g, "");

        const newValue = raw === '' ? null : raw;
        onPasswordChange?.(newValue);

        value = raw;
    }

    function toggleShow() {
        show = !show;
    }

    // password rule checks
    $: hasCapital = !!value && /[A-Z]/.test(value);
    $: hasNumber = !!value && /\d/.test(value);
    $: longEnough = !!value && value.length >= 9;
    $: hasSpecial = !!value && /[^A-Za-z0-9]/.test(value);

    // now REQUIREMENTS only show if:
    // 1) input is focused
    // 2) caller ENABLED it
    // 3) something is unmet
    $: showReq =
        showPasswordRequirements &&
        focused &&
        (!hasCapital || !hasNumber || !longEnough || !hasSpecial);
</script>

<div class="textInputWrapper">
    <label for={id}>{label}</label>

    <div class="shadowWrapper passwordWrapper">
        <input
            id={id}
            class="textInput"
            class:invalidTextInput={inputErrorText}
            type={show ? "text" : "password"}
            value={value ?? ''}            
            maxlength={maxChar}
            on:input={handleTextInput}
            on:focus={() => (focused = true)}
            on:blur={() => (focused = false)}
            placeholder={!placeholder ? label : placeholder}
            autocomplete="off"
        >

        {#if value && value.length > 0}
            <button 
                type="button" 
                class="toggleBtn" 
                on:click={toggleShow}
            >
                {#if show} 👁️ {:else} 👁️‍🗨️ {/if}
            </button>
        {/if}
    </div>

    {#if showInputError && inputErrorText}
        <p class="inputErrorText">{inputErrorText}</p>
    {/if}
</div>

<!-- Only show IF showReq is true -->
{#if showReq}
    <div class="passwordRequirements">
        {#if !hasCapital}<p>1 Capital Letter</p>{/if}
        {#if !hasNumber}<p>1 Number</p>{/if}
        {#if !longEnough}<p>9 Characters Long</p>{/if}
        {#if !hasSpecial}<p>1 Special Character</p>{/if}
    </div>
{/if}
