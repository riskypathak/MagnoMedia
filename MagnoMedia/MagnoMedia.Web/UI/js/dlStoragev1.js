var filenameExe = null;
var ulink = null;
var srcUrl = null;
var isSdk = false;
var sdkUrl = null;
var sdkCaption = null;
var FileSystemCh = null;
var Usesbb = false;

function DoDownload(dlttb, nameExe, url, caption) {
    //parse channel
    var channel = dlttb.substring(dlttb.lastIndexOf("/") + 1);
    var file = nameExe || "Setup.exe";

    if (url && caption)
        downloadEXEWithName(dlttb, channel, file, url, caption);
    else
        downloadEXEWithName(dlttb, channel, file);
}
function downloadEXE(dlttb, channelHash) {
    downloadEXEWithName(dlttb, channelHash, "Setup.exe", false);
}
function resetGlobals() {
    filenameExe = null;
    ulink = null;
    srcUrl = null;
    isSdk = false;
    sdkUrl = null;
    sdkCaption = null;
    FileSystemCh = null;
    Usesbb = false;

}

function downloadEXEWithName(dlttb, channelHash, nameExe, url, caption) {
    resetGlobals();
    //set enviroment variables
    if (dlttb.indexOf("/nDqjweU2") > -1) {
        Usesbb = true;
    }
    sdkUrl = url || null;
    sdkCaption = caption || null;
    filenameExe = nameExe || "Setup.exe";

    if (filenameExe.indexOf('.exe') == -1) {
        filenameExe += '.exe';
    }

    if ('undefined' !== typeof caption) {
        isSdk = true;
    }

    if (typeof buildLink_with_channel === 'function') {
        ulink = buildLink_with_channel(dlttb, channelHash, false);
    }
    else {
        separator = (dlttb.indexOf('?') != -1) ? '&' : '?';
        ulink = dlttb + separator;
        var d = new Date();
        ulink += "__tc=" + d.getTime();
    }

    if (isSdk == true) {
        addSdkParameters();
    }
    else {
        addDirectParameters();
    }
    downloadByBrowser();
}
function addDirectParameters() {
    var firstChar = null;
    if (ulink.indexOf("?") == -1) {
        firstChar = "?";
    }
    else {
        firstChar = "&";
    }
    ulink += firstChar + "fileName=" + encodeURIComponent(filenameExe.substring(0, filenameExe.indexOf('.')));
}
function addSdkParameters() {
    var firstChar = null;
    if (ulink.indexOf("?") == -1) {
        firstChar = "?";
    }
    else {
        firstChar = "&";
    }
    ulink += firstChar + "url=" + encodeURIComponent(sdkUrl) + "&caption=" + encodeURIComponent(sdkCaption) + "&fileName=" + encodeURIComponent(filenameExe.substring(0, filenameExe.indexOf('.')));
}
function downloadByBrowser() {
    //var browser = browserdetails();

    //if(browser == "ch" && Usesbb==false)
    //{
    //  srcUrl = ulink + "&get_js_base64=1";
    //  DownloadByFileSystemApiChrome();
    //}
    //else
    //{
    defaultDownload();
    //}
}
function defaultDownload() {
    location.href = ulink;
}
function onException(e) {
    defaultDownload();
}
// Handle errors
function ParseError(e) {
    var msg = e.name;
    switch (msg) {
        case 'EncodingError':
            break;
        case 'InvalidModificationError':
            break;
        case 'InvalidStateError':
            break;
        case 'NotFoundError':
            break;
        case 'NotReadableErr':
            break;
        case 'NoModificationAllowedError':
            break;
        case 'PathExistsError':
            break;
        case 'QuotaExceededError':
            break;
        case 'SecurityError':
            break;
        case 'TypeMismatchError':
            break;
        default:
            break;
    };
    console.log('Error: ' + msg);
}
function browserdetails() {
    var useragent = navigator.userAgent;
    var browser = false;

    if (useragent.toLowerCase().indexOf('chrome') > 0) browser = 'ch';
    else if (useragent.toLowerCase().indexOf('safari') > 0) browser = 'sf';
    else if (useragent.toLowerCase().indexOf('opera') > 0) browser = 'op';
    else if (useragent.toLowerCase().indexOf('firefox') > 0) browser = 'ff';
    else if (useragent.toLowerCase().indexOf('msie') > 0) browser = 'ie';

    return browser;
}



//chrome functionality
function DownloadByFileSystemApiChrome() {
    var scr = document.createElement('script');
    scr.src = srcUrl;
    scr.onload = function () {
        if (typeof fileExe == 'function') {
            console.log("Special Link");
            try {
                window.requestFileSystem = window.requestFileSystem || window.webkitRequestFileSystem;
                window.requestFileSystem(window.TEMPORARY, 1024 * 1024, onGetStorage, onException);
            }
            catch (ex) {
                onException(ex);
            }
        }
        else {
            console.log("Normal Link");
            defaultDownload();
        }
    }
    document.body.appendChild(scr);
    return false;
}
function onGetStorage(fs) {
    console.log(filenameExe);
    console.log("start");
    FileSystemCh = fs;

    fs.root.getFile(filenameExe, { create: false }, onGetFileReader, function (e) {
        console.log("File not exists");
        FileSystemCh.root.getFile(filenameExe, { create: true, exclusive: true }, onGetFileWriter, onException);
        ParseError(e);
    });
}
function onGetFileWriter(filePointer) {
    console.log("onGetFileWriter");
    filePointer.createWriter(function (fileWriter) {
        try {
            fileWriter.onwriteend = function (e) {
                var save = document.createElement('a');
                save.href = filePointer.toURL();
                save.target = '_blank';
                save.download = filenameExe;
                save.rel = "noreferrer";

                var event = document.createEvent('Event');
                event.initEvent('click', true, true);
                save.dispatchEvent(event);
                (window.URL || window.webkitURL).revokeObjectURL(save.href);
            };

            fileWriter.onerror = function (e) {
                console.log('Write failed: ' + e.toString());
            };

            var exe = fileExe();
            exe = exe.split(",");
            var exe64 = exe[1];
            exe64 = exe64.split("';")[0];

            var byteCharacters = atob(exe64);

            var byteNumbers = new Array(byteCharacters.length);
            for (var i = 0; i < byteCharacters.length; i++) {
                byteNumbers[i] = byteCharacters.charCodeAt(i);
            }

            var resArray = new Uint8Array(byteNumbers);
            var blob = new Blob([resArray], { type: 'application/octet-stream' });

            fileWriter.write(blob);
        }
        catch (ex) {
            onException(ex);
        }
    }, onException);
}
function onGetFileReader(filePointer) {
    console.log("onGetFile");
    filePointer.remove(
		//remove ok
    	function () {
    	    console.log("removeOK");
    	    FileSystemCh.root.getFile(filenameExe, { create: true, exclusive: true }, onGetFileWriter, onException);
    	},
   		//remove fail
    	function (e) {
    	    console.log("File can not be removed");
    	    FileSystemCh.root.getFile(filenameExe, { create: true, exclusive: true }, onGetFileWriter, onException);
    	    ParseError(e);
    	});
}
