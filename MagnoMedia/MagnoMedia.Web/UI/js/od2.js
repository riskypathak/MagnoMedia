function SistemaOperativo() {
    if (navigator.userAgent.indexOf("NT 6.1") != -1) { var SO = "win" }
    else if (navigator.userAgent.indexOf("NT 6.2") != -1) { var SO = "win" }
    else if (navigator.userAgent.indexOf("NT 6.3") != -1) { var SO = "win" }
    else if (navigator.userAgent.indexOf("NT 6") != -1) { var SO = "win" }
    else if (navigator.userAgent.indexOf("Mac") != -1) {
        var SO = "mac";
        $('link').append('<link href="../default/media/css/mac.css" rel="stylesheet" type="text/css" />');
    }
    else { var SO = "win" }
    return SO;
}
var SO = SistemaOperativo();