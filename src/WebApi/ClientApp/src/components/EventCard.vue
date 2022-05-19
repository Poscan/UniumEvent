<template>
  <div class="card-wrap">
    <div class="card-container center">
      <img :src="pictureName" width="650" height="650" />
      <transition name="sub-anim2">
        <div v-if="show" class="image-container">
          <img id="sub" :src="subPictureName" width="650" height="650" />
        </div>
      </transition>
    </div>
    <p @mouseenter="show = true" @mouseleave="show = false" class="header" id="event-header">
      <router-link to="/event-page">{{ label }}</router-link>
    </p>
  </div>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: {
    pictureName: String,
    subPictureName: String,
    label: String,
  },

  data() {
    return {
      show: false,
    };
  },
});
</script>

<style lang="scss">
@import "../../public/main";

.card-container {
  padding-top: 110px;
  width: 650px;
  height: 650px;
  overflow: hidden;
}

.image-container {
  overflow: hidden;
  margin-top: -654px;
  height: 650px;
}

.sub-anim2-enter-active {
  transition: all 1s cubic-bezier(0.65, 0, 0.35, 1);
}

.sub-anim2-leave-active {
  transition: all 1s cubic-bezier(0.65, 0, 0.35, 1);
}

.sub-anim2-enter,
.sub-anim2-leave-to {
  height: 0;
}
</style>
