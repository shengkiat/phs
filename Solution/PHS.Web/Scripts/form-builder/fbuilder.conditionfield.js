function displayOrHide(inputOption, selectedValue, conditionId) {

    $(inputOption).parent().parent().parent().parent().parent().find("[data-reference]=" + conditionId).each(function () {
        var criteria = $(this).attr('data-criteria');
        var options = $(this).attr('data-options');
        if (criteria == "==") {
            if (options.includes(selectedValue)) {
                $(this).attr('style', '');
            }

            else {
                $(this).attr('style', 'display:none;');
            }
        }

        else if (criteria == "!=") {
            if (options.includes(selectedValue)) {
                $(this).attr('style', 'display:none;');
            }

            else {
                $(this).attr('style', '');
            }
        }
    });
}