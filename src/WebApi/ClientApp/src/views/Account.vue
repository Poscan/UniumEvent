<template>
  <div class="account-wrap">
    <div class="account-info m-label">
        <router-view />
    </div>

    <div>
      <div class="account-card">
        <div class="h-label">
          {{ client.lastName }}<br />
          {{ client.firstName }}
        </div>
        <div><img src="../assets/ManAvatar.svg" class="avatar" /></div>
      </div>
      <br />
      <div class="account-sidebar">
        <router-link
          to="/account/user-profile"
          :class="['account-sidebar-item', 'l-label', { select: $router.currentRoute.path === '/account/user-profile' || 
                                                                $router.currentRoute.path === '/account/edit-user-profile'  }]"
        >
          Информация о пользователе
        </router-link>
        <router-link
          to="/account/user-events"
          :class="['account-sidebar-item', 'l-label', { select: $router.currentRoute.path === '/account/user-events' }]"
        >
          События пользователя
        </router-link>
        <router-link
            to="/account/edit-users"
            :class="['account-sidebar-item', 'l-label', { select: $router.currentRoute.path === '/account/edit-users' }]"
        >
          Модерация пользователей
        </router-link>
        <router-link
            to="/account/edit-events"
            :class="['account-sidebar-item', 'l-label', { select: $router.currentRoute.path === '/account/edit-events' }]"
        >
          Модерация мероприятий
        </router-link>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import ClientService from "@/services/ClientService";
import Client from "@/services/models/Client";
import Vue from "vue";

export default Vue.extend({
  data() {
    return {
      client: new Client(),
    };
  },

  async mounted() {
    this.$store.state.isLoading = true;
    const client = (await ClientService.getCurrentUser()) as any;

    if (client?.data) {
      this.client = new Client(client.data.data);
      this.$store.state.client = this.client;
    }

    this.$store.state.isLoading = false;
  },
});
</script>

<style lang="scss">
.account-wrap {
  margin: 130px 340px 30px 340px;
  display: grid;
  gap: 20px;
  grid-template-columns: auto 400px;
}

.account-sidebar {
  border: 2px solid #ddd;
  border-radius: 3px;
  padding: 20px;
  display: flex;
  flex-direction: column;
  align-items: stretch;
}

.account-sidebar-item {
  padding: 10px 0;

  &.select {
    background: #ddd;
    padding-left: 10px;
  }
}

.account-info {
  border: 2px solid #ddd;
  border-radius: 3px;
  padding: 20px;
}

.account-info-grid {
  display: grid;
  gap: 20px 20px;
  grid-template-columns: 200px auto;
  margin: 20px 0;
}

.account-info-column-right {
  justify-self: end;
  align-self: center;
  color: rgb(98, 109, 122);
}

.account-card {
  display: flex;
  justify-content: space-between;
  flex-grow: 20;
  padding: 20px;
  border: 2px solid #ddd;
  border-radius: 3px;
}

.avatar {
  --height: 150px;
  margin-top: calc(-1 * ((var(--height) / 2)));
  margin-right: calc(-1 * ((var(--height) / 2)));
  border-radius: 50%;
  height: var(--height);
}

.fade-enter-active,
.fade-leave-active {
  transition: opacity 0.5s cubic-bezier(0.33, 1, 0.68, 1);
}

.fade-enter,
.fade-leave-active {
  opacity: 0;
}

.header-edit {
  display: flex;
  justify-content: space-between;
}
</style>
