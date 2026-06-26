window.nomadCharts = {
    pieChart: null,
    lineChart: null,

    renderPieChart: function (elementId, labels, data, colors) {
        const canvas = document.getElementById(elementId);
        if (!canvas) return;

        let existingChart = Chart.getChart(canvas);
        if (existingChart) {
            existingChart.destroy();
        }

        const ctx = canvas.getContext('2d');
        this.pieChart = new Chart(ctx, {
            type: 'doughnut',
            data: {
                labels: labels,
                datasets: [{
                    data: data,
                    backgroundColor: colors,
                    borderWidth: 0,
                    hoverOffset: 10
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                cutout: '75%',
                plugins: {
                    legend: {
                        position: 'right',
                        labels: { color: '#334155', font: { family: 'Outfit, Inter', size: 12 } }
                    }
                }
            }
        });
    },

    renderLineChart: function (elementId, labels, dataArr) {
        const canvas = document.getElementById(elementId);
        if (!canvas) return;

        let existingChart = Chart.getChart(canvas);
        if (existingChart) {
            existingChart.destroy();
        }

        const ctx = canvas.getContext('2d');
        this.lineChart = new Chart(ctx, {
            type: 'line',
            data: {
                labels: labels,
                datasets: [{
                    label: 'Daily Spending (USD)',
                    data: dataArr,
                    borderColor: '#38bdf8', 
                    backgroundColor: 'rgba(56, 189, 248, 0.1)', 
                    borderWidth: 3,
                    fill: true,
                    tension: 0.4,
                    pointBackgroundColor: '#38bdf8',
                    pointBorderColor: '#fff',
                    pointHoverBackgroundColor: '#fff',
                    pointHoverBorderColor: '#38bdf8'
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                scales: {
                    y: {
                        beginAtZero: true,
                        grid: { color: 'rgba(0, 0, 0, 0.04)' },
                        ticks: { color: '#64748b', font: { family: 'Outfit, Inter' } }
                    },
                    x: {
                        grid: { display: false },
                        ticks: { color: '#64748b', font: { family: 'Outfit, Inter' } }
                    }
                },
                plugins: {
                    legend: { display: false }
                }
            }
        });
    }
};
