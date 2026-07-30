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

// ==========================================
// 1. FIXED JQUERY INTERCEPTOR (Using ajaxError)
// ==========================================
$(document).ajaxError(function (event, jqXHR, ajaxSettings, thrownError) {
    // Safety 1: If the request that failed WAS the refresh token itself, DO NOT retry. Boot user.
    if (ajaxSettings.url === '/refresh-token') {
        return; 
    }

    if (jqXHR.status === 401) {
        const deferred = $.Deferred();

        if (isRefreshing) {
            failedQueue.push({
                resolve: () => {
                    $.ajax(ajaxSettings).done(deferred.resolve).fail(deferred.reject);
                },
                reject: (err) => {
                    deferred.reject(err);
                }
            });
            return deferred.promise();
        }

        isRefreshing = true;

        // FIX: Extracting from your actual Razor meta tag structure
        const antiForgeryValue = $('meta[name="xsrf-token"]').attr('content') || "";

        $.ajax({
            url: '/refresh-token',
            type: 'POST',
            headers: {
                "X-XSRF-TOKEN": antiForgeryValue 
            }
        }).then(function () {
            isRefreshing = false;
            processQueue(null, true);

            // Re-execute original call and resolve the wrapper deferred tracking
            $.ajax(ajaxSettings).done(deferred.resolve).fail(deferred.reject);

        }).fail(function (refreshXhr) {
            isRefreshing = false;
            processQueue(refreshXhr, false);

            console.warn("Refresh token expired. Redirecting to login.");
            window.location.href = '/Auth/Login?returnUrl=' + encodeURIComponent(window.location.pathname);
        });

        return deferred.promise();
    }
});

// ==========================================
// 2. FIXED NATIVE FETCH OVERRIDE WRAPPER
// ==========================================
const originalFetch = window.fetch;

window.fetch = async (resource, config = {}) => {
    config.headers = config.headers || {};
    
    // Normalize string URLs or Request objects to safely check the path
    const requestUrl = typeof resource === 'string' ? resource : resource.url;
    
    let response = await originalFetch(resource, config);

    // If a 401 happens, but it came from the refresh endpoint itself, break out immediately
    if (response.status === 401 && requestUrl.includes('/refresh-token')) {
        console.warn("Refresh token expired on fetch invocation. Redirecting.");
        window.location.href = '/Auth/Login?returnUrl=' + encodeURIComponent(window.location.pathname);
        return response;
    }

    if (response.status === 401) {

        if (isRefreshing) {
            return new Promise((resolve, reject) => {
                failedQueue.push({ resolve, reject });
            }).then(() => originalFetch(resource, config))
              .catch(err => Promise.reject(err));
        }

        isRefreshing = true;

        try {
            // FIX: Grab safely from the correct meta tag configuration
            const tokenVal = document.querySelector('meta[name="xsrf-token"]')?.getAttribute('content') || "";

            const refreshResponse = await originalFetch('/refresh-token', {
                method: 'POST',
                headers: {
                    "X-XSRF-TOKEN": tokenVal 
                }
            });

            if (refreshResponse.ok) {
                isRefreshing = false;
                processQueue(null, true);

                return originalFetch(resource, config);
            }
        } catch (err) {
            console.error("Background token rotation exception caught", err);
        }

        isRefreshing = false;
        processQueue(new Error("Refresh failed"), false);

        window.location.href = '/Auth/Login?returnUrl=' + encodeURIComponent(window.location.pathname);
    }

    return response;
};
