var entries = function () {


    function bindBasicActions() {
        $('.select-all').live('click', function () {
            if ($(this).is(':checked')) {
                $('.select-item').attr('checked', true)
            } else {
                $('.select-item').attr('checked', false)                
            }

        });

        $('.select-item').live('click', function () {
            if ($('.select-item').not(':checked').length == 0) {
                $('.select-all').attr('checked', true)
            }else {
                $('.select-all').removeAttr('checked')
            }
        });

        $('#sortButton').live('click', function () {
            $('#trSortRow0').clone().show().appendTo($('#trSortRow0').parent());
        });

        $('#removeSortButton').live('click', function () {
            $(this).parent().parent().remove();
        });

        $('#criteriaButton').live('click', function () {
            $('#trCriteriaRow0').clone().show().appendTo($('#trCriteriaRow0').parent());
        });

        $('#addCriteriaConditionButton').live('click', function () {
            $(this).parent().parent().parent().find("#trCriteriaRow1").each(function () {
                $(this).clone().show().appendTo($(this).parent());
                return false;
            });

            toggleCriteriaFields($(this));

        });

        $('#addNextCriteriaConditionButton').live('click', function () {
            $(this).parent().parent().clone().show().appendTo($(this).parent().parent().parent());
        });

        $('#removeCriteriaButton').live('click', function () {
            $(this).parent().parent().parent().remove();
        });

        $('#removeCriteriaConditionButton').live('click', function () {
            $(this).parent().parent().remove();
        });
    }

    function bindTableRows() {
        $('.entries-inner-table tr:even').addClass("even");
        $('.entries-inner-table tr:odd').addClass("odd");

    }


    function init() {
        bindBasicActions();
        bindTableRows();
    }

    function toggleCriteriaFields(selectedobject) {
        
        $(selectedobject).parent().parent().parent().find("#criteriaField").each(function () {

            var label = $(this).text;
            var selectField = "criteria[" + label + "]";

            $(selectedobject).parent().parent().parent().find("#tdCriteriaFields").each(function () {
                $(this).find('input, select').each(function () {
                    if ($(this).attr("id") == selectField) {
                        $(this).show();
                    } else {
                        $(this).hide();
                    }

                });
            });
        });
    }

    return {
        init: init
    }
} ();