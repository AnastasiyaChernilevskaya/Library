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
            command: ["edit", "destroy"],
            title: "&nbsp;",
            width: "250px"
        }],
        dataSource: {
            transport: {
                read: function(e) {
                    getData(e);
                },
                update: function(e) {
                    updateData(e);
                },
                destroy: {
                    url: "/Products/Destroy",
                    dataType: "jsonp"
                },
                create: {
                    url: "/Products/Create",
                    dataType: "jsonp"
                }
            },
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

function updateData(e) {
    $.ajax({
        type: "POST",
        url: "UpdateBook",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        data: e.data,
        success: function (data) {
            console.log(data);
            e.success(data);
        },
        error: function (data) {
            console.log(data)
        }
    });
}