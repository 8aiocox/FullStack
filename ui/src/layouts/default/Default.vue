<template>
  <v-app>
    <!--<default-bar />-->
    <div class="d-flex">
      <div class="mx-auto">
        <img alt="StaffCircle Logo" src="/src/assets/logo.png" class="app__logo" style="max-width:200px;" />
      </div>
    </div>
    <div class="d-flex">
      <div class="mx-auto" id="nav">
        <router-link to="/">Send</router-link> |
        <router-link to="/list">List</router-link>
      </div>
    </div>

    <default-view />
  </v-app>
</template>

<script lang="ts" setup>
  import DefaultBar from './AppBar.vue'
  import DefaultView from './View.vue'
</script>
