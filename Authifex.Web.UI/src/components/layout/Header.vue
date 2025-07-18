<template>
  <el-header class="header">
    <div class="left">
      <strong>AuthServer 用户中心</strong>
    </div>
    <div class="right">
      <el-button type="primary" @click="logout">退出</el-button>
    </div>
  </el-header>
</template>

<script setup lang="ts">
import { useUserStore } from '@/stores/user'
import { useRouter } from 'vue-router'

const userStore = useUserStore()
const router = useRouter()

const logout = () => {
  userStore.logout()
  router.push('/login')
}
</script>

<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 0 20px;
}
</style>
