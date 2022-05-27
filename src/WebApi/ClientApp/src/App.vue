<template>
  <div id="app">
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
        <router-link to="/authorize" class="m-label" v-if="$route.name != 'Authorize'"><span class="nav-label">ВОЙТИ</span></router-link>
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
      const email = this.$store.state.client?.email;

      return email;
    },
  },
});
</script>

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
</style>
