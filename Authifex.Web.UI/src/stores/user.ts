import { defineStore } from 'pinia'
import { ref, computed } from 'vue'

interface User {
  username: string
  token: string
}

export const useUserStore = defineStore('user', () => {
  const isLoggedIn = ref(false)
  const username = ref('')
  const token = ref('')

  async function login(payload: { username: string; password: string }) {
    console.log('调用 login，参数:', payload)
    // 模拟请求，或者真实请求
    const res = await fetch('/api/auth/login', {
      method: 'POST',
      headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(payload),
    })
    // 非 2xx 统一抛错
    if (!res.ok) {
      // 附带后端返回的错误信息
      const { message = '登录失败' } = await res.json().catch(() => ({}))
      throw new Error(message)
    }
    const data = await res.json()
    console.log('登录接口返回数据', data)
    username.value = data.user.username
    token.value = data.token
    isLoggedIn.value = true
  }

  function logout() {
    username.value = ''
    token.value = ''
    isLoggedIn.value = false
  }

  async function getProfile() {
    const res = await fetch('/api/user/profile', {
      headers: { Authorization: `Bearer ${token.value}` },
    })
    if (!res.ok) throw new Error('获取用户信息失败')
    return res.json()   // { avatar, nickname, email, ... }
  }

  async function updateProfile(payload: any) {
    const res = await fetch('/api/user/profile', {
      method: 'PUT',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${token.value}`,
      },
      body: JSON.stringify(payload),
    })
    if (!res.ok) throw new Error('更新失败')
    // 更新成功后把本地用户名也同步一下
    const data = await res.json()
    username.value = data.username ?? username.value
  }

  return {
    isLoggedIn,
    username,
    token,
    login,
    logout,
    getProfile,      // 导出
    updateProfile,   // 导出
  }
})
