$("#addForm").submit(function (event) {
    $("#check").prop("checked") === true ? $("#check").val(true) : null;
});


function validateAddForm() {

    var Name = document.forms["addForm"]["Name"].value;
    name = Name.split(' ').join('');
    if (name === "") {
        alert("Name must be filled out");
        return false;
    }

    var Publisher = document.forms["addForm"]["Publisher"].value;
    publisher = Publisher.split(' ').join('');
    if (publisher === "") {
        alert("Add publisher");
        return false;
    }

    var yearOfPublishing = document.forms["addForm"]["YearOfPublishing"].value;
    if (yearOfPublishing === "") {
        alert("Add year of publishing");
        return false;
    }
    return true;
}

function validateBookAddForm() {
    if (!(validateAddForm() && validateBookAuthorAddForm())) {
        return false;
    }
}

function validateBookAuthorAddForm() {

    var author = document.forms["addForm"]["Author"].value;
    author = author.split(' ').join('');
    if (author === "") {
        alert("Add at list 1 author");
        return false;
    }
    return true;
}