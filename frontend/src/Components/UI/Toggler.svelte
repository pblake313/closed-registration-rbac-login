<script lang="ts">
    import './Toggler.css';

    export let value: boolean = false;

    // Svelte 5 callback-style event prop
    export let onChange: ((newValue: boolean) => void) | undefined;

    function toggle() {
        const newValue = !value;   // derive new
        onChange?.(newValue);      // tell parent
    }

    function handleKey(e: KeyboardEvent) {
        if (e.key === ' ' || e.key === 'Enter') {
            e.preventDefault();
            toggle();
        }
    }
</script>

<button
    type="button"
    class="toggleWrapper"
    on:click={toggle}
    aria-pressed={value}
>
    <div class="toggleTrack {value ? 'on' : 'off'}">
        <div class="toggleThumb {value ? 'thumb-on' : 'thumb-off'}"></div>
    </div>
</button>
