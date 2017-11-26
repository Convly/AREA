//Events on canvas here (onclick etc..)
var canvas = document.getElementById("canvasTree");
var ctx = canvas.getContext("2d");
var posInCanvas = null;

//Get the relative mouse position in a canvas
function getMousePosInCanvas(canvas, event) {
    var rect = canvas.getBoundingClientRect();
    return ({
        "x": event.clientX - rect.left,
        "y": event.clientY - rect.top
    });
}

function checkPointInCircle(posCircle, posPoint, radius) {
    return (Math.sqrt((posPoint.x - posCircle.x) * (posPoint.x - posCircle.x) + (posPoint.y - posCircle.y) * (posPoint.y - posCircle.y)) < radius);
}

function checkPointInRect(posRect, posPoint, sizeRect) {
    return (posPoint.x >= posRect.x && posPoint.x < posRect.x + sizeRect.x &&
            posPoint.y >= posRect.y && posPoint.y < posRect.y + sizeRect.y);
}

function clickedAnTreeElement(pos) {
    var t = getTree(GetSelectedSideItemIndex());
    var nodeClicked = null;

    ctx.font = "14px Roboto";

    t.traverseBFS(function (node) {
        if (node) {
            if (node.data.type === "action") {
                var radius = (ctx.measureText(treeData.root.data.name).width + 30) / 2;
                if (checkPointInCircle(node.data.pos, pos, radius)) {
                    nodeClicked = node;
                    return;
                }
            } else if (node.data.type === "reaction") {
                var sizeRect = {
                    "x": ctx.measureText(node.data.name).width + 20,
                    "y": 14 + 14
                }
                var posRect = {
                    "x": node.data.pos.x - sizeRect.x / 2,
                    "y": node.data.pos.y - sizeRect.y / 4
                }
                if (checkPointInRect(posRect, pos, sizeRect)) {
                    nodeClicked = node;
                    return;
                }
            }
        }
    });
    return (nodeClicked);
}

// Left click event in the canvas
$("#canvasTree").click(function (event) {
    if (typeof itemIndex != "undefined" && itemIndex !== -1) {
        posInCanvas = getMousePosInCanvas(canvas, event);
        console.log("Left click at pos [", posInCanvas.x + ",", posInCanvas.y, "]\nThe current selected AREA is", GetSelectedSideItemIndex());
        drawTreeData();
        $("#addNode").css("display", "");
    }
});

// Right click event in the canvas
$("#canvasTree").contextmenu(function (event) {
    var itemIndex = GetSelectedSideItemIndex();
    if (itemIndex !== -1) {
        console.log("Right click at pos [", getMousePosInCanvas(canvas, event).x + ",", getMousePosInCanvas(canvas, event).y, "]\nThe current selected AREA is", GetSelectedSideItemIndex());
        var elem;
        var t = getTree(itemIndex);
        if ((elem = clickedAnTreeElement(getMousePosInCanvas(canvas, event))) && elem.data.type != "reaction") {
            console.log("Node to remove:", elem);
            if (confirm("Do you really want to remove the " + elem.data.type + " \"" + elem.data.name + "\" ?")) {
                t.remove(elem.data);
                setTreeData(t);
                drawTreeData();
            }
        }
    }
    return (false);
});