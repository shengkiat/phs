function displayStandardReferenceResult(value, fieldId) {
    if(value != "" && value != null) {
        var standardReferenceID = document.getElementById("SubmitFields["+fieldId+"].StandardReferenceId").value

        if (standardReferenceID != "")
        {
            var jsonObject = {
                "standardReferenceId": standardReferenceID,
                "value": value
            };

            $.ajax({
                url: '/phs/FormAccess/GetReferenceRange',
                type: "POST",
                data: JSON.stringify(jsonObject),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                error: function (response) {
                    if (response.Error != undefined) {
                        document.getElementById("SubmitFields[" + fieldId + "].StandardReferenceResult").innerText = response.Error;
                    }
                },
                success: function (response) {
                    if (response.Status != undefined) {
                        document.getElementById("SubmitFields[" + fieldId + "].StandardReferenceResult").innerText = response.Status;
                        if (response.Highlight) {
                            document.getElementById("SubmitFields[" + fieldId + "].StandardReferenceResult").style.color = "red";
                        }
                    }
                }
            });
        }
    }
}