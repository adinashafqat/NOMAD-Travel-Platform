window.nomadStorage = {
    saveCards: function (key, cards) {
        localStorage.setItem(key, JSON.stringify(cards));
    },
    loadCards: function (key) {
        const data = localStorage.getItem(key);
        return data ? JSON.parse(data) : null;
    },
    triggerGPS: function() {
        return "41.0082° N, 28.9784° E";
    }
};
