<script lang="ts">

    export let buttonClass: 'primary' | 'secondary' | 'darkback' | 'stayWhite'  = 'primary'
    export let fullWidth: boolean = false

</script>


<button class="{buttonClass}" class:fuller={fullWidth} on:click>
    <slot></slot>
</button>

<style>
    .primary {
        background-color: #FF7B00;
        color: white;
    }
    .primary:hover {
        background-color: #da6a00;
    }

    .secondary {
        background-color: transparent;
        color: rgb(60, 60, 60);
        border-color: lightgray;
    }
    .secondary:hover {
      background-color: rgb(60, 60, 60);  
      color: white;
    }


    .darkback {
        background-color: #3c3c3c;  
        border-color: #3c3c3c;
        color: white;

    }
    .darkback:hover {
        background-color: white;
        color: rgb(60, 60, 60);
    }



    .stayWhite{
        background-color: #eeeeee;
        color: black;
        border-color: #eeeeee;

    }
    .fuller {
        width: 100%;
    }
    @media (pointer: coarse) {
        .primary:hover {
            background-color: #FF7B00;
        }

        .secondary:hover {
            background-color: transparent;
            color: white;
        }

        .darkback:hover {
            background-color: #3c3c3c;  
            color: rgb(60, 60, 60);
        }
        .stayWhite:hover{
            background-color: #eeeeee;
            color: black;
            border-color: #eeeeee;
        }
    }
</style>