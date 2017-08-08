function displayOrHide(inputOption, selectedValue, conditionId) {

    $(inputOption).parent().parent().parent().parent().parent().parent().find("[data-reference]=" + conditionId).each(function () {
        var criteria = $(this).attr('data-criteria');
        var options = $(this).attr('data-options');

        if (criteria == "" || options == "") {
            $(this).attr('style', '');
        }

        else if (selectedValue == "")
        {
            $(this).attr('style', 'display:none;');
        }

        else if (criteria == "==") {

            if (options.indexOf(selectedValue) > -1) {
                $(this).attr('style', '');
            }

            else {
                $(this).attr('style', 'display:none;');
            }
        }

        else if (criteria == "!=") {
            if (options.indexOf(selectedValue) > -1) {
                $(this).attr('style', 'display:none;');
            }

            else {
                $(this).attr('style', '');
            }
        }
    });
}

function displayOrHideFromCheckBox(inputOption, selectedValue, conditionId) {

    $(inputOption).parent().parent().parent().parent().parent().parent().find("[data-reference]=" + conditionId).each(function () {
        var criteria = $(this).attr('data-criteria');
        var options = $(this).attr('data-options');

        if (criteria == "" || options == "") {
            $(this).attr('style', '');
        }

        else if (selectedValue == "") {
            $(this).attr('style', 'display:none;');
        }

        else if (criteria == "==") {
            var foundOption = false;

            $.each(selectedValue, function (i, val) {
                if (!foundOption && options.indexOf(selectedValue[i]) > -1)
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
                if (!foundOption && options.indexOf(selectedValue[i]) > -1) {
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