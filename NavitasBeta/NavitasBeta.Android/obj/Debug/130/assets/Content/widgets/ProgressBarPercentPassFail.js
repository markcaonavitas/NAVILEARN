function progressBar(canvas, percentComplete = 0, imageToDisplay, units = '%', fullScale = 100, barColor = '#e7f2ba', defaultWording = ' ')
{
    try
    {
        var start = 4.72;
        var context = canvas.getContext('2d');
        var cw = context.canvas.width / 2;
        var ch = context.canvas.height / 2;
        var diff;

        diff = (percentComplete / fullScale) * Math.PI * 2;

        context.clearRect(0, 0, context.canvas.width, context.canvas.height);
        context.beginPath();
        context.arc(cw, ch, 50, 0, 2 * Math.PI, false);
        context.fillStyle = '#FFF0'; //transparent inners
        context.fill();
        context.strokeStyle = barColor;
        context.stroke();
        context.fillStyle = '#000';
        context.strokeStyle = '#b3cf3c';
        context.textAlign = 'center';
        context.lineWidth = 15;
        context.font = '25pt Verdana';
        context.beginPath();

        if (percentComplete <= fullScale)
            context.arc(cw, ch, 50, start, diff + start, false);
        else //anything above 100 just causes a reverse circle
            context.arc(cw, ch, 50, start, -diff + start, false);

        context.stroke();

        if (units != '')
            context.fillText(percentComplete + units, cw + 0, ch + 10);
        else
            context.fillText(defaultWording, cw + 0, ch + 10);

        //not a fan of this but image.scr is asychronous and really needs a .onload() to finish properly
        //don't like doing a new Image() every redraw, but it works for now
        passImage = new Image();
        passImage.src = './images/pass-icon.svg';
        failImage = new Image();
        failImage.src = './images/fail-icon.svg';
        if (imageToDisplay == "Pass")
        {
            context.clearRect(0, 0, context.canvas.width, context.canvas.height);
            context.drawImage(passImage, (context.canvas.width - passImage.width)/ 2, (context.canvas.height - passImage.height)/ 2);
        }
        else if (imageToDisplay == "Fail")
        {
            context.clearRect(0, 0, context.canvas.width, context.canvas.height);
            context.drawImage(failImage, (context.canvas.width - failImage.width) / 2, (context.canvas.height - failImage.height) / 2);
        }
        percentComplete++;
    }
    catch (e)
    { //sometimes it is easier to debug by letting the normal console tell you where the error is,
        // so comment out the whole try catch
        console.log("Something went wrong" + e.toString());
        // MyConsoleLog("Something went wrong in ProgressBarPercentPassFail.js" + e.toString());
    }

    return (percentComplete)
}