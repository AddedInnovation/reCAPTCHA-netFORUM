var netFORUM = (function ($) {
    /* Private Variables & Functions */

    function _onPageLoad(handler) {
        if (typeof (Sys) !== 'undefined' && typeof (Sys.WebForms) !== 'undefined' && typeof (Sys.WebForms.PageRequestManager) !== 'undefined') {
            Sys.WebForms.PageRequestManager.getInstance().add_pageLoaded(handler);
        }
        else if ($) {
            $(handler);
        }
        else if (document.readyState === 'complete' || (document.readyState !== 'loading' && !document.documentElement.doScroll)) {
            window.setTimeout(handler);
        }
        else if (typeof (addEventListener) === 'function') {

            function loaded() {
                document.removeEventListener('DOMContentLoaded', loaded);
                window.removeEventListener('load', loaded);
                handler();
            };

            document.addEventListener('DOMContentLoaded', loaded);
            window.addEventListener('load', loaded);
        }
        else if (window.attachEvent) {
            window.attachEvent('onload', handler);
        }
        else {
            window.onload = (function (onload_orig) {
                return function () {
                    onload_orig && onload_orig();
                    handler();
                };
            })(window.onload);
        }
    };

    /* Public Variables & Functions */

    return {
        onPageLoad: _onPageLoad
    };

})(typeof (jQuery) !== 'undefined' ? jQuery : null);

netFORUM.reCAPTCHA = (function ($) {
    /* Private Variables & Functions */

    var onsuccess = function () { };
    var defaultOptions = {
        'callback': _successCallback,
        'expired-callback': _expiredCallback,
        'error-callback': _errorCallback
    };

    function _wireRecaptchaChallenge(elem) {
        var onclick_orig = elem.onclick;
        elem.onclick = function (event) {
            event.preventDefault();
            onsuccess = function () {
                if (typeof onclick_orig === 'function') {
                    onclick_orig.apply(elem, event);
                }
                else if (elem.nodeName === "INPUT" && elem.type === "submit") {
                    $(elem).closest('form').submit();
                }
            };
            grecaptcha.execute();
            return false;
        };
    };

    function _renderRecaptcha(container, options) {

        var parameters = {};

        if (typeof options === 'string') {
            if ($ && typeof $.parseJSON === 'function') {
                parameters = $.parseJSON(options);
            }
            else if (JSON && typeof JSON.parse === 'function') {
                parameters = JSON.parse(options);
            }
        }
        else {
            parameters = options;
        }

        grecaptcha.render(container, $.extend({}, defaultOptions, parameters), true);
    };

    function _successCallback(args) {
        if (typeof onsuccess === 'function') {
            onsuccess(args);
        }
    }

    function _expiredCallback(args) {
        console.log('reCAPTCHA token expired');
    }

    function _errorCallback(args) {
        console.error('reCAPTCHA error occurred', args);
    }

    /* Public Variables & Functions */

    return {
        renderRecaptcha: _renderRecaptcha,
        wireRecaptchaChallenge: _wireRecaptchaChallenge,
        defaultCallbacks: {
            success: _successCallback,
            expired: _expiredCallback,
            error: _errorCallback
        }
    };

})(typeof (jQuery) !== 'undefined' ? jQuery : null);