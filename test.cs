$(document).ready(function () {
    $("#btn-sign-up").click(function(){
        var user = {};
        user.Name = $("#name").val();
        user.Phone = $("#phone").val();
        user.Password = $("#pass").val();
        ajaxcall("api/user", user, "post", checkSignup)
    });
});

function checkSignup(data){
    if(data.Status){
        $(".alert-success").removeClass("hidden");
        $(".alert-danger").attr('class', 'alert alert-danger hidden');
    }else{
        $(".alert-danger").removeClass("hidden");
        $(".alert-success").attr('class', 'alert alert-success hidden');
    }
}
