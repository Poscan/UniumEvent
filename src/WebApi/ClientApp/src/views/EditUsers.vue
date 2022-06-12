<template>
  <div>
    <div class="header-edit">
      <div class="h-label">Модерация пользователей</div>
    </div>

    <div class="autocomplete mt-16">
      <input class="autocomplete-input m-label" ref="input" type="text" v-model="searchString"
             autocomplete="off"
             placeholder="Введите фио пользователя"
             @focusin="input = true" @focusout.passive="focusout">
      <div id="autocomplete-list" :style="{width: width - 4+ 'px'}" v-if="input && searchString.length > 0">
        <div v-if="clients.length > 0">
          <div v-for="client in clients" :key="client.id" class="m-label autocomplete-list-item"
               @click.prevent.stop="selectClient(client)">
            {{ client.lastName }}
            {{ client.firstName }}
            {{ client.patrName }}
          </div>
        </div>
        <div v-else class="m-label autocomplete-list-item">
          Ничего не нашлось(
        </div>
      </div>
    </div>

    <div class="mt-16" v-if="editClient.id !== 0">
      <div class="account-info-grid">
        <div class="account-info-column-right">Фамилия:</div>
        <EditInput v-model="editClient.lastName" placeholder="Иванов"/>
        <div class="account-info-column-right">Имя:</div>
        <EditInput v-model="editClient.firstName" placeholder="Иван"/>
        <div class="account-info-column-right">Отчество:</div>
        <EditInput v-model="editClient.patrName" placeholder="Иванович"/>
        <div class="account-info-column-right">Дата рождения:</div>
        <EditInput v-model="editClient.birthday" placeholder="01.01.1991"/>
        <div class="account-info-column-right">Телефон:</div>
        <EditInput v-model="editClient.mobilePhone" placeholder="+7 (966) 666 66-66"/>
        <div class="account-info-column-right">Email:</div>
        <EditInput v-model="editClient.email" placeholder="email@gmail.com"/>
        <div class="account-info-column-right">Школа:</div>
        <EditInput v-model="editClient.schoolNumber" placeholder="1"/>
      </div>

      <div class="h-label">Информация о родителях</div>
      <div class="account-info-grid">
        <div class="account-info-column-right">Фамилия:</div>
        <EditInput v-model="editClient.parentLastName" placeholder="Петров"/>
        <div class="account-info-column-right">Имя:</div>
        <EditInput v-model="editClient.parentFirstName" placeholder="Петр"/>
        <div class="account-info-column-right">Отчество:</div>
        <EditInput v-model="editClient.parentPatrName" placeholder="Петрович"/>
        <div class="account-info-column-right">Телефон:</div>
        <EditInput v-model="editClient.parentMobilePhone" placeholder="+7 (922) 222 22-22"/>
      </div>

      <div class="h-label">Роль пользователя</div>
      <div class="account-info-grid">
        <div class="account-info-column-right">Роль:</div>
        <select v-model="editClientRole" class="m-label select">
          <option v-for="role in roles" :key="role.roleId" :value="role" class="m-labe select-item">
            {{ role.roleName }}
          </option>
        </select>
      </div>

      <div class="flex-justify-content-end">
        <div class="button-save" @click="SaveClient">
          сохранить
        </div>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Client, {IClient} from "@/services/models/Client";
import Vue from "vue";
import ClientService from "@/services/ClientService";
import Role, {IRole} from "@/services/models/Role";
import RoleService from "@/services/RoleService";
import EditInput from "@/components/EditInput.vue";

export default Vue.extend({
      components: {
        EditInput
      },

      data() {
        return {
          editClient: new Client(),
          clientRole: new Role(),
          editClientRole: new Role(),
          clients: new Array<Client>(),
          roles: new Array<Role>(),
          searchString: "",
          input: false,
          width: 0,
        }
      },

      methods: {
        async SaveClient() {
          this.$store.state.isLoading = true;

          await ClientService.SaveUser(this.editClient);
          
          if(this.editClientRole.roleId !== this.clientRole.roleId) {
            await RoleService.deleteUserRole(this.editClient.userId, this.clientRole.roleId);
            await RoleService.addUserRole(this.editClient.userId, this.editClientRole.roleId);
          }

          this.$store.state.isLoading = false;
        },

        async FindClient(searchString: string) {
          const result = await ClientService.findCliens(searchString);

          if (result?.data.isSuccessful) {
            this.clients = result.data.data.map((x: IClient) => new Client(x));
          }
        },

        selectClient(client: Client) {
          this.editClient = client;
        },

        sleep(ms: number) {
          return new Promise(resolve => setTimeout(resolve, ms));
        },


        async focusout() {
          await this.sleep(200);
          this.input = false;
        },

        async fetchUserRole(userId: string) {
          const result = await RoleService.getUserRole(userId);

          if (result?.data.isSuccessful) {
            this.clientRole = new Role(result.data.data[0]);
            this.editClientRole = new Role(result.data.data[0]);
          }
        }
      },

      async mounted() {
        this.$store.state.isLoading = true;

        const result = await RoleService.getRoles() as any;

        if (result?.data.isSuccessful) {
          this.roles = result.data.data.map((x: IRole) => new Role(x));
        }

        const input = this.$refs.input;
        input?.focus();
        const size = input?.getBoundingClientRect();
        this.width = size.width;

        this.$store.state.isLoading = false;
      },

      watch: {
        searchString(newValue: string) {
          if(newValue.length > 0) {
            this.FindClient(newValue);
          }
        },

        editClient(newValue: IClient) {
          this.fetchUserRole(newValue.userId);
        }
      }
    }
);
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

.mt-16 {
  margin-top: 16px;
}

.flex-justify-content-end {
  display: flex;
  justify-content: flex-end;
}

.autocomplete {
  .autocomplete-input {
    padding: 10px;
    border: 2px solid #ddd;
    border-radius: 3px;
    width: calc(100% - 22px);

    &:active, &:focus {
      border: 2px solid #ddd;
      border-radius: 3px;
    }
  }

  #autocomplete-list {
    border: 2px solid #ddd;
    border-radius: 3px;
    position: absolute;
    z-index: 999999;
    margin-top: -2px;
    background: white;
  }

  .autocomplete-list-item {
    height: 30px;
    display: flex;
    padding: 10px;
    background: white;

    &:hover {
      background: gray;
      cursor: pointer;
    }
  }
}

.select {
  padding: 3px 10px;
  border: 2px solid #ddd;
  border-radius: 3px;
}

.select-item {
  padding: 3px 10px;
}

</style>
