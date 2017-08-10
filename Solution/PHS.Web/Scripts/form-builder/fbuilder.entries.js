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

            var templateId = document.getElementById('templateId').value

            var data = {
                "templateId": templateId
            };

            $.ajax({
                type: "GET",
                data: data,
                url: '/phs/FormExport/AddNewSortEntries',
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
            var templateId = document.getElementById('templateId').value

            var data = {
                "templateId": templateId
                };

            $.ajax({
                type: "GET",
                data: data,
                url: '/phs/FormExport/AddNewCriteriaEntries',
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

            var criteriaindex = $(selectedobject).parent().parent().parent().find("input[name='CriteriaFields.index']").val();

            var criteriafieldindex = "CriteriaFields[" + criteriaindex + "].";

            var templateId = document.getElementById('templateId').value

            var data = {
                "templateId": templateId
            };

            $.ajax({
                type: "GET",
                data: data,
                url: '/phs/FormExport/AddNewCriteriaSubEntries',
                error: function (response) {
                    //alert(response);
                },
                success: function (response) {
                    $(selectedobject).parent().parent().find("#criteriaSubTable").each(function () {
                        $(this).append("<div>" + response + " </div>");
                        toggleCriteriaFields(selectedobject);
                        amendCriteriaSubFields(selectedobject, criteriafieldindex);
                    })
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

    function amendCriteriaSubFields(selectedobject, index) {

        $(selectedobject).parent().parent().find("#criteriaSubTable").each(function () {

            $(this).find('input, select').each(function () {
                if ($(this).attr("name").startsWith("CriteriaSubFields")) {
                    var newindex = index + $(this).attr("name");
                    $(this).attr('name', newindex);
                }
            });
        });
    }

    return {
        init: init
    }
} ();