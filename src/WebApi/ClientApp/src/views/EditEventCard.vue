<template>
  <div>
    <div class="event-card-header" @click="isOpen = !isOpen">
      <div>{{ event.name }}</div>
      <img src="@/assets/MiniArrow.svg" height="10px"
           :style="{transform: 'rotate(' + (isOpen ? 180 : 0) + 'deg)' }">
    </div>

    <div class="event-card-body l-label" v-if="isOpen">
      <div v-if="users.length > 0">
        <div v-for="user in users" :key="user.id" class="user-item">{{ user.lastName }} {{ user.firstName }}
          {{ user.patrName }}
        </div>
      </div>
      <div v-else class="no-user-item">
        На данное мероприятие еще нет записанных участников
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import EventUserService from "@/services/EventUserService";
import Event from "@/services/models/Event";
import Client from "@/services/models/Client";
import {IEventUser} from "@/services/models/EventUser";

export default Vue.extend({
  name: 'edit-event-card',
  props: {
    event: {
      type: Event
    },
  },

  data() {
    return {
      users: new Array<Client>(),
      isOpen: false,
    }
  },

  methods: {
    async fetchUsers() {
      this.$store.state.isLoading = true;
      
      const result = await EventUserService.GetAllUsers(this.event?.id);

      if (result.isSuccessful) {
        this.users = result.data?.map((x: IEventUser) => new Client(x.client)) ?? new Array<Client>();
      }

      this.$store.state.isLoading = false;
    }
  },

  watch: {
    isOpen(newValue: boolean) {
      if (newValue) {
        this.fetchUsers();
      }
    }
  }
});
</script>

<style lang="scss">
@import "../../public/main.scss";

.event-card-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.event-card-body {
}

.user-item {
  border: 2px solid #ddd;
  border-radius: 3px;
  margin: 10px 0;
  padding: 10px;
}

.no-user-item {
  border: 2px solid #ddd;
  border-radius: 3px;
  margin: 10px 0;
  padding: 10px;
  display: flex;
  justify-content: center;
}

</style>