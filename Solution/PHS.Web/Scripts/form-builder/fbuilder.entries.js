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

            var formId = document.getElementById('formId').value

            var data = {
                "formId": formId
            };

            $.ajax({
                type: "GET",
                data: data,
                url: '/forms/AddNewSortEntries',
                error: function (response) {
                    //alert(response);
                },
                success: function (response) {
                    $('#sortTable').append("<div>" + response + "</div>");
                }
            })
            //$('#trSortRow0').clone().show().appendTo($('#trSortRow0').parent());
        });

        $('#removeSortButton').live('click', function () {
            $(this).parent().parent().remove();
        });

        $('#criteriaButton').live('click', function () {
            var formId = document.getElementById('formId').value

            var data = {
                "formId": formId
                };

            $.ajax({
                type: "GET",
                data: data,
                url: '/forms/AddNewCriteriaEntries',
                error: function (response) {
                    //alert(response);
                },
                success: function (response) {
                    $('#criteriaTable').append("<div>" + response + " <br style=\"clear:both\" /> </div>");
                }
            })
        
            //$('#trCriteriaRow0').clone().show().appendTo($('#trCriteriaRow0').parent());
        });

        $('#addCriteriaConditionButton').live('click', function () {

            var selectedobject = $(this);

            var formId = document.getElementById('formId').value

            var data = {
                "formId": formId
            };

            $.ajax({
                type: "GET",
                data: data,
                url: '/forms/AddNewCriteriaSubEntries',
                error: function (response) {
                    //alert(response);
                },
                success: function (response) {
                    $('#criteriaSubTable').append("<div>" + response + " </div>");
                    toggleCriteriaFields(selectedobject);
                }
            })

        });

        $('#removeCriteriaButton').live('click', function () {
            $(this).parent().parent().parent().remove();
            //$(this).parent().parent().parent().remove();
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

            var label = $(this).attr('value');
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