
$(function(){
    $(".jqueryui-marker-datepicker")
        .wrap('<div class="input-group">')
        .datepicker({
            dateFormat: "yy-mm-dd",
            changeYear: true,
            showOn: "button"
    });

    $("#sectionaform").validate({
        rules: {
            ciftype: "required",
            cnic: "required",
            name: "required",
            fathername: "required",
            dateofbirth: "required",
            message: "required"

        },
        messages: {
            ciftype: "Please specify type",
            cnic: "Please provide CNIC",
            name: "Please enter name",
            fathername: "Enter father name",
            dateofbirth: "Enter date of birth",
            message: "Enter message"
        }


    });
});