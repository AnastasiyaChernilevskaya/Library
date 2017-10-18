$(document).ready(function () {
    $("#grid").kendoGrid({

        height: 550,
        editable: "inline",
        sortable: true,
        toolbar: ["create"],
        pageable: {
            refresh: false,
            pageSizes: true
        },
        columns: [{
            field: "Id",
            hidden: true
        },
         //  {
        //    field: "Check",
        //    title: "Check",
        //    type: "boolean",
        //    template: '<input type="checkbox" #= Check ? \'checked="checked"\' : "" # class="chkbx" />',
        //    width: "100px"

        //},
            {
                field: "Name",
                title: "Name"
            }, {
                field: "Author",
                title: "Author"
            }, {
                field: "YearOfPublishing",
                title: "Year of publishing",
                width: 150
            }, {
                field: "Publisher",
                title: "Publisher"
            }, {
                command: [{
                    name: "Delete",
                    click: function (e) {
                        deleteData(e);
                    }
                },
                {
                    name: "edit",
                }],
                width: "110px"

            }
        ],
        dataSource: {
            //            autoSync: true,
            transport: {
                read: function (e) {
                    getData(e);
                },
                update: function(options) {
                    updateData(options);
                    return true;
                },
            },
            schema: {
                model: {
                    id: "Id",
                    fields: {
                        Id: { editable: false, nullable: true },
                        Name: { type: "string" },
                        Author: { type: "string" },
                        YearOfPublishing: { type: "number" },
                        Publisher: { type: "string" }
                    }
                }
            },
            batch: true

        }
    });
});
function getData(e) {
    $.ajax({
        type: "GET",
        url: "GetBooks",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        success: function (data) {
            console.log(data);
            e.success(data);
        },
        error: function (data) {
            console.log(data)
        }
    });
}
function updateData(options) {

    $.ajax({
        type: "POST",
        url: "UpdateBook",
        contentType: "application/json; charset =utf-8",
        data: JSON.stringify(options.data.models[0]),
        datatype: 'json',
        success: function (data) {
            console.log(data);
            console.log("ss");
        },
        error: function (data) {
            console.log(data);
            console.log("k");
        }
    });
}
function createData(e) {
    $.ajax({
        url: "CreateBook",
        type: "POST",
        contentType: "application/json; charset =utf-8",
        dataType: 'json',
        data: getBookModel(e),
        success: function (data) {
            console.log(data);
        },
        error: function (data) {
            console.log(data)
        }
    });
}
function deleteData(e) {

    var id = getId(e);
    $.ajax({
        type: "GET",
        url: "DestroyBook?id=" + id,
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        success: function (data) {
            console.log(data);
        },
        error: function (data) {
            console.log(data)
        }
    });
}

function getId(e) {
    return $(e.target).closest("tr")[0].childNodes[0].innerText;
}

function setUpdate() {
    $(".k-grid-update").click(function (e) {
        //updateData(e);
        var grid = $("#grid").getKendoGrid();
        var item = grid.dataItem($(e.target).closest("tr"));
        console.log(item);
    })
}

function getBookModel(e) {
    var row = $(e.target).closest("tr")[0].childNodes;
    var book = {
        id: row[0].innerText,
        name: row[1].innerText,
        author: row[2].innerText,
        yearOfPublishing: row[3].innerText,
        publisher: row[4].innerText
    }
    return JSON.stringify(book);
}