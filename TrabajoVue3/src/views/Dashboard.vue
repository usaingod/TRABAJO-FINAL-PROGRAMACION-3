
<template>

<div class="container-fluid">

    <h1 class="dashboard-title">

        Dashboard

    </h1>

    <div class="row">

        <div class="col-lg-6">

            <Card>

                <h5 class="dashboard-card-title">

                    <i class="bi bi-wallet2"></i>

                    Patrimonio Total

                </h5>

                <h2 class="dashboard-card-value">

                     $ {{ patrimonio.toLocaleString()}} ARS

                </h2>

            </Card>

        </div>

        <div class="col-lg-6">

            <Card>

                <h5 class="dashboard-card-title">

                    <i class="bi bi-graph-up-arrow"></i>

                    Variación

                </h5>

                <h2 class="dashboard-card-value">

                    --

                </h2>

            </Card>

        </div>

    </div>

</div>

</template>

<script setup>
    
    import { ref, onMounted } from 'vue'
    import Card from '../components/ui/Card.vue'
    import { obtenerPatrimonio } from '../services/api'
    
    const patrimonio = ref(0)
    
    onMounted(async () => {
    
        try {
    
            patrimonio.value = await obtenerPatrimonio()
    
            console.log("Patrimonio:", patrimonio.value)
    
        }
        catch (error) {
    
            console.error(error)
    
        }
    
    })
</script>