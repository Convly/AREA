var canvas = document.getElementById("canvasTree");
var ctx = canvas.getContext("2d");

function clearCanvasContent()
{
    ctx.clearRect(0, 0, canvas.width, canvas.height);
}

function drawTextInCanvas(text, fontSize, fontFamily, posX, posY) {
    ctx.font = fontSize.toString() + "px " + fontFamily;
    ctx.fillText(text, posX, posY);
}