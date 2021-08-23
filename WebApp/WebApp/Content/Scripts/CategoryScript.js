// Global varible to use dataset
$dataObject = null;
$isCreating = null;

$(document).ready(function () {
    $dataObject = $("#tblCategories").DataTable();
    LoadTable();

    $("#btnNewOpenModal").click(function () {
        CleanModal(true);
        $("#txtId").prop("hidden", true);
    });

    $("#btnSave").click(function () {
        var category = GetJsonToSave();
        if ($isCreating) {
            Create(category);
        }
    });
});

function CleanModal(isCreating) {
    $isCreating = isCreating;
    $("#txtName").val("");
    $("#txtDescription").val("");
    $("#txtId").val("");
}

function GetJsonToSave() {
    var model = {};
    model.Name = $("#txtName").val();
    model.Description = $("#txtDescription").val();
    model.Id = $("#txtId").val();

    return model;
}

function Create(category) {
    $.ajax({
        type: "POST",
        url: urlCreate,
        data: "{category: " + JSON.stringify(category) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response === "OK") {
                LoadTable();
                alert("Saved Successfully"); 
                $("#btnCloseModal").trigger("click");
            }
            else {
                alert("Error. try again");
            }
        },
        failure: function (response) {
            alert(response);
        }
    });
}

function LoadTable() {
    $.ajax({
        type: "GET",
        url: urlGetCategories,        
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response != null) {
                BuildTable(response);
            }            
        },
        failure: function (response) {
            alert(response);
        }
    });
}

function BuildTable(response) {
    if ($dataObject != null) {
        $dataObject.destroy();
    }

    $tableObjects = $("#tblCategories").DataTable({
        data: response,
        columns: [            
            { "data": 'Id' },
            { "data": 'Name' },
            { "data": 'Description' },
            { "data": "Created" },
            { "defaultContent": "<button class='edit btn btn-primary inputButton' data-toggle='tooltip' title='Edit' > <span aria-hidden='true' class='bi bi-pencil'></span></button> <button class='delete btn btn-primary inputButton' data-toggle='tooltip' title='Delete' > <span aria-hidden='true' class='bi bi-trash'></span></button>", "searchable": false }
        ]
    });
}
