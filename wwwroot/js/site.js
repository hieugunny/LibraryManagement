// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function ValidateForm() {
    console.log("adfasdf");
    var startDate = new Date(document.getElementById("borrow-date").value);
    var endDate = new Date(document.getElementById("due-date").value);
    var warnTag = document.getElementById("warn-tag");
    if (startDate > endDate) {
        warnTag.innerHTML = "Hạn trả không thể nhỏ hơn ngày mươn"
    }
    console.log(startDate);
    console.log(endDate);
    console.log(warnTag);
}
console.log("alo 123");

console.log(startDate);
console.log(endDate);
console.log(warnTag)