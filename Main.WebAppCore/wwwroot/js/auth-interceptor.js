// Global flag to prevent multiple overlapping refresh requests
let isRefreshing = false;
let failedQueue = [];

const processQueue = (error, success = false) => {
    failedQueue.forEach(prom => {
        if (success) {
            prom.resolve();
        } else {
            prom.reject(error);
        }
    });
    failedQueue = [];
};

// Intercept all global jQuery AJAX completions
$.ajaxSetup({
    statusCode: {
        401: function (xhr, textStatus, errorThrown) {
            // Keep track of the original AJAX settings that just failed
            const originalSettings = this;

            // If we are already in the middle of refreshing, queue this request
            if (isRefreshing) {
                return new Promise((resolve, reject) => {
                    failedQueue.push({ resolve, reject });
                }).then(() => {
                    return $.ajax(originalSettings);
                }).catch((err) => {
                    return Promise.reject(err);
                });
            }

            isRefreshing = true;

            // Grab the verification token safely from any input, or use an empty string fallback
            const antiForgeryValue = $('input[name="__RequestVerificationToken"]').val() || "";

            // Make a hidden POST request to your Auth/Refresh token endpoint
            return $.ajax({
                url: '/refresh-token',
                type: 'POST',
                headers: {
                    // FIX: Ensures the background refresh passes your custom tenant validation filter
                    "X-XSRF-TOKEN": antiForgeryValue 
                }
            }).then(function (response) {
                isRefreshing = false;
                processQueue(null, true);

                // Retry the original AJAX call that failed now that cookies are updated
                return $.ajax(originalSettings);

            }).fail(function (refreshXhr) {
                isRefreshing = false;
                processQueue(refreshXhr, false);

                console.warn("Refresh token expired or revoked. Redirecting to login.");
                // FIX: Aligned redirect path from '/account/login' to '/Auth/Login'
                window.location.href = '/Auth/Login?returnUrl=' + encodeURIComponent(window.location.pathname);
            });
        }
    }
});

// Native Fetch Override Wrapper Setup
const originalFetch = window.fetch;

window.fetch = async (resource, config = {}) => {
    // Standardize config objects safely to prevent property reading errors
    config.headers = config.headers || {};
    
    let response = await originalFetch(resource, config);

    // If the short-lived access cookie expired, intercept the 401
    if (response.status === 401) {

        if (isRefreshing) {
            return new Promise((resolve, reject) => {
                failedQueue.push({ resolve, reject });
            }).then(() => originalFetch(resource, config))
              .catch(err => Promise.reject(err));
        }

        isRefreshing = true;

        try {
            // Grab token parameter value safely
            const tokenVal = document.querySelector('input[name="__RequestVerificationToken"]')?.value || "";

            // Run background token rotation
            const refreshResponse = await originalFetch('/refresh-token', {
                method: 'POST',
                headers: {
                    "Content-Type": "application/json",
                    // FIX: Changed header name to match your backend filter layout configuration
                    "X-XSRF-TOKEN": tokenVal 
                }
            });

            if (refreshResponse.ok) {
                isRefreshing = false;
                processQueue(null, true);

                // Retry original request with the fresh cookie set
                return originalFetch(resource, config);
            }
        } catch (err) {
            // Network or server failure handling
            console.error("Background token rotation exception caught", err);
        }

        // Failure: Clear state and boot user out
        isRefreshing = false;
        processQueue(new Error("Refresh failed"), false);

        // FIX: Aligned redirect path from '/account/login' to '/Auth/Login'
        window.location.href = '/Auth/Login?returnUrl=' + encodeURIComponent(window.location.pathname);
    }

    return response;
};
