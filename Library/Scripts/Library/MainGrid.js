$(document).ready(function () {

    var grid = $("#grid").kendoGrid({
        height: 550,
        sortable: true,
        pageable: {
            refresh: true,
            buttonCount: 5
        },
        columns: [
            {
                field: "Id",
                hidden: true
            }, 
            {
                field: "IncludeToPage",
                title: "Include",
                type: "boolean",
                template: '<input type="checkbox" class="chkbx" id="Mycheckbox" #= IncludeToPage ? \'checked="checked"\' : "" # />',
                width: "100px"
            },
            {
                field: "LibraryType",
                title: "Library Type",
                template: '<span > #= LibraryType ==0 ? \'Book\' : ( LibraryType == 1? \'Newspaper\':  \'Periodical\')  # </span>'
            },
            {
                field: "Name",
                title: "Name",
                width: "250"
            },
            {
                field: "Publisher",
                title: "Publisher",
                width: "250px"
            },
            {
                template: "<a class='DestroyButton k-button'\"><span class='k-icon k-delete'></span>Delete</a>",
                title: "&nbsp;",
                width: "100px"
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
                        "IncludeToPage": { type: "boolean" },
                        "Name": { type: "string" },
                        "Publisher": { type: "string" },
                        "LibraryType": { type: "string" }                    
                    }
                }
            }
        }

    }).data("kendoGrid");

    grid.element.on('click', '.DestroyButton', function () {
        var dataItem = grid.dataItem($(this).closest('tr'));
        deleteData(dataItem);
    });

    grid.element.on('click', ".chkbx", function (e) {
        var dataItem = grid.dataItem($(e.target).closest("tr"));
        console.log(dataItem + "   " + e.target);
        $(e.target).prop("checked") === true ? dataItem.IncludeToPage = true : dataItem.IncludeToPage = false;
        updateData(dataItem);
    });
});

function getData(e) {
    $.ajax({
        type: "GET",
        url: "GetLibrary",
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
        url: "DestroyLibraryItem?id=" + JSON.stringify(dataItem.id) + "&entityType=" + dataItem.LibraryType,
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

//function addToFile(format) {

//    location.href = '/MyLibrary/GetFile?format=' + format;
//}

//$(document).ready(function () {
//    $("#typeForm").submit(function (event) {
//        event.preventDefault();
//        var filetype = $('input[name=filetype]:checked').val();
//        if (filetype === "xml") {
//            format = "xml";
//        }
//        if (filetype === "txt") {
//            format = "txt";
//        }
//        addToFile(format);
//    })
//})

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
    $('#grid').data('kendoGrid').dataSource.read();
    $('#grid').data('kendoGrid').refresh();
}

function updateData(dataItem) {

    $.ajax({
        type: "POST",
        url: "UpdateLibrary?id=" + JSON.stringify(dataItem.id) + "&entityType=" + dataItem.LibraryType,
        contentType: "application/json; charset =utf-8",
        data: JSON.stringify(dataItem),
        datatype: 'json',
        success: function (data) {
            console.log(data);
            console.log(JSON.stringify(dataItem));
            console.log("ssU");
        },
        error: function (dataItem) {
            console.log(dataItem);
            console.log(JSON.stringify(dataItem));
            console.log("errU");
        }
    });

}