<template>
    <transition name="fade">

        <div
            v-if="mostrarMensaje"
            class="toast-personalizado">

            {{ mensaje }}

        </div>

    </transition>

<div class="container-fluid">

    <h1 class="dashboard-title">

        Comprar Criptomoneda

    </h1>

    <div class="row">

        <div class="col-lg-7">

            <div class="wallet-card">

                <div class="mb-4">

                    <label class="form-label">

                        Criptomoneda

                    </label>

                    <select class="form-select" v-model="criptomoneda">

                        <option value="btc">Bitcoin (BTC)</option>

                        <option value="eth">Ethereum (ETH)</option>

                        <option value="usdc">USDC</option>

                        <option value="sol">Solana (SOL)</option>

                    </select>

                </div>

                <div class="mb-4">

                    <label class="form-label">

                        Cantidad

                    </label>

                    <input
                        class="form-control"
                        type="number"
                        placeholder="Ingrese la cantidad"
                        v-model="cantidad">

                </div>

                <button class="btn btn-primary" @click="realizarCompra">

                    Comprar

                </button>

            </div>

        </div>

        <div class="col-lg-5">

            <div class="wallet-card">

                <h5 class="dashboard-card-title">

                    Cotización Compra

                </h5>

                <h2 class="dashboard-card-value">

                    {{ cotizacion ? '$' + cotizacion.precioCompra.toLocaleString('es-AR') + ' ARS' : 'ARS $ --' }}

                </h2>

            </div>

            <div class="wallet-card">

                <h5 class="dashboard-card-title">

                    Total a pagar 

                </h5>

                <h2 class="dashboard-card-value">

                    $ {{ total.toLocaleString('es-AR') }} ARS

                </h2>

            </div>

        </div>

    </div>

</div>

</template>

<script setup>

import { ref, watch, onMounted, computed } from 'vue'
import { obtenerCotizacion, comprar } from '../services/api'

const criptomoneda = ref("btc")

const cantidad = ref(0)

//para enzo

const cotizacion = ref(null)

const total = computed(() => {

    if (!cotizacion.value)
        return 0

    return cantidad.value * cotizacion.value.precioCompra

})
const mostrarMensaje = ref(false)

const mensaje = ref("")

async function cargarCotizacion() {

    try {

        cotizacion.value = await obtenerCotizacion(criptomoneda.value)

        console.log(cotizacion.value)

    }
    catch (error) {

        console.error(error)

    }

}
async function realizarCompra() {

    try {

        await comprar({

            codigoCripto: criptomoneda.value,

            cantidadCripto: Number(cantidad.value)

        })

        mensaje.value = "Compra realizada correctamente."

        mostrarMensaje.value = true

        setTimeout(() => {

            mostrarMensaje.value = false

        }, 5000)

    }
    catch(error){

        console.error(error)

        alert("Ocurrió un error al realizar la compra.")

    }

}

onMounted(() => {

    cargarCotizacion()

})

watch(criptomoneda, () => {

    cargarCotizacion()

})

</script>