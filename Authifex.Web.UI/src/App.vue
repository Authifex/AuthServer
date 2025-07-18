<!-- App.vue -->
<template>
  <el-container style="min-height: 100vh;">
    <el-header class="app-header"><AppHeader /></el-header>
    <el-container>
      <el-aside width="220px" class="app-aside"><AppSidebar /></el-aside>
      <el-main class="app-main"><router-view /></el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
  import AppHeader from './components/layout/AppHeader.vue'
  import AppSidebar from './components/layout/AppSidebar.vue'
</script>

<style scoped>
  .app-header {
    background-color: #fff;
    box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
    padding: 0;
  }

  .app-aside {
    background-color: #f5f7fa;
    border-right: 1px solid #ebeef5;
  }

  .app-main {
    padding: 24px;
    background-color: #f0f2f5;
  }
</style>
