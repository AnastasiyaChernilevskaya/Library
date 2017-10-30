﻿$(document).ready(function () {

    var grid = $("#grid").kendoGrid({

        height: 550,      
        sortable: true,
        toolbar: [
            {
                template: "<a class='addButton k-button' onclick='return toolbarAddClick()'><span class='k-icon k-add'></span>Add new record</a>"
            },
            {
                template: "<a class='getToXML k-button' onclick='return WriteToXML()'><span class='k-icon k-add'></span>Add book to XML file</a>"
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
                template: '<input type="checkbox"  id="Mycheckbox" #= IncludeToPage ? \'checked="checked"\' : "" # class="chkbx"/>',
                width: "100px"                
            },
            {
                field: "Id",
                hidden: true
            }, {
                field: "Name",
                title: "Name",
                width: "250"
            }, {
                field: "Author",
                title: "Author"
            }, {
                field: "YearOfPublishing",
                title: "Year of publishing",
                width: "100px"
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
                        "Name": { type: "string",},
                        "Author": { type: "string", },
                        "YearOfPublishing": { type: "number",  },
                        "Publisher": { type: "string",  },
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

    grid.element.on('click',".chkbx", function (e) {
        var dataItem = grid.dataItem($(e.target).closest("tr"));
        console.log(dataItem + "   " + e.target);
        $(e.target).prop("checked") === true ? dataItem.IncludeToPage = true : dataItem.IncludeToPage = false;
        updateData(dataItem);
    })
});


function WriteToXML() {
    console.log("Toolbar command save is clicked!");
    var txt = prompt("Please enter xml file-name :", "FileXML.xml");
    if (txt == null || txt == "") {
        alert('File will not be create');
        return false;
    }
    Test1(txt);   
} 

WriteToXML

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

function Test() {
    $.ajax({
        type: "GET",
        url: "GetXmlFile",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        success: function (data) {
            console.log(data);
            console.log("ss");
        },
        error: function (data) {
            console.log(data);
            console.log("err");
        }
    });
}

function Test1(options) {
    $.ajax({
        type: "GET",
        url: "WriteToXML?fileName=" + options,
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        success: function (data) {
            console.log(data);
            console.log("ss");
        },
        error: function (data) {
            console.log(data);
            console.log("err");
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

function addToFileXML(data) {
    $.ajax({
        url: "GetSerializedBook",
        type: "POST",
        contentType: "application/json; charset =utf-8",
        datatype: 'json',
        data: JSON.stringify(data),
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

function getChecked() {
    var books = [];
    $("input[type='checkbox']").each(function (index, element) {
        if ($(element).prop("checked") !== false) {
            var grid = $("#grid").data("kendoGrid");
            var dataItem = grid.dataItem($(element).closest('tr'));
            books.push(dataItem.id);
        }
    })
    console.log(books);
    Test(books);
}

function refreshGrid() {
    $('#grid').data('kendoGrid').dataSource.read();
    $('#grid').data('kendoGrid').refresh();
}

function updateData(data) {

    $.ajax({
        type: "POST",
        url: "UpdateBook",
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