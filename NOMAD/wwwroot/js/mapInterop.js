window.nomadMap = {
    map: null,
    markers: [],
    routeLayer: null,
    heatLayer: null,
    userMarker: null,
    liveLocation: null,

    initMap: function (elementId, lat, lng, zoom) {
        if (this.map) {
            this.map.remove();
            this.map = null;
        }

        const self = this;

        function loadMap(userLat, userLng) {
            self.map = L.map(elementId, {
                zoomControl: false
            }).setView([userLat, userLng], zoom);

            L.tileLayer('https://{s}.basemaps.cartocdn.com/rastertiles/voyager/{z}/{x}/{y}{r}.png', {
                attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OSM</a> contributors &copy; <a href="https://carto.com/">CARTO</a>',
                subdomains: 'abcd',
                maxZoom: 20
            }).addTo(self.map);

            L.control.zoom({ position: 'bottomright' }).addTo(self.map);

            self.updateUserMarker(userLat, userLng);
        }

        if (this.liveLocation) {
            loadMap(this.liveLocation.lat, this.liveLocation.lng);
            return;
        }

        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(
                function (position) {
                    var liveLat = position.coords.latitude;
                    var liveLng = position.coords.longitude;
                    self.liveLocation = { lat: liveLat, lng: liveLng };
                    loadMap(liveLat, liveLng);
                },
                function () {
                    loadMap(lat, lng);
                }
            );
        } else {
            loadMap(lat, lng);
        }
    },

    updateUserMarker: function (lat, lng) {
        if (this.userMarker) {
            this.map.removeLayer(this.userMarker);
        }

        const userIcon = L.divIcon({
            className: 'user-marker-pulse',
            html: '<div class="pulse-ring"></div><div class="pulse-core"></div>',
            iconSize: [24, 24],
            iconAnchor: [12, 12]
        });
 
        this.userMarker = L.marker([lat, lng], { icon: userIcon })
            .addTo(this.map)
            .bindPopup('<b style="color:var(--color-primary);">Active Location Base</b><br/>Uplink Secured', { className: 'nomad-popup' });
    },

    clearOverlays: function () {
        if (this.markers) {
            this.markers.forEach(m => this.map.removeLayer(m));
            this.markers = [];
        }
        if (this.routeLayer) {
            this.map.removeLayer(this.routeLayer);
            this.routeLayer = null;
        }
        if (this.heatLayer) {
            this.map.removeLayer(this.heatLayer);
            this.heatLayer = null;
        }
    },

    recenterMap: function (lat, lng) {
        if (this.map) {
            this.map.setView([lat, lng], 13);
            this.updateUserMarker(lat, lng);
        }
    },

    addPlaces: function (places) {
        places.forEach(p => {
            let color = '#64748b';
            let iconClass = 'bi-pin-map-fill';

            if (p.type === 'Hospital') { color = '#10b981'; iconClass = 'bi-hospital-fill'; }
            else if (p.type === 'Police') { color = '#0ea5e9'; iconClass = 'bi-shield-fill'; }
            else if (p.type === 'Embassy') { color = '#f59e0b'; iconClass = 'bi-flag-fill'; }
            else if (p.type === 'Hotel') { color = '#6366f1'; iconClass = 'bi-house-fill'; }

            const customIcon = L.divIcon({
                className: 'custom-place-marker',
                html: `<div style="color: ${color}; font-size: 20px; filter: drop-shadow(0 2px 4px rgba(0,0,0,0.15));"><i class="bi ${iconClass}"></i></div>`,
                iconSize: [24, 24],
                iconAnchor: [12, 24],
                popupAnchor: [0, -20]
            });

            const marker = L.marker([p.lat, p.lng], { icon: customIcon })
                .addTo(this.map)
                .bindPopup(`<b style="color:${color};">${p.title}</b><br/>${p.description}`, { className: 'nomad-popup' });
            
            this.markers.push(marker);
        });
    },

    drawRoute: function (coordinates, color) {
        if (this.routeLayer) {
            this.map.removeLayer(this.routeLayer);
        }

        this.routeLayer = L.polyline(coordinates, {
            color: color || '#e2a76f',
            weight: 5,
            opacity: 0.8,
            className: 'glowing-route'
        }).addTo(this.map);

        this.map.fitBounds(this.routeLayer.getBounds(), { padding: [50, 50] });
    },

    addHeatmap: function (dangerZones) {
        if (this.heatLayer) {
            this.map.removeLayer(this.heatLayer);
        }

        const heatData = dangerZones.map(z => [z.lat, z.lng, z.intensity]);

        this.heatLayer = L.heatLayer(heatData, {
            radius: 35,
            blur: 20,
            maxZoom: 15,
            gradient: {
                0.4: 'rgba(226, 167, 111, 0.6)',
                0.7: 'rgba(216, 90, 90, 0.8)',
                1.0: '#d85a5a'
            }
        }).addTo(this.map);
    }
};
