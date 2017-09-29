function jq(myid) {
    return myid.replace(/(:|\.|\[|\]|,|=|@)/g, "\\$1");
}

function displayOrHide(inputOption, selectedValue, conditionId) {

    $(inputOption).parent().parent().parent().parent().parent().parent().find("[data-id=" + conditionId + "]").each(function () {
        $(this).attr('data-value', selectedValue);
    });

    //check re

    $(inputOption).parent().parent().parent().parent().parent().parent().find("[data-reference=" + conditionId + "]").each(function () {
        var fieldId = $(this).attr('data-id');
        var criteria = $(this).attr('data-criteria');
        var options = $(this).attr('data-options');

        var displayValue = '';

        if (criteria == "" || options == "") {
            displayValue = '';
        }

        else if (selectedValue == "")
        {
            displayValue = 'display:none;';
        }

        else if (criteria == "==") {

            if (options.indexOf(selectedValue) > -1) {
                displayValue = '';
            }

            else {
                displayValue = 'display:none;';
            }
        }

        else if (criteria == "!=") {
            if (options.indexOf(selectedValue) > -1) {
                displayValue = 'display:none;';
            }

            else {
                displayValue = '';
            }
        }

        $(this).attr('style', displayValue);

        displayOrHideInner($(this), fieldId);
    });
}

function displayOrHideFromCheckBox(inputOption, selectedValue, conditionId) {

    $(inputOption).attr('data-value', selectedValue);

    $(inputOption).parent().parent().parent().parent().parent().parent().find("[data-reference=" + conditionId + "]").each(function () {
        var fieldId = $(this).attr('data-id');
        var criteria = $(this).attr('data-criteria');
        var options = $(this).attr('data-options');
        var displayValue = '';

        if (criteria == "" || options == "") {
            displayValue = '';
        }

        else if (selectedValue == "") {
            displayValue = 'display:none;';
        }

        else if (criteria == "==") {
            var foundOption = false;

            $.each(selectedValue, function (i, val) {
                if (!foundOption && options.indexOf(selectedValue[i]) > -1) {
                    foundOption = true;
                }
            });

            if (foundOption) {
                displayValue = '';
            }

            else {
                displayValue = 'display:none;';
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
                displayValue = 'display:none;';
            }

            else {
                displayValue = '';
            }
        }

        $(this).attr('style', displayValue);

        displayOrHideInner($(this), fieldId);
    });
}

function displayOrHideInner(inputOption, conditionId) {
    var displayValue = $(inputOption).attr('style');
    var dataValue = "";

    $(inputOption).parent().parent().parent().parent().parent().parent().find("[data-id=" + conditionId + "]").each(function () {
        dataValue = $(this).attr('data-value');
    });

    if (dataValue != "" && dataValue !== undefined) {
        $(inputOption).parent().parent().parent().parent().parent().parent().find("[data-reference=" + conditionId + "]").each(function () {
            var fieldId = $(this).attr('data-id');

            if (displayValue === undefined) {
                displayValue = '';
            }

            $(this).attr('style', displayValue);

            if (fieldId != "" && fieldId !== undefined) {
                displayOrHideInner($(this), fieldId);
            }
        });
    }
}

