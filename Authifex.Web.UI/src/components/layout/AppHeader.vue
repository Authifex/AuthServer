<template>
  <el-header class="header">
    <div class="left">
      <strong>AuthServer 用户中心</strong>
    </div>

    <div class="right">
      <!-- 只有登录后才显示 -->
      <el-button v-if="userStore.isLoggedIn"
                 type="primary"
                 @click="logout">
        退出
      </el-button>

      <!-- 未登录时显示「登录」按钮 -->
      <el-button v-else
                 type="primary"
                 @click="$router.push('/login')">
        登录
      </el-button>
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
    router.replace('/login')
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
