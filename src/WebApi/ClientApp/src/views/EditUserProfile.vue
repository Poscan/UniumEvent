<template>
  <div>
    <div class="header-edit">
      <div class="h-label">Информация о пользователе</div>
      <div class="button-save" >
        сохранить
      </div>
    </div>
    <div class="account-info-grid">
      <div class="account-info-column-right">Фамилия:</div>
      <EditInput :value="client.lastName" placeholder="Иванов" />
      <div class="account-info-column-right">Имя:</div>
      <EditInput :value="client.firstName" placeholder="Иван" />
      <div class="account-info-column-right">Отчество:</div>
      <EditInput :value="client.patrName" placeholder="Иванович" />
      <div class="account-info-column-right">Дата рождения:</div>
      <EditInput :value="client.birthday" placeholder="01.01.1991" />
      <div class="account-info-column-right">Телефон:</div>
      <EditInput :value="client.mobilePhone" placeholder="+7 (966) 666 66-66" />
      <div class="account-info-column-right">Email:</div>
      <EditInput :value="client.email" placeholder="email@gmail.com" />
      <div class="account-info-column-right">Школа:</div>
      <EditInput :value="client.schoolNumber" placeholder="1" />
    </div>

    <div class="h-label">Информация о родителях</div>
    <div class="account-info-grid">
      <div class="account-info-column-right">Фамилия:</div>
      <EditInput :value="client.parentLastName" placeholder="Петров" />
      <div class="account-info-column-right">Имя:</div>
      <EditInput :value="client.parentFirstName" placeholder="Петр" />
      <div class="account-info-column-right">Отчество:</div>
      <EditInput :value="client.parentPatrName" placeholder="Петрович" />
      <div class="account-info-column-right">Телефон:</div>
      <EditInput :value="client.parentMobilePhone" placeholder="+7 (922) 222 22-22" />
    </div>
  </div>
</template>

<script lang="ts">
import Client from "@/services/models/Client";
import Vue from "vue";
import EditInput from "@/components/EditInput.vue";

export default Vue.extend({
  components: {
    EditInput,
  },

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
@import "../../public/main.scss";

.button-save {
  padding: 10px;
  background: $green;
  color: white;
  border-radius: 3px;
}
</style>
