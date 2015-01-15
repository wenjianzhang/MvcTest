$(document).ready(function () {

    $("#form-wizards").formwizard({
        //formPluginEnabled: true,
        //validationEnabled: true,
        //focusFirstInput: true,
        //disableUIStyles: true,

        //formOptions: {
        //    success: function (data) { $("#status").fadeTo(500, 1, function () { $(this).html("<span>Form was submitted!</span>").fadeTo(5000, 0); }) },
        //    beforeSubmit: function (data) { $("#submitted").html("<span>Form was submitted with ajax. Data sent to the server: " + $.param(data) + "</span>"); },
        //    dataType: 'json',
        //    resetForm: true
        //},
        validationOptions: {
            rules: {
                password: "required",
                password2: {
                    equalTo: "#password"
                }
            },
            messages: {
                password: "你必须填写一个密码",
                password2: { equalTo: "两次输入不一致" }
            },
            errorClass: "help-inline",
            errorElement: "span",
            highlight: function (element, errorClass, validClass) {
                $(element).parents('.control-group').addClass('error');
            },
            unhighlight: function (element, errorClass, validClass) {
                $(element).parents('.control-group').removeClass('error');
            }
        }
    });
});