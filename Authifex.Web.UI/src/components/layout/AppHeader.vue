<template>
  <div class="header">
    <div class="left">
      <span class="title">AuthServer 用户中心</span>
    </div>
    <div class="right">
      <el-button v-if="userStore.isLoggedIn"
                 type="primary"
                 @click="logout"
                 size="small">退出</el-button>
      <el-button v-else
                 type="primary"
                 @click="$router.push('/login')"
                 size="small">登录</el-button>
    </div>
  </div>
</template>

<script setup lang="ts">
  import { useUserStore } from '@/stores/user'
  import { useRouter } from 'vue-router'

  const userStore = useUserStore()
  const router = useRouter()

  const logout = () => {
    userStore.logout()
    router.replace('/login')
  }
</script>

<style scoped>
  .header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    height: 60px;
    padding: 0 20px;
    background-color: #fff;
  }

  .title {
    font-weight: bold;
    font-size: 18px;
  }
</style>
