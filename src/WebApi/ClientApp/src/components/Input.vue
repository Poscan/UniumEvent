<template>
  <input v-model="value" :placeholder="placeholder" />
</template>

<script lang="ts">
import Vue from "vue";

export default Vue.extend({
  props: ["value", "placeholder"],
});
</script>

<style></style>
