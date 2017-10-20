$(document).ready(function () {

    $("#grid").kendoGrid({

        height: 550,
        editable: "inline",
        
        sortable: true,
        toolbar: ["create"],
        //selectable: "multiple row",
        //persistSelection: false,
        pageable: {
            refresh: true,
            buttonCount: 5
        },
        columns: [
            //  {
            //    field: "Check",
            //    title: "Check",
            //    type: "boolean",
            //    template: '<input type="checkbox" #= Check ? \'checked="checked"\' : "" # class="chkbx" />',
            //    width: "100px"

            //},
            {
                field: "Id",
                hidden: true
            }, {
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
                    name: "destroy"                    
                },
                {
                    name: "edit"
                    }
                ],
                width: "200px",
                title: "&nbsp;"
            }
        ],

        dataSource: {
            page: 1,
            pageSize: 7,
            autoSync: true,
            transport: {
                read: function (e) {
                    getData(e);
                },
                update: function (options) {                    
                    updateData(options);
                    return true;
                },
                create: function (options) {
                    createData(options);
                    return true;
                },
                destroy: function (options) {
                    deleteData(options);
                    return true;
                },
                parameterMap: function (options, operation) {
                    if (operation !== "read" && options.models) {
                        return { models: kendo.stringify(options.models) };
                    }
                }
            },
            requestStart: function (e) {
                if (e.type !== "read") {
                    console.log(kendo.format("Request start ({0})", e.type));
                }
            },
            requestEnd: function (e) {
                if (e.type !== "read") {
                    console.log(kendo.format("Request end ({0})", e.type));
                }
            },
            schema: {
                model: {
                    type: "json",
                    id: "Id",
                    fields: {
                        "Id": { editable: false, nullable: true, type: "number" },
                        "Name": { type: "string", validation: { required: true }},
                        "Author": { type: "string", validation: { required: true }},
                        "YearOfPublishing": { type: "number", validation: { required: true } },
                        "Publisher": { type: "string", validation: { required: true }}
                    }
                }
            }            
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
            console.log("ssget");
            e.success(data);
        },
        error: function (data) {
            console.log(data);
            console.log("errget");
        }
    });
}

function updateData(options) {

    $.ajax({
        type: "POST",
        url: "UpdateBook",
        contentType: "application/json; charset =utf-8",
        data: JSON.stringify(options.data),
        datatype: 'json',
        success: function (data) {
            console.log(data);
            console.log(JSON.stringify(options.data));
            console.log("ssU");
        },
        error: function (data) {
            console.log(data);
            console.log(JSON.stringify(options.data));
            console.log("errU");
        }
    });
}
function createData(options) {
    $.ajax({
        url: "CreateBook",
        type: "POST",
        contentType: "application/json; charset =utf-8",
        dataType: 'json',
        data: JSON.stringify(options.data),
        success: function (data) {
            console.log(data);
            console.log("ssC" + JSON.stringify(options.data));//
        },
        error: function (data) {
            console.log(data);
            console.log("errC" + JSON.stringify(options.data));//
        }
    });
}
//function deleteData(e) {
//    //var id = getId(e);
//    $.ajax({
//        url: "DestroyBook?id=" + JSON.stringify(options.data.id),
//        type: "GET",
//        contentType: "application/json; charset =utf-8",
//        datatype: 'json',
//        success: function (data) {
//            console.log(data);
//        },
//        error: function (data) {
//            console.log(data)
//        }
//    });
//}

function deleteData(options) {
    var id = getId(options);
    $.ajax({
        url: "DestroyBook",
        type: "POST",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        data: id,
        success: function (data) {
            console.log(data);
            console.log("ssD" /*+ JSON.stringify(options.data)*/);
        },
        error: function (data) {
            console.log(data);
            console.log("errD" /*+ JSON.stringify(options.data)*/);
        }
    });
}

function getId(options) {
    return JSON.stringify(options.data);
}

//function setUpdate() {
//    $(".k-grid-update").click(function (e) {
//        //updateData(e);
//        var grid = $("#grid").getKendoGrid();
//        var item = grid.dataItem($(e.target).closest("tr"));
//        console.log(item);
//    })
//}

//function getBookModel(e) {
//    var row = $(e.target).closest("tr")[0].childNodes;
//    var book = {
//        id: row[0].innerText,
//        name: row[1].innerText,
//        author: row[2].innerText,
//        yearOfPublishing: row[3].innerText,
//        publisher: row[4].innerText
//    }
//    return JSON.stringify(book);
//}