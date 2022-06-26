<template>
  <div class="event-page">
    <preview-section :pictureUrl="image" label="ГОРОДСКАЯ АКАДЕМИЯ ЮНИУМ"/>
    <img src="@/assets/MiniArrow.svg" height="20px" class="mini-arrow">
    <section class="text-wrap" id="first">
      <TextLine>
        <template v-slot:header>1 СМЕНА</template>

        <template v-slot:date>
          с 30 мая
          <br/>
          по 10 июня
        </template>

        <template v-slot:description>
          Английский язык «Harry Potter and the students of Ю» - курс позволит погрузиться в атмосферу вселенной Гарри
          Поттера, а также хочешь выучить
          новую лексику, грамматику и применить ее на практике.
          <br/>
          <br/>
          Создание игр: дополненная и виртуальная реальность – научат программировать под различные платформы и
          создавать целые виртуальные миры, игры
          и приложения, поддерживающие данные технологии.
        </template>
      </TextLine>
    </section>

    <section class="site-wrap">
      <img src="../assets/First.svg" class="section-image"/>
    </section>

    <section class="text-wrap">
      <TextLine>
        <template v-slot:header>2 СМЕНА</template>

        <template v-slot:date>
          с 13 июня
          <br/>
          по 24 июня
        </template>

        <template v-slot:description>
          Коллаж - техника, в которой сочетаются навыки рисования, лепки, вырезания, оцениваются основы колористики,
          графики и дизайна. Развивает
          креативность и мелкую моторику.
          <br/>
          <br/>
          Гитара для начинающих – ребенок научится базовым аккордам и техникам игры, играть по табулатурам, узнает как
          располагаются ноты на грифе
        </template>
      </TextLine>
    </section>

    <section class="site-wrap">
      <img src="../assets/Second.svg" class="section-image"/>
    </section>

    <section class="text-wrap">
      <TextLine>
        <template v-slot:header>3 СМЕНА</template>

        <template v-slot:date>
          с 27 июня
          <br/>
          по 8 июля
        </template>

        <template v-slot:description>
          Актерское мастерство - в рамках курса ваш ребенок сможет: - обрести чувство уверенности в себе, избавиться от
          боязни сцены; - научится
          работать с дикцией, мимикой, пластикой, жестами, позами;
          <br/>
          <br/>
          Гитара для начинающих – ребенок научится базовым аккордам и техникам игры, играть по табулатурам, узнает как
          располагаются ноты на грифе
        </template>
      </TextLine>
    </section>

    <section class="site-wrap">
      <img src="../assets/Third.svg" class="section-image"/>
    </section>

    <button v-if="eventUser.event.id !== 0" class="unsubscribe" @click="Unsubscribe">ОТПИСАТЬСЯ</button>
    <button v-else class="subscribe" @click="Subscribe">ЗАПИСАТЬСЯ</button>
  </div>
</template>

<script lang="ts">
import Vue from "vue";
import PreviewSection from "@/components/PreviewSection.vue";
import TextLine from "@/components/TextLine.vue";
import image from "../assets/Gau.svg";
import Event from "@/services/models/Event";
import ClientService from "@/services/ClientService";
import EventUserService from "@/services/EventUserService";
import EventUser, {IEventUser} from "@/services/models/EventUser";
import Client from "@/services/models/Client";

export default Vue.extend({
  components: {
    PreviewSection,
    TextLine,
  },

  data() {
    return {
      image: image,
      event: new Event({
        id: 1,
        name: "Городская Академия Юниум",
        pageName: "GAU",
        startDate: new Date(1, 6, 2022),
        endDate: new Date(1, 7, 2022)
      }),
      eventUser: new EventUser(),
    };
  },

  computed: {
    isAuthorize(): boolean {
      return this.$store.state.client.id != 0;
    }
  },

  methods: {
    async Subscribe() {
      this.$store.state.isLoading = true;

      const response = await ClientService.getCurrentUser();

      if (response.isSuccessful) {
        const user = new Client(response.data);
        const eventUser = new EventUser({event: this.event, client: user, id: 0});

        const result = await EventUserService.SignUpEvent(eventUser);

        if (result.isSuccessful) {
          this.eventUser = new EventUser(result.data);
        }
      } else {
        await this.$router.push("/authorize");
      }

      this.$store.state.isLoading = false;
    },

    async Unsubscribe() {
      this.$store.state.isLoading = true;

      const response = await EventUserService.DeleteEventUser(this.event.id);

      if (response.isSuccessful) {
        this.eventUser = new EventUser();
      }

      this.$store.state.isLoading = false;
    }
  },

  async mounted() {
    this.$store.state.isLoading = true;
    
    const response = await EventUserService.GetAllEvent(this.$store.state.client.id);
    
    if (response.isSuccessful) {
      const eventUsers = response.data?.map((x: IEventUser) => new EventUser(x)) ?? new Array<EventUser>();
      this.eventUser = eventUsers.find(x => x.event.id == this.event.id) ?? new EventUser();
    }

    this.$store.state.isLoading = false;
  }
});
</script>

<style lang="scss" scoped>
@import "../../public/main";

.mini-arrow {
  position: absolute;
  top: 95vh;
  left: 50%;
  transform: translateX(-50%);
}

.event-page {
  background: rgb(245, 245, 245);
  padding-top: 100vh;
  z-index: -999;
}

.site-wrap {
  margin: 0 auto;
  background: rgb(30, 100, 130);
}

@media (max-width: 768px) {
  .site-wrap {
    padding: 60px 30px;
  }
  
  .subscribe {
    padding: 5px 10px;
  }
}

@media (min-width: 768px) and (max-width: 991px) {
  .site-wrap {
    padding: 60px 100px;
  }

  .subscribe {
    padding: 7px 15px;
  }
}

@media (min-width: 991px) and (max-width: 1199px) {
  .site-wrap {
    padding: 60px 200px;
  }

  .subscribe {
    padding: 10px 20px;
  }
}

@media (min-width: 1200px) {
  .site-wrap {
    padding: 60px 300px;
  }

  .subscribe {
    padding: 10px 20px;
  }
}

.section-image {
  width: 100%;
}

.text-wrap {
  background: white;
  padding: 80px 0;
}

.ma {
  margin-top: 100px;
}

pre {
  font-family: "Roboto", sans-serif;
  white-space: pre-wrap;
  font-weight: 400;
  font-size: 32px;
  line-height: 62px;
}

.subscribe {
  font-family: "Roboto", sans-serif;
  font-weight: 400;
  position: fixed;
  right: 20px;
  bottom: 20px;
  color: $green;
  font-size: calc((100vw - 480px) / (1904 - 480) * (32 - 16) + 16px);;
  border: 3px $green solid;
  border-radius: 3px;
  transition: 0.4s ease-out;
  background: transparent;

  &:hover {
    background-color: $primary;
    border: 3px burlywood solid;
    color: burlywood;
  }
}

.unsubscribe {
  font-family: "Roboto", sans-serif;
  font-weight: 400;
  position: fixed;
  right: 20px;
  bottom: 20px;
  padding: 10px 20px;
  color: #888;
  font-size: 32px;
  border: 3px #888 solid;
  border-radius: 3px;
  transition: 0.4s ease-out;

  &:hover {
    background-color: $primary;
    border: 3px burlywood solid;
    color: burlywood;
  }
}
</style>
