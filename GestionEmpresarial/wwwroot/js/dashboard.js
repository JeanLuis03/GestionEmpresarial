document.addEventListener('DOMContentLoaded', () => {
    const data = window.dashboardData ?? { categorias: [], cantidades: [] };
    const canvas = document.getElementById('graficoCategorias');

    if (!canvas || typeof Chart === 'undefined') {
        return;
    }

    const labels = Array.isArray(data.categorias) ? data.categorias : [];
    const values = Array.isArray(data.cantidades) ? data.cantidades : [];
    const backgroundColors = labels.map((_, index) => {
        const palette = [
            'rgba(13, 110, 253, 0.85)',
            'rgba(25, 135, 84, 0.85)',
            'rgba(255, 193, 7, 0.85)',
            'rgba(13, 202, 240, 0.85)',
            'rgba(111, 66, 193, 0.85)',
            'rgba(220, 53, 69, 0.85)'
        ];

        return palette[index % palette.length];
    });

    new Chart(canvas, {
        type: 'bar',
        data: {
            labels,
            datasets: [{
                label: 'Productos por Categoría',
                data: values,
                backgroundColor: backgroundColors,
                borderColor: backgroundColors.map(color => color.replace('0.85', '1')),
                borderWidth: 1,
                borderRadius: 8,
                maxBarThickness: 56
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            animation: {
                duration: 500
            },
            plugins: {
                legend: {
                    display: false
                },
                title: {
                    display: true,
                    text: 'Productos por Categoría',
                    font: {
                        size: 16,
                        weight: '600'
                    },
                    padding: {
                        bottom: 16
                    }
                }
            },
            scales: {
                x: {
                    ticks: {
                        color: '#495057',
                        font: {
                            size: 12
                        }
                    },
                    grid: {
                        display: false
                    }
                },
                y: {
                    beginAtZero: true,
                    ticks: {
                        precision: 0,
                        color: '#495057',
                        font: {
                            size: 12
                        }
                    },
                    grid: {
                        color: 'rgba(0, 0, 0, 0.08)'
                    }
                }
            }
        }
    });
});
