// Global varible to use dataset
$dataObject = null;
$isCreating = null;

$(document).ready(function () {
    $dataObject = $("#tblProducts").DataTable();
    LoadTable();
    LoadCategories();

    $("#btnNewOpenModal").click(function () {
        CleanModal(true);
        $("#dvId").prop("hidden", true);
    });

    $("#btnSave").click(function () {
        var product = GetJsonToSave();
        if ($isCreating) {
            Create(product);
        }
        else {
            Edit(product);
        }
    });

    $('#tblProducts tbody').on('click', '.edit', function () {
        var data = $dataObject.row($(this).parents('tr')).data();
        LoadModelEdit(data);
    });

    $('#tblProducts tbody').on('click', '.delete', function () {
        var data = $dataObject.row($(this).parents('tr')).data();
        if (confirm("Are you sure you want delete " + data.Id + "-" + data.Name)) {
            Delete(data);
        }
    });
});

function LoadModelEdit(data) {
    CleanModal(false);
    $("#txtName").val(data.Name);
    $("#txtPresentation").val(data.Presentation);
    $("#txtId").val(data.Id);
    $("#txtPrice").val(data.Price);
    $("#slcCategory").val(data.CategoryId);
    $("#dvId").prop("hidden", false);
    
    $("#mdlSave").modal("show");
}

function CleanModal(isCreating) {
    $isCreating = isCreating;
    $("#txtName").val("");
    $("#txtPresentation").val("");
    $("#txtId").val("");
    $("#txtPrice").val("");
    $("#slcCategory").val(0);
}

function GetJsonToSave() {
    var model = {};
    model.Name = $("#txtName").val().trim();
    model.Presentation = $("#txtPresentation").val().trim();
    model.Id = $("#txtId").val().trim();
    model.Price = $("#txtPrice").val();
    model.CategoryId = $("#slcCategory").val();

    return model;
}

function Create(data) {
    $.ajax({
        type: "POST",
        url: urlCreate,
        data: "{product: " + JSON.stringify(data) + "}",
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

function Edit(data) {
    $.ajax({
        type: "POST",
        url: urlEdit,
        data: "{product: " + JSON.stringify(data) + "}",
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
        data: "{product: " + JSON.stringify(category) + "}",
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
        url: urlGetProducts,
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

function LoadCategories() {
    $.ajax({
        type: "GET",
        url: urlGetCategories,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            if (response != null) {
                $.each(response, function (i, obj) {
                    $("#slcCategory").append("<option value='" + obj.Id + "'>" + obj.Name + "</option>");
                });
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

    $dataObject = $("#tblProducts").DataTable({
        data: response,
        columns: [
            { "data": 'Id' },
            { "data": 'Name' },
            { "data": 'Presentation' },
            { "data": 'Price' },
            { "data": 'CategoryName' },
            { "data": "Created" },
            { "defaultContent": "<button class='edit btn btn-primary inputButton' data-toggle='tooltip' title='Edit' > <span aria-hidden='true' class='bi bi-pencil'></span></button> <button class='delete btn btn-primary inputButton' data-toggle='tooltip' title='Delete' > <span aria-hidden='true' class='bi bi-trash'></span></button>", "searchable": false }
        ]
    });
}
