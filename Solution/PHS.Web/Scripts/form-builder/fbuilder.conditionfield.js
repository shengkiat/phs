function displayOrHide(inputOption, selectedValue, conditionId) {

    $(inputOption).parent().parent().parent().parent().parent().find("[data-reference]=" + conditionId).each(function () {
        var criteria = $(this).attr('data-criteria');
        var options = $(this).attr('data-options');

        if (selectedValue == "")
        {
            $(this).attr('style', 'display:none;');
        }

        else if (criteria == "==") {

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

function displayOrHideFromCheckBox(inputOption, selectedValue, conditionId) {

    $(inputOption).parent().parent().parent().parent().parent().find("[data-reference]=" + conditionId).each(function () {
        var criteria = $(this).attr('data-criteria');
        var options = $(this).attr('data-options');

        if (selectedValue == "") {
            $(this).attr('style', 'display:none;');
        }

        else if (criteria == "==") {
            var foundOption = false;

            $.each(selectedValue, function (i, val) {
                if (!foundOption && options.includes(selectedValue[i]))
                {
                    foundOption = true;
                }
            });

            if (foundOption) {
                $(this).attr('style', '');
            }

            else {
                $(this).attr('style', 'display:none;');
            }
        }

        else if (criteria == "!=") {
            var foundOption = false;

            $.each(selectedValue, function (i, val) {
                if (!foundOption && options.includes(selectedValue[i])) {
                    foundOption = true;
                }
            });

            if (foundOption) {
                $(this).attr('style', 'display:none;');
            }

            else {
                $(this).attr('style', '');
            }
        }
    });
}