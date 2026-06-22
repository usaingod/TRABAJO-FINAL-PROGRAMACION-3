<template>

<div class="container-fluid">

    <div class="wallet-card">

        <div class="d-flex justify-content-between align-items-center mb-4">

            <h1 class="dashboard-title mb-0">

                Historial

            </h1>

            <div class="d-flex align-items-center gap-3">

                <i
                    class="bi bi-chevron-left pagination-arrow"
                    @click="paginaActual > 1 && paginaActual--">
                </i>

                <span class="pagination-text">

                    Página {{ paginaActual }} de {{ totalPaginas }}

                </span>

                <i
                    class="bi bi-chevron-right pagination-arrow"
                    @click="paginaActual < totalPaginas && paginaActual++">
                </i>

            </div>

        </div>

        <table class="table table-dark table-hover align-middle mb-0">

            <thead>

                <tr>

                    <th>Fecha</th>
                    <th>Acción</th>
                    <th>Criptomoneda</th>
                    <th>Cantidad</th>
                    <th>Total</th>

                </tr>

            </thead>

            <tbody>

                <tr
                    v-for="transaccion in historialPaginado"
                    :key="transaccion.id">

                    <td>

                        {{ new Date(transaccion.fechaHora).toLocaleString("es-AR") }}

                    </td>

                    <td>

                        <span
                            :class="transaccion.accion === 'Compra'
                            ? 'text-success'
                            : 'text-danger'">

                            <i class="bi bi-circle-fill me-2" style="font-size:8px"></i>

                            {{ transaccion.accion }}

                        </span>

                    </td>

                    <td>

                        {{ transaccion.codigoCripto.toUpperCase() }}

                    </td>

                    <td>

                        {{ transaccion.cantidadCripto }}

                    </td>

                    <td>

                         $ {{ transaccion.dinero.toLocaleString("es-AR") }} ARS

                    </td>

                </tr>

            </tbody>

        </table>

    </div>

</div>

</template>


<script setup>

import { ref, onMounted, computed } from 'vue'
import { obtenerHistorial } from '../services/api'

const historial = ref([])

const paginaActual = ref(1)

const registrosPorPagina = 10

const historialPaginado = computed(() => {

    const inicio = (paginaActual.value - 1) * registrosPorPagina

    const fin = inicio + registrosPorPagina

    return historial.value.slice(inicio, fin)

})

const totalPaginas = computed(() => {

    return Math.ceil(historial.value.length / registrosPorPagina)

})

onMounted(async () => {

    try {

        historial.value = await obtenerHistorial()

        console.log(historial.value)

    }

    catch (error) {

        console.error(error)

    }

})

</script>

