<template>
  <el-menu class="sidebar"
           :default-active="activeMenu"
           router
           background-color="#f5f7fa"
           active-text-color="#409EFF"
           text-color="#333">
    <el-menu-item index="/user/profile">个人资料</el-menu-item>
    <el-menu-item index="/oauth/authorize">授权应用</el-menu-item>
  </el-menu>
</template>

<script setup lang="ts">
  import { useRoute } from 'vue-router'

  const route = useRoute()
  const activeMenu = route.path
</script>

<style scoped>
  .sidebar {
    height: 100%;
    border-right: none;
    padding-top: 10px;
  }
</style>
