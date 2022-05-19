<template>
  <div id="app">
    <router-link to="/">
      <img src="Logo.svg" class="logo" />
    </router-link>
    <div id="nav">
      <router-link to="/authorize" class="m-label" v-if="$route.name != 'Authorize'"><span class="nav-label">ВОЙТИ</span></router-link>
    </div>
    <transition name="fade">
      <router-view />
    </transition>
  </div>
</template>

<style scoped lang="scss">
#nav {
  top: 40px;
  right: 40px;
  position: fixed;
  z-index: 999;
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s cubic-bezier(0.33, 1, 0.68, 1);
}

.fade-enter,
.fade-leave-active {
  opacity: 0;
}

.nav-label {
  color: burlywood;
}

.logo {
  width: 300px;
  position: absolute;
  top: 20px;
  left: 40px;
  z-index: 999;
}
</style>
