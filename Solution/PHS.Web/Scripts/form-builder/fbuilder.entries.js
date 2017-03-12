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
            $("#sortTable").append('<tr><td width="30%"></td><td width="20%"><select id="test"><option value="Select a value"></option><option value="ASC">ASC</option><option value="DESC">DESC</option></select></td><td width="50%"><a id="removeButton" name="removeButton" class="hyperlink-button light-blue-button">-</a></td></tr>');
        });

        $('#removeButton').live('click', function () {
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

    return {
        init: init
    }
} ();