$.ajaxPrefilter(function (options, originalOptions, jqXHR) {
    options.xhrFields = options.xhrFields || {};
    options.xhrFields.withCredentials = true;

    options.headers = options.headers || {};
    const requestType = options.type ? options.type.toUpperCase() : "GET";

    if (requestType === "POST" || requestType === "PUT" || requestType === "DELETE") {
        // FIX: Looked up the meta tag instead of the input field
        const antiForgeryTokenValue = $('meta[name="xsrf-token"]').attr('content');

        if (antiForgeryTokenValue) {
            options.headers["X-XSRF-TOKEN"] = antiForgeryTokenValue;
        }
    }
});

async function secureFetch(url, options = {}) {
    options.headers = options.headers || {};
    options.credentials = 'include';

    const method = options.method ? options.method.toUpperCase() : "GET";

    if (method === "POST" || method === "PUT" || method === "DELETE") {
        // FIX: Looked up the meta tag instead of the input field
        const antiForgeryTokenValue = document.querySelector('meta[name="xsrf-token"]')?.getAttribute('content');

        if (antiForgeryTokenValue) {
            options.headers["X-XSRF-TOKEN"] = antiForgeryTokenValue;
        }
    }
    
    return fetch(url, options);
}

$(document).on("submit", "form", function () {
    const currentForm = $(this);

    if (currentForm.find('input[name="__RequestVerificationToken"]').length === 0) {
        // FIX: Extract from the meta tag to inject into native HTML forms
        const globalTokenValue = $('meta[name="xsrf-token"]').attr('content');

        if (globalTokenValue) {
            currentForm.append(
                $('<input>', { type: 'hidden', name: '__RequestVerificationToken', value: globalTokenValue })
            );
        }
    }
});
