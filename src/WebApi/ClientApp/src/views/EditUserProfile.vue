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
      <EditInput :mask="'##.##.####'" @input="(value) => updateDate(value)" v-model="dateString" placeholder="01.01.1991" />
      <div class="account-info-column-right">Телефон:</div>
      <EditInput :mask="'+7 (###) ### ##-##'" v-model="client.mobilePhone" placeholder="+7 (966) 666 66-66" />
      <div class="account-info-column-right">Email:</div>
      <EditInput v-model="client.email" placeholder="email@gmail.com" />
      <div class="account-info-column-right">Школа:</div>
      <EditInput v-model="client.schoolNumber" placeholder="1" />
      <div class="account-info-column-right">Пол:</div>
      <EditInput v-model="client.sex" placeholder="1" />
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
      <EditInput :mask="'+7 (###) ### ##-##'" v-model="client.parentMobilePhone" placeholder="+7 (922) 222 22-22" />
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
      client: new Client(),
      dateString: ""
    }
  },
  
  methods: {
    async SaveClient() {
      this.$store.state.isLoading = true;
      
      const result = await ClientService.SaveUser(this.client);
      if(result.isSuccessful) {
        const response = await ClientService.getCurrentUser();
        
        if(response.isSuccessful){
          this.$store.state.client = new Client(response.data);
        }
        await this.$router.push("/account/user-profile");
      }

      this.$store.state.isLoading = false;
    },

    updateDate(newValue: string) {
      if(newValue.length === 10) {
        const num = newValue.split('.');
        const date = new Date(num[2] + '-' + num[1] + '-' + num[0] + 'T00:00:00Z');
        this.client.birthday = date;
        console.log(date);
      }
    }
  },
  
  mounted() {
    const client = this.$store.state.client;
    this.client = client ? new Client(client) : new Client();
    this.dateString = this.client && this.client.birthday ? new Date(this.client.birthday).toLocaleDateString() : "";
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
