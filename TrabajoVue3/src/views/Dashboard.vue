<template>

<div class="container-fluid">

    <h1 class="dashboard-title">

        Inicio

    </h1>

    <!-- Gráfico -->

    <div class="row mb-4">

        <div class="col-12">

            <Card>

                <h5 class="dashboard-card-title">

                    <i class="bi bi-graph-up-arrow"></i>

                    Variación de la cartera

                </h5>

                <div style="height:220px">

                    <canvas ref="grafico"></canvas>

                </div>

            </Card>

        </div>

    </div>

    <!-- Patrimonio -->

    <div class="row">

        <div class="col-12">

            <Card>

                <h5 class="dashboard-card-title">

                    <i class="bi bi-wallet2"></i>

                    Patrimonio Total

                </h5>

                <h2 class="dashboard-card-value">

                    $ {{ patrimonio.toLocaleString() }} ARS

                </h2>

            </Card>

        </div>

    </div>

</div>

</template>

<script setup>
    
    import { ref, onMounted } from 'vue'
    import Chart from 'chart.js/auto'
    import { obtenerPatrimonio, obtenerVariacion } from '../services/api'
    import Card from '../components/ui/Card.vue'
    
    const patrimonio = ref(0)
    const grafico = ref(null)
    const variacion = ref([])
    
    onMounted(async () => {

    try {

        patrimonio.value = await obtenerPatrimonio()

        variacion.value = await obtenerVariacion()

        const labels = variacion.value.map(item => item.fecha)

        const datos = variacion.value.map(item => item.patrimonio)

        new Chart(grafico.value, {

            type: 'line',

            data: {

                labels: labels,

                datasets: [

                    {

                        label: 'Patrimonio',

                        data: datos,

                        borderColor: '#2563EB',

                        backgroundColor: 'rgba(37,99,235,.15)',

                        borderWidth: 4,

                        pointRadius: 0,

                        pointHoverRadius: 6,

                        pointHoverBackgroundColor: '#2563EB',

                        pointHoverBorderWidth: 2,

                        fill: true,

                        tension: 0.35,

                        pointRadius: 2

                    }

                ]

            },

            options: {

                        responsive: true,

                        maintainAspectRatio: false,

                interaction: {

                                intersect: false,

                                mode: 'index'

                },

                plugins: {

                    legend: {

                        display: false

                    }

                },

                scales: {

                    x: {

                        ticks: {

                            color: '#ffffff',

                            maxTicksLimit: 8

                        },

                    grid: {

                display: false

                },

                    border: {

                    display: false

                    }

                },

                    y: {

                        ticks: {

                        color: '#ffffff',

                        maxTicksLimit: 6,

                        callback: function(value) {

                            const numero = Number(value);

                            if (Math.abs(numero) >= 1000000000)
                                return '$ ' + (numero / 1000000000).toFixed(1) + ' B';

                            if (Math.abs(numero) >= 1000000)
                                return '$ ' + (numero / 1000000).toFixed(0) + ' M';

                            if (Math.abs(numero) >= 1000)
                                return '$ ' + (numero / 1000).toFixed(0) + ' K';

                            return '$ ' + numero;

                        }

                },

                grid: {

                    color: 'rgba(255,255,255,.04)'

                },

                    border: {

                    display: false

                }

            }

        }

    }

    })

    }
    catch (error) {

        console.error(error)

    }

})
</script>