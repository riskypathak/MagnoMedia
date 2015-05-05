
//deteccion del navegador
var BrowserDetect = {
    init: function () {
        this.browser = this.searchString(this.dataBrowser, false) || "An unknown browser";
        this.bwshort = this.searchString(this.dataBrowser, true) || "NA";
        this.version = this.searchVersion(navigator.userAgent) || this.searchVersion(navigator.appVersion) || "an unknown version";
        this.OS = this.searchString(this.dataOS) || "an unknown OS";
    },
    searchString: function (data, retShort) {
        for (var i = 0; i < data.length; i++) {
            var dataString = data[i].string;
            var dataProp = data[i].prop;
            this.versionSearchString = data[i].versionSearch || data[i].identity;
            if (dataString) {
                if (dataString.indexOf(data[i].subString) != -1)
                    return retShort ? data[i]._short : data[i].identity;
            } else if (dataProp)
                return retShort ? data[i]._short : data[i].identity;
        }
    },
    searchVersion: function (dataString) {
        var index = dataString.indexOf(this.versionSearchString);
        var ver = "";
        if ((this.bwshort == 'ie') && (index == -1)) {
            index = dataString.indexOf('rv'); // fix for IE 11
            ver = dataString.substring(index + 3);
        } else {
            ver = dataString.substring(index + this.versionSearchString.length + 1);
        }
        if (index == -1) return;
        return parseFloat(ver);
    },
    displayBwDiv: function (divID) {
        var dvBrid = false;

        if ($(divID + '_' + this.bwshort + this.version).length > 0) dvBrid = divID + '_' + this.bwshort + this.version;
        else if ($(divID + '_' + this.bwshort).length > 0) dvBrid = divID + '_' + this.bwshort;
        else if ($(divID).length > 0) dvBrid = divID;

        if (dvBrid)
            $(dvBrid).show();
    },
    dataBrowser: [
	{
	    string: navigator.userAgent,
	    subString: "OmniWeb",
	    versionSearch: "OmniWeb/",
	    identity: "OmniWeb",
	    _short: "ow"
	},
	{
	    string: navigator.vendor,
	    subString: "Apple",
	    identity: "Safari",
	    versionSearch: "Version",
	    _short: "sf"
	},
	{
	    string: navigator.vendor,
	    subString: "Opera",
	    identity: "Opera",
	    versionSearch: "OPR",
	    _short: "op"
	},
	{
	    string: navigator.vendor,
	    subString: "iCab",
	    identity: "iCab",
	    _short: "ic"
	},
	{
	    string: navigator.vendor,
	    subString: "KDE",
	    identity: "Konqueror",
	    _short: "ko"
	},
	{
	    string: navigator.userAgent,
	    subString: "Firefox",
	    identity: "Firefox",
	    _short: "ff"
	},
	{
	    string: navigator.userAgent,
	    subString: "Maxthon",
	    identity: "Maxthon",
	    versionSearch: "Maxthon",
	    _short: "mx"
	},
	{
	    string: navigator.userAgent,
	    subString: "Chrome",
	    identity: "Chrome",
	    versionSearch: "Chrome",
	    _short: "cr"
	},
	{
	    string: navigator.vendor,
	    subString: "Camino",
	    identity: "Camino",
	    _short: "cm"
	},
	{   // for newer Netscapes (6+)
	    string: navigator.userAgent,
	    subString: "Netscape",
	    identity: "Netscape",
	    _short: "nt"
	},
	{
	    string: navigator.userAgent,
	    subString: "MSIE",
	    identity: "Explorer",
	    versionSearch: "MSIE",
	    _short: "ie"
	},
	{
	    string: navigator.userAgent,
	    subString: ".NET4.",
	    identity: "Explorer",
	    versionSearch: "MSIE",
	    _short: "ie"
	},
	{
	    string: navigator.userAgent,
	    subString: "Gecko",
	    identity: "Mozilla",
	    versionSearch: "rv",
	    _short: "mo"
	},
	{ // for older Netscapes (4-)
	    string: navigator.userAgent,
	    subString: "Mozilla",
	    identity: "Netscape",
	    versionSearch: "Mozilla",
	    _short: "nt"
	}
    ],


    dataOS: [
	{
	    string: navigator.platform,
	    subString: "Win",
	    identity: "Windows"
	},
	{
	    string: navigator.platform,
	    subString: "Mac",
	    identity: "Mac"
	},
	{
	    string: navigator.platform,
	    subString: "Linux",
	    identity: "Linux"
	}
    ]

};
BrowserDetect.init();

