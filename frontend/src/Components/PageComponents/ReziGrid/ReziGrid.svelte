
<script lang="ts">
    export let heading: string = `Enter 'heading'`
    export let subHeading: string = `Enter 'subHeading'`
</script>


<style>
    .wrapG {
        width: 80%;
        margin: auto;
        max-width: 1650px;
        padding: 20px 0px 100px 0px;
    }
    .servicesWrap {
        border: 3px solid transparent;
        border-radius: 5px;
        width: calc(100%);
    }
    ul {
        margin: 0px;
        padding: 0px;
        margin-left: 40px;
    }
    .actualGrid {
        display: grid;
        grid-template-columns: repeat(2, 1fr);
        grid-gap: 50px;
    }
    @media (max-width: 1165px){
        .wrapG{
            width: 90%;
        }
    }
    @media (max-width: 900px){
        .actualGrid{
            grid-template-columns: repeat(1, 1fr);

        }
    }
    @media (max-width: 750px){
        .wrapG{
            padding: 20px 0px 50px 0px;

        }
    }
</style>


<div class="wrapG">
    <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">{subHeading}</p>
    <h2>{heading}</h2>

    <div class="actualGrid">
        <div class="servicesWrap">
            <h3>Roofing</h3>
            <ul>
                <li>Comprehensive roof inspections</li>
                <li>Expert installation of new roofing systems</li>
                <li>Roof maintenance and repair</li>
            </ul>
        </div>
        <div class="servicesWrap">
            <h3>Siding</h3>
            <ul>
                <li>Repair and replacement services to address wear and tear</li>
                <li>Professional installation with a focus on durability and visual appeal</li>
                <li>Wide selection of siding materials</li>
            </ul>
        </div>

        <div class="servicesWrap">
            <h3>Trim</h3>
            <ul>
                <li>Custom trim installation, including window casings, door frames, and baseboards</li>
                <li>A variety of material choices</li>
                <li>Protects the structural integrity of your home by sealing gaps and preventing moisture intrusion</li>
            </ul>
        </div>
        <div class="servicesWrap">
            <h3>Gutters</h3>
            <ul>
                <li>Gutter installation, ensuring proper water diversion away from your home</li>
                <li>Gutter cleaning, repair, and maintenance services</li>
                <li>Prevents water damage to your home’s foundation, siding, and landscape</li>
            </ul>
        </div>
    
    </div>

</div>