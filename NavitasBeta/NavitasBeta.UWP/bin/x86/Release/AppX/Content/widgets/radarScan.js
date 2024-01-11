class RadarScan //public class NavitasMotorControllerModels
{
    constructor(_canvas) {
        const _this = this;
        this.canvas = _canvas;
        this.ctx = this.canvas.getContext("2d");
        this.FPS = 60; //animation frame rate (per second)
        this.radarWidth = 40;
        this.start = 0;
        this.direction = "right";
        this.lineWidth = 1;
        this.scanRate = 3; //1,2,3,4
        this.stopScan = false;
    }
    stopRadarScan() {
        this.stopScan = true;
    }
    startRadarScan(frameRate) {

        let canvas = this.canvas;
        let ctx = canvas.getContext("2d");
        let FPS = 60; //animation frame rate (per second)
        let radarWidth = this.radarWidth;
        let start = this.start;
        let direction = this.direction;
        let lineWidth = this.lineWidth;
        let scanRate = this.scanRate; //1,2,3,4

        if (direction == "right") {
            ctx.clearRect(start - radarWidth - lineWidth * scanRate, 0 , radarWidth + lineWidth * scanRate, canvas.height)
            for (var i = 0; i > -radarWidth; i -= lineWidth) {
                ctx.beginPath();
                ctx.moveTo(start + i, 0);
                ctx.lineTo(start + i, canvas.height);
                ctx.lineWidth = lineWidth;  // line thickness
                var lineOpacity = (radarWidth + i) / radarWidth;
                ctx.strokeStyle = 'rgba(0, 255, 0, ' + lineOpacity + ')';  // line colour
                ctx.stroke();
            }
        }
        if (direction == "left") {
            ctx.clearRect(start, 0, radarWidth + lineWidth * scanRate, canvas.height)
            for (var i = 0; i < radarWidth; i += lineWidth) {
                ctx.beginPath();
                ctx.moveTo(start + i, 0);
                ctx.lineTo(start + i, canvas.height);
                ctx.lineWidth = lineWidth;  // line thickness
                var lineOpacity = (radarWidth - i) / radarWidth;
                ctx.strokeStyle = 'rgba(0, 255, 0, ' + lineOpacity + ')';  // line colour
                ctx.stroke();
            }
        }


        if (direction == "right")
            this.start += lineWidth * scanRate;
        else
            this.start -= lineWidth * scanRate;

        if (start >= canvas.width + radarWidth)
            this.direction = "left";
        if (start <= -radarWidth)
            this.direction = "right";

        //stop at a nice visual state
        if (this.stopScan && start <= -radarWidth) {
            this.stopScan = false; //so that next call to start works
            ctx.clearRect(0, 0, canvas.width, canvas.height)
        }
        else
            setTimeout(() => this.startRadarScan(frameRate), frameRate);
    }

}