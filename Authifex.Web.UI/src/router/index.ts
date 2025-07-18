import { createRouter, createWebHistory, RouteRecordRaw } from 'vue-router'
import UserLogin from '@/views/Auth/UserLogin.vue'
import UserRegister from '@/views/Auth/UserRegister.vue'
import OAuthCallback from '@/views/Auth/OAuthCallback.vue'
import AuthorizeApp from '@/views/Auth/AuthorizeApp.vue'
import UserProfile from '@/views/User/UserProfile.vue'

const routes: RouteRecordRaw[] = [
  { path: '/login', name: 'UserLogin', component: UserLogin },
  { path: '/register', name: 'Register', component: UserRegister },
  { path: '/oauth/callback', name: 'OAuthCallback', component: OAuthCallback },
  { path: '/oauth/authorize', name: 'AuthorizeApp', component: AuthorizeApp },
  { path: '/user/profile', name: 'UserProfile', component: UserProfile },
]

const router = createRouter({
  history: createWebHistory(),
  routes,
})

export default router
