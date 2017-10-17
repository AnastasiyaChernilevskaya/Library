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
            command: ["destroy"],
            title: "&nbsp;",
            width: "250px",
            click: function (e) {
                destroyData(e);
            }

        }],
        dataSource: {
            //            autoSync: true,
            transport: {
                read: function (e) {
                    getData(e);
                //},
                //update: {
                //    url: "/MyLibrary/UpdateBook",
                //    dataType: "jsonp"
                //},
                //destroy: {
                //    url: "/MyLibrary/DestroyBook",
                //    dataType: "jsonp"
                //},
                //create: {
                //    url: "/MyLibrary/CreateBook",
                //    dataType: "jsonp"
                //},
                //parameterMap: function (options, operation) {
                //    if (operation !== "read" && options.models) {
                //        return { models: kendo.stringify(options.models) };
                //    }
                }
            },
        },

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
function updateData(e) {
    console.log("kkk")
    $.ajax({
        type: "POST",
        url: "UpdateBook",
        contentType: "application/json; charset =utf-8",
        data: data,
        datatype: 'jsonp',
        success: function (data) {
            console.log(data);
            e.success(data);
            console.log("ss");
        },
        error: function (data) {
            console.log(data);
            console.log("k");
        }
    });
}

////function deleteData(e) {
////    $.ajax({
////        type: "GET",
////        url: "DestroyBook",
////        contentType: "application/json; charset =utf-8",
////        datatype: 'json',
////        success: function (data) {
////            console.log(data);
////            e.success(data);
////        },
////        error: function (data) {
////            console.log(data)
////        }
////    });
////}

function destroyData(e) {
    e.preventDefault();
    console.log($(e.target).closest("tr"));
}

//$(.k-buttonk - button - icontext k- grid - delete).button.click(function (e) {
//    deleteData(e);
//});
////function aa(e) {
////    $(".k-button.k-button-icontext.k-primary.k-grid-update").click(function (e) {
////        deleteData(e)
////    })
////}
//$.extend(); var object = $.extend({}, object1, object2);

//function updateData(e) {
//    $.ajax({
//        url: "UpdateBook",
//        type: "POST",
//        contentType: "application/json; charset =utf-8",
//        dataType: 'json',
//        data: e.data,
//        success: function (data) {
//            console.log(data);
//            e.success(data);
//        },
//        error: function (data) {
//            console.log(data)
//        }
//    });
//<a role="button" class="k-button k-button-icontext k-grid-delete" href="#"><span class="k-icon k-i-close"></span>Delete</a>

//}<a role="button" class="k-button k-button-icontext k-primary k-grid-update" href="#"><span class="k-icon k-i-check"></span>Update</a>