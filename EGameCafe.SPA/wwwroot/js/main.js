function changeTabActiveClassOnClick(inputRef, activeClass) {
    $(inputRef).click(function () {

        var listItems = $(inputRef);

        for (let i = 0; i < listItems.length; i++) {
            listItems[i].classList.remove(activeClass);
        }

        this.classList.add(activeClass);
    });
}