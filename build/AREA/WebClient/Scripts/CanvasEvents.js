//Events on canvas here (onclick etc..)
var canvas = document.getElementById("canvasTree");

//Get the relative mouse position in a canvas
function getMousePosInCanvas(canvas, event) {
    var rect = canvas.getBoundingClientRect();
    return ({
        "x": event.clientX - rect.left,
        "y": event.clientY - rect.top
    });
}

// Left click event in the canvas
$("#canvasTree").click(function (event) {
    var pos = getMousePosInCanvas(canvas, event);
    if (itemIndex != -1) {
        console.log("Left click at pos [", pos.x + ",", pos.y, "]\nThe current selected AREA is", GetSelectedSideItemIndex());

    }
});

// Right click event in the canvas
$("#canvasTree").contextmenu(function (event) {
    var pos = getMousePosInCanvas(canvas, event);
    var itemIndex = GetSelectedSideItemIndex();
    if (itemIndex != -1) {
        console.log("Right click at pos [", pos.x + ",", pos.y, "]\nThe current selected AREA is", GetSelectedSideItemIndex());
        console.log("Before:", getTree(itemIndex));
        var name = prompt("Please enter a name");
        if (name) {
            var t = getTree(itemIndex);
            console.log(t);
            if (!addNode(itemIndex, t.root, { "name": name, "pos": pos, "type": "action"}))
                addNode(itemIndex, t.root.data, { "name": name, "pos": pos, "type": "reaction" });
            setTreeData(t);
            drawTreeData();
        }
    }
    return (false);
});