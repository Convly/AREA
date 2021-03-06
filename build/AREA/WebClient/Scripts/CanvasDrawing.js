﻿/// DOM Elements
var canvas = document.getElementById("canvasTree");
var canvasCont = document.getElementById("canvasContainer");
var ctx = canvas.getContext("2d");


/// TREEDATA DRAWING
treeData = null;

//Set the data of the current tree
function setTreeData(tree) {
    treeData = tree;
    if (treeData && treeData.root) {
        $("#sendButton").css("display", "");
    }
    else {
        $("#sendButton").css("display", "none");
    }
}

//Draw the tree on screen
function drawTreeData() {
    clearCanvasContent();
    if (treeData && treeData.root)
    {
        var fullRootName = (treeData.root.data.name === "" ? "" : "[" + treeData.root.data.name + "] - ") +
            treeData.root.data.serviceName + " (" + treeData.root.data.eventName + ")";
        treeData.traverseBFS(function (node) {
            if (node) {
                if (node.data.type === "action") {
                    console.log("wut");
                    ctx.font = "14px Roboto";
                    var fullName = (node.data.name === "" ? "" : "[" + node.data.name + "] - ") +
                        node.data.serviceName + " (" + node.data.eventName + ")";
                    var sizeText = {
                        "width": ctx.measureText(fullName).width + 20,
                        "height": 14 + 14
                    };
                    var w = ctx.measureText(fullRootName).width + 20;
                    drawLineInCanvas({ "x": node.data.pos.x, "y": node.data.pos.y },
                                     { "x": treeData.root.data.pos.x, "y": treeData.root.data.pos.y }, 2, "#E66A39");
                    drawRectInCanvas(node.data.pos.x - sizeText.width / 2, node.data.pos.y - 14 / 2, sizeText.width, sizeText.height, "#FFFFFF", true);
                    drawTextInCanvas(fullName, 14, "Roboto", "#353C3E", node.data.pos.x, node.data.pos.y, "center", "top");
                }
            }
        });
        if (treeData.root.data.type === "reaction") {
            ctx.font = "14px Roboto";
            var sizeText = {
                "width": ctx.measureText(fullRootName).width + 30,
                "height": 14
            };
            drawCircleInCanvas(treeData.root.data.pos.x, treeData.root.data.pos.y + sizeText.height / 2, sizeText.width / 2, "#FFFFFF", true);
            drawTextInCanvas(fullRootName, 14, "Roboto", "#353C3E", treeData.root.data.pos.x, treeData.root.data.pos.y, "center", "top");
        }
    }
    if (posInCanvas) {
        drawLineInCanvas({ x: posInCanvas.x - 5, y: posInCanvas.y - 5 }, { x: posInCanvas.x + 5, y: posInCanvas.y + 5 }, 3, "#E66A39");
        drawLineInCanvas({ x: posInCanvas.x - 5, y: posInCanvas.y + 5 }, { x: posInCanvas.x + 5, y: posInCanvas.y - 5 }, 3, "#E66A39");
    }
}


/// DRAWING FUNCTIONS
//Draw a line in the canvas context
function drawLineInCanvas(pos1, pos2, thickness, color) {
    ctx.beginPath();
    ctx.moveTo(pos1.x, pos1.y);
    ctx.lineTo(pos2.x, pos2.y);
    ctx.lineWidth = thickness;
    ctx.strokeStyle = color;
    ctx.stroke();
}

//Draw a circle in the canvas context
function drawCircleInCanvas(posX, posY, radius, color, isFilled) {
    ctx.beginPath();
    ctx.arc(posX, posY, radius, 0, 2 * Math.PI);
    if (!isFilled) {
        ctx.strokeStyle = color;
        ctx.stroke();
    } else {
        ctx.fillStyle = color;
        ctx.fill();
    }
}

//Draw a rectangle in the canvas context
function drawRectInCanvas(posX, posY, sizeX, sizeY, color, isFilled) {
    ctx.beginPath();
    if (!isFilled) {
        ctx.strokeStyle = color;
        ctx.rect(posX, posY, sizeX, sizeY);
        ctx.stroke();
    } else {
        ctx.fillStyle = color;
        ctx.fillRect(posX, posY, sizeX, sizeY);
    }
}

//Clear the canvas context
function clearCanvasContent() {
    canvas.width = canvas.width;
}

//Draw a text in the canvas context
function drawTextInCanvas(text, fontSize, fontFamily, color, posX, posY, textAlign, baseline) {
    ctx.beginPath();
    ctx.font = fontSize.toString() + "px " + fontFamily;
    ctx.fillStyle = color;
    ctx.textAlign = textAlign;
    ctx.textBaseline = baseline;
    ctx.fillText(text, posX, posY);
}


/// GETTER FUNCTIONS
//Get canvas size
function getCanvasSize() {
    return {
        "width": canvas.width,
        "height": canvas.height
    };
}


/// DOM FUNCTIONS
$(document).ready(function () {
    canvas.width = canvasCont.clientWidth;
    canvas.height = canvasCont.clientHeight;
});

window.onresize = function () {
    canvas.width = canvasCont.clientWidth;
    canvas.height = canvasCont.clientHeight;
    drawTreeData();
};