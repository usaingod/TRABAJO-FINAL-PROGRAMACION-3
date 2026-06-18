<script setup>
import Sidebar from './Sidebar.vue'
import { RouterView } from 'vue-router'
</script>

<template>

<div class="d-flex">

    <Sidebar />

    <main class="main-content">

        <RouterView />

    </main>

</div>

</template>