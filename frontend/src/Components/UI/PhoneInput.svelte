<script lang="ts">
    import './PhoneInput.css';

    export let label: string | null = null;
    export let value: string | null = null;
    export let id: string | null = null;
    export let placeholder: string | null = null;
    export let isInvalid: boolean = false;

    // Svelte 5 callback prop
    export let onPhoneChange: ((value: string | null) => void) | undefined;

    function formatPhoneNumber(input: string): string {
        const digits = input.replace(/\D/g, '').slice(0, 10); // max 10 digits
        const parts: string[] = [];

        if (digits.length > 0) parts.push('(' + digits.slice(0, 3));
        if (digits.length >= 4) parts[0] += ')';
        if (digits.length >= 4) parts.push(' ' + digits.slice(3, 6));
        if (digits.length >= 7) parts.push('-' + digits.slice(6, 10));

        return parts.join('');
    }

    function handleTextInput(event: Event) {
        const target = event.target as HTMLInputElement;

        const formatted = formatPhoneNumber(target.value);
        target.value = formatted;

        const newValue = formatted.trim() === '' ? null : formatted;

        onPhoneChange?.(newValue);
    }
</script>

<div class="phoneInputWrapper">
    <label for={id}>{label}</label>
	<input
		class="phoneInput"
		id={id}
		class:invalidPhone={isInvalid}
		value={value ?? ''}
		type="text"
		on:input={handleTextInput}
		placeholder={!placeholder ? label : placeholder}
		autocomplete="off"
	>
</div>
