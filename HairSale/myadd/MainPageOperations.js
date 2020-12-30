$(function ($, undefined) {

    UpdateBasket();
});

function UpdateBasket() {
    ClearBasket();
    $.get("/Basket/GetBasket", function (data) {
        if (data) {
            $('#cart > ul').append(data);

            $.get("/Basket/GetBasketTotalCountAndPrice", function (data) {
                $('#cart-total > span.count-items').text(data.count);
                $('#cart-total > span.cost-items').text(data.price);
                $('#BasketTotal').text(data.price);
            });
        }
        else {
            $.get("/Login/RefreshCookie", function (data) {
                UpdateBasket();
            });
        }

    });
}

function AddItemToBasket(id, quality) {
    $.ajax({
        url: "/Basket/AddBasketItem",
        method: 'Post',
        data: { itemId: id },
        dataType: 'json',
        success: function (data) {
            if (data) {
                UpdateBasket();
            };
        }
    });

}

function RemoveItemFromBasket(basketId, itemId) {
    $.ajax({
        url: "/Basket/DeleteBasketItem",
        method: 'delete',
        data: { basketID: basketId, itemId: itemId },
        dataType: 'json',
        success: function (data) {
            if (data) {
                UpdateBasket();
            };
        }
    });

}

function ClearBasket() {
    $("#cart >ul> li").remove();
}