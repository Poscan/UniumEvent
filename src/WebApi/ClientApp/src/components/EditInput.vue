<template>
  <input class="edit-input m-label" type="text" v-model="value" :placeholder="placeholder" />
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: ["value", "placeholder"],
});
</script>

<style>
.edit-input {
  border: 2px #ddd solid;
  border-radius: 3px;
  padding: 3px 3px 3px 10px;
}
</style>
