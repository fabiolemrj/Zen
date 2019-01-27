function fnOpenNormalDialog(id) {
    $("#dialog-confirm").html("Deseja realmente apagar o registro " + id + "?");

    // Define the Dialog and its properties.
    $("#dialog-confirm").dialog({
        resizable: false,
        modal: true,
        title: "Apagar registro",
        height: 250,
        width: 400,
        buttons: {
            "Sim": function () {
                $(this).dialog('close');
                callback(true);
            },
            "Não": function () {
                $(this).dialog('close');
                callback(false);
            }
        }
    });
}

function callback(value) {
    if (value) {
        $("#result").html("Confirmed");
    } else {
        $("#result").html("Rejected");
    }
}