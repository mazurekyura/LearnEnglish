$(document).ready(function () {

    const buttonElem = document.querySelector(".card");
    const optionElem = document.querySelector(".card-option");

    buttonElem.addEventListener("click", () => {
        optionElem.classList.toggle("hidden");
    });

});