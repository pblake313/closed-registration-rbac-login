<script lang="ts">
    import './EmailInput.css';

    export let label: string | null = null;
    export let value: string | null = null;
    export let id: string | null = null;
    export let placeholder: string | null = null;
    export let showInputError: boolean = true;
    export let inputErrorText: string | null = null;

    // Svelte 5-style callback prop
    export let onEmailChange: ((value: string | null) => void) | undefined;

    // Sanitize on input (after the character is entered)
    function handleEmailInput(event: Event) {
        const target = event.target as HTMLInputElement;

        // Lowercase everything
        let v = target.value.toLowerCase();

        // Only allow email-safe characters
        v = v.replace(/[^a-z0-9@._+-]/g, '');

        // Remove spaces just in case
        v = v.replace(/\s/g, '');

        target.value = v;

        const newValue = v.trim() === '' ? null : v;
        onEmailChange?.(newValue);
    }

    // Block invalid characters before they enter the field
    function preventInvalidKeys(event: KeyboardEvent) {
        const allowed = /^[a-z0-9@._+-]$/;

        // Always allow:
        if (['Backspace', 'Delete', 'ArrowLeft', 'ArrowRight', 'Tab'].includes(event.key)) {
            return;
        }

        // Block spaces
        if (event.key === ' ') {
            event.preventDefault();
            return;
        }

        // Block any character not in allowed list
        if (!allowed.test(event.key.toLowerCase())) {
            event.preventDefault();
        }
    }
</script>

<div class="emailInputWrapper">
    <label for={id}>{label}</label>

    <input
        class="emailInput"
        class:invalidEmailInput={inputErrorText}
        id={id}
        value={value ?? ''}
        type="text"
        on:input={handleEmailInput}
        on:keydown={preventInvalidKeys}
        placeholder={!placeholder ? label : placeholder}
    >

    {#if showInputError && inputErrorText}
        <p class="inputErrorText">{inputErrorText}</p>
    {/if}
</div>
