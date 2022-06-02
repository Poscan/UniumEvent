<template>
  <div class="previewSection" ref="prev">
    <div class="center st" :style="{ clip: 'rect(' + y + 'px, ' + (x + width) + 'px, ' + (y + height) + 'px, ' + x + 'px)' }">
      <div class="img">
        <img :src="pictureUrl" id="preview-image" />
      </div>
      <p class="text text-in-block">{{ label }}</p>
    </div>
    <p class="text text-out-block">{{ label }}</p>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: {
    label: String,
    pictureUrl: String,
  },

  data() {
    return {
      x: 0,
      y: 0,
      width: 650,
      height: 650,
      imageHeight: 650,
    };
  },

  methods: {
    onScroll() {
      const y = window.scrollY;
      const elem = this.$refs["prev"] as HTMLElement;
      const widthSection = elem?.clientWidth;
      const heightSection = elem?.clientHeight;

      if (y > heightSection && this.y + this.height == heightSection) return;

      var k = (heightSection - this.imageHeight) / heightSection;
      this.height = k * y + this.imageHeight;

      k = (widthSection - this.imageHeight) / heightSection;
      this.width = k * y + this.imageHeight;

      this.x = widthSection / 2 - this.width / 2;
      this.y = heightSection / 2 - this.height / 2;
    },
  },

  mounted() {
    document.onreadystatechange = () => {
      if (document.readyState == "complete") {
        var height = document.getElementById("preview-image")?.clientHeight;

        this.imageHeight = height ?? 650;
        this.height = height ?? 650;
        this.width = height ?? 650;
        this.onScroll();
      }
    };

    var height = document.getElementById("preview-image")?.clientHeight;

    this.imageHeight = height ?? 650;
    this.height = height ?? 650;
    this.width = height ?? 650;
    this.onScroll();
    window.addEventListener("scroll", this.onScroll);

    window.addEventListener(
      "resize",
      () => {
        var height = document.getElementById("preview-image")?.clientHeight;

        this.imageHeight = height ?? 650;
        this.height = height ?? 650;
        this.width = height ?? 650;
        this.onScroll();
      },
      true,
    );
  },

  destroyed() {
    window.addEventListener("scroll", this.onScroll);
  },
});
</script>

<style lang="scss">
@import "../../public/main";

.st {
  background: $primary;
  clip: rect(160px, 1250px, 810px, 600px);
  position: absolute;
  width: 100vw;
  height: 100vh;
}

.previewSection {
  display: flex;
  position: absolute;
  top: 0;
  height: 100vh;
  width: 100vw;
  z-index: 0;
}

#preview-image {
  --size: 650px;
  width: var(--size);
}

@media (max-width: 500px) {
  #preview-image {
    --size: 100%;
  }
}

@media (min-width: 500px) and (max-width: 768px) {
  #preview-image {
    --size: 500px;
  }
}
@media (min-width: 768px) and (max-width: 991px) {
  #preview-image {
    --size: 550px;
  }
}
@media (min-width: 991px) and (max-width: 1199px) {
  #preview-image {
    --size: 600px;
  }
}
@media (min-width: 1200px) {
  #preview-image {
    --size: 650px;
  }
}

.img {
  position: absolute;
  left: 50%;
  top: 50%;
  transform: translate3d(-50%, -50%, 0);
  z-index: 0;
}

.text {
  margin: 0;
  left: 0;
  font-family: "Montserrat Alternates", sans-serif;
  // font-family: "Open Sans", sans-serif;
  // font-family: "Roboto", sans-serif;
  color: black;
  font-weight: 900;
  font-size: calc((100vw - 480px) / (1904 - 480) * (150 - 50) + 50px);
  line-height: calc((100vw - 480px) / (1904 - 480) * (236 - 100) + 100px);
  text-align: center;
  letter-spacing: 0.045em;
  padding: 0 10px;
}

.text-in-block {
  top: 50%;
  transform: translateY(-50%);
  -webkit-text-fill-color: transparent;
  -webkit-text-stroke: calc((100vw - 480px) / (1904 - 480) * (8 - 2) + 2px) burlywood;
  color: burlywood;
  z-index: 0;
  position: absolute;
}

.text-out-block {
  -webkit-text-fill-color: transparent;
  -webkit-text-stroke: calc((100vw - 480px) / (1904 - 480) * (8 - 2) + 2px) black;
  top: 50%;
  transform: translateY(-50%);
  z-index: -2;
  position: absolute;
}
</style>
