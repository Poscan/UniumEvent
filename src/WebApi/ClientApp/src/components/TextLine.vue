<template>
  <div class="grid-container">
    <div class="grid-item" />
    <div class="grid-item">
      <pre class="l-label">
<span class="h-label"><slot name="header" /></span>

<slot name="date" />
      </pre>
    </div>
    <div class="grid-item" />
    <div class="grid-item m-label">
      <slot name="description" />
    </div>
    <div class="grid-item" />
  </div>
</template>

<script lang="ts">
export default {};
</script>

<style>
.grid-container {
  margin: 0 120px;
  display: grid;
  gap: 20px;
  grid-template-columns: 200px 200px 200px auto 200px;
}

.grid-item {
  display: grid;
}
</style>
