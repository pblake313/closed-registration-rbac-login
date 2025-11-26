<script lang="ts">
    import './TextInput.css';

    export let label: string | null = null;
    export let value: string | null = null;
    export let id: string | null = null;
    export let placeholder: string | null = null;
    export let showInputError: boolean = true;
    export let inputErrorText: string | null = null;
    export let maxChar: number = 150;

    // Svelte 5-style callback prop
    export let onTextChange: ((value: string | null) => void) | undefined;

    function handleTextInput(event: Event) {
        const target = event.target as HTMLInputElement;
        const raw = target.value;
        const newValue = raw.trim() === '' ? null : raw;
        onTextChange?.(newValue);
    }
</script>

<div class="textInputWrapper">
    <label for={id}>{label}</label>
    <div class="shadowWrapper">
        <input
            id={id}
            class="textInput"
            class:invalidTextInput={inputErrorText}
            type="text"
            value={value ?? ''}            
            maxlength={maxChar}
            on:input={handleTextInput}
            placeholder={!placeholder ? label : placeholder}
            autocomplete="off"
        >
    </div>

    {#if showInputError && inputErrorText}
        <p class="inputErrorText">{inputErrorText}</p>
    {/if}
</div>
