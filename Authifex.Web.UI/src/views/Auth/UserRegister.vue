<!-- src/views/Auth/Register.vue -->
<template>
  <el-card style="max-width: 400px; margin: 100px auto;">
    <el-form @submit.prevent="onRegister" label-width="auto" size="large">
      <el-form-item label="用户名">
        <el-input v-model="form.username" placeholder="4~16 位字母/数字/下划线" />
      </el-form-item>
      <el-form-item label="邮箱">
        <el-input v-model="form.email" type="email" placeholder="用于找回密码" />
      </el-form-item>
      <el-form-item label="密码">
        <el-input v-model="form.password" type="password" show-password placeholder="至少 6 位" />
      </el-form-item>
      <el-form-item label="确认密码">
        <el-input v-model="form.confirm" type="password" show-password />
      </el-form-item>

      <el-button type="primary" style="width: 100%;" :loading="loading" @click="onRegister">
        注册
      </el-button>

      <div style="margin-top: 12px; text-align: center;">
        <el-link type="primary" href="/login">已有账号？去登录</el-link>
      </div>
    </el-form>
  </el-card>
</template>

<script setup lang="ts">
  import { reactive, ref } from 'vue'
  import { ElMessage } from 'element-plus'
  import { useRouter } from 'vue-router'
  import { useUserStore } from '@/stores/user'

  const form = reactive({
    username: '',
    email: '',
    password: '',
    confirm: ''
  })

  const loading = ref(false)
  const userStore = useUserStore()
  const router = useRouter()

  const onRegister = async () => {
    if (form.password !== form.confirm) {
      ElMessage.warning('两次密码不一致')
      return
    }
    loading.value = true
    try {
      await userStore.register(form)
      ElMessage.success('注册成功，请登录')
      router.replace('/login')
    } catch (e: any) {
      ElMessage.error(e?.message || '注册失败')
    } finally {
      loading.value = false
    }
  }
</script>
