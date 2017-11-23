$(document).ready(function () {
    loadCartTable();
});

var total = 0;

function loadCartTable(){
    var cart = JSON.parse(localStorage.getItem('cart'));
    $(".tbl-cart-body").empty();
    cart.forEach(function(element) {
        $(".tbl-cart-body").append(getRow(element));
    }, this);
    $(".tbl-cart-sum").text(total);
}

function getRow(element){
    total = total + element.Price * element.Quantity;
    return '<tr>'
            +'<td>'+ element.Code +'</td>'
            +'<td>'+ element.Name +'</td>'
            +'<td>'+ element.Price +'</td>'
            +'<td>'+ element.Quantity +'</td>'
            +'<td>'+ element.Price * element.Quantity +'</td>'
        +'</tr>'
}

function cancelCart(){
    localStorage.setItem("cart", JSON.stringify([]));
    redirect("product.html");
}

function addOrder(){
    var order = {};
    order.CodeUser = JSON.parse(localStorage.getItem('user')).Code;
    order.Products = JSON.parse(localStorage.getItem('cart'));
    ajaxcall('api/order', order, 'post', function(){});
    localStorage.setItem("cart", JSON.stringify([]));
    redirect("order.html");
}
