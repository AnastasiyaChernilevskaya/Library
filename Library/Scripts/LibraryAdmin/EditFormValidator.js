$("#editForm").submit(function (event) {
    $("#check").prop("checked") === true ? $("#check").val(true) : null;
});

function validateEditForm() {

    var Name = document.forms["editForm"]["Name"].value;
    name = Name.split(' ').join('');
    if (name === "") {
        alert("Name must be filled out");
        return false;
    }

    var Publisher = document.forms["editForm"]["Publisher"].value;
    publisher = Publisher.split(' ').join('');
    if (publisher === "") {
        alert("Add publisher");
        return false;
    }

    var yearOfPublishing = document.forms["editForm"]["YearOfPublishing"].value;
    if (yearOfPublishing === "") {
        alert("Add year of publishing");
        return false;
    }
    return true;
}
function validateBookEditForm() {
    if (!(validateEditForm() && validateBookAuthorEditForm())) {
        return false;
    }
}

function validateBookAuthorEditForm() {
    var author = document.forms["editForm"]["Author"].value;
    author = author.split(' ').join('');
    if (author === "") {
        alert("Add at list 1 author");
        return false;
    }
    return true;
}
