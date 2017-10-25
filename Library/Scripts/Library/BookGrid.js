$(document).ready(function () {

    var grid = $("#grid").kendoGrid({

        height: 550,      
        sortable: true,
        toolbar: [
            {
                template: "<a class='addButton k-button' onclick='return toolbarAddClick()'><span class='k-icon k-add'></span>Add new record</a>"
            }, {
                template: "<a class='getToXML k-button' onclick='return toolbarGetToXMLClick()'><span class='k-icon k-add'></span>Add book to file</a>"
            }],
        pageable: {
            refresh: true,
            buttonCount: 5
        },
        columns: [
            {
                field: "IncludeToPage",
                title: "Include",
                type: "boolean",
                template: '<input type="checkbox" #= IncludeToPage ? \'checked="checked"\' : "" # class="chkbx" id="Mycheckbox" />',
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
            {
                template: "<a class='DestroyButton k-button'\"><span class='k-icon k-delete'></span>Delete</a>",
                title: "&nbsp;",
                width: "100px", 
            },{
                template: "<a class='EditButton k-button' onclick=\"editBook('#=Id#')\"><span class='k-icon k-edit'></span>Edit</a>",
                title: "&nbsp;",
                width: "100px",            
            }
            
        ],

        dataSource: {
            page: 1,
            pageSize: 7,
            autoSync: true,
            transport: {
                read: function (e) {
                    getData(e);
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
                        "Publisher": { type: "string", validation: { required: true } },
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

    //grid.element.on('change', '#Mycheckbox', function (e) {
    //    var dataItem = grid.dataItem(e.Item);
    //    var check = DataItem.FindControl("Mycheckbox"); //accessing the checkbox
    //    check.Checked = true;
    //    alert(dataItem.id + ' was clicked!!!' + event);
    //});
});

//function toolbarSave_click() {
//    console.log("Toolbar command save is clicked!");
//    window.location.href = 
//    return false;
//}
function toolbarGetToXMLClick() {
    console.log("Toolbar command XML clicked!");
    addToFileXML();
    return false;

}

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

function addToFileXML() {
    $.ajax({
        url: "GetSerializedBook",
        type: "POST",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        data: getDataById,
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

function getDataById() {
    $.ajax({
        url: "GetBook?id=3",
        type: "GET",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        success: function (data) {
            console.log(data);
            console.log("ss");
            refreshGrid();
        },
        error: function (data) {
            console.log(data);
            console.log("err");
        }
    });
}

function refreshGrid() {
    $('#grid').data('kendoGrid').dataSource.read();
    $('#grid').data('kendoGrid').refresh();
}
//$("#Mycheckbox").change(function () {
//    if ($(this).is('checked')) alert("checked");
//    if ($(this).is('')) alert("Unchecked");
//});
//$(":checkbox").parent().click(function (evt) {
//    if (evt.target.type !== 'checkbox') {
//        var $checkbox = $(":checkbox", this);
//        $checkbox.attr('checked', !$checkbox.attr('checked'));
//        $checkbox.change();
//    }
//});
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


//http://demos.telerik.com/kendo-ui/datasource/xml-data
//https://docs.telerik.com/kendo-ui/framework/save-files/introduction
//https://www.telerik.com/forums/how-to-export-data-from-context-menu-to-a-text-file
//https://demos.telerik.com/kendo-ui/grid/editing-inline
//https://demos.telerik.com/kendo-ui/grid/editing-popup
//http://jsbin.com/EjUPiZoK/4/edit?html,output
//