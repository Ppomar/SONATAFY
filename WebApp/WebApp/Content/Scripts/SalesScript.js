$dataObject = null;
$(document).ready(function () {
	$dataObject = $("#tblProducts").DataTable();

	$("#btnAddProduct").click(function () {
		var idProduct = $("#txtProductId").val();
		if (idProduct != "" && idProduct > 0) {
			GetByIdProduct(idProduct);
		}
		else {
			alert("Add a valid Product!");
        }
	});

	$("#btnCash").click(function () {
		var rowNumber = $dataObject.column(0)
			.data()
			.length;

		if (rowNumber <= 0) {
			alert("You need to add at least one product!");
		}
		else {
			var products = [];
			var data = $dataObject.rows().data().toArray();

			$.each(data, function (i, obj) {
				var product = {};
				product.Id = parseInt(obj[0]);
				product.Name = obj[1];
				product.Presentation = obj[2];
				product.Price = parseFloat(obj[3]);
				product.Taxe = parseFloat(obj[4]);
				product.CategoryName = obj[5];

				products.push(product);
			});

			CreatePurchase(products);			
        }
	});

	$('#tblProducts tbody').on('click', '.remove', function () {
		var data = $dataObject.row($(this).parents('tr')).remove().draw();		
	});

	$(function () {
		$("#txtSearchProduct")
			.bind("keydown", function (event) {
				if (event.keyCode === $.ui.keyCode.TAB && $(this).autocomplete("instance").menu.active) {
					event.preventDefault();
				}
				$("#txtProductId").val("");
			})
			.autocomplete({
				minLength: 0,
				source: function (request, response) {
					$.ajax({
						url: urlFilterProducts,
						type: "GET",
						dataType: "json",
						data: {
							productName: $("#txtSearchProduct").val(),														
						},
						success: function (data) {
							$("#ui-id-2").css("z-index", "2000");
							response(data);
						},
						error: function (ex) {
						}
					});
				}, select: function (event, ui) {
					$("#txtSearchProduct").val(ui.item.Name);
					$("#txtProductId").val(ui.item.Id);
					
					return false;
				}
			})
			.data('ui-autocomplete')._renderItem = function (ul, item) {
				$("#ui-id-2").css("display", "inline");
				return $('<li>')
					.data('item.autocomplete', item)
					.append("<a><b>" + item.CategoryName + " : </b>" + item.Name + "</a>")
					.appendTo(ul);
			};
	})
});

function GetByIdProduct(idProduct) {
	$.ajax({
		url: UrlGetByIdProduct,
		type: "GET",
		contentType: "application/json; charset=utf-8",
		dataType: "json",
		data: {
			id: idProduct,
		},
		success: function (data) {
			if (data != null) {
				AppeendToProductTable(data);
            }
		},
		error: function (ex) {
		}
	});
}

function CreatePurchase(list) {
	var dataToSend = {
		products: list
	}

	$.ajax({
		url: urlCreatePurchase,
		type: "POST",
		data: dataToSend,
		success: function (data) {
			var printing = window.open("", "Print");
			printing.document.open();
			printing.document.write(data);
			printing.document.close();
			printing.print();

			$dataObject.clear().draw();
			$("#txtProductId").val("");
			$("#txtSearchProduct").val(""); 
		},
		error: function (ex) {
		}
	});
}

function AppeendToProductTable(data) {	
		$dataObject.row.add([
			data.Id,
			data.Name,
			data.Presentation,
			data.Price,
			data.Taxe,
			data.CategoryName,			
			"<button class='remove btn btn-danger inputButton' data-toggle='tooltip' title='Remove' ><span aria-hidden='true' class='bi bi-trash'></span></button>"
		]).draw(false);
}