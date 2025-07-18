<!-- src/views/User/Profile.vue -->
<template>
  <el-card>
    <template #header>
      个人资料
    </template>
    <el-form :model="form" label-width="80">
      <el-form-item label="头像">
        <el-upload class="avatar-uploader"
                   :show-file-list="false"
                   :action="uploadUrl"
                   :headers="{ Authorization: `Bearer ${token}` }"
                   :on-success="onAvatarSuccess">
          <img v-if="form.avatar" :src="form.avatar" class="avatar" />
          <el-icon v-else class="avatar-uploader-icon"><Plus /></el-icon>
        </el-upload>
      </el-form-item>

      <el-form-item label="昵称">
        <el-input v-model="form.nickname" />
      </el-form-item>
      <el-form-item label="邮箱">
        <el-input v-model="form.email" />
      </el-form-item>

      <el-button type="primary" :loading="loading" @click="onSave">保存</el-button>
    </el-form>
  </el-card>
</template>

<script setup lang="ts">
  import { reactive, ref, onMounted } from 'vue'
  import { ElMessage } from 'element-plus'
  import { useUserStore } from '@/stores/user'

  const userStore = useUserStore()

  const form = reactive({
    avatar: '',
    nickname: '',
    email: ''
  })
  const loading = ref(false)

  const uploadUrl = import.meta.env.VITE_API_BASE + '/user/avatar'
  const token = userStore.token

  onMounted(async () => {
    const profile = await userStore.getProfile()
    Object.assign(form, profile)
  })

  const onAvatarSuccess = (res: any) => {
    form.avatar = res.url
  }

  const onSave = async () => {
    loading.value = true
    try {
      await userStore.updateProfile(form)
      ElMessage.success('已保存')
    } catch (e: any) {
      ElMessage.error(e?.message || '保存失败')
    } finally {
      loading.value = false
    }
  }
</script>

<style scoped>
  .avatar-uploader .avatar {
    width: 80px;
    height: 80px;
    border-radius: 50%;
  }

  .avatar-uploader-icon {
    font-size: 28px;
    width: 80px;
    height: 80px;
    border: 1px dashed var(--el-border-color);
    display: flex;
    align-items: center;
    justify-content: center;
  }
</style>
