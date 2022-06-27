<template>
  <div>
    <input v-if="mask" class="edit-input m-label" v-mask="mask" type="text" :value="value"  @input="$emit('input', $event.target.value)" :placeholder="placeholder" />
    <input v-else class="edit-input m-label" type="text" :value="value"  @input="$emit('input', $event.target.value)" :placeholder="placeholder" />
  </div>
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: ["value", "placeholder", "mask"],
});
</script>

<style>
.edit-input {
  border: 2px #ddd solid;
  border-radius: 3px;
  padding: 3px 3px 3px 10px;
}
</style>
