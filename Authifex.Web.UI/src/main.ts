import { createApp } from 'vue'
import App from './App.vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import router from './router'  // 导入路由
import { createPinia } from 'pinia'
import worker from '@/mocks/browser'
import zhCn from 'element-plus/es/locale/lang/zh-cn'

//console.log('是否是开发环境:', import.meta.env.DEV)

// 创建应用实例
const app = createApp(App)

const pinia = createPinia()

// 仅开发环境启动 MSW
if (import.meta.env.DEV) {
  import('./mocks/browser').then(m => m.default.start())
}

// 使用 Element Plus 插件
app.use(ElementPlus)
app.use(ElementPlus, { locale: zhCn })

// 注册路由
app.use(router)

// 全局状态
app.use(pinia)

// 挂载到 DOM
app.mount('#app')
