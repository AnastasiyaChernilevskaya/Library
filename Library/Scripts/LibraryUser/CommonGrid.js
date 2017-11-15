$(document).ready(function () {

    var gridCommon = $("#commonGrid").kendoGrid({
        height: 550,
        sortable: true,
        pageable: {
            refresh: true,
            buttonCount: 5
        },
        columns: [
            {
                field: "LibraryType",
                title: "Library Type",
                template: '<span  > #= LibraryType ==0 ? \'Book\' : ( LibraryType == 1? \'Newspaper\':  \'Periodical\')  # </span>'
            }, {
                field: "Name",
                title: "Name",
                width: "250"
            }, {
                field: "Publisher",
                title: "Publisher",
                width: "250px"
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
                        "Name": { type: "string" },
                        "Publisher": { type: "string" },
                        "LibraryType": { type: "string" }                    
                    }
                }
            }
        }

    }).data("kendoGrid");
});

function getData(e) {
    $.ajax({
        type: "GET",
        url: "CommonLibrary/GetCommonLibrary",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        success: function (data) {
            e.success(data);
        },
        error: function (data) {
        }
    });
}

function refreshGrid() {
    $('#commonGrid').data('kendoGrid').dataSource.read();
    $('#commonGrid').data('kendoGrid').refresh();
}