const Api = (() => {

    const obtenerToken = () => {

        return document.querySelector(
            'input[name="__RequestVerificationToken"]')?.value;

    };

    const request = async (url, method = "GET", data = null) => {

        const options = {

            method,

            headers: {}

        };

        if (method !== "GET") {

            options.headers["RequestVerificationToken"] = obtenerToken();

            options.headers["Content-Type"] = "application/json";

            options.body = JSON.stringify(data);

        }

        const response = await fetch(url, options);

        let result = null;

        try {

            result = await response.json();

        }
        catch {

        }

        return {

            ok: response.ok,

            status: response.status,

            data: result

        };

    };

    return {

        get: (url) => request(url),

        post: (url, data) => request(url, "POST", data)

    };

})();