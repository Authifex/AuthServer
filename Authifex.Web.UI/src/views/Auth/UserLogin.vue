<template>
  <el-card style="max-width: 400px; margin: 100px auto;">
    <el-form @submit.prevent="onLogin" label-width="auto" size="large">
      <el-form-item label="用户名">
        <el-input v-model="username" placeholder="请输入用户名" />
      </el-form-item>
      <el-form-item label="密码">
        <el-input v-model="password" type="password" placeholder="请输入密码" show-password />
      </el-form-item>
      <el-button type="primary" style="width: 100%;" @click="onLogin" :loading="loading">
        登录
      </el-button>

      <div style="margin-top: 12px; text-align: center;">
        <el-link type="primary" href="/register">去注册</el-link>
      </div>
    </el-form>
  </el-card>
</template>

<script setup lang="ts">
  import { ref } from 'vue'
  import { ElMessage } from 'element-plus'
  import { useRouter } from 'vue-router'
  import { useUserStore } from '@/stores/user'

  const username = ref('')
  const password = ref('')
  const loading = ref(false)

  const userStore = useUserStore()
  const router = useRouter()

  const onLogin = async () => {
    if (!username.value || !password.value) {
      ElMessage.warning('请完整填写')
      return
    }
    loading.value = true
    try {
      await userStore.login({ username: username.value, password: password.value })
      ElMessage.success('登录成功')
      const redirect = (router.currentRoute.value.query.redirect as string) || '/'
      router.replace(redirect)
    } catch (e: any) {
      ElMessage.error(e?.message || '登录失败')
    } finally {
      loading.value = false
    }
  }
</script>
