import { setupWorker } from 'msw/browser'
import { http, HttpResponse } from 'msw'

// 类型安全：告诉 TS 这是浏览器端 worker
const worker = setupWorker(
  // 用 http.post 代替旧的 rest.post
  http.post('/api/auth/login', async ({ request }) => {
    const body = (await request.json()) as { username: string; password: string }

    // 简单校验
    if (body.username === 'admin' && body.password === '123456') {
      return HttpResponse.json({
        token: 'mock-jwt-token',
        user: { id: 1, username: 'admin', email: 'admin@example.com' },
      })
    }

    return HttpResponse.json(
      { message: '用户名或密码错误' },
      { status: 401 }
    )
  })
)

export default worker
