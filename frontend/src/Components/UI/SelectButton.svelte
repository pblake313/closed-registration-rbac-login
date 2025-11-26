<script lang="ts">
    import { createEventDispatcher, onMount } from 'svelte';
    import './SelectButton.css'
  
    export let options: string[] = [];
    export let label = '';
    export let id = '';
    export let extraClass = ''
  
    let isOpen = false;
    export let selectedOption: string | null = null;
  
    const dispatch = createEventDispatcher();
  
    function selectOption(option: any) {
		selectedOption = option;
		isOpen = false;
		dispatch('select', option);
    }
  
    // Initialize selected option if autoSelectFirstOption is true
    onMount(() => {
	
		const handleClickOutside = (event: any) => {
			if (!event.target.closest(`#${id}`)) {
			isOpen = false;
			}
		};
	
		document.addEventListener('click', handleClickOutside);
		return () => document.removeEventListener('click', handleClickOutside);
    });
</script>
  
  

  
<div class="dropdown-container" id={id}>
    <label class="dropdown-label" for={id}>{label}</label>
    <button class="selbut dropdown-btn {isOpen ? 'dropdown-open' : ''} {extraClass}" tabindex="0" on:click={() => isOpen = !isOpen}>
      	{selectedOption || 'Select...'}
    </button>
    {#if isOpen}
		<div class="options-container">
			{#each options as option}
				<button class="selbut option {selectedOption === option ? 'option-selected' : ''}" on:click={() => selectOption(option)}>
					{option}
				</button>
			{/each}
		</div>
    {/if}
</div>
  