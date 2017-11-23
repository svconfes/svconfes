$(document).ready(function () {
    ajaxcall("api/category", {}, "get", loadCategory);
});

function loadCategory(data) {
    $(".tbl-category").empty();
    data.forEach(function (element) {
        $(".tbl-category").append(getRowCate(element));
    }, this);
}

function getRowCate(element) {
    console.log(element);
    return '<tr>'
        + '<td>' + element.Ord + '</td>'
        + '<td>' + element.Code + '</td>'
        + '<td>' + element.Name + '</td>'
        + `<td><a class="btn btn-danger" href="#" onclick="deleteCate('`+ element.Ord +`')"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></a></td>`
        + '</tr>';
}

function deleteCate(ord) {
    var value = { "Ord": ord };
    ajaxcall("api/category/5", value, "delete", showPopup);
    
}

function showPopup(bool){
    if(bool){
        location.reload();
    }else{
        $(".alert-danger").removeClass("hidden");
    }
}

function addCategory(){
    var cate = {};
    cate.Code = $("#cate-code").val();
    cate.Name = $("#cate-name").val();
    ajaxcall("api/category", cate, "post", location.reload());
}
