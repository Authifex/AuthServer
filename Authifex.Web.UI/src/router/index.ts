import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import Login from '@/views/Auth/Login.vue'
// import Register from '@/views/Auth/Register.vue'
// import OAuthCallback from '@/views/Auth/OAuthCallback.vue'
// import AuthorizeApp from '@/views/Auth/AuthorizeApp.vue'
// import UserProfile from '@/views/User/Profile.vue'

const routes: RouteRecordRaw[] = [
  { path: '/login', name: 'Login', component: Login },
  // { path: '/register', name: 'Register', component: Register },
  // { path: '/oauth/callback', name: 'OAuthCallback', component: OAuthCallback },
  // { path: '/oauth/authorize', name: 'AuthorizeApp', component: AuthorizeApp },
  // { path: '/user/profile', name: 'UserProfile', component: UserProfile },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
