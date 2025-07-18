import { defineStore } from 'pinia'
import { ref } from 'vue'

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
    if (!res.ok) {
      console.error('登录接口返回错误', res.status)
      return
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

  return { isLoggedIn, username, token, login, logout }
})
