<template>
  <div id="container">
    <img id="pointer1" class="box-pointer" src="Arrow.svg" height="64px" />
    <EventCard pictureName="Gau.svg" subPictureName="Gau.png" id="home" label="ГОРОДСКАЯ АКАДЕМИЯ ЮНИУМ" />
  </div>
</template>

<script lang="ts">
import { Vue } from "vue-property-decorator";
import EventCard from "@/components/EventCard.vue";

export default Vue.extend({
  components: {
    EventCard,
  },

  mounted() {
    var pointerElem = document.getElementById("pointer1");

    document.onmousemove = function (event) {
      var mouseX = event.pageX;
      var mouseY = event.pageY;

      var parentElement = document.getElementById("event-header");
      var pixels = 0;

      if (parentElement) {
        var styles = window.getComputedStyle(parentElement);
        var margin = parseFloat(styles["marginBottom"]);
        pixels = margin + 21;
      }

      var element = document.getElementById("home")?.getBoundingClientRect();
      var pX = ((element?.left ?? 0) + (element?.right ?? 0)) / 2;
      var pY = (element?.bottom ?? 0) - pixels;

      var pril = pX - mouseX;
      var prot = pY - mouseY;
      var gip = Math.sqrt(pril * pril + prot * prot);

      requestAnimationFrame(function movePointer() {
        if (pointerElem) {
          var degree = Math.acos(pril / gip);
          degree *= mouseY > pY ? -1 : 1;

          pointerElem.style.left = Math.floor(mouseX) - 32 + "px";
          pointerElem.style.top = Math.floor(mouseY) - 32 + "px";
          pointerElem.style.transform = "rotate(" + degree + "rad)";
        }
      });
    };
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
  transition: all 0.1s ease;
}
</style>
