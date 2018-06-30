var Admin = function () {
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


    var Login = function () {
        var FrmLogin = $('#FrmLogin');
        if (!validateLogin(FrmLogin)) return false;
        var Email = $('#email').val();
        var Password = $('#password').val();
        if ((Email == 'admin@gmail.com') & (Password == '1234')) {
            window.location.href = '/Admin/Dashboard';
        }
        else
        {
            messagebox.error('Username or Password incorrect...');
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

    var ProductGridActions = function () {

        var Id = $(this).attr('data-id1');
        var option = $(this).attr('data-option');
        if (option == 1) {
            window.location.href = AdminUrl.AddEditProduct + '?Id=' + Id;
        }
        if (option == 2) {
            messagebox.confirm('Are you sure you want to delete?', 'Confirmation', function () {
                $.get(AdminUrl.DeleteProduct + '?Id=' + Id, null, function (result) {
                    if (result.Status) {
                        messagebox.success("Product Successfully Deleted!");
                        window.location.href = AdminUrl.ManageProducts;
                    }

                });
            });
        }
    }

    var SaveProduct = function () {
        var FrmAEProducts = $('#FrmAEProducts');
        if (!validateProducts(FrmAEProducts)) return false;
        var url = AdminUrl.SaveProduct;
        var data = $('#FrmAEProducts').serialize();
        var ImageUrl = $('#FrmAEProducts').find('#imageurl').val();
        $.ajax({
            url: AdminUrl.SaveProduct,
            data: $('#FrmAEProducts').serialize(),
            type: 'POST',
            success: function (data) {
                if (data.Status) {
                    messagebox.success(data.Message, null, function () {
                        if(data.Id==0){
                            window.location.href = AdminUrl.Image;
                        }
                        else
                        {
                            window.location.href = AdminUrl.ManageProducts;
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

    var validateProducts = function (form) {
        var leadsValidator = new validator();

        var name = $('#FrmAEProducts').find('#name').val();
        if ((name == 0) | (name == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEProducts').find('#name')));
        }

        var sku = $('#FrmAEProducts').find('#sku').val();
        if ((sku == 0) | (sku == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEProducts').find('#sku')));
        }
        var stockavailability = $('#FrmAEProducts').find('#stockavailability').val();
        if ((stockavailability == 0) | (stockavailability == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEProducts').find('#stockavailability')));
        }
        var price = $('#FrmAEProducts').find('#price').val();
        if ((price == 0) | (price == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEProducts').find('#price')));
        }
        var shortdescription = $('#FrmAEProducts').find('#shortdescription').val();
        if ((shortdescription == 0) | (shortdescription == "")) {
            leadsValidator.isValid = false;
            leadsValidator.pushError(($('#FrmAEProducts').find('#shortdescription')));
        }

        leadsValidator.isOnlyNumericAndDot($('#FrmAEProducts').find('#price'));

        if (!leadsValidator.isValid) {
            messagebox.error(leadsValidator.getErrors());
            return false;
        }
        return true;
    }


    return {

        Login: Login,
        validateLogin: validateLogin,
        ProductGridActions: ProductGridActions,
        SaveProduct: SaveProduct,
        validateProducts: validateProducts

    };

}();


$(document).ready(function () {

    $('#login').unbind('click').bind('click', Admin.Login);
    $("#product-grid tbody tr a").unbind('click').bind('click', Admin.ProductGridActions)
               .hover(function () {
                   $(this).css({ cursor: 'pointer' });
               }, function () {
                   $(this).css({ cursor: 'default' });
               });
    $('#saveproduct').unbind('click').bind('click', Admin.SaveProduct);

});




