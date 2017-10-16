$(document).ready(function () {
    $("#grid").kendoGrid({


        height: 550,
        sortable: true,
        pageable: {
            refresh: true,
            pageSizes: true,
            buttonCount: 5
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
        //dataSource:

    });
});