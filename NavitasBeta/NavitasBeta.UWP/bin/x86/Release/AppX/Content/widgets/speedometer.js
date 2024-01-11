        console.log("heh");

        $(document).ready(function ()
        {
            var iCurrentSpeed = 0,
                iTargetSpeed = 0,
                job = null;

            function degToRad(angle)
            {
                // Degrees to radians
                return ((angle * Math.PI) / 180);
            }

            function radToDeg(angle)
            {
                // Radians to degree
                return ((angle * 180) / Math.PI);
            }

            function drawLine(options, line)
            {
                // Draw a line using the line object passed in
                options.ctx1.beginPath();
                // Set attributes of open
                options.ctx1.globalAlpha = line.alpha;
                options.ctx1.lineWidth = line.lineWidth;
                options.ctx1.fillStyle = line.fillStyle;
                options.ctx1.strokeStyle = line.fillStyle;
                options.ctx1.moveTo(line.from.X,
                    line.from.Y);
                // Plot the line
                options.ctx1.lineTo(
                    line.to.X,
                    line.to.Y
                );
                options.ctx1.stroke();
            }

            function drawTriangle(options, triangle, )
            {
                // Draw a triangle using the triangle object passed in
                options.ctx2.beginPath();
                // Set attributes of open
                options.ctx2.globalAlpha = triangle.alpha;
                options.ctx2.lineWidth = triangle.lineWidth;
                options.ctx2.fillStyle = triangle.fillStyle;
                options.ctx2.strokeStyle = triangle.fillStyle;
                options.ctx2.moveTo(triangle.from.X1, triangle.from.Y1);
                // Plot the triangle
                options.ctx2.lineTo(triangle.to.X, triangle.to.Y);
                options.ctx2.lineTo(triangle.from.X2, triangle.from.Y2);
                options.ctx2.closePath();
                // the outline
                options.ctx2.lineWidth = 1;
                options.ctx2.strokeStyle = '#FFFFFF';
                options.ctx2.stroke();
                // the fill color
                options.ctx2.fillStyle = triangle.fillStyle;
                options.ctx2.fill();
            }

            function createLine(fromX, fromY, toX, toY, fillStyle, lineWidth, alpha)
            {
                // Create a line object using Javascript object notation
                return {
                    from:
                    {
                        X: fromX,
                        Y: fromY
                    },
                    to:
                    {
                        X: toX,
                        Y: toY
                    },
                    fillStyle: fillStyle,
                    lineWidth: lineWidth,
                    alpha: alpha
                };
            }

            function createTriangle(fromX1, fromY1, fromX2, fromY2, toX, toY, fillStyle, lineWidth, alpha)
            {
                // Create a line object using Javascript object notation
                return {
                    from:
                    {
                        X1: fromX1,
                        Y1: fromY1,
                        X2: fromX2,
                        Y2: fromY2,
                    },
                    to:
                    {
                        X: toX,
                        Y: toY
                    },
                    fillStyle: fillStyle,
                    lineWidth: lineWidth,
                    alpha: alpha
                };
            }

            function drawOuterMetallicArc(options)
            {
                /* Draw the metallic border of the speedometer
                 * Outer grey area
                 */
                options.ctx1.beginPath();
                // Nice shade of grey
                options.ctx1.strokeStyle = options.lineColor;
                // Draw the outer circle
                options.ctx1.arc(options.center.X,
                    options.center.Y,
                    options.radius,
                    Math.PI + (Math.PI / 180 * -30),
                    0 - (Math.PI / 180 * -30),
                    false);
                // Fill the last object
                options.ctx1.stroke();
                // options.ctx1.beginPath();
                // options.ctx1.globalAlpha = alphaValue;
                // options.ctx1.lineWidth = 5;
                // options.ctx1.strokeStyle = strokeStyle;
                // options.ctx1.arc(options.center.X,
                //     options.center.Y,
                //     options.levelRadius,
                //     Math.PI + (Math.PI / 180 * startPos),
                //     0 - (Math.PI / 180 * startPos),
                //     false);
                // options.ctx1.stroke();
            }

            function drawInnerMetallicArc(options)
            {
                /* Draw the metallic border of the speedometer
                 * Inner white area
                 */
                options.ctx1.beginPath();
                // White
                options.ctx1.fillStyle = options.lineColor;
                options.ctx1.strokeStyle = options.lineColor;
                options.ctx1.lineWidth = 5;
                // Outer circle (subtle edge in the grey)
                options.ctx1.arc(options.center.X,
                    options.center.Y,
                    (options.radius * .8),
                    Math.PI + (Math.PI / 180 * -30),
                    Math.PI + (Math.PI / 180 * (180 + 30)),
                    false);
                options.ctx1.stroke();
            }

            function drawMetallicArc(options)
            {
                /* Draw the metallic border of the speedometer
                 * by drawing two semi-circles, one over lapping
                 * the other with a bot of alpha transparency
                 */
                //drawOuterMetallicArc(options);
                drawInnerMetallicArc(options);
            }

            function drawBackground(options)
            {
                /* Black background with alphs transparency to
                 * blend the edges of the metallic edge and
                 * black background
                 */
                var i = 0;
                options.ctx1.globalAlpha = 0.2;
                options.ctx1.fillStyle = "rgb(0,0,0)";
                // Draw semi-transparent circles
                for (i = options.gaugeOptions.radius - 30; i < options.gaugeOptions.radius + 10; i++)
                {
                    options.ctx1.beginPath();
                    options.ctx1.arc(options.center.X,
                        options.center.Y,
                        i,
                        Math.PI + (Math.PI / 180 * -30),
                        0 - (Math.PI / 180 * -30),
                        false);
                    options.ctx1.fill();
                }
            }

            function applyDefaultContextSettings(options)
            {
                /* Helper function to revert to gauges
                 * default settings
                 */
                options.ctx1.lineWidth = 2;
                options.ctx1.globalAlpha = 1;
                options.ctx1.strokeStyle = options.lineColor;
                options.ctx1.fillStyle = options.lineColor;
                options.ctx2.lineWidth = 2;
                options.ctx2.globalAlpha = 1;
                options.ctx2.strokeStyle = options.lineColor;
                options.ctx2.fillStyle = options.lineColor;
            }

            function drawSmallTickMarks(options)
            {
                /* The small tick marks against the coloured
                 * arc drawn every 5 mph from 10 degrees to
                 * 170 degrees.
                 */
                var tickvalue = options.levelRadius - 8,
                    iTick = 0,
                    gaugeOptions = options.gaugeOptions,
                    iTickRad = 0,
                    onArchX,
                    onArchY,
                    innerTickX,
                    innerTickY,
                    fromX,
                    fromY,
                    line,
                    toX,
                    toY;
                applyDefaultContextSettings(options);
                // Tick every 20 degrees (small ticks)
                for (iTick = -30; iTick < 220; iTick += 20)
                {
                    iTickRad = degToRad(iTick);
                    /* Calculate the X and Y of both ends of the
                     * line I need to draw at angle represented at Tick.
                     * The aim is to draw the a line starting on the
                     * coloured arc and continueing towards the outer edge
                     * in the direction from the center of the gauge.
                     */
                    onArchX = gaugeOptions.radius - (Math.cos(iTickRad) * tickvalue);
                    onArchY = gaugeOptions.radius - (Math.sin(iTickRad) * tickvalue);
                    innerTickX = gaugeOptions.radius - (Math.cos(iTickRad) * gaugeOptions.radius);
                    innerTickY = gaugeOptions.radius - (Math.sin(iTickRad) * gaugeOptions.radius);
                    fromX = (options.center.X - gaugeOptions.radius) + onArchX;
                    fromY = (gaugeOptions.center.Y - gaugeOptions.radius) + onArchY;
                    toX = (options.center.X - gaugeOptions.radius) + innerTickX;
                    toY = (gaugeOptions.center.Y - gaugeOptions.radius) + innerTickY;
                    // Create a line expressed in JSON
                    line = createLine(fromX, fromY, toX, toY, "rgb(256,256,256)", 3, .7);
                    // Draw the line
                    drawLine(options, line);
                }
            }

            function drawLargeTickMarks(options)
            {
                /* The large tick marks against the coloured
                 * arc drawn every 10 mph from 10 degrees to
                 * 170 degrees.
                 */
                var tickvalue = options.levelRadius - 8,
                    iTick = 0,
                    gaugeOptions = options.gaugeOptions,
                    iTickRad = 0,
                    innerTickY,
                    innerTickX,
                    onArchX,
                    onArchY,
                    fromX,
                    fromY,
                    toX,
                    toY,
                    line;
                applyDefaultContextSettings(options);
                tickvalue = options.levelRadius - 2;
                // 10 units (major ticks)
                for (iTick = -20; iTick < 220; iTick += 20)
                {
                    iTickRad = degToRad(iTick);
                    /* Calculate the X and Y of both ends of the
                     * line I need to draw at angle represented at Tick.
                     * The aim is to draw the a line starting on the
                     * coloured arc and continueing towards the outer edge
                     * in the direction from the center of the gauge.
                     */
                    onArchX = gaugeOptions.radius - (Math.cos(iTickRad) * tickvalue);
                    onArchY = gaugeOptions.radius - (Math.sin(iTickRad) * tickvalue);
                    innerTickX = gaugeOptions.radius - (Math.cos(iTickRad) * gaugeOptions.radius);
                    innerTickY = gaugeOptions.radius - (Math.sin(iTickRad) * gaugeOptions.radius);
                    fromX = (options.center.X - gaugeOptions.radius) + onArchX;
                    fromY = (gaugeOptions.center.Y - gaugeOptions.radius) + onArchY;
                    toX = (options.center.X - gaugeOptions.radius) + innerTickX;
                    toY = (gaugeOptions.center.Y - gaugeOptions.radius) + innerTickY;
                    // Create a line expressed in JSON
                    line = createLine(fromX, fromY, toX, toY, "rgb(127,127,127)", 3, 1);
                    // Draw the line
                    drawLine(options, line);
                }
            }

            function drawTicks(options)
            {
                /* Two tick in the coloured arc!
                 * Small ticks every 5
                 * Large ticks every 10
                 */
                drawSmallTickMarks(options);
                drawLargeTickMarks(options);
            }

            function drawTextMarkers(options)
            {
                /* The text labels marks above the coloured
                 * arc drawn every 10 mph from 10 degrees to
                 * 170 degrees.
                 */
                let innerTickX = 0,
                    innerTickY = 0,
                    iTick = 0,
                    gaugeOptions = options.gaugeOptions,
                    iTickToPrint = 0,
                    textOffsetX = 0,
                    degree = 0,
                    portion = 0,
                    fontsize = gaugeOptions.radius / 7;
                applyDefaultContextSettings(options);

                // Calculation for displaying number on speedometer when users assign max speed
                if ((options.maxSpeed % 5) == 0) {
                    portion = options.maxSpeed / 5;
                    if ((options.maxSpeed % 10) == 0 && options.maxSpeed >= 50) {
                        portion = options.maxSpeed / 10;
                    }
                    degree = (6 / portion) * 40;
                }
                else {
                    portion = 6;
                    degree = 40;
                }
                // Font styling
                options.ctx1.font = 'italic ' + fontsize.toString() + 'px sans-serif';
                options.ctx1.textBaseline = 'top';
                options.ctx1.beginPath();
                // Tick every 20 (small ticks), remember y coordinate grows positive down the screen not up (negative)
                for (iTick = 180 + 30; iTick >= -30; iTick -= degree)
                {
                    innerTickX = Math.cos(degToRad(iTick)) * gaugeOptions.radius * .8;
                    innerTickY = Math.sin(degToRad(iTick)) * gaugeOptions.radius * .8;

                    //X offset needs to go from width*0 to -width*1 when angle goes from -180 to 0
                    textOffsetX = -Math.sin(degToRad((iTick - 180) / 2)) * options.ctx1.measureText(iTickToPrint.toFixed(0)).width;
                    //Y offset is 0 at 180 and full height at 90
                    textOffsetY = Math.sin(degToRad(iTick)) * options.ctx1.measureText('M').width; //M is square, ie. width = height
                    // Some cludging to center the values (TODO: Improve)
                    let textXCoord = (options.center.X + innerTickX - textOffsetX);
                    let textYCoord = (gaugeOptions.center.Y - innerTickY);
                    {
                        options.ctx1.fillText(iTickToPrint.toFixed(0), textXCoord,
                            textYCoord); //negative is up the screen
                    }
                    // MPH increase by 10 every 20 degrees
                    //iTickToPrint += Math.round(2160 / 9);
                    if (iTickToPrint == '0')
                    {
                        poSitionOfZeroSpeedText = textYCoord;
                        
                        //MyConsoleLog("poSitionOfZeroSpeedText="  + poSitionOfZeroSpeedText.toFixed(0));
                    }
                    iTickToPrint += (options.maxSpeed / portion);
                }
                options.ctx1.stroke();
            }

            function drawSpeedometerPart(options, alphaValue, strokeStyle, startPos, endPos, lineWidth)
            {
                /* Draw part of the arc that represents
                 * the colour speedometer arc
                 */
                options.ctx1.beginPath();
                options.ctx1.globalAlpha = alphaValue;
                options.ctx1.lineWidth = lineWidth;
                options.ctx1.strokeStyle = strokeStyle;
                options.ctx1.arc(options.center.X,
                    options.center.Y,
                    options.levelRadius - 20,
                    Math.PI + (Math.PI / 180 * startPos),
                    Math.PI + (Math.PI / 180 * endPos),
                    false);
                options.ctx1.stroke();
            }

            function drawSpeedometerColourArc(options)
            {
                /* Draws the colour arc.  Three different colours
                 * used here; thus, same arc drawn 3 times with
                 * different colours.
                 * TODO: Gradient possible?
                 */
                var startOfGreen = -30,
                    endOfGreen = 90 + 30,
                    endOfOrange = 90 + 80,
                    end = 180 - startOfGreen;
                lineWidth = 10;
                drawSpeedometerPart(options, 1.0, "#008000", startOfGreen, end, lineWidth);
                drawSpeedometerPart(options, 0.9, "#FFFF00", endOfGreen, end, lineWidth);
                drawSpeedometerPart(options, 0.9, "#FF0000", endOfOrange, end, lineWidth);
            }

            function drawNeedleDial(options, alphaValue, strokeStyle, fillStyle)
            {
                /* Draws the metallic dial that covers the base of the
                 * needle.
                 */
                var i = 0;
                options.ctx2.globalAlpha = alphaValue;
                options.ctx2.lineWidth = 3;
                options.ctx2.strokeStyle = strokeStyle;
                options.ctx2.fillStyle = fillStyle;
                // Draw several transparent circles with alpha
                for (i = 0; i < 10; i++)
                {
                    options.ctx2.beginPath();
                    options.ctx2.arc(options.center.X,
                        options.center.Y,
                        i,
                        0,
                        2 * Math.PI,
                        true);
                    options.ctx2.fill();
                    options.ctx2.stroke();
                }
            }

            function convertSpeedToAngle(options)
            {
                /* Helper function to convert a speed to the
                 * equivelant angle.
                 */
                var iSpeed = options.speed * (180 + 30 + 30) / 30,
                    iSpeedAsAngle = iSpeed - 30;
                // Ensure the angle is within range
                // if (iSpeedAsAngle > 180) {
                //     iSpeedAsAngle = iSpeedAsAngle - 180;
                // } else if (iSpeedAsAngle < 0) {
                //     iSpeedAsAngle = iSpeedAsAngle + 180;
                // }
                return iSpeedAsAngle;
            }

            function drawNeedle(options)
            {
                /* Draw the needle in a nice read colour at the
                 * angle that represents the options.speed value.
                 */
                var iSpeedAsAngle = convertSpeedToAngle(options),
                    iSpeedAsAngleRad = degToRad(iSpeedAsAngle),
                    gaugeOptions = options.gaugeOptions,
                    triangleBasePosXOffset = Math.sin(iSpeedAsAngleRad) * 3,
                    triangleBasePosYOffset = Math.cos(iSpeedAsAngleRad) * -3,
                    triangleBaseNegXOffset = Math.sin(iSpeedAsAngleRad) * -3,
                    triangleBaseNegYOffset = Math.cos(iSpeedAsAngleRad) * 3,
                    fromX1 = gaugeOptions.center.X + triangleBasePosXOffset,
                    fromY1 = gaugeOptions.center.Y + triangleBasePosYOffset,
                    fromX2 = gaugeOptions.center.X + triangleBaseNegXOffset,
                    fromY2 = gaugeOptions.center.Y + triangleBaseNegYOffset,
                    endNeedleX = gaugeOptions.radius - (Math.cos(iSpeedAsAngleRad) * gaugeOptions.radius),
                    endNeedleY = gaugeOptions.radius - (Math.sin(iSpeedAsAngleRad) * gaugeOptions.radius),
                    toX = (gaugeOptions.center.X - gaugeOptions.radius) + endNeedleX,
                    toY = (gaugeOptions.center.Y - gaugeOptions.radius) + endNeedleY,
                    triangle = createTriangle(fromX1, fromY1, fromX2, fromY2, toX, toY, "#FFFFF", 5, 1.0);
                    drawTriangle(options, triangle);
                // Two circle to draw the dial at the base (give its a nice effect?)
                drawNeedleDial(options, 0.6, "#FFFFF", options.lineColor);
                drawNeedleDial(options, 0.2, "#FFFFF", "#FFFFF");
            }

            function buildOptionsAsJSON(canvas, canvas2, iSpeed, radius, outerRadius, maxSpeed)
            {
                /* Setting for the speedometer
                 * Alter these to modify its look and feel
                 */
                //canvas.width = 10000;
                //canvas.height = 10000;
                //radius = 200,
                //outerRadius = 300;
                // Create a speedometer object using Javascript object notation
                return {
                    ctx1: canvas.getContext('2d'),
                    ctx2: canvas2.getContext('2d'),
                    speed: iSpeed,
                    center:
                    {
                        X: canvas.width / 2,
                        Y: canvas.height / 2
                    },
                    levelRadius: radius - 10,
                    gaugeOptions:
                    {
                        center:
                        {
                            X: canvas.width / 2,
                            Y: canvas.height / 2
                        },
                        radius: radius
                    },
                    radius: outerRadius,
                    lineColor: "#FFFFFF",
                    textColor: "#FFFFFF",
                    maxSpeed: maxSpeed,
                };
            }

            function clearCanvas1(options)
            {
                options.ctx1.clearRect(0, 0, canvas.width, canvas.height);
                applyDefaultContextSettings(options);
            }

            function clearCanvas2(options)
            {
                options.ctx2.clearRect(0, 0, canvas.width, canvas.height);
            }
            var canvas;
            var canvas2;
            var options;
            var poSitionOfZeroSpeedText;
            var outerRadius;

            function setupAllCanvasLayers(maxSpeed)
            {
                // Canvas good?
                canvas = document.getElementById('speedometerBackground');
                canvas2 = document.getElementById('speedometerForeground');
                options = null;
                if (canvas !== null && canvas2 !== null && canvas.getContext && canvas2.getContext)
                {
                    //console.log("w x h " + window.innerWidth.toString() + " x " + window.innerHeight.toString());
                    canvas.style.position = "fixed";
                    canvas2.style.position = "fixed";
                    var a = window.innerWidth / window.innerHeight;

                    if (a < 1)
                    {
                        canvas.setAttribute("width", 1000);
                        canvas.setAttribute("height", 1000 / a);
                        outerRadius = canvas.width * 0.4;
                    }
                    else
                    {
                        canvas.setAttribute("width", 1000 * a);
                        canvas.setAttribute("height", 1000);
                        outerRadius = canvas.height * .5;
                    }

                    radius = outerRadius * 2.0 / 2.7;

                    canvas2.setAttribute("width", canvas.width);
                    canvas2.setAttribute("height", canvas.height);

                    options = buildOptionsAsJSON(canvas, canvas2, iCurrentSpeed, radius, outerRadius, maxSpeed);
                    return true;
                }
                return false;
            }
            var drawing = false;
            var odometer;
            var PNRD;

            function draw(maxSpeed = 60)
            {
                
                /* Main entry point for drawing the speedometer
                 * If canvas is not support alert the user.
                 */
                if (setupAllCanvasLayers(maxSpeed))
                {
                    // Clear canvas
                    clearCanvas1(options);
                    // Draw the metallic styled edge
                    drawMetallicArc(options);
                    // Draw thw background
                    drawBackground(options);
                    // Draw tick marks
                    drawTicks(options);
                    // Draw labels on markers
                    drawTextMarkers(options);
                    // Draw speeometer colour arc
                    drawSpeedometerColourArc(options);
                    // Draw the needle and base
                    drawJustNeedle(options);

                    miles = document.getElementById('miles');
                    odometer = document.getElementById('odometer');
                    speedometer = document.getElementById('speedometerBackground');
                    PRND = document.getElementById('PRNDContainer');
                    rpm = document.getElementById('RPM');
                    rpmText = document.getElementById('RPMText');
                    let a = window.innerWidth / window.innerHeight;
                    let outerRadiusScreenDimensions;

                    //MyConsoleLog("w=" + window.innerWidth + " h=" + window.innerHeight);
                    //MyConsoleLog("poSitionOfZeroSpeedText=" + poSitionOfZeroSpeedText.toFixed(0) + " h=" + window.innerHeight);
                    odometer.style.marginLeft = window.innerWidth / 2 - odometer.offsetWidth / 2;
                    odometer.style.marginLeft = window.innerWidth / 2 - odometer.offsetWidth / 2;
                    PRND.style.top = window.innerHeight - PRND.offsetHeight * 2.0;
                    PRND.style.marginLeft = (window.innerWidth / 2 - PRND.offsetWidth / 2);
                    //rpm.style.marginLeft = (window.innerWidth / 2 - odometer.offsetWidth / 2) + (odometer.offsetWidth / 2 - rpm.offsetWidth / 2);

                    if (a < 1)
                    {
                        outerRadiusScreenDimensions = outerRadius / 1000 * window.innerWidth;
                        miles.style.top = poSitionOfZeroSpeedText / (1000) * window.innerWidth - rpm.offsetHeight - miles.offsetHeight;
                        odometer.style.top = poSitionOfZeroSpeedText / (1000) * window.innerWidth + (odometer.offsetHeight * 0.5);
                        rpm.style.top = poSitionOfZeroSpeedText / (1000) * window.innerWidth - (rpm.offsetHeight * 0.5);
                        rpmText.style.top = poSitionOfZeroSpeedText / (1000) * window.innerWidth - (rpm.offsetHeight * 0.5);
                        //MotorTemperatureGauge.style.top = poSitionOfZeroSpeedText / 1000 * window.innerWidth - MotorTemperatureGauge.dataset.height * .7;
                        //BatteryVoltageGauge.style.top = poSitionOfZeroSpeedText / 1000 * window.innerWidth - BatteryVoltageGauge.dataset.height * .7;
                        if (a < 0.5) {
                            //Phone
                            MotorTemperatureGauge.style.top = "37%";
                            BatteryVoltageGauge.style.top = "37%";
                        }
                        else {
                            //Tablet
                            MotorTemperatureGauge.style.top = "30%";
                            BatteryVoltageGauge.style.top = "30%";
                        }
                        //MyConsoleLog("a<1" + window.innerWidth.toString() + " x " + window.innerHeight.toString() + " PRND.style.top" + PRND.style.top);
                    }
                    else
                    {
                        //iOS speedometer.offsetHeight is screw up just when resizing to landscape
                        outerRadiusScreenDimensions = outerRadius / 1000 * window.innerHeight;
                        miles.style.top = poSitionOfZeroSpeedText / (1000) * window.innerHeight - rpm.offsetHeight - miles.offsetHeight;
                        odometer.style.top = poSitionOfZeroSpeedText / (1000) * window.innerHeight + (odometer.offsetHeight * 0.5);
                        rpm.style.top = poSitionOfZeroSpeedText / (1000) * window.innerHeight - (rpm.offsetHeight * 0.5);
                        rpmText.style.top = poSitionOfZeroSpeedText / (1000) * window.innerHeight - (rpm.offsetHeight * 0.5);
                        //MotorTemperatureGauge.style.top = poSitionOfZeroSpeedText / 1000 * window.innerHeight - MotorTemperatureGauge.dataset.height * .7;
                        //BatteryVoltageGauge.style.top = poSitionOfZeroSpeedText / 1000 * window.innerHeight - BatteryVoltageGauge.dataset.height * .7;
                        MotorTemperatureGauge.style.top = "15%";
                        BatteryVoltageGauge.style.top = "15%";
                        //MyConsoleLog("a>=1" + window.innerWidth.toString() + " x " + window.innerHeight.toString() + " PRND.style.top" + PRND.style.top);
                    }


                    MotorTemperatureGauge.dataset.height = outerRadiusScreenDimensions * 1.5;
                    //MotorTemperatureGauge.dataset.width = outerRadiusScreenDimensions * .45;
                    MotorTemperatureGauge.dataset.width = outerRadiusScreenDimensions * .45;
                    MotorTemperatureGauge.style.left = -outerRadiusScreenDimensions * .05;

                    BatteryVoltageGauge.dataset.height = outerRadiusScreenDimensions * 1.5;
                    BatteryVoltageGauge.dataset.width = outerRadiusScreenDimensions * .45;
                    BatteryVoltageGauge.style.right = -outerRadiusScreenDimensions * .05;
                }
                else
                {
                    alert("Canvas not supported by your browser!");
                }
            }

            function drawJustNeedle()
            {
                // Clear canvas
                clearCanvas2(options);
                options.speed = iCurrentSpeed;
                // Draw the needle and base
                drawNeedle(options);
                if (iTargetSpeed == iCurrentSpeed)
                {
                    clearTimeout(job);
                    drawing = false;
                    return;
                }
                iCurrentSpeed += (iTargetSpeed - iCurrentSpeed) * .01;
                if (Math.abs(iTargetSpeed - iCurrentSpeed) < .1)
                {
                    iCurrentSpeed = iTargetSpeed;
                    drawing = false;
                    return;
                }
                job = setTimeout(drawJustNeedle, 10);
                drawing = true;
            }
            var mileage = 12.3;
            var previousWidth, previousHeight, previousMaxSpeed;

            DrawSpeedometer = function (speed, maxSpeed)
            {   //doing a function this way makes it Global,  function drawWithInputValue(speed) cannot be found from the main html doc
                if (speed !== null)
                {
                    iTargetSpeed = speed;
                    mileage += speed / 500;
                    $('.odometer').html(mileage.toString());
                    if (!drawing)
                        drawJustNeedle(); //otherwise it's already drawing
                }
                //resize does not seem to always finish properly so check here to reposition everything
                
                //Call draw when maxspeed change 
                if (previousWidth != window.innerWidth || previousHeight != window.innerHeight || previousMaxSpeed != maxSpeed)
                {
                    //MyConsoleLog("w x h " + window.innerWidth.toString() + " x " + window.innerHeight.toString());
                    draw(maxSpeed);
                }
                previousWidth = window.innerWidth;
                previousHeight = window.innerHeight;
                previousMaxSpeed = maxSpeed;
            }

            window.addEventListener('resize', draw, false);
            draw();
        });