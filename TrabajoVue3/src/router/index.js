import { createRouter, createWebHistory } from 'vue-router'

import Dashboard from '../views/Dashboard.vue'
import Comprar from '../views/Comprar.vue'
import Vender from '../views/Vender.vue'
import Portfolio from '../views/Portfolio.vue'
import Historial from '../views/Historial.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),

  routes: [
    {
      path: '/',
      name: 'dashboard',
      component: Dashboard
    },
    {
      path: '/comprar',
      name: 'comprar',
      component: Comprar
    },
    {
      path: '/vender',
      name: 'vender',
      component: Vender
    },
    {
      path: '/portfolio',
      name: 'portfolio',
      component: Portfolio
    },
    {
      path: '/historial',
      name: 'historial',
      component: Historial
    }
  ]
})

export default router
