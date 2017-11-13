var wnd,
    detailsTemplate;

$(document).ready(function () {

    var newspapersGrid = $("#NewspapersGrid").kendoGrid({

        height: 550,
        sortable: true,
        columnResizeHandleWidth: 20,
        resizable: true,       
        pageable: {
            refresh: true,
            buttonCount: 5
        },
        columns: [
            
            {
                field: "Id",
                hidden: true
            }, {
                field: "IncludeToPage",
                title: "Include",
                type: "boolean",
                template: '<input type="checkbox"  id="Mycheckbox" #= IncludeToPage ? \'checked="checked"\' : "" # class="chkbx"/>',
                width: "100px"
            },
            {
                field: "LibraryType",
                title: "Library Type",
                template: '<span  > #= LibraryType ==0 ? \'Book\' : ( LibraryType == 1? \'Newspaper\':  \'Periodical\')  # </span>'

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
                field: "YearOfPublishing",
                title: "Date of publishing",
                width: "100px",
                template: "#= kendo.toString(kendo.parseDate(YearOfPublishing, 'yyyy-MM-dd'), 'MM/dd/yyyy') #"
            },          
            
            //{
            //    template: "<a class='DestroyButton k-button'\"><span class='k-icon k-delete'></span>Delete</a>",
            //    title: "&nbsp;",
            //    width: "100px"
            //}, {
            //    template: "<a class='EditButton k-button' onclick=\"editNewspaper('#=Id#')\"><span class='k-icon k-edit'></span>Edit</a>",
            //    title: "&nbsp;",
            //    width: "100px"
            //}
            {
                command: { text: "View Details", click: showDetails },
                title: "&nbsp;",
                width: "180px"
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
                        "LibraryType": { type: "string" },
                        "YearOfPublishing": { type: "date" }
                    }
                }
            }
        }

    }).data("kendoGrid");

    //newspapersGrid.element.on('click', '.DestroyButton', function () {
    //    var dataItem = newspapersGrid.dataItem($(this).closest('tr'));
    //    deleteData(dataItem);
    //});

    newspapersGrid.element.on('click', ".chkbx", function (e) {
        var dataItem = newspapersGrid.dataItem($(e.target).closest("tr"));
        console.log(dataItem + "   " + e.target);
        $(e.target).prop("checked") === true ? dataItem.IncludeToPage = true : dataItem.IncludeToPage = false;
        updateData(dataItem);
    });

    wnd = $("#details")
        .kendoWindow({
            actions: ["Close"],
            title: "Details",
            modal: false,
            visible: false,
            resizable: false,
            width: 300
        }).data("kendoWindow");

    detailsTemplate = kendo.template($("#template").html());
});

function showDetails(e) {
    e.preventDefault();

    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    wnd.content(detailsTemplate(dataItem));
    wnd.center().open();
}

function destroyNewspaper() {
    var id = $('#objectId').val();
    deleteData(id);
    wnd.destroy();
}

function editNewspaper() {
    var id = $('#objectId').val();
    window.location.href = 'EditNewspaper/' + id;
    wnd.destroy();
}

function getData(e) {
    $.ajax({
        type: "GET",
        url: "GetNewspapers",
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

function deleteData(id) {

    $.ajax({
        url: "DestroyNewspaper?id=" + id,
        type: "GET",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        success: function (id) {
            alert(id + ' was Delited!!!');
            console.log("ssD");
            refreshGrid();
        },
        error: function (data) {
            console.log(data);
            console.log("errD" + id);
        }
    });
}

function addToFile(format) {

    location.href = 'GetFile?format=' + format;
}

$(document).ready(function () {
    $("#typeForm").submit(function (event) {
        event.preventDefault();
        var filetype = $('input[name=filetype]:checked').val();
        if (filetype === "xml") {
            format = "xml";
        }
        if (filetype === "txt") {
            format = "txt";
        }
        addToFile(format);
    });
});

function refreshGrid() {
    $('#NewspapersGrid').data('kendoGrid').dataSource.read();
    $('#NewspapersGrid').data('kendoGrid').refresh();
}

function updateData(data) {

    $.ajax({
        type: "POST",
        url: "UpdateNewspaper",
        contentType: "application/json; charset =utf-8",
        data: JSON.stringify(data),
        datatype: 'json',
        success: function (data) {
            console.log(data);
            console.log(JSON.stringify(data));
            console.log("ssU");
        },
        error: function (data) {
            console.log(data);
            console.log(JSON.stringify(data));
            console.log("errU");
        }
    });
}