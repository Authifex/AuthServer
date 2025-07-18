<!-- src/views/Auth/AuthorizeApp.vue -->
<template>
  <el-card style="max-width: 500px; margin: 100px auto;">
    <h2>授权确认</h2>
    <p>应用 <strong>{{ app.name }}</strong> 想访问你的以下信息：</p>
    <ul>
      <li v-for="s in scopes" :key="s">{{ s }}</li>
    </ul>

    <div style="margin-top: 24px; text-align: right;">
      <el-button @click="onDeny">拒绝</el-button>
      <el-button type="primary" :loading="loading" @click="onAllow">允许</el-button>
    </div>
  </el-card>
</template>

<script setup lang="ts">
  import { ref, onMounted } from 'vue'
  import { ElMessage } from 'element-plus'
  import { useRoute, useRouter } from 'vue-router'
  import { useUserStore } from '@/stores/user'

  const route = useRoute()
  const router = useRouter()
  const userStore = useUserStore()

  const app = ref({ name: '' })
  const scopes = ref<string[]>([])
  const loading = ref(false)

  onMounted(async () => {
    const { client_id } = route.query
    if (!client_id) {
      ElMessage.error('缺少参数')
      router.replace('/')
      return
    }
    // 调接口获取第三方应用信息
    const info = await userStore.getOAuthAppInfo(client_id as string)
    app.value = info.app
    scopes.value = info.scopes
  })

  const onAllow = async () => {
    loading.value = true
    try {
      const { redirect_uri } = route.query
      await userStore.grantOAuthApp(route.query)
      location.href = redirect_uri as string
    } catch (e: any) {
      ElMessage.error(e?.message || '授权失败')
    } finally {
      loading.value = false
    }
  }

  const onDeny = () => {
    const { redirect_uri } = route.query
    // 回跳并带上 error=access_denied
    location.href = `${redirect_uri}?error=access_denied`
  }
</script>
