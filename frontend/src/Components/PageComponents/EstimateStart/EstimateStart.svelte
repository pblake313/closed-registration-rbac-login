<script>
    import Button from '../../UI/Button.svelte';
import './EstimateStart.css'
</script>


<!-- removed comments below....  
    also updated two classes in estimateStart.css 
    also removed the engineering services form on route /estimates/engineering-services
-->

<div class="wrapStart">
    <br>
    <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">Free estimates</p>
    <h2>Select an estimate type.</h2>
    <br>
    <div class="estimatesWrapper">
        <a class="singleEstimate" href="/estimates/residential">
            <h3>Residential Estimate</h3>
            <p>Ideal for home repairs, setups, or remodels.</p>
            <br>
            <div class="checker">
                <img class="check" src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FIcons%2Fcheck.png?alt=media&token=f57df295-557c-4219-8cea-9f7902a30c58" alt="checkmark">
                <div class="textCo">
                    <h4>Roofing</h4>
                </div>
            </div>
            <div class="checker">
                <img class="check" src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FIcons%2Fcheck.png?alt=media&token=f57df295-557c-4219-8cea-9f7902a30c58" alt="checkmark">
                <div class="textCo">
                    <h4>Siding</h4>
                </div>
            </div>
            <div class="checker">
                <img class="check" src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FIcons%2Fcheck.png?alt=media&token=f57df295-557c-4219-8cea-9f7902a30c58" alt="checkmark">
                <div class="textCo">
                    <h4>Trim</h4>
                </div>
            </div>
            <div class="checker">
                <img class="check" src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FIcons%2Fcheck.png?alt=media&token=f57df295-557c-4219-8cea-9f7902a30c58" alt="checkmark">
                <div class="textCo">
                    <h4>Gutters</h4>
                </div>
            </div>
            <br>
            <Button buttonClass='secondary'>Get Residential Estimate</Button>
        </a>


        <!-- <a class="singleEstimate two" href="/estimates/engineering-services">
            <h3>Engineering Services Estimate</h3>
            <p>Add an experienced manufacturing professional to your team today, on contract, contract to direct, or direct basis!</p>
            <br>
            <div class="checker">
                <img class="check" src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FIcons%2Fcheck.png?alt=media&token=f57df295-557c-4219-8cea-9f7902a30c58" alt="checkmark">
                <div class="textCo">
                    <h4>Project / Program Managers</h4>
                </div>
            </div>
            <div class="checker">
                <img class="check" src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FIcons%2Fcheck.png?alt=media&token=f57df295-557c-4219-8cea-9f7902a30c58" alt="checkmark">
                <div class="textCo">
                    <h4>AutoCAD Engineers</h4>
                </div>
            </div>
            <div class="checker">
                <img class="check" src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FIcons%2Fcheck.png?alt=media&token=f57df295-557c-4219-8cea-9f7902a30c58" alt="checkmark">
                <div class="textCo">
                    <h4>Controls Engineers</h4>
                </div>
            </div>
            <div class="checker">
                <img class="check" src="https://firebasestorage.googleapis.com/v0/b/sa-construction-dc032.appspot.com/o/Public%2FIcons%2Fcheck.png?alt=media&token=f57df295-557c-4219-8cea-9f7902a30c58" alt="checkmark">
                <div class="textCo">
                    <h4>Manufacturing Engineers</h4>
                </div>
            </div>
            <br>
            <Button buttonClass='secondary'>Get Engineering Services Estimate</Button>
        </a> -->



    </div>

</div>

<div class="learnMore">
    <p style="text-transform: uppercase; font-size: 9pt; letter-spacing: .15em;">More Information</p>
    <h2>Services Overview</h2>
    <br>
    <div class="learnWrap">
        <div class="servicesSide">
            <h3>Residential</h3>
            <br>
            <p>S.A. Construction specializes in providing top-tier roofing solutions, expertly crafted trim work, and seamless gutter installations. Our dedicated team of professionals brings unparalleled expertise and attention to detail to every project, ensuring that your home or business not only looks its best but also benefits from the highest quality of construction available. With a commitment to excellence and customer satisfaction, S.A. Construction is your trusted partner for all your roofing, trim, and gutter needs.</p>
            <br>
            <p>For more information on our residential construction services, click the button below.</p>
            <br>
            <a href="/residential">
                <Button buttonClass='secondary'>Residential Services</Button>
            </a>
        </div>


        <!-- <div class="servicesSide two">
            <h3>Engineering Services</h3>
            <br>
            <p>S.A. Construction offers specialized on-site engineering support tailored to the manufacturing, heavy industrial, and automotive industries. Our comprehensive suite of services includes deploying expert engineers directly to your location, where they can provide immediate, hands-on assistance with a range of projects. Some of the positions we offare are ...</p>
            <ul>
                <li>Controls Engineers</li>
                <li>Project Managers</li>
                <li>Project Schedulers</li>
                <li>Manufacturing Engineers</li>
                <li>Electrical Engineers</li>
                <li>Mechanical Engineers</li>
                <li>Industrial Engineers</li>
            </ul>
            <br>
            <p>For a full list of the engineering services provided click the button below.</p>
            <br>
            <a href="/engineering">
                <Button buttonClass='secondary'>Engineering Services</Button>
            </a>
        </div> -->


    </div>
</div>