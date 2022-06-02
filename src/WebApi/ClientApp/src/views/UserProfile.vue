<template>
  <div>
    <div class="header-edit">
      <div class="h-label">Информация о пользователе</div>
      <router-link to="edit-user-profile">
        <div class="button-edit">
          Редактировать
        </div>
      </router-link>
    </div>
    <div class="account-info-grid">
      <div class="account-info-column-right">Фамилия:</div>
      <div>{{ client.lastName }}</div>
      <div class="account-info-column-right">Имя:</div>
      <div>{{ client.firstName }}</div>
      <div class="account-info-column-right">Отчество:</div>
      <div>{{ client.patrName }}</div>
      <div class="account-info-column-right">Дата рождения:</div>
      <div>{{ localeDate }}</div>
      <div class="account-info-column-right">Телефон:</div>
      <div>{{ client.mobilePhone }}</div>
      <div class="account-info-column-right">Email:</div>
      <div>{{ client.email }}</div>
      <div class="account-info-column-right">Школа:</div>
      <div>{{ client.schoolNumber }}</div>
    </div>

    <div class="h-label">Информация о родителях</div>
    <div class="account-info-grid">
      <div class="account-info-column-right">Фамилия:</div>
      <div>{{ client.parentLastName }}</div>
      <div class="account-info-column-right">Имя:</div>
      <div>{{ client.parentFirstName }}</div>
      <div class="account-info-column-right">Отчество:</div>
      <div>{{ client.parentPatrName }}</div>
      <div class="account-info-column-right">Телефон:</div>
      <div>{{ client.parentMobilePhone }}</div>
    </div>
  </div>
</template>

<script lang="ts">
import Client from "@/services/models/Client";
import Vue from "vue";

export default Vue.extend({
  computed: {
    localeDate(): string {
      return this.client && this.client.birthday ? new Date(this.client.birthday).toLocaleDateString() : "";
    },

    client(): Client {
      const client = this.$store.state.client;
      return client ? client : new Client();
    },
  },
});
</script>

<style lang="scss">
@import "@/../public/main";

.button-edit {
  padding: 10px;
  background: $primary;
  color: white;
  border-radius: 3px;
}
</style>
