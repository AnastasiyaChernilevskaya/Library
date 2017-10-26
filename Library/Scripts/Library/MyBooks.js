$(document).ready(function () {

    var grid = $("#Mygrid").kendoGrid({

        height: 550,
        sortable: true,
        toolbar: [
            {
                template: "<a class='addButton k-button' onclick='return toolbarAddClick()'><span class='k-icon k-add'></span>Add new record</a>"
            }, {
                template: "<a class='getToXML k-button' onclick='return getChecked()'><span class='k-icon k-add'></span>Add book to XML file</a>"
            }

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
                title: "Year",
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

    grid.element.on('click', '.DestroyButton', function () {
        var dataItem = grid.dataItem($(this).closest('tr'));
        alert(dataItem.id + ' was clicked!!!');
        deleteData(dataItem);
    });

    grid.element.on('click', ".chkbx", function (e) {
        var dataItem = grid.dataItem($(e.target).closest("tr"));
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

function addToFileXML(data) {
    $.ajax({
        url: "GetSerializedBook",
        type: "POST",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        data: JSON.stringify(data),
        success: function (data) {
            console.log(data);
            console.log("ssAdd");
            refreshGrid();
        },
        error: function (data) {
            console.log(data);
            console.log("erradd");
        }
    });
}

function getChecked() {
    var books = [];
    $("input[type='checkbox']").each(function (index, element) {
        if ($(element).prop("checked") !== false) {
            var grid = $("#Mygrid").data("kendoGrid");
            var dataItem = grid.dataItem($(element).closest('tr'));
            books.push(dataItem);
        }
    })
    console.log(books);
    addToFileXML(books);
}
function refreshGrid() {
    $('#Mygrid').data('kendoGrid').dataSource.read();
    $('#Mygrid').data('kendoGrid').refresh();
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