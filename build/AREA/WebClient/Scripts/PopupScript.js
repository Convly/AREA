$("#selectService").change(function () {
    console.log(posInCanvas);
    $("#selectEventContainer").css("display", "");
    $("#sendCompleteNodeContainer").css("display", "");
    console.log(services[$('#selectService').find(":selected").index() - 1].Actions);
    $('#selectEvent').empty();
    console.log(getTree(itemIndex));
    var it = (getTree(itemIndex).root == null) ? (services[$('#selectService').find(":selected").index() - 1].Reactions) :
        (services[$('#selectService').find(":selected").index() - 1].Actions);
    $.each(it, function (key, value) {
        $('#selectEvent').append($('<option>', {
            value: key,
            text: key
        }));
    });
})

$("#sendCompleteNode").click(function () {
    if (itemIndex !== -1) {
        var t = getTree(itemIndex);
        var name = (($('#nodeNameField').val() == "") ? ("") : ("[" + $('#nodeNameField').val() + "] - ")) +
                   $('#selectService').find(":selected").text() +
                   " (" + $('#selectEvent').find(":selected").text() + ")";
        var objData = {
            "name": $('#nodeNameField').val(),
            "serviceName": $('#selectService').find(":selected").text(),
            "eventName": $('#selectEvent').find(":selected").text(),
            "pos": posInCanvas,
            "type": "reaction"
        }
        if (!addNode(itemIndex, t.root, objData)) {
            objData["type"] = "action"
            addNode(itemIndex, t.root.data, objData);
        }
        posInCanvas = null;
        $("#addNode").css("display", "none");
        $('#nodeNameField').val("")
        $('#selectService').val("choose")
        $('#selectEvent').find(":selected").index(0)
        $("#selectEventContainer").css("display", "none");
        $("#sendCompleteNodeContainer").css("display", "none");
        setTreeData(t);
        drawTreeData();
    }
})