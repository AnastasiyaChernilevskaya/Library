$(document).ready(function () {

    var grid = $("#grid").kendoGrid({

        height: 550,
        sortable: true,
        toolbar: [
            {
                template: "<a class='addButton k-button' onclick='return toolbarAddClick()'><span class='k-icon k-add'></span>Add new record</a>"
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
                template: '<input type="checkbox" #= IncludeToPage ? \'checked="checked"\' : "" # class="chkbx" />',
                width: "100px",
                defaultValue: "true"
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
                title: "Year",
                width: "150px"
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
                        "Name": { type: "string", validation: { required: true } },
                        "Author": { type: "string", validation: { required: true } },
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
}); 