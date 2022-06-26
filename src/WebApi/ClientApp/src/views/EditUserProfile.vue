<template>
  <div>
    <div class="header-edit">
      <div class="h-label">Информация о пользователе</div>
      <div class="button-save" @click="SaveClient" >
        сохранить
      </div>
    </div>
    <div class="account-info-grid">
      <div class="account-info-column-right">Фамилия:</div>
      <EditInput v-model="client.lastName" placeholder="Иванов" />
      <div class="account-info-column-right">Имя:</div>
      <EditInput v-model="client.firstName" placeholder="Иван" />
      <div class="account-info-column-right">Отчество:</div>
      <EditInput v-model="client.patrName" placeholder="Иванович" />
      <div class="account-info-column-right">Дата рождения:</div>
      <EditInput v-model="client.birthday" placeholder="01.01.1991" />
      <div class="account-info-column-right">Телефон:</div>
      <EditInput v-model="client.mobilePhone" placeholder="+7 (966) 666 66-66" />
      <div class="account-info-column-right">Email:</div>
      <EditInput v-model="client.email" placeholder="email@gmail.com" />
      <div class="account-info-column-right">Школа:</div>
      <EditInput v-model="client.schoolNumber" placeholder="1" />
    </div>

    <div class="h-label">Информация о родителях</div>
    <div class="account-info-grid">
      <div class="account-info-column-right">Фамилия:</div>
      <EditInput v-model="client.parentLastName" placeholder="Петров" />
      <div class="account-info-column-right">Имя:</div>
      <EditInput v-model="client.parentFirstName" placeholder="Петр" />
      <div class="account-info-column-right">Отчество:</div>
      <EditInput v-model="client.parentPatrName" placeholder="Петрович" />
      <div class="account-info-column-right">Телефон:</div>
      <EditInput v-model="client.parentMobilePhone" placeholder="+7 (922) 222 22-22" />
    </div>
  </div>
</template>

<script lang="ts">
import Client from "@/services/models/Client";
import Vue from "vue";
import EditInput from "@/components/EditInput.vue";
import ClientService from "@/services/ClientService";

export default Vue.extend({
  components: {
    EditInput,
  },
  
  data() {
    return {
      client: new Client()
    }
  },

  computed: {
    localeDate(): string {
      return this.client && this.client.birthday ? new Date(this.client.birthday).toLocaleDateString() : "";
    }
  },
  
  methods: {
    async SaveClient() {
      this.$store.state.isLoading = true;
      
      const result = await ClientService.SaveUser(this.client);
      if(result.isSuccessful) {
        await this.$router.push("/account/user-profile");
      }

      this.$store.state.isLoading = false;
    }
  },
  
  mounted() {
    const client = this.$store.state.client;
    this.client = client ? client : new Client();
  }
});
</script>

<style lang="scss">
@import "../../public/main.scss";

.button-save {
  padding: 10px;
  background: $green;
  color: white;
  border-radius: 3px;
  
  &:hover {
    cursor: pointer;
  }
}
</style>
