$(function ($, undefined) {

    $("body").on("click", "#AdminPageHairButton", function () {
        ClearTable();
        ShowHairs();
    });

    $("body").on("click", "#AdminPageHairLengthButton", function () {
        ClearTable();
        ShowHairLength();
    });

    $("body").on("click", "#AdminPageHairColorButton", function () {
        ClearTable();
        ShowHairColor();
    });

    $("body").on("click", "#AdminPageUserButton", function () {
        ClearTable();
        ShowUsersCount();
    });

    $("body").on("click", "#AdminPageOrderButton", function () {
        ClearTable();
        ShowOrders();
    });
    ///////////////////////////////////
    $("body").on("click", "#AddHairLengthButton", function () {

        var length = parseInt($("#Length").val());
        if (Number.isInteger(length)) {
            var formData = new FormData();
            formData.append('Length', length);
            $.ajax({
                url: "/Hair/AddHairLenght",
                method: 'Post',
                data: formData,
                cache: false,
                dataType: 'json',
                processData: false,
                contentType: false,
                success: function (data) {
                    if (data == true) {
                        $('#modalwindow').modal('hide');
                        ClearModalWindow();
                        ClearTable();
                        ShowHairLength();
                    } else {
                        alert("Что-то пошло не так");
                    }
                }
            });
        }
    });

    $("body").on("click", "#AddHairColorButton", function () {

        var color = $("#Color").val();
        var formData = new FormData();
        formData.append('Color', color);
        $.ajax({
            url: "/Hair/AddHairColor",
            method: 'Post',
            data: formData,
            cache: false,
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function (data) {
                if (data == true) {
                    $('#modalwindow').modal('hide');
                    ClearModalWindow();
                    ClearTable();
                    ShowHairColor();
                } else {
                    alert("Что-то пошло не так");
                }
            }
        });
    });
    //////////////////////////////////////
    $("body").on("click", "#DeleteUsersButton", function () {
        var days = parseInt($("#DeleteUsersInput").val());
        $("#DeleteUsersInput").val("");
        if (Number.isInteger(days)) {
            $.ajax({
                url: "/api/User/" + days,
                method: 'DELETE',
                dataType: 'html',
                data: { text: days },
                success: function (data) {
                    ClearTable();
                    ShowUsersCount();
                    alert("success");
                }
            });
        } else { alert("Wrong value, please enter a number"); }
    });

    $("body").on("click", "#AddHairsButton", function () {
        ClearTable();
        $.get("/Hair/AddHair", function (data) {
            var table = $("#AdminPageTable");
            table.append(data);
        });
    });

    $("body").on("click", "#AddLengthButton", function () {
        $.get("/Hair/AddHairLenght", function (data) {
            ClearModalWindow();
            var modal = $("#modal-content");
            modal.append(data);
            $('#modalwindow').modal('show')
        });
    });

    $("body").on("click", "#AddColorButton", function () {
        $.get("/Hair/AddHairColor", function (data) {
            ClearModalWindow();
            var modal = $("#modal-content");
            modal.append(data);
            $('#modalwindow').modal('show')
        });
    });

    $("body").on("click", "#AddNewHairButton", function () {
        var imageInput = $("#AddHairImage").get(0);
        var files = imageInput.files;
        ReadFileAsDataUrlAndSendData(files[0]);
    });
});

function ReadFileAsDataUrlAndSendData(file) {
    var reader = new FileReader();
    if (file) {
        
        var name = $("#Name").val();
        var price = $("#Price").val();
        var type = $("#HairType").val();

        var model = new Object();
        model.Name = name;
        model.Price = price;
        model.HairType = type;
        model.HairLengths = new Array();
        model.HairColors = new Array();
        $("input:checkbox[name=HairLengths]:checked").each(function () {
            model.HairLengths.push(new Object({ Id: $(this).attr("id"), Length: $(this).val() }));
        });

        $("input:checkbox[name=HairColors]:checked").each(function () {
            model.HairColors.push(new Object({ Id: $(this).attr("id"), Color: $(this).val() }))
        });

        reader.readAsDataURL(file);
    } else {
        alert('Выберите изображение');
    }

    reader.onloadend = function () {      
        model.PostedImageData = reader.result;
        SendAfterReadFile(model);
    }
}

function SendAfterReadFile(model) {
    $.ajax({
            url: "/Hair/AddHair",
            type: "Post",        
            contentType: 'application/json',          
            data: JSON.stringify(model) ,
            processData: false,
            success: function (data) {
                if (data == true) {
                    alert("Успешно добавлено");
                    ClearTable();
                    ShowHairs();
                } else {
                    alert("Заполните все поля");
                }
            }
        });
}

function ClearTable() {
    $("#AdminPageTable > div").remove();
}

function ClearModalWindow() {
    $("#modal-content > div").remove();
}

function ClearOrdersTable() {
    $("#OrderTable > tbody").remove();
}


function ShowUsers() {
    $.get("/api/User", function (data) {
        var table = $("#AdminPageTable");
        table.append('<table id="table1" class="table table-hover"><tr><th scope="col">User id</th><th scope="col">Last Enter</th><th scope="col"><label for="DeleteUsersInput">Удалить всех кто не заходил N дней</label><input type="text" class="mr-5 ml-5" id="DeleteUsersInput" size="5"><button type="button" id="DeleteUsersButton" class="btn btn-danger">Delete</button></th></tr></table>');
        var cc = $("#table1");
        $.each(data, function (index, value) {
            cc.append('<tr><td>' + value.UserName + '</td><td>' + value.LastEnter);
        });
    });
}

function ShowUsersCount() {
    $.get("/api/User", function (data) {
        var table = $("#AdminPageTable");
        table.append('<div class="container-fluid">Колличество пользователей в базе: ' + data + '</div>');
        table.append('<div class="container-fluid"><label for="DeleteUsersInput">Удалить всех кто не заходил N дней</label><input type="text" class="mr-5 ml-5" id="DeleteUsersInput" size="5"><button type="button" id="DeleteUsersButton" class="btn btn-danger">Delete</button></div>');
    });
}

function ShowHairs() {
    $.get("/Hair/GetHairs", function (data) {
        var table = $("#AdminPageTable");
        table.append(data);
    });
}

function ShowHairLength() {
    $.get("/Hair/GetHairLength", function (data) {
        var table = $("#AdminPageTable");
        table.append(data);
    });
}

function ShowHairColor() {
    $.get("/Hair/GetHairColor", function (data) {
        var table = $("#AdminPageTable");
        table.append(data);
    });
}

function ShowOrders() {
    $.get("/Order/GetOrderViewMenu", function (data) {
        var table = $("#AdminPageTable");
        table.append(data);
        GetWaitingOrders();
    });
}

function DeleteHair(id) {
    $.ajax({
        url: "/Hair/DeleteHair/" + id,
        type: 'delete',
        cache: false,
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            ClearTable();
            ShowHairs();
            if (data == true) {
                alert("Удалено");
            } else {
                alert("Ошибка");
            }
        }
    });
}

function ViewOrder(orderId) {
    $.get("/Order/GetOrder/" + orderId, function (data) {
        ClearTable();
        var table = $("#AdminPageTable");
        table.append(data);
    });
}

function GetAllOrders() {
    //alert("all");
    ClearOrdersTable();
    $.get("/Order/GetAllOrders/", function (data) {

        var table = $("#OrderTable");
        table.append(data);
    });
}

function GetCompleteOrders() {
    //alert("comp");
    ClearOrdersTable();
    $.get("/Order/GetCompleteOrders/", function (data) {

        var table = $("#OrderTable");
        table.append(data);
    });
}

function GetWaitingOrders() {
    //alert("wait");
    ClearOrdersTable();
    $.get("/Order/GetWaitingOrders/", function (data) {

        var table = $("#OrderTable");
        table.append(data);
    });
}

function CompleteOrder(orderId, event) {
    alert("Complete+" + orderId);
    event.stopPropagation();
}

function DeleteHairLength(Id) {
    $.ajax({
        url: "/Hair/DeleteHairLength/" + Id,
        type: 'delete',
        cache: false,
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            ClearTable();
            ShowHairLength();
            if (data == true) {
                alert("Удалено");
            } else {
                alert("Ошибка");
            }
        }
    });
}

function DeleteHairColor(Id) {
    $.ajax({
        url: "/Hair/DeleteHairColor/" + Id,
        type: 'delete',
        cache: false,
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            ClearTable();
            ShowHairColor();
            if (data == true) {
                alert("Удалено");
            } else {
                alert("Ошибка");
            }
        }
    });
}