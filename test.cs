$(document).ready(function () {
    $("#btn-login").click(function () {
        ajaxcall('api/user', {}, 'get', checklogin);
    });
    if (localStorage.getItem('user') != null) {
        if (JSON.parse(localStorage.getItem('user')).RoleCode == 'adm') {
            redirect("order.html")
        }
        if (JSON.parse(localStorage.getItem('user')).RoleCode == 'ctm') {
            redirect("product.html")
        }
    }
});

function checklogin(data) {
    data.forEach(function (element) {
        var user = $("#user").val();
        var pass = $("#pass").val();
        if ((user == element.Code || user == element.Phone) && pass == element.Password) {
            localStorage.setItem("user", JSON.stringify(element));
            if(element.RoleCode == 'ctm'){
                redirect("product.html")
            }else{
                redirect("order.html")
            }
        }
    }, this);
    $(".alert-danger").removeClass("hidden");
}
