<template>
  <el-card style="max-width: 400px; margin: 100px auto;">
    <el-form @submit.prevent="onLogin">
      <el-form-item label="用户名">
        <el-input v-model="username" placeholder="请输入用户名" />
      </el-form-item>
      <el-form-item label="密码">
        <el-input v-model="password" type="password" placeholder="请输入密码" />
      </el-form-item>
      <el-button type="primary" style="width: 100%;" @click="onLogin">登录</el-button>
    </el-form>
  </el-card>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { useUserStore } from '@/stores/user'

const username = ref('')
const password = ref('')

const userStore = useUserStore()

const onLogin = async () => {
  await userStore.login({ username: username.value, password: password.value })
}
</script>
