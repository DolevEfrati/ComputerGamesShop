$(document).ready(function () {
    var zoom = 6.5;

    if ($("#mapid").is(":visible")) {
        var map = L.map("mapid");
        if (map) {
            map.setView([32.0926173, 34.83920569999998], zoom);

            L.tileLayer("https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png", {
                attribution:
                    '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
            }).addTo(map);

            makeHttpRequest('/api/stores', "GET").then(result => {
                if (result && result.stores) {
                    result.stores.forEach(store => {
                        L.marker([store.latitude, store.longitude])
                            .addTo(map)
                            .bindPopup(store.displayName);
                    });
                }
            });
        }
    }
});

async function makeHttpRequest(url, method) {
    try {
        const res = await fetch(url, {
            method
        });

        const result = await res.json();
        if (res.status === 404 || res.status === 409) {
            console.error(result.message);
            return;
        }

        return result;
    } catch (err) {
        console.error(err);
    }
}