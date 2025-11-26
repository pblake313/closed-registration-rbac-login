<script lang="ts">
    import Angiesvg from "../../SVG/angiesvg.svelte";
    import Button from "../../UI/Button.svelte";

    export let estimateHeading: boolean = false

</script>


<div class="angieflex" class:estimateHeader={estimateHeading}>
    <img class="angiePic" src="/Images/Gutters/gutterup.jpg" alt="angie certified pro">
    <div class="angieOverlay"></div>
    <Angiesvg></Angiesvg>
    <p style="color: white;">Certified Pro</p>
</div>

<style>
    .angieflex {
        position: relative;
        display: flex;
        flex-direction: column;
        align-items: center;
        width: 90%;
        margin: auto;
        max-width: 1600px;
        margin-top: 150px;
        padding: 60px 0px;
        border-radius: 10px;
        overflow: hidden;
    }
    .estimateHeader {
        margin-top: 90px;
    }
    .angiePic {
        position: absolute;
        width: 100%;
        height: 100%;
        object-fit: cover;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        z-index: -5;
    }
    .angieOverlay {
        position: absolute;
        z-index: -2;
        width: 100%;
        height: 100%;
        position: absolute;
        top: 0px;
        left: 0px;
        background-color: rgba(0, 0, 0, 0.678);
    }
    @media (max-width: 1000px){
        .angieflex {
            margin-top: 75px;
        }
    }
</style>