$.ajaxPrefilter(function (options, originalOptions, jqXHR) {
    // KEEP THIS: Forces the browser to send your authentication and anti-forgery cookies
    options.xhrFields = options.xhrFields || {};
    options.xhrFields.withCredentials = true;

    options.headers = options.headers || {};

    const requestType = options.type ? options.type.toUpperCase() : "GET";

    if (requestType === "POST" || requestType === "PUT" || requestType === "DELETE")
    {
        const antiForgeryTokenValue = $('input[name="__RequestVerificationToken"]').val();

        if (antiForgeryTokenValue) {
            // FIX: Changed header name to match what your backend filter reads
            options.headers["X-XSRF-TOKEN"] = antiForgeryTokenValue;
        }
    }
});

async function secureFetch(url, options = {}) {
    options.headers = options.headers || {};

    // EQUIVALENT TO withCredentials: true (Forces cookies to send)
    options.credentials = 'include';

    const method = options.method ? options.method.toUpperCase() : "GET";

    if (method === "POST" || method === "PUT" || method === "DELETE") {
        const antiForgeryTokenValue = document.querySelector('input[name="__RequestVerificationToken"]')?.value;

        if (antiForgeryTokenValue) {
            // FIX: Changed header name to match what your backend filter reads
            options.headers["X-XSRF-TOKEN"] = antiForgeryTokenValue;
        }
    }
    
    // Remember to execute the actual fetch wrapper call at the end of your utility function
    return fetch(url, options);
}

$(document).on("submit", "form", function () {
    const currentForm = $(this);

    // Check if the form is already missing the token field
    if (currentForm.find('input[name="__RequestVerificationToken"]').length === 0) {
        // Grab the single global token from the top of the body
        const globalTokenValue = $('body > input[name="__RequestVerificationToken"]').val();

        if (globalTokenValue) {
            // Append it cleanly so the browser submits it natively
            currentForm.append(
                $('<input>', { type: 'hidden', name: '__RequestVerificationToken', value: globalTokenValue })
            );
        }
    }
});
