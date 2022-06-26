<template>
  <div id="app">
    <div class="loading" v-if="isLoading">
      <div id="container">
        <svg viewBox="0 0 100 100">
          <defs>
            <filter id="shadow">
              <feDropShadow dx="0" dy="0" stdDeviation="1.5"
                            flood-color="#fc6767"/>
            </filter>
          </defs>
          <circle id="spinner" style="fill:transparent;stroke:#dd2476;stroke-width: 7px;stroke-linecap: round;filter:url(#shadow);" cx="50" cy="50" r="45"/>
        </svg>
      </div>
    </div>
    <router-link to="/">
      <img src="./assets/Logo.svg" class="logo" id="logo" />
      <img src="./assets/LogoMobile.svg" class="logo" id="logo-mobile" />
    </router-link>
    <div id="nav">
      <div v-if="email">
        <router-link to="/account/user-profile" class="m-label">
          <span class="nav-label">{{ email }}</span>
        </router-link>
      </div>
      <div v-else>
        <router-link to="/authorize" class="m-label" v-if="$route.name !== 'Authorize'"><span class="nav-label">ВОЙТИ</span></router-link>
      </div>
    </div>
    <transition name="fade">
      <router-view />
    </transition>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  computed: {
    email() {
      return this.$store.state.client?.email;
    },
    
    isLoading() {
      return this.$store.state.isLoading;
    }
  },
});
</script>

<style scoped lang="scss">
#nav {
  top: 40px;
  right: 40px;
  position: absolute;
  z-index: 999;
}

.loading {
  position: fixed;
  background: rgba(0,0,0,0.2);
  height: 100vh;
  width: 100vw;
  top: 0;
  left: 0;
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 9999999;
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
  height: 86px;
  position: absolute;
  top: 20px;
  left: 40px;
  z-index: 999;
}

@media (max-width: 768px) {
  #logo-mobile {
    display: block;
  }

  #logo {
    display: none;
  }
}
@media (min-width: 768px) {
  #logo-mobile {
    display: none;
  }

  #logo {
    display: block;
  }
}

@keyframes animation {
  0% {
    stroke-dasharray: 1 98;
    stroke-dashoffset: -105;
  }
  50% {
    stroke-dasharray: 80 10;
    stroke-dashoffset: -160;
  }
  100% {
    stroke-dasharray: 1 98;
    stroke-dashoffset: -300;
  }
}

#container {
  width: 200px;
  height: 200px;
}

#spinner {
  transform-origin: center;
  animation-name: animation;
  animation-duration: 1.2s;
  animation-timing-function: cubic-bezier;
  animation-iteration-count: infinite;
}
</style>
