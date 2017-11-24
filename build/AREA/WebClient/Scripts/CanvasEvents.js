//Events on canvas here (onclick etc..)
var canvas = document.getElementById("canvasTree");
var ctx = canvas.getContext("2d");

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
    var pos = getMousePosInCanvas(canvas, event);
    if (itemIndex !== -1) {
        console.log("Left click at pos [", pos.x + ",", pos.y, "]\nThe current selected AREA is", GetSelectedSideItemIndex());

    }
});

// Right click event in the canvas
$("#canvasTree").contextmenu(function (event) {
    var pos = getMousePosInCanvas(canvas, event);
    var itemIndex = GetSelectedSideItemIndex();
    if (itemIndex !== -1) {
        console.log("Right click at pos [", pos.x + ",", pos.y, "]\nThe current selected AREA is", GetSelectedSideItemIndex());
        var elem;
        var t = getTree(itemIndex);
        if ((elem = clickedAnTreeElement(pos))) {
            console.log("Node to remove:", elem);
            if (confirm("Do you really want to remove the " + elem.data.type + " \"" + elem.data.name + "\" ?")) {
                t.remove(elem.data);
                setTreeData(t);
                drawTreeData();
            }
        } else {
            var name = prompt("Please name your new node:");
            if (name) {
                if (!addNode(itemIndex, t.root, { "name": name, "pos": pos, "type": "action" }))
                    addNode(itemIndex, t.root.data, { "name": name, "pos": pos, "type": "reaction" });
                setTreeData(t);
                drawTreeData();
            }
        }
    }
    return (false);
});