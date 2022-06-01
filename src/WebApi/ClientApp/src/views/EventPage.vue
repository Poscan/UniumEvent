<template>
  <div class="event-page">
    <preview-section :pictureUrl="image" label="ГОРОДСКАЯ АКАДЕМИЯ ЮНИУМ" />
    <section class="text-wrap" id="first">
      <TextLine>
        <template v-slot:header> 1 СМЕНА </template>

        <template v-slot:date>
          с 30 мая
          <br />
          по 10 июня
        </template>

        <template v-slot:description>
          Английский язык «Harry Potter and the students of Ю» - курс позволит погрузиться в атмосферу вселенной Гарри Поттера, а также хочешь выучить
          новую лексику, грамматику и применить ее на практике.
          <br />
          <br />
          Создание игр: дополненная и виртуальная реальность – научат программировать под различные платформы и создавать целые виртуальные миры, игры
          и приложения, поддерживающие данные технологии.
        </template>
      </TextLine>
    </section>

    <section class="site-wrap">
      <img src="../assets/First.svg" class="section-image" />
    </section>

    <section class="text-wrap">
      <TextLine>
        <template v-slot:header> 2 СМЕНА </template>

        <template v-slot:date>
          с 13 июня
          <br />
          по 24 июня
        </template>

        <template v-slot:description>
          Коллаж - техника, в которой сочетаются навыки рисования, лепки, вырезания, оцениваются основы колористики, графики и дизайна. Развивает
          креативность и мелкую моторику.
          <br />
          <br />
          Гитара для начинающих – ребенок научится базовым аккордам и техникам игры, играть по табулатурам, узнает как располагаются ноты на грифе
        </template>
      </TextLine>
    </section>

    <section class="site-wrap">
      <img src="../assets/Second.svg" class="section-image" />
    </section>

    <section class="text-wrap">
      <TextLine>
        <template v-slot:header> 3 СМЕНА </template>

        <template v-slot:date>
          с 27 июня
          <br />
          по 8 июля
        </template>

        <template v-slot:description>
          Актерское мастерство - в рамках курса ваш ребенок сможет: - обрести чувство уверенности в себе, избавиться от боязни сцены; - научится
          работать с дикцией, мимикой, пластикой, жестами, позами;
          <br />
          <br />
          Гитара для начинающих – ребенок научится базовым аккордам и техникам игры, играть по табулатурам, узнает как располагаются ноты на грифе
        </template>
      </TextLine>
    </section>

    <section class="site-wrap">
      <img src="../assets/Third.svg" class="section-image" />
    </section>

    <button v-if="isAuthorize" class="subscribe" @click="Subscribe">ОТПИСАТЬСЯ</button>
    <button v-else class="unsubscribe" @click="Unsubscribe">ЗАПИСАТЬСЯ</button>
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
import EventUser from "@/services/models/EventUser";
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
    };
  },
  
  computed: {
    isAuthorize() : boolean {
      return this.$store.state.client.id != 0;
    }
  },
  
  methods: {
    async Subscribe() {
      const response = (await ClientService.getCurrentUser()) as any;

      const user = new Client(response.data.data);
      const eventUser = new EventUser({event: this.event, client: user, id: 0});

      const res = await EventUserService.SignUpEvent(eventUser);
    },

    async Unsubscribe() {
      console.log("unsubscribe");
    }
  },
});
</script>

<style lang="scss" scoped>
@import "../../public/main";

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
}
@media (min-width: 768px) and (max-width: 991px) {
  .site-wrap {
    padding: 60px 100px;
  }
}
@media (min-width: 991px) and (max-width: 1199px) {
  .site-wrap {
    padding: 60px 200px;
  }
}
@media (min-width: 1200px) {
  .site-wrap {
    padding: 60px 300px;
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
  padding: 10px 20px;
  color: $green;
  font-size: 32px;
  border: 3px $green solid;
  border-radius: 3px;
  transition: 0.4s ease-out;

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
