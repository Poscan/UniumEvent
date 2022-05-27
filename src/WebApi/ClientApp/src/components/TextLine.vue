<template>
  <div class="grid-container">
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
  </div>
</template>

<script lang="ts">
export default {};
</script>

<style>
.grid-container {
  display: grid;
  gap: 20px;
  grid-template-columns: 1fr 1fr 3fr;
}

@media (max-width: 768px) {
  .grid-container {
    margin: 0 30px;
  }
}

@media (min-width: 768px) {
  .grid-container {
    margin: 0 120px;
  }
}

.grid-item {
  display: grid;
}
</style>
