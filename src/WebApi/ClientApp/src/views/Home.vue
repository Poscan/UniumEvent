<template>
  <div id="container">
    <img id="pointer1" class="box-pointer" src="../assets/Arrow.svg" height="64px" />
    <EventCard :pictureName="pictrure" :subPictureName="subPicture" id="home" label="ГОРОДСКАЯ АКАДЕМИЯ ЮНИУМ" />
  </div>
</template>

<script lang="ts">
import { Vue } from "vue-property-decorator";
import EventCard from "@/components/EventCard.vue";
import picture from "../assets/Gau.svg";
import subPicture from "../assets/Gau.png";
import ClientService from "@/services/ClientService";
import Client from "@/services/models/Client";

export default Vue.extend({
  components: {
    EventCard,
  },

  data() {
    return {
      pictrure: picture,
      subPicture: subPicture,
    };
  },

  methods: {
    async loadUser() {
      this.$store.state.isLoading = true;
      
      const response = (await ClientService.getCurrentUser()) as any;

      if (response.data) {
        this.$store.state.client = new Client(response.data.data);
      }

      this.$store.state.isLoading = false;
    }
  },
  
  mounted() {
    const pointerElem = document.getElementById("pointer1");

    document.onmousemove = function (event) {
      const mouseX = event.pageX;
      const mouseY = event.pageY;

      const parentElement = document.getElementById("event-header");
      let pixels = 0;

      if (parentElement) {
        const styles = window.getComputedStyle(parentElement);
        const margin = parseFloat(styles["marginBottom"]);
        pixels = margin + 21;
      }

      const element = document.getElementById("home")?.getBoundingClientRect();
      const pX = ((element?.left ?? 0) + (element?.right ?? 0)) / 2;
      const pY = (element?.bottom ?? 0) - pixels;

      const pril = pX - mouseX;
      const prot = pY - mouseY;
      const gip = Math.sqrt(pril * pril + prot * prot);

      requestAnimationFrame(function movePointer() {
        if (pointerElem) {
          let degree = Math.acos(pril / gip);
          degree *= mouseY > pY ? -1 : 1;

          pointerElem.style.left = Math.floor(mouseX) - 32 + "px";
          pointerElem.style.top = Math.floor(mouseY) - 32 + "px";
          pointerElem.style.transform = "rotate(" + degree + "rad)";

          const target = event.target;
          if (target != null) {
            const cursor = window.getComputedStyle(target as Element)["cursor"];
            pointerElem.style.opacity = cursor == "pointer" ? "0" : "1";
          }
        }
      });
    };
    this.loadUser()
  },
});
</script>

<style lang="scss" scoped>
#container {
  display: flex;
  justify-content: center;
  align-items: center;
  height: 100vh;
  width: 100vw;
  margin: 0;
  position: absolute;
  top: 0;
  left: 0;
}

.box-pointer {
  position: absolute;
  pointer-events: none;
  transition: all 0.3s cubic-bezier(.5,.5,.5,1.2);
}

@media (max-width: 768px) {
  .box-pointer {
    display: none;
  }
}
</style>
