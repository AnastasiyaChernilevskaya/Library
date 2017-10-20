$(document).ready(function () {

    var grid = $("#grid").kendoGrid({

        height: 550,
        //editable: "inline",
        //editable: true,
        //editable: {
        //    create: true,
        //    read: true,
        //    update: true,
        //},        
        sortable: true,
        toolbar: [
            {
                template: "<a class='saveButton k-button' onclick='return toolbarSave_click()'><span class='k-icon k-save'></span>Save changes</a>"
            }, {
                template: "<a class='cancelButton k-button' onclick='return toolbarCancel_click()'><span class='k-icon k-cancel'></span>Cancel changes</a>"
            }, {
                template: "<a class='addButton k-button' onclick='return toolbarAdd_click()'><span class='k-icon k-add'></span>Add new record</a>"
            }],
        //selectable: "multiple row",
        //persistSelection: false,
        pageable: {
            refresh: true,
            buttonCount: 5
        },
        columns: [
            {
                field: "IncludeToPage",
                title: "Include To Page",
                type: "boolean",
                template: '<input type="checkbox" #= Check ? \'checked="checked"\' : "" # class="chkbx" />',
                width: "100px"
            },
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
                title: "Publisher",
                width: "250px"
            },
            //    {
            //    command: [{
            //        name: "Destroy",
            //        click: function (options) {
            //            deleteData(options);
            //        },
            //        width: "80px",
            //        title: "&nbsp;"
            //    }],
               
            //},
            [{
                template: "<a class='DestroyButton k-button' onclick=\"editBook('#=Id#')\"><span class='k-icon k-delete'></span>Delete</a>"
            },{
                template: "<a class='EditButton k-button' onclick=\"editBook('#=Id#')\"><span class='k-icon k-edit'></span>Edit book</a>",
                title: "&nbsp;",
                width: "100px"
            }]
        ],

        dataSource: {
            page: 1,
            pageSize: 7,
            autoSync: true,
            transport: {
                read: function (e) {
                    getData(e);
                }
                //update: function (options) {                    
                //    updateData(options);
                //    return true;
                //},
                //create: function (options) {
                //    createData(options);
                //    return true;
                //},
                //destroy: function (options) {
                //    deleteData(options);
                //    return true;
                //},
                //parameterMap: function (options, operation) {
                //    if (operation !== "read" && options.models) {
                //        return { models: kendo.stringify(options.models) };
                //    }
                //}
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

    }).data("kendoGrid");
    grid.element.on('click', '.DestroyButton', function () {
        var dataItem = grid.dataItem($(this).closest('tr'));
        alert(dataItem.id + ' was clicked!!!');
        deleteData(dataItem);
    });
});

//function toolbarSave_click() {
//    console.log("Toolbar command save is clicked!");
//    window.location.href = 
//    return false;
//}
//function toolbarCancel_click() {
//    console.log("Toolbar command cancel is clicked!");
//    return false;
//}
//function toolbarAdd_click() {
//    console.log("Toolbar command add is clicked!");
//    return false;
//}

function editBook(id) {
    window.location.href = '/Book/EditBook/' + id;
}

function addPost() {
    window.location.href = 'Book/AddBook';
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
        },
        error: function (data) {
            console.log(data);
            console.log("errD" + id);
        }
    });
}

//______________________________________________________________
////function deleteData(dataItem) {
////    var id = JSON.stringify(dataItem.id);
////    $.ajax({
////        url: "DestroyBook",
////        type: "POST",
////        contentType: "application/json; charset =utf-8",
////        datatype: 'json',
////        data: id,
////        success: function (data) {
////            console.log(data);
////            console.log("ssD" /*+ JSON.stringify(options.data)*/);
////        },
////        error: function (data) {
////            console.log(data);
////            console.log("errD"+ id /*+ JSON.stringify(options.data)*/);
////        }
////    });
////}


//function updateData(options) {

//    $.ajax({
//        type: "POST",
//        url: "UpdateBook",
//        contentType: "application/json; charset =utf-8",
//        data: JSON.stringify(options.data),
//        datatype: 'json',
//        success: function (data) {
//            console.log(data);
//            console.log(JSON.stringify(options.data));
//            console.log("ssU");
//        },
//        error: function (data) {
//            console.log(data);
//            console.log(JSON.stringify(options.data));
//            console.log("errU");
//        }
//    });
//}
//function createData(options) {
//    $.ajax({
//        url: "CreateBook",
//        type: "POST",
//        contentType: "application/json; charset =utf-8",
//        dataType: 'json',
//        data: JSON.stringify(options.data),
//        success: function (data) {
//            console.log(data);
//            console.log("ssC" + JSON.stringify(options.data));//
//        },
//        error: function (data) {
//            console.log(data);
//            console.log("errC" + JSON.stringify(options.data));//
//        }
//    });
//}

////////function deleteData(options) {
////////    var id = getId(options);
////////    $.ajax({
////////        url: "DestroyBook",
////////        type: "POST",
////////        contentType: "application/json; charset =utf-8",
////////        datatype: 'json',
////////        data: id,
////////        success: function (data) {
////////            console.log(data);
////////            console.log("ssD" /*+ JSON.stringify(options.data)*/);
////////        },
////////        error: function (data) {
////////            console.log(data);
////////            console.log("errD" /*+ JSON.stringify(options.data)*/);
////////        }
////////    });
////////}

////function getId(options) {
////    return JSON.stringify(options.data);
////}

////function setUpdate() {
////    $(".k-grid-update").click(function (e) {
////        //updateData(e);
////        var grid = $("#grid").getKendoGrid();
////        var item = grid.dataItem($(e.target).closest("tr"));
////        console.log(item);
////    })
////}

////function getBookModel(e) {
////    var row = $(e.target).closest("tr")[0].childNodes;
////    var book = {
////        id: row[0].innerText,
////        name: row[1].innerText,
////        author: row[2].innerText,
////        yearOfPublishing: row[3].innerText,
////        publisher: row[4].innerText
////    }
////    return JSON.stringify(book);
////}