$("#editForm").submit(function (event) {
    $("#check").prop("checked") == true ? $("#check").val(true) : null
})

$("#addForm").submit(function (event) {
    $("#check").prop("checked") == true ? $("#check").val(true) : null
})
function validateForm() {
    var name = document.forms["editForm"]["Name"].value;
    if (name == "") {
        alert("Name must be filled out");
        return false;
    }
    var author = document.forms["editForm"]["Author"].value;
    if (author == "") {
        alert("Add at list 1 author");
        return false;
    }
    var publisher = document.forms["editForm"]["Publisher"].value;
    if (publisher == "") {
        alert("Add publisher of the book");
        return false;
    }
    var yearOfPublishing = document.forms["editForm"]["YearOfPublishing"].value;
    if (yearOfPublishing == "") {
        alert("Add year of publishing");
        return false;
    }
}
function validateForm() {
    var name = document.forms["addForm"]["Name"].value;
    if (name == "") {
        alert("Name must be filled out");
        return false;
    }
    var author = document.forms["addForm"]["Author"].value;
    if (author == "") {
        alert("Add at list 1 author");
        return false;
    }
    var publisher = document.forms["addForm"]["Publisher"].value;
    if (publisher == "") {
        alert("Add publisher of the book");
        return false;
    }
    var yearOfPublishing = document.forms["addForm"]["YearOfPublishing"].value;
    if (yearOfPublishing == "") {
        alert("Add year of publishing");
        return false;
    }
}