<script lang="ts">
    import Arrow from "../SVG/Arrow.svelte";

    export let direction: 'right' | 'left' = 'right'

    export let isDisabled: boolean = false

</script>



<button class="swiperButton" disabled={isDisabled} on:click>

    <div class="containMain">

        {#if direction === 'right'}
            <div class="containRightArrow">
                <Arrow height={'20px'}></Arrow>
            </div>
        {/if}

        {#if direction === 'left'}
            <div class="containLeftArrow">
                <Arrow height={'20px'}></Arrow>
            </div>
        {/if}

    </div>

</button>

<style>
    .swiperButton {
        border: 1px solid #dbdbdb;
        padding: 0px;
        margin: 0px;
        height: 40px;
        width: 40px;
        border-radius: 40px;
        position: relative;
        /* overflow: hidden; */
        transition: .3s;
        background-color: #dbdbdb;
    }
    .swiperButton:active {
        background-color: #9e9e9e; /* or whatever color you want */
        transition: .3s;

    }
    .containMain {
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        padding-top: 3px;
    }
    .containLeftArrow {
        transform: rotate(180deg);
        padding-top: 6px;
    }
    .containRightArrow {
        padding-top: 1px;

    }
    .swiperButton:disabled {
        opacity: .4;
        cursor: auto;
    }

    @media(pointer:coarse){
        .swiperButton:focus {
            background-color: #dbdbdb;
        }
        .swiperButton:disabled:active {
            background-color: #dbdbdb;
        }
    }

</style>