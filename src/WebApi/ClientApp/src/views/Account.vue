<template>
  <div class="account-wrap">
    <div class="account-content">
      <div class="account-info m-label">
        <router-view/>
      </div>

      <div>
        <div class="account-card">
          <div class="h-label">
            {{ getClient.lastName }}<br/>
            {{ getClient.firstName }}
          </div>
          <div><img :src="getClient.sex === 'Ж' ? GirlAvatar : ManAvatar" class="avatar"/></div>
        </div>
        <br/>
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
  </div>
</template>

<script lang="ts">
import ClientService from "@/services/ClientService";
import Client from "@/services/models/Client";
import Vue from "vue";
import ManAvatar from "@/assets/ManAvatar.svg";
import GirlAvatar from "@/assets/GirlAvatar.svg";
import {mapGetters} from "vuex";

export default Vue.extend({
  data() {
    return {
      GirlAvatar: GirlAvatar,
      ManAvatar: ManAvatar
    };
  },
  
  computed: {
    ...mapGetters(['getClient']),
  },

  async mounted() {
    this.$store.state.isLoading = true;
    const client = await ClientService.getCurrentUser();

    if (client.isSuccessful) {
      this.$store.state.client = new Client(client.data);
    }

    this.$store.state.isLoading = false;
  },
});
</script>

<style lang="scss">
.account-wrap {
  display: flex;
  justify-content: center;
}

.account-content {
  margin: 130px 0;
}

@media (max-width: 768px) {
  .account-content {
    margin: 130px 30px 30px 30px;
    width: 100%;
    display: flex;
    flex-direction: column-reverse;
  }
}

@media (min-width: 768px) {
  .account-content {
    display: grid;
    gap: 20px;
    grid-template-columns: 2fr 1fr;
  }
}

@media (min-width: 768px) and (max-width: 991px) {
  .account-content {
    width: 690px;
  }
}

@media (min-width: 991px) and (max-width: 1199px) {
  .account-content {
    width: 915px;
  }
}

@media (min-width: 1200px) {
  .account-content {
    width: 1125px;
  }
}

.account-sidebar {
  border: 2px solid #ddd;
  border-radius: 3px;
  padding: 20px;
  display: flex;
  flex-direction: column;
  align-items: stretch;
  margin-bottom: 20px;
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
  grid-template-columns: auto 1fr;
  margin: 20px 20px;
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
  --height: calc((100vw - 480px) / (1904 - 480) * (150 - 75) + 75px);
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
