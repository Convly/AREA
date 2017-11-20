//Data variables
treeData = {}

//DOM Elements
var canvas = document.getElementById("canvasTree");
var canvasCont = document.getElementById("canvasContainer");
var ctx = canvas.getContext("2d");

/// TREEVIEW FUNCTIONS
//Draw the tree on screen

function drawTreeData(tree) {
    treeData = tree;
    clearCanvasContent();
    //TODO: draw the tree on screen
}

/// DRAWING FUNCTIONS
//Fill a rectangle in the canvas context
function drawRectInCanvas(posX, posY, sizeX, sizeY, color, isFilled) {
    if (!isFilled) {
        ctx.fillStyle = color;
        ctx.rect(posX, posY, sizeX, sizeY);
        ctx.stroke();
    } else {
        ctx.fillStyle = color;
        ctx.fillRect(posX, posY, sizeX, sizeY);
    }
}

//Clear the canvas context
function clearCanvasContent() {
    ctx.clearRect(0, 0, canvas.width, canvas.height);
}

//Draw a text in the canvas context
function drawTextInCanvas(text, fontSize, fontFamily, color, posX, posY, textAlign) {
    ctx.font = fontSize.toString() + "px " + fontFamily;
    ctx.fillStyle = color;
    ctx.textAlign = textAlign
    ctx.fillText(text, posX, posY);
}

/// GETTER FUNCTIONS
//Get canvas size
function getCanvasSize() {
    return ({
        "width": canvas.width,
        "height": canvas.height
    })
}


/// DOM FUNCTIONS
//Canvas resizing to a square
//var size = (canvasCont.clientWidth < canvasCont.clientHeight) ? (canvasCont.clientWidth) : (canvasCont.clientHeight);
//function resizeCanvasToSquare(size) {
//    canvas.width = size;
//    canvas.height = size;
//    drawTreeData(treeData);
//}

$(document).ready(function () {
    canvas.width = canvasCont.clientWidth;
    canvas.height = canvasCont.clientHeight;
})

window.onresize = function (event) {
    canvas.width = canvasCont.clientWidth;
    canvas.height = canvasCont.clientHeight;
};