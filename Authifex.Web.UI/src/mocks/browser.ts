import { setupWorker } from 'msw/browser'
import { http, HttpResponse } from 'msw'

// 类型安全：告诉 TS 这是浏览器端 worker
const worker = setupWorker(
  // 登录接口
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
  }),

  // 用户信息
  http.get('/api/user/profile', () =>
    HttpResponse.json({
      avatar: '',
      nickname: 'MockUser',
      email: 'mock@example.com'
    })
  ),

  // 更新用户信息
  http.put('/api/user/profile', async ({ request }) => {
    const body = await request.json()
    return HttpResponse.json(body)   // 原样返回即可
  })
)

export default worker

/* ------------------------------------------------------------------
 * 备忘 / Reminder
 * 1. 仅开发环境使用：main.ts 里 `import.meta.env.DEV` 判断
 *
 * 2. 浏览器报 “The operation is insecure” 时：
 *    • Chrome / Edge
 *      - 地址栏输入：chrome://flags/#allow-insecure-localhost → 启用
 *      - 或改用 http://localhost:5173（去掉 https）
 *    • Firefox
 *      - 退出隐私窗口 / 严格跟踪保护
 *      - about:config → dom.serviceWorkers.enabled = true
 *
 * 3. 若出现 404 / mockServiceWorker.js
 *    终端执行：npx msw init public/
 * ------------------------------------------------------------------ */
