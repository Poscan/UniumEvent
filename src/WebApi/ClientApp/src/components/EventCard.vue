<template>
  <div class="card-wrap">
    <div class="card-container center">
      <img class="event-image" :src="pictureName" />
      <transition name="sub-anim2">
        <div v-if="show" class="image-container">
          <img class="event-image" id="sub" :src="subPictureName" />
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

.card-wrap {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.card-container {
  padding-top: 110px;
  width: var(--size);
  height: var(--size);
  overflow: hidden;
}

.image-container {
  display: none;
  margin-top: calc(-1 * var(--size) - 4px);
  overflow: hidden;
  height: var(--size);
}

.event-image {
  width: var(--size);
  height: var(--size);
}

#event-header {
  a {
    transition: all 0.4s ease-in-out;
    color: black;
    
    &:hover {
      color: $primary;
    }
  }
}

@media (max-width: 500px) {
  .card-container {
    --size: 100%;
  }
}

@media (min-width: 500px) and (max-width: 768px) {
  .card-container {
    --size: 500px;
  }
}
@media (min-width: 768px) and (max-width: 991px) {
  .card-container {
    --size: 550px;
  }
}
@media (min-width: 991px) and (max-width: 1199px) {
  .card-container {
    --size: 600px;
  }
}
@media (min-width: 1200px) {
  .card-container {
    --size: 650px;
  }
}

@media (min-width: 768px) {
  .image-container {
    display: block;
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
}
</style>