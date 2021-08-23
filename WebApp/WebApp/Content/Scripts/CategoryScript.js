// Global varible to use dataset
$dataObject = null;
$isCreating = null;

$(document).ready(function () {
    $dataObject = $("#tblCategories").DataTable();
    LoadTable();

    $("#btnNewOpenModal").click(function () {
        CleanModal(true);
        $("#dvId").prop("hidden", true);
    });

    $("#btnSave").click(function () {
        var category = GetJsonToSave();
        if ($isCreating) {
            Create(category);
        }
        else {
            Edit(category);
        }
    });

    $('#tblCategories tbody').on('click', '.edit', function () {
        var data = $dataObject.row($(this).parents('tr')).data();
        LoadModelEdit(data);
    });

    $('#tblCategories tbody').on('click', '.delete', function () {
        var data = $dataObject.row($(this).parents('tr')).data();
        if (confirm("Are you sure you want delete " + data.Id + "-" + data.Name)) {
            Delete(data);
        }        
    });
});

function LoadModelEdit(data) {
    CleanModal(false);
    $("#txtName").val(data.Name);
    $("#txtDescription").val(data.Description);
    $("#dvId").prop("hidden", false);
    $("#txtId").val(data.Id);
    $("#mdlSave").modal("show");
}

function CleanModal(isCreating) {
    $isCreating = isCreating;
    $("#txtName").val("");
    $("#txtDescription").val("");
    $("#txtId").val("");
}

function GetJsonToSave() {
    var model = {};
    model.Name = $("#txtName").val().trim();
    model.Description = $("#txtDescription").val().trim();
    model.Id = $("#txtId").val().trim();

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

function Edit(category) {
    $.ajax({
        type: "POST",
        url: urlEdit,
        data: "{category: " + JSON.stringify(category) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response === "OK") {
                LoadTable();
                alert("Saved Successfully");
                $("#btnCloseModal").trigger("click");
            }
            else if (response === "NOK") {
                alert("Not saved, try again!");
            }
            else {
                alert("Error, try again later!");
            }
        },
        failure: function (response) {
            alert(response);
        }
    });
}

function Delete(category) {
    $.ajax({
        type: "POST",
        url: urlDelete,
        data: "{category: " + JSON.stringify(category) + "}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response === "OK") {
                LoadTable();
                alert("Deleted Successfully");
                $("#btnCloseModal").trigger("click");
            }
            else if (response === "NOK") {
                alert("Not deleted, try again!");
            }
            else {
                alert("Error, try again later!");
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

    $dataObject = $("#tblCategories").DataTable({
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
