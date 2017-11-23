function ajaxcall(url, value, method, callback) {
    $.ajax({
        url: constants.server + url, 
        type: method,
        data: value,
        cache: false,
        dataType: "json",
        crossOrigin: false,
        success: function (result) {
            callback(result);
        },
        false:function(result){
            callback(result);
        }
    });
}

function ajaxput(url, value, method, callback){
    $.ajax({
        url: constants.server + url, 
        type: "POST",
        data: value,
        contentType : "application/json",
        cache: false,
        dataType: "json",
        crossOrigin: false,
        headers: {
            "X-HTTP-Method-Override": method
        },
        success: function (result) {
            callback(result);
        },
        false:function(result){
            callback(result);
        }
    });
}

jQuery.support.cors = true;
crossOrigin: false;
