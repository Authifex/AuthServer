<template>
  <el-container style="min-height: 100vh;">
    <el-header><AppHeader /></el-header>
    <el-container>
      <el-aside width="200px"><AppSidebar /></el-aside>
      <el-main><router-view /></el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
import AppHeader from './components/layout/AppHeader.vue'
import AppSidebar from './components/layout/AppSidebar.vue'
</script>
