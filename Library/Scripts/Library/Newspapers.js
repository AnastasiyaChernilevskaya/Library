$(document).ready(function () {

    var newspapersGrid = $("#NewspapersGrid").kendoGrid({

        height: 550,
        sortable: true,
        toolbar: [
            {
                template: "<a class='addButton k-button' onclick='return toolbarAddClick()'><span class='k-icon k-add'></span>Add new record</a>"
            }
            //{
            //    template: "<a class='fileButton k-button' href='#myPopup'><span class='k-icon k-add'></span>Add book to XML file</a>"
            //}
        ],
        pageable: {
            refresh: true,
            buttonCount: 5
        },
        columns: [
            {
                field: "IncludeToPage",
                title: "Include",
                type: "boolean",
                template: '<input type="checkbox"  id="Mycheckbox" #= IncludeToPage ? \'checked="checked"\' : "" # class="chkbx"/>',
                width: "100px"
            },
            {
                field: "Id",
                hidden: true
            }, {
                field: "Name",
                title: "Name",
                width: "250"
            }, {
                field: "Author",
                title: "Author"
            }, {
                field: "YearOfPublishing",
                title: "Year of publishing",
                width: "100px"
            }, {
                field: "Publisher",
                title: "Publisher",
                width: "250px"
            },
            {
                template: "<a class='DestroyButton k-button'\"><span class='k-icon k-delete'></span>Delete</a>",
                title: "&nbsp;",
                width: "100px",
            }, {
                template: "<a class='EditButton k-button' onclick=\"editBook('#=Id#')\"><span class='k-icon k-edit'></span>Edit</a>",
                title: "&nbsp;",
                width: "100px",
            }
        ],

        dataSource: {
            page: 1,
            pageSize: 12,
            autoSync: true,
            transport: {
                read: function (e) {
                    getData(e);
                }
            },
            schema: {
                model: {
                    type: "json",
                    id: "Id",
                    fields: {
                        "Id": { editable: false, nullable: true, type: "number" },
                        "Name": { type: "string", },
                        "Author": { type: "string", },
                        "YearOfPublishing": { type: "number", },
                        "Publisher": { type: "string", },
                        "IncludeToPage": { type: "boolean" }
                    }
                }
            }
        }

    }).data("kendoGrid");

    newspapersGrid.element.on('click', '.DestroyButton', function () {
        var dataItem = newspapersGrid.dataItem($(this).closest('tr'));
        alert(dataItem.id + ' was clicked!!!');
        deleteData(dataItem);
    });

    newspapersGrid.element.on('click', ".chkbx", function (e) {
        var dataItem = newspapersGrid.dataItem($(e.target).closest("tr"));
        console.log(dataItem + "   " + e.target);
        $(e.target).prop("checked") === true ? dataItem.IncludeToPage = true : dataItem.IncludeToPage = false;
        updateData(dataItem);
    })
});

function toolbarAddClick() {
    console.log("Toolbar command add is clicked!");
    addBook();
    return false;
}

function editBook(id) {
    window.location.href = 'EditBook/' + id;
}

function addBook() {
    window.location.href = 'AddBook';
}

function getData(e) {
    $.ajax({
        type: "GET",
        url: "GetBooks",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        success: function (data) {
            console.log(data);
            console.log("ssget");
            e.success(data);
        },
        error: function (data) {
            console.log(data);
            console.log("errget");
        }
    });
}

function deleteData(dataItem) {

    $.ajax({
        url: "DestroyBook?id=" + JSON.stringify(dataItem.id),
        type: "GET",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        success: function (data) {
            console.log(data);
            console.log("ssD");
            refreshGrid();
        },
        error: function (data) {
            console.log(data);
            console.log("errD" + id);
        }
    });
}

function addToFile(format) {

    location.href = '/MyLibrary/GetFile?format=' + format;
}

$(document).ready(function () {
    $("#typeForm").submit(function (event) {
        event.preventDefault();
        var filetype = $('input[name=filetype]:checked').val();
        if (filetype === "xml") {
            format = "xml";
        }
        if (filetype === "txt") {
            format = "txt";
        }
        addToFile(format);
    })
})

//function getChecked() {
//    var books = [];
//    $("input[type='checkbox']").each(function (index, element) {
//        if ($(element).prop("checked") !== false) {
//            var grid = $("#grid").data("kendoGrid");
//            var dataItem = grid.dataItem($(element).closest('tr'));
//            books.push(dataItem.id);
//        }
//    })
//    console.log(books);
//    Test(books);
//}

function refreshGrid() {
    $('#NewspapersGrid').data('kendoGrid').dataSource.read();
    $('#NewspapersGrid').data('kendoGrid').refresh();
}

function updateData(data) {

    $.ajax({
        type: "POST",
        url: "UpdateBook",
        contentType: "application/json; charset =utf-8",
        data: JSON.stringify(data),
        datatype: 'json',
        success: function (data) {
            console.log(data);
            console.log(JSON.stringify(data));
            console.log("ssU");
        },
        error: function (data) {
            console.log(data);
            console.log(JSON.stringify(data));
            console.log("errU");
        }
    });
}