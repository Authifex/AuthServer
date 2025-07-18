<!-- src/views/Auth/OAuthCallback.vue -->
<template>
  <div style="margin-top: 100px; text-align: center;">
    <el-icon size="32" class="is-loading"><Loading /></el-icon>
    <p>正在登录中...</p>
  </div>
</template>

<script setup lang="ts">
  import { onMounted } from 'vue'
  import { ElMessage } from 'element-plus'
  import { useRoute, useRouter } from 'vue-router'
  import { useUserStore } from '@/stores/user'

  const route = useRoute()
  const router = useRouter()
  const userStore = useUserStore()

  onMounted(async () => {
    const { code, state } = route.query
    if (!code) {
      ElMessage.error('缺少授权码')
      router.replace('/login')
      return
    }
    try {
      await userStore.oauthLogin({ code: code as string, state: state as string })
      ElMessage.success('登录成功')
      router.replace('/')
    } catch (e: any) {
      ElMessage.error(e?.message || '授权登录失败')
      router.replace('/login')
    }
  })
</script>
