/*
** nopCommerce ajax cart implementation
*/


var AjaxCart = {
    loadWaiting: false,
    usepopupnotifications: false,
    topcartselector: '',
    topwishlistselector: '',
    flyoutcartselector: '',

    init: function (usepopupnotifications, topcartselector, topwishlistselector, flyoutcartselector) {
        this.loadWaiting = false;
        this.usepopupnotifications = usepopupnotifications;
        this.topcartselector = topcartselector;
        this.topwishlistselector = topwishlistselector;
        this.flyoutcartselector = flyoutcartselector;
    },

    setLoadWaiting: function (display) {
        displayAjaxLoading(display);
        this.loadWaiting = display;
    },

    //add a product to the cart/wishlist from the catalog pages
    addproducttocart_catalog: function (urladd) {
        if (this.loadWaiting != false) {
            return;
        }
        this.setLoadWaiting(true);

        $.ajax({
            cache: false,
            url: urladd,
            type: 'post',
            success: this.success_process,
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    },

    //add a product to the cart/wishlist from the product details page
    addproducttocart_details: function (urladd, productId, shoppingCartTypeId, formselector) {
        if (this.loadWaiting != false) {
            return;
        }
        this.setLoadWaiting(true);

        var qty = $("#addtocart_EnteredQuantity_" + productId).val();

        $.ajax({
            cache: false,
            url: urladd,
            data: { "productId": productId, "shoppingCartTypeId": shoppingCartTypeId, "qty": qty },
            type: 'post',
            success: function (data) {
                if (data.Status) {
                    messagebox.success(data.Message, null, function () {
                            window.location.href = '/Shop/Index';
                    });
                }
                else {
                    messagebox.confirm('You need to log-in to the system to continue?', 'Confirmation', function (){
                                window.location.href = '/Home/Login?FormId=2';
                    });
                }
            },
            complete: this.resetLoadWaiting,
            error: function (error) {
                messagebox.error("Unsuccessful!", null);
            }
        });
    },


    DeleteWish: function (urladd, productId) {
        messagebox.confirm('Are you sure, you want to remove this item from wishlist?', 'Confirmation', function () {
            $.ajax({
                cache: false,
                url: urladd,
                data: { "productId": productId },
                type: 'post',
                success: function (data) {
                    if (data.Status) {
                        messagebox.success("Sucessfully removed the item from wishlist", null, function () {
                            window.location.href = '/WishList/Index';
                        });
                    }
                    else {

                    }
                },

                error: function (error) {
                    messagebox.error("Unsuccessful!", null);
                }
            });
        });
    },

    DeleteCart: function (urladd, productId) {
        messagebox.confirm('Are you sure, you want to remove this item from shopping cart?', 'Confirmation', function () {
            $.ajax({
                cache: false,
                url: urladd,
                data: { "productId": productId },
                type: 'post',
                success: function (data) {
                    if (data.Status) {
                        messagebox.success("Sucessfully removed the item from shooping cart", null, function () {
                            window.location.href = '/ShoppingCart/Index';
                        });
                    }
                    else {

                    }
                },

                error: function (error) {
                    messagebox.error("Unsuccessful!", null);
                }
            });
        });
    },


    Checkout: function (urladd) {
        messagebox.confirm('Please confirm to checkout the shopping cart...', 'Confirmation', function () {
            $.ajax({
                cache: false,
                url: urladd,
                type: 'post',
                success: function (data) {
                    if (data.Status) {
                        messagebox.success("Sucessfully added your order. Our manager will contact you soon...", null, function () {
                            window.location.href = '/Shop/Index';
                        });
                    }
                    else {

                    }
                },

                error: function (error) {
                    messagebox.error("Unsuccessful!", null);
                }
            });
        });
    },


    //add a product to compare list
    addproducttocomparelist: function (urladd) {
        if (this.loadWaiting != false) {
            return;
        }
        this.setLoadWaiting(true);

        $.ajax({
            cache: false,
            url: urladd,
            type: 'post',
            success: this.success_process,
            complete: this.resetLoadWaiting,
            error: this.ajaxFailure
        });
    },

    success_process: function (response) {
        if (response.updatetopcartsectionhtml) {
            $(AjaxCart.topcartselector).html(response.updatetopcartsectionhtml);
        }
        if (response.updatetopwishlistsectionhtml) {
            $(AjaxCart.topwishlistselector).html(response.updatetopwishlistsectionhtml);
        }
        if (response.updateflyoutcartsectionhtml) {
            $(AjaxCart.flyoutcartselector).replaceWith(response.updateflyoutcartsectionhtml);
        }
        if (response.message) {
            //display notification
            if (response.success == true) {
                //success
                if (AjaxCart.usepopupnotifications == true) {
                    displayPopupNotification(response.message, 'success', true);
                }
                else {
                    //specify timeout for success messages
                    displayBarNotification(response.message, 'success', 3500);
                }
            }
            else {
                //error
                if (AjaxCart.usepopupnotifications == true) {
                    displayPopupNotification(response.message, 'error', true);
                }
                else {
                    //no timeout for errors
                    displayBarNotification(response.message, 'error', 0);
                }
            }
            return false;
        }
        if (response.redirect) {
            location.href = response.redirect;
            return true;
        }
        return false;
    },

    resetLoadWaiting: function () {
        AjaxCart.setLoadWaiting(false);
    },

    ajaxFailure: function () {
        alert('Failed to add the product. Please refresh the page and try one more time.');
    }
};