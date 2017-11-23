$(document).ready(function(){
    ajaxcall("api/order", {}, "get", loadOrder);
});

var user = {};

function loadOrder(data){
    $(".tbl-order").empty();
    user = JSON.parse(localStorage.getItem('user'));
    data.forEach(function(element) {
        if(user.RoleCode == 'ctm'){
            if(user.Code == element.CodeUser){
                $(".tbl-order").append(ctmRole(element));
            }
        }
        if(user.RoleCode == 'adm'){
            $(".tbl-order").append(adminRole(element));
        }
    }, this);
}

function adminRole(element){
    return (element.CodeStatus == 'working' ? `<tr class="warning">` : element.CodeStatus == 'done' ? `<tr class="success">` : `<tr class="danger">`)
        +`<td>`+ element.Ord +`</td>`
        +`<td>`+ element.Code +`</td>`
        +`<td>`+ element.CodeUser +`</td>`
        +`<td>`+ element.CodeStatus +`</td>`
        +`<td>`+ element.Price +`</td>`
        +`<td>`
        +`<a href="#"  data-toggle="modal" data-target="#myModal" data-order='`+ JSON.stringify(element) +`' onclick="loadCartTable(this)">`
            +`<span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>`
        +`</a>` 
        +`</td>`
        +`<td>`
        + (element.CodeStatus == 'working' ? 
            `<a href="#" data-order='`+ JSON.stringify(element) +`' onclick="donelOrder(this)">`
                +`<span class="glyphicon glyphicon-ok" aria-hidden="true"></span>`
            +`</a>` : '')
        +`</td>`
        +`<td>`
        + (element.CodeStatus == 'working' ? 
            `<a href="#" data-order='`+ JSON.stringify(element) +`' onclick="cancelOrder(this)">`
                +`<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>`
            +`</a>` : '')
        +`</td>`
        +`</tr>`;
}

function ctmRole(element){
    return (element.CodeStatus == 'working' ? `<tr class="warning">` : element.CodeStatus == 'done' ? `<tr class="success">` : `<tr class="danger">`)
        +`<td>`+ element.Ord +`</td>`
        +`<td>`+ element.Code +`</td>`
        +`<td>`+ element.CodeUser +`</td>`
        +`<td>`+ element.CodeStatus +`</td>`
        +`<td>`+ element.Price +`</td>`
        +`<td>`
        +`<a href="#"  data-toggle="modal" data-target="#myModal" data-order='`+ JSON.stringify(element) +`' onclick="loadCartTable(this)">`
            +`<span class="glyphicon glyphicon-eye-open" aria-hidden="true"></span>`
        +`</a>` 
        +`</td>`
        +`<td>`
        + (element.CodeStatus == 'working' ? 
            `<a href="#" data-order='`+ JSON.stringify(element) +`' onclick="cancelOrder(this)">`
                +`<span class="glyphicon glyphicon-remove" aria-hidden="true"></span>`
            +`</a>` : '')
        +`</td>`
        +`<td>`+ `` +`</td>`
        +`</tr>`;
}

function cancelOrder(element){
    var order = $(element).data("order");
    order.CodeStatus = 'cancelled'
    ajaxcall("api/order/5", order, "PUT", function(){});
    location.reload();
}

function donelOrder(element){
    var order = $(element).data("order");
    order.CodeStatus = 'done'
    ajaxcall("api/order/5", order, "PUT", function(){});
    location.reload();
}

var total = 0;
function loadCartTable(element){
    total = 0
    $(".tbl-cart-body").empty();
    var order = $(element).data("order");
    (order.Products).forEach(function(element) {
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
