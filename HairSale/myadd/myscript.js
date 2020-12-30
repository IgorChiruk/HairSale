$(function ($, undefined) {

    $("body").on("click", "#AdminPageHairButton", function () {
        ClearTable();
        ShowHairs();
    });

    $("body").on("click", "#AdminPageUserButton", function () {
        ClearTable();
        ShowUsersCount();
    });

    $("body").on("click", "#AdminPageOrderButton", function () {
        ClearTable();      
    });

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
        var table = $("#AdminPageTable");
        table.append('<div class="container-fluid mt-5" id="EditUserMenu"></div >');
        var EditUserMenu = $("#EditUserMenu");
        EditUserMenu.append('<p><label for= "AddHairName" >Название : </label><input type="text" class= "mr-5 ml-5" id = "AddHairName" size = "5" ></p>');
        EditUserMenu.append('<p><label for= "AddHairPrice" >Название : </label><input type="text" class= "mr-5 ml-5" id = "AddHairPrice" size = "5" ></p>');
        EditUserMenu.append('<p><label for= "AddHairImage" >Название : </label><input type="file" class= "mr-5 ml-5" id = "AddHairImage"></p>');
        EditUserMenu.append('<p><button type="button" id="AddNewHairButton" class="btn btn-success">AddNew</button</p>');
    });

    $("body").on("click", "#AddNewHairButton", function () {
        var imageInput = $("#AddHairImage").get(0);
        var files = imageInput.files;
        var name = $("#AddHairName").val();
        var price = $("#AddHairPrice").val();

        var formData = new FormData();
        formData.append('name', name);
        formData.append('price', price);
        formData.append('postedFile', files[0]);

        $.ajax({
            url: "/Hair/AddHair",   
            method: 'Post',
            data: formData,
            cache: false,
            dataType: 'json',
            processData: false,
            contentType: false,
            success: function (data) {
                if (data == true) {
                    alert("Успешно добавлено");
                    ClearTable();
                    ShowHairs();
                } else {
                    alert("Ошибка");
                }
            }
        });
    });
});

function ClearTable() {
    $("#AdminPageTable > div").remove();
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

function DeleteHair(id) {
    $.ajax({
        url: "/Hair/DeleteHair/"+id,
        type: 'delete',       
        cache: false,
        dataType: 'json',
        processData: false,
        contentType: false,
        success: function (data) {
            ClearTable();
            ShowHairs();
            if (data==true) {
                alert("Удалено");
            } else {
                alert("Ошибка");
            }
        }
    });  
}