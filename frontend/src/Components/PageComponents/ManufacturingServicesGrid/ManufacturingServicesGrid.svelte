<script>
    import Button from '../../UI/Button.svelte';
import './ManufacturingServicesGrid.css'
</script>

<div class="gridder">
    <div class="finnaWraa">
        <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">Manufacturing & Engineering Services</p>
        <h2>Engineering services tailored to your business.</h2>
    </div>
    <br>
    <div class="manGrid">
        <div class="manItem">
       
            <div class="imageHold">
                <img src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FImages%2FEngineeringPics%2Fsquarecar.png?alt=media&token=5b47ba74-26fb-4232-bf44-d2fbee90e150" alt="Project Planning">
            </div>
            <br>
            <h3>Project Planning</h3>
            <p>Streamlining manufacturing processes for optimal efficiency and innovation in every project in a timely manner.</p>
            <br>
            <a href="/engineering">
                <Button buttonClass='secondary'>Learn More</Button>
            </a>

        </div>
        <div class="manItem">
         
            <div class="imageHold">
                <img src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FImages%2Fcityconstruct.jpg?alt=media&token=2a20cbbd-fb7f-4cf7-a876-f562a7dea235" alt="Building project management">
            </div>
            <br>
            <h3>CAD Engineers</h3>
            <p>Expertise across diverse CAD software: AutoCAD, SolidWorks, Revit, and Fusion 360, empowering precise design and engineering solutions.</p>
            <br>
            <a href="/engineering">
                <Button buttonClass='secondary'>Learn More</Button>
            
            </a>

        </div>
        <div class="manItem">
            <div class="imageHold">
                <img src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FImages%2Fmanufacturingcar.jpg?alt=media&token=712cd2e3-ff2c-4d4a-a570-535d26e89136" alt="Manufacturing Engineering">
            </div>
            <br>
            <h3>Manufacturing Engineers</h3>
            <p>Specializing in process optimization, product design, quality control, and automation for cutting-edge manufacturing excellence.</p>
            <br>
            <a href="/engineering">
                <Button buttonClass='secondary'>Learn More</Button>
            
            </a>

        </div>
        
    </div>
</div>