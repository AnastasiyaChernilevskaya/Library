$(document).ready(function () {
    $("#grid").kendoGrid({
       
        height: 550,
        editable: true,
        sortable: true,

        pageable: {
            refresh: true,
            pageSizes: true
        },
        columns: [{
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
            command: "destroy",
            width: 110
        }],
        toolbar: ["create"],
        dataSource: {
            transport: {
                read: function (e) {
                    getData(e);
                }
            },
        }
        
    });
});
function getData(e) {
    $.ajax({
        type: "GET",
        url: "MyLibrary/GetBooks",
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