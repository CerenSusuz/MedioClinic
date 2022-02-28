

//Use the DOMContentLoaded event to run code after the DOM is fully loaded.

document.addEventListener("DOMContentLoaded", function () {
    var sidenavElements = document.querySelectorAll(".sidenav");
    M.Sidenav.init(sidenavElements);
    var dropdownElements = document.querySelectorAll(".dropdown-trigger");

    M.Dropdown.init(dropdownElements, {
        hover: false
    });
});