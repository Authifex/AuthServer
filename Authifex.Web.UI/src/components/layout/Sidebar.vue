<template>
  <el-menu
    class="sidebar"
    :default-active="activeMenu"
    router
  >
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
  border-right: 1px solid #ebeef5;
}
</style>
