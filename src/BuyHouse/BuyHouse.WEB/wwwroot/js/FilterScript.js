/*set cheked values to input*/
function setChekedValue(inputName, inputId, key) {
    document.forms.filterForm.addEventListener("submit", function (e) {
        const form = document.forms.filterForm;
        var input = document.querySelector(`input[name="${inputName}"]:checked`);

        if (input.checked) {
            let value = input.value;

            localStorage.setItem(key, value);
        }
    });

    var inputs = document.querySelectorAll(`#${inputId}`);
    for (const item2 of inputs) {
        if (item2.value == localStorage.getItem(key)) {
            item2.checked = true;
        }
    }
}

/*Type of price change style*/
function changeTypeOfPriceStyle() {
    var typeOfPrice = document.querySelector(`input[name="filter.TypeOfPrice"]:checked`)
    var totalPrice = document.querySelectorAll("#totalPrice");
    var pricePerSquareMeter = document.querySelectorAll("#pricePerSquareMeter");
    if (typeOfPrice.value == "TotalPrice") {
        for (const item of totalPrice) {
            item.style.fontSize = "35px";
        }
        for (const item of pricePerSquareMeter) {
            item.style.fontSize = "15px";
        }
    }
    else {
        for (const item of totalPrice) {
            item.style.fontSize = "15px";
        }
        for (const item of pricePerSquareMeter) {
            item.style.fontSize = "35px";
        }
    }
}

/*change 0 to " " in input max or min value */
function setNull() {
    var input = document.querySelectorAll("#inputMinMaxValue");
    for (const item of input) {
        if (item.value == "0")
            item.value = ""
    }
}

setChekedValue("filter.CountRooms", "countRooms", "countRoomKey")
setChekedValue("filter.TypeOfPrice", "typeOfPrice", "typeOfPriceKey")
setChekedValue("filter.Currency", "currency", "currencyKey")
setChekedValue("pageSize", "pageSize", "pageSizeKey" )
changeTypeOfPriceStyle()
setNull()