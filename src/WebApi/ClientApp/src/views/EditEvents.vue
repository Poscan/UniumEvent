<template>
  <div>
    <div class="header-edit">
      <div class="h-label">Модерация мероприятий</div>
    </div>

    <div class="mt-16">
      <div v-for="event in events" :key="event.id" class="event-card">
        <edit-event-card :event="event"/>
      </div>
    </div>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import Event, {IEvent} from "@/services/models/Event";
import EditInput from "@/components/EditInput.vue";
import EventService from "@/services/EventService";
import EditEventCard from "@/views/EditEventCard.vue";

export default Vue.extend({
      components: {
        EditEventCard,
        EditInput
      },

      data() {
        return {
          editEvent: new Event(),
          events: new Array<Event>(),
        }
      },

      methods: {
        async SaveEvent() {
          this.$store.state.isLoading = true;

          this.$store.state.isLoading = false;
        },
      },

      async mounted() {
        this.$store.state.isLoading = true;

        const result = await EventService.GetAllEvents();

        if (result.isSuccessful) {
          this.events = result.data?.map((x: IEvent) => new Event(x)) ?? new Array<Event>();
        }

        this.$store.state.isLoading = false;
      },
    }
);
</script>

<style lang="scss">
@import "../../public/main.scss";

.event-card {
  border: 2px solid #ddd;
  border-radius: 3px;
  padding: 10px;
  margin-top: 10px;

  &:first-child {
    margin-top: 0;
  }
}

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
