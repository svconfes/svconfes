$(document).ready(function () {
    ajaxcall('api/category', {}, 'get', loadCategory);
    ajaxcall('api/product', {}, 'get', loadProduct);
});

function loadCategory(data) {
    if (JSON.parse(localStorage.getItem('user')).RoleCode == 'adm') {
        $('.add-new-product').append("<button class='btn btn-primary' onclick='addNewProduct()' data-toggle='modal' data-target='#myModal'><span class='glyphicon glyphicon-plus' aria-hidden='true'></span> Add New Product</button>");
    }

    $(".list-category").empty();
    data.forEach(function (element) {
        $(".list-category").append('<a href="#" class="list-group-item" data-code="' + element.Code + '" onclick="loadProductBycodeCate(this)">' + element.Name + '</a>');
    }, this);
}

var code = "";

function loadProductBycodeCate(atribute) {
    code = atribute.getAttribute("data-code");
    ajaxcall('api/product', {}, 'get', loadProduct);
}

function loadProduct(data) {
    $(".content").empty();
    if (code == "") {
        localStorage.setItem("products", JSON.stringify(data));
        data.forEach(function (element) {
            $(".content").append(card(element));
        }, this);
        loadCarousel(data)
    } else {
        data.forEach(function (element) {
            if (element.CodeCategory == code) {
                $(".content").append(card(element));
            }
        }, this);
    }
}


function loadCarousel(data) {
    $(".carousel").empty();
    data.forEach(function (element) {
        console.log(element.IsAds);
        if (element.IsAds) {
            $(".carousel").append('<div><img src="' + element.UrlImage + '" alt="Smiley face" height="350px" width="100%"></div>');
        }
    });
    $('.carousel').slick({
        dots: true,
        infinite: true,
        speed: 1000,
        autoplay: true,
        autoplaySpeed: 3000,
    });
}



function card(element) {
    return '<div class="card  col-lg-4">'
        + '<div class="h-100">'
        + '<a href="#">'
        + '<img class="card-img-top" src="' + element.UrlImage + '" alt="">'
        + '</a>'
        + '<div class="card-body">'
        + '<h4 class="card-title">'
        + '<a href="#">' + element.Name + '</a>'
        + '</h4>'
        + '<h5>' + element.Price + '</h5>'
        + '<p class="card-text">' + element.Detail + '</p>'
        + '</div>'
        + '<div class="card-footer text-right">'
        + (JSON.parse(localStorage.getItem('user')).RoleCode == 'adm' ? `<button class="btn btn-success" data-product='` + JSON.stringify(element) + `' onclick="edit(this)" data-toggle="modal" data-target="#myModal"><span class="glyphicon glyphicon-pencil" aria-hidden="true"></span></button> - <button class="btn btn-danger" onclick="deleteProduct('` + element.Code + `')"><span class="glyphicon glyphicon-remove" aria-hidden="true"></span></button> - ` : '')
        + '<button class="btn btn-primary" data-code-product="' + element.Code + '" onclick="pick(this)">'
        + '<span class="glyphicon glyphicon-shopping-cart" aria-hidden="true"></span>'
        + '</button>'
        + '</div>'
        + '</div>'
        + '</div>';
}

function pick(product) {
    var code = $(product).attr("data-code-product");
    var products = JSON.parse(localStorage.getItem('products'));
    var cart = JSON.parse(localStorage.getItem('cart'));
    if (cart == null) {
        cart = [];
    }
    var boo = true;
    var element = {};
    products.forEach(function (element1) {
        if (element1.Code == code) {
            cart.forEach(function (element2) {
                if (element2.Code == code) {
                    element2.Quantity = element2.Quantity + 1;
                    boo = false;
                }
            }, this);
        }
        if (element1.Code == code) {
            element = element1;
            element.Quantity = 1;
        }
    }, this);
    if (boo) {
        cart.push(element);
    }
    localStorage.setItem("cart", JSON.stringify(cart));
    redirect("cart.html");
}

function edit(element) {
    $("#btnEdit").removeClass("hidden");
    $("#btnAddNew").attr('class', 'btn btn-success hidden');
    var product = JSON.parse($(element).attr("data-product"));
    $("#product-codeCategory").prop('readonly', true);
    $("#product-code").val(product.Code);
    $("#product-codeCategory").val(product.CodeCategory);
    $("#product-name").val(product.Name);
    $("#product-quantity").val(product.Quantity);
    $("#product-price").val(product.Price);
    $("#product-detail").val(product.Detail);
    $("#product-urlImage").val(product.UrlImage);
    $("#product-isAds").prop('checked', product.IsAds);
    console.log(product);
}

function doUpdateProduct() {
    var product = {};
    product.Code = $("#product-code").val();
    product.CodeCategory = $("#product-codeCategory").val();
    product.Name = $("#product-name").val();
    product.Quantity = $("#product-quantity").val();
    product.Price = $("#product-price").val();
    product.Detail = $("#product-detail").val();
    product.UrlImage = $("#product-urlImage").val();
    product.IsAds = $("#product-isAds").is(":checked");
    ajaxcall('api/product/5', product, 'PUT', location.reload());
}

function addNewProduct() {
    $("#btnAddNew").removeClass("hidden");
    $("#btnEdit").attr('class', 'btn btn-success hidden');
    $("#product-code").val("");
    $("#product-codeCategory").prop('readonly', false);
    $("#product-codeCategory").val("");
    $("#product-name").val("");
    $("#product-quantity").val("");
    $("#product-price").val("");
    $("#product-detail").val("");
    $("#product-urlImage").val("");
    $("#product-isAds").prop('checked', false);
}

function doAddNewProduct() {
    var product = {};
    product.Code = $("#product-code").val();
    product.CodeCategory = $("#product-codeCategory").val();
    product.Name = $("#product-name").val();
    product.Quantity = $("#product-quantity").val();
    product.Price = $("#product-price").val();
    product.Detail = $("#product-detail").val();
    product.UrlImage = $("#product-urlImage").val();
    product.IsAds = $("#product-isAds").is(":checked");
    ajaxcall('api/product', product, 'POST', location.reload());
}

function deleteProduct(code) {

    ajaxcall('api/product/5', { "Code": code }, 'DELETE', location.reload());
}
