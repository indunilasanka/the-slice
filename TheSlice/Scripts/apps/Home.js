var Home = function () {
    var preStatus;
    var JSDateTimeFormat, DateTimeFormat, shortDateFormat, maskbydate, maskbydatetime;
    var SetDates = function (datetimedetails) {


        JSDateTimeFormat = datetimedetails.JSDateTimeFormat;
        shortDateFormat = datetimedetails.shortDateFormat;
        maskbydatetime = datetimedetails.maskbydatetime;
        maskbydate = datetimedetails.maskbydate;
        DateTimeFormat = datetimedetails.DateTimeFormat;
        DateFormat = datetimedetails.DateFormat;
        JsTimeFormat = datetimedetails.JsTimeFormat;
    }
    

    var SearchUsers = function () {

        $.ajax({
            type: 'POST',
            url: '/Admin/SearchUsers',
            data: $("#frmSearchUsers").serialize(),
            success: function (response) {
                $('#user-grid').html(response);
                $("#user-grid tbody tr a").unbind('click').bind('click', user.UserGridActions)
                .hover(function () {
                    $(this).css({ cursor: 'pointer' });
                }, function () {
                    $(this).css({ cursor: 'default' });
                });
            },
        })
    }

    var Login = function () {
        var FrmLogin = $('#FrmLogin');
        if (!validateLogin(FrmLogin)) return false;
        var url = '/Home/LoginSys';
        var Email = $('#email').val();
        var FormId = $('#FormId').val();
        var Password = $('#password').val();
        if ((Email == "admin@gmail.com") & (Password == "1234"))
        {
            window.location.href = '/Admin/Dashboard';
        }
        else {
            $.ajax({
                url: url,
                data: { "Email": Email, "Password": Password },
                type: 'POST',
                success: function (data) {
                    if (data.Status) {
                        messagebox.success('Successful!', null, function () {
                            if (FormId == 1) {
                                window.location.href = '/Home/Index';
                            }
                            else {
                                window.location.href = '/Shop/Index';
                            }
                        });
                    }
                    else {
                        messagebox.error(data.Message);
                    }
                },
                error: function (error) {
                    messagebox.error("Unsuccessful!", null);
                }
            });
        }
    }

    var validateLogin = function (form) {
        var leadsValidator = new validator();

        var Email = $('#FrmLogin').find('#email').val();
        if ((Email == "") | (Email == 0)) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmLogin').find('#email')));
        }
        var Password = $('#FrmLogin').find('#password').val();
        if ((Password == 0) | (Password == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmLogin').find('#password')));
        }

        leadsValidator.isEmail($('#FrmLogin').find('#email'));

        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

  
    var Register = function () {
        var FrmReg = $('#FrmReg');
        if (!validateRegister(FrmReg)) return false;
        var url = '/Home/RegisterSys';
        var FormIdReg = $('#FormIdReg').val();
        $.ajax({
            url: url,
            data: $('#FrmReg').serialize(),
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success('Successful!', null, function () {
                        if (FormIdReg == 1) {
                            window.location.href = '/Home/Login?FormId='+FormIdReg;
                        }
                        else {
                            window.location.href = '/Home/Login?FormId=' + FormIdReg;
                        }
                    });
                }
                else {
                    messagebox.error(data.Message);
                }
            },
            error: function (error) {
                messagebox.error("Unsuccessful!", null);
            }
        });
    }

    var validateRegister = function (form) {
        var leadsValidator = new validator();

        var Email = $('#FrmReg').find('#Email').val();
        if ((Email == "") | (Email == 0)) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmReg').find('#Email')));
        }
        var Password = $('#FrmReg').find('#Password').val();
        if ((Password == 0) | (Password == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmReg').find('#Password')));
        }

        var Address = $('#FrmReg').find('#Address').val();
        if ((Address == 0) | (Address == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmReg').find('#Address')));
        }

        var FullName = $('#FrmReg').find('#FullName').val();
        if ((FullName == 0) | (FullName == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmReg').find('#FullName')));
        }

        var ConPassword = $('#FrmReg').find('#ConPassword').val();
        if ((ConPassword != Password)) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmReg').find('#ConPassword')));
        }

        leadsValidator.isEmail($('#FrmReg').find('#Email'));

        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }

    return {
        
        Login: Login,
        validateLogin: validateLogin,
        validateRegister: validateRegister,
        Register:Register

    };

}();


$(document).ready(function () {
    
    $('#login').unbind('click').bind('click', Home.Login);
    $('#btnreg').unbind('click').bind('click', Home.Register);

});




