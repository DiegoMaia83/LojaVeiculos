
// Não permite inserir dados não numéricos no campo
function toNumber(string) {
    var numsStr = string.replace(/[^0-9]/g, '');
    return numsStr;
}

// Alertas personalizados de DIV
function callAlertSuccess(div, msg, callback) {
    $(div).removeClass();
    $(div).html(msg).addClass("alert alert-success small").show("normal");
    setTimeout(function () {        
        callback();
    }, 3000);
}

function callAlertDanger(div, msg, callback) {
    $(div).removeClass();
    $(div).html(msg).addClass("alert alert-danger small").show("normal");
    setTimeout(function () {        
        callback();
    }, 3000);
}

function callAlertWarning(div, msg, callback) {
    $(div).removeClass();
    $(div).html(msg).addClass("alert alert-warning small").show("normal");
    setTimeout(function () {        
        callback();
    }, 3000);
}

function callAlertNeutral(div, msg, callback) {
    $(div).removeClass();
    $(div).html(msg).addClass("alert alert-secondary small").show("normal");
    setTimeout(function () {
        callback();
    }, 3000);
}



var loading = function (color) {
    var html = ""
    html += "<div class='spinner-border text-" + color + "' role='status'>";
    html += "</div>";
    return html
}