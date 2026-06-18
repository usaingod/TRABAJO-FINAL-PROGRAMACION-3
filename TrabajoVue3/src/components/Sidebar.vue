<template>

<div class="sidebar">

    <div class="sidebar-header">

        <h2>
            <i class="bi bi-wallet2"></i>
            Wallet Cripto
        </h2>

        <p>Billetera Virtual</p>

    </div>

    <ul class="nav flex-column">

        <li class="nav-item mb-2">
            <RouterLink class="nav-link text-white" to="/">
                <i class="bi bi-house-fill"></i>
                Dashboard
            </RouterLink>
        </li>

        <li class="nav-item mb-2">
            <RouterLink class="nav-link text-white" to="/comprar">
                <i class="bi bi-cart-plus"></i>
                Comprar
            </RouterLink>
        </li>

        <li class="nav-item mb-2">
            <RouterLink class="nav-link text-white" to="/vender">
                <i class="bi bi-currency-exchange"></i>
                Vender
            </RouterLink>
        </li>

        <li class="nav-item mb-2">
            <RouterLink class="nav-link text-white" to="/portfolio">
                <i class="bi bi-graph-up"></i>
                Portfolio
            </RouterLink>
        </li>

        <li class="nav-item">
            <RouterLink class="nav-link text-white" to="/historial">
                <i class="bi bi-clock-history"></i>
                Historial
            </RouterLink>
        </li>

    </ul>

</div>

</template>

<script setup>
import { RouterLink } from 'vue-router'
</script>