<script lang="ts">
    import './TextInput.css';
    import { createEventDispatcher } from 'svelte';

    export let label: string | null = null;
    export let value: string | null = null;
    export let id: string | null = null;
    export let placeholder: string | null = null;
    export let isInvalid: boolean = false;

    const dispatch = createEventDispatcher();

    function handleEmailInput(event: Event) {
        const target = event.target as HTMLInputElement;
        const cleanedValue = target.value.replace(/\s/g, '');
        target.value = cleanedValue;
        const newValue = cleanedValue.trim() === '' ? null : cleanedValue;
        dispatch('inputUpdated', newValue);
    }

    function preventSpace(event: KeyboardEvent) {
        if (event.key === ' ') {
            event.preventDefault();
        }
    }
</script>

<div class="inputWrapper">
    <label for={id}>{label}</label>
    <div class="shadowWrapper">
        <input
            class:invalid={isInvalid}
            id={id}
            value={value}
            type="email"
            on:input={handleEmailInput}
            on:keydown={preventSpace}
            placeholder={!placeholder ? label : placeholder}
            autocomplete="off"
            spellcheck="false"
        >
    </div>
</div>
