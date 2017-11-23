$(document).ready(function () {
    ajaxcall("api/user", {}, "get", loadUser);
});

function loadUser(data) {
    $(".tbl-user").empty();
    data.forEach(function (element) {
        $(".tbl-user").append(getRowUser(element));
    }, this);
}

function getRowUser(element) {
    console.log(element);
    return '<tr>'
        + '<td>' + element.Ord + '</td>'
        + '<td>' + element.Code + '</td>'
        + '<td>' + element.Name + '</td>'
        + '<td>' + element.Password + '</td>'
        + '<td>' + element.RoleCode + '</td>'
        + '<td>' + element.Phone + '</td>'
        + `<td><a class="btn btn-primary" data-toggle="modal" data-target="#myModal" onclick="editUser(this)" data-element='` + JSON.stringify(element) + `'><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></></td>`
        + `<td><a class="btn btn-danger" href="#" onclick="deleteUser('` + element.Code + `')"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></a></td>`
        + '</tr>';
}

function deleteUser(code) {
    var value = { "Code": code };
    ajaxcall("api/user/5", value, "delete", showPopup);
}

function showPopup(bool) {
    if (bool) {
        location.reload();
    } else {
        $(".alert-danger").removeClass("hidden");
    }
}

function adduser() {
    var user = {};
    user.Code = $("#User-code").val();
    user.Name = $("#User-name").val();
    ajaxcall("api/user", user, "post", location.reload());
}

function editUser(element){
    element = JSON.parse($(element).attr('data-element'));
    $("#user-code").val(element.Code);
    $("#user-role").val(element.RoleCode);
    $("#user-name").val(element.Name);
    $("#user-password").val(element.Password);
    $("#user-phone").val(element.Phone);
    console.log(element);
}

function doUpdateUser(){
    var user = {};
    user.Code = $("#user-code").val();
    user.RoleCode = $("#user-role").val();
    user.Name = $("#user-name").val();
    user.Password = $("#user-password").val();
    user.Phone = $("#user-phone").val();
    ajaxcall("api/user/5", user, "put", location.reload());
}
