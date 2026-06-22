<template>

<div class="container-fluid">

    <h1 class="dashboard-title">

        Portfolio

    </h1>

    <div class="row g-4">

        <div
            class="col-md-6"
            v-for="cripto in portfolio"
            :key="cripto.codigoCripto">

            <div class="wallet-card portfolio-card">

                <h5 class="dashboard-card-title">

                    {{ obtenerNombreCripto(cripto.codigoCripto) }}

                </h5>

                <h2 class="portfolio-value">

                    {{ cripto.cantidad }}

                    {{ cripto.codigoCripto.toUpperCase() }}

                </h2>

                <p class="portfolio-price">

                    {{ '$ ' + cripto.valorEnPesos.toLocaleString('es-AR') + ' ARS' }}

                </p>

            </div>

        </div>

    </div>

</div>

</template>

<script setup>

import { ref, onMounted } from 'vue'
import { obtenerPortfolio } from '../services/api'

function obtenerNombreCripto(codigo) {

    switch (codigo.toLowerCase()) {

        case "btc":
            return "Bitcoin"

        case "eth":
            return "Ethereum"

        case "usdc":
            return "USD Coin"

        case "sol":
            return "Solana"

        default:
            return codigo.toUpperCase()

    }

}

const portfolio = ref([])

async function cargarPortfolio() {

    try {

        portfolio.value = await obtenerPortfolio()

    }
    catch (error) {

        console.error(error)

    }

}

onMounted(() => {

    cargarPortfolio()

})

</script>
