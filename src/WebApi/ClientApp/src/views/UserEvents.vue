<template>
  <div>
    <div class="h-label" style="margin-bottom: 20px">Мероприятия с вашим участием</div>
    <div v-if="eventUsers.length > 0">
      <div v-for="eventUser in eventUsers" :key="eventUser.event.id" class="event-item">
        <div>
          {{ eventUser.event.name }}
        </div>
        <div>
          {{ eventUser.event.startDate.toLocaleDateString() }}
        </div>
        <button class="unsubscribe-button l-label" @click="unsubscribe(eventUser.event.id)">Отписаться</button>
      </div>
    </div>
    <div v-else class="l-label">К сожалению вы еще не записывались ни на одно событие, но вы можете быстро это
      исправить
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import EventUserService from "@/services/EventUserService";
import EventUser, {IEventUser} from "@/services/models/EventUser";

export default Vue.extend({
  data() {
    return {
      eventUsers: new Array<EventUser>(),
    };
  },

  methods: {
    async unsubscribe(eventId: number) {
      this.$store.state.isLoading = true;
      
      const response = await EventUserService.DeleteEventUser(eventId);

      if (response.isSuccessful) {
        const index = this.eventUsers.findIndex(x => x.event.id == eventId);
        this.eventUsers.splice(index, 1);
      }

      this.$store.state.isLoading = false;
    }
  },

  async mounted() {
    this.$store.state.isLoading = true;
    const clientId = this.$store.state.client.id;
    const response = await EventUserService.GetAllEvent(clientId);

    if(response.isSuccessful) {
      this.eventUsers = response.data?.map((x: IEventUser) => new EventUser(x)) ?? new Array<EventUser>();
    }

    this.$store.state.isLoading = false;
  }
});
</script>

<style lang="scss">
.event-item {
  display: flex;
  justify-content: space-between;
  align-items: center;
  border: 2px #ddd solid;
  border-radius: 3px;
  padding: 10px;
}

.unsubscribe-button {
  border: none;
  padding: 10px;

  transition: background-color 0.2s ease-in-out;

  &:hover {
    cursor: pointer;
    background: #ccc;
  }
}
</style>
