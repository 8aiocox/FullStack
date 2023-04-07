/**
 * main.ts
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Components
import App from './App.vue'

// Composables
import { createApp } from 'vue'
import Vue from 'vue'

// Plugins
import { registerPlugins } from '@/plugins'
import VueDatePicker from '@vuepic/vue-datepicker';
import '@vuepic/vue-datepicker/dist/main.css'

import moment from 'moment'
import VueMoment from 'vue-moment'
const app = createApp(App)
registerPlugins(app)
app.component('VueDatePicker', VueDatePicker);
// app.use(require('vue-moment'));
app.mount('#app')
