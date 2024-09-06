<script setup lang="ts">
import { useVehicleForm } from '@/composables/useVehicleForm'
import InputText from 'primevue/inputtext'
import IconField from 'primevue/iconfield'
import InputIcon from 'primevue/inputicon'
const { basePrice, vehicleType, vehicleTypes, formattedTotalCost, formattedFees } = useVehicleForm()
</script>

<template>
  <form class="vehicle-form">
    <div class="form-group">
      <label for="base-price">Vehicle Base Price:</label>
      <IconField>
        <InputIcon class="pi pi-dollar" />
        <InputText v-model="basePrice" placeholder="Search" />
      </IconField>
    </div>
    <div class="form-group">
      <label for="vehicle-type">Vehicle Type:</label>
      <select id="vehicle-type" v-model="vehicleType">
        <option v-for="type in vehicleTypes" :key="type.id" :value="type.id">
          {{ type.name }}
        </option>
      </select>
    </div>
    <div class="fees-section">
      <h3>Fees:</h3>
      <ul>
        <li v-for="fee in formattedFees" :key="fee.id">{{ fee.name }}: {{ fee.amount }}</li>
      </ul>
    </div>
    <div class="total-cost">
      <h3>Total Cost: {{ formattedTotalCost }}</h3>
    </div>
  </form>
</template>

<style scoped>
.vehicle-form {
  width: 500px;
  margin: 2rem auto;
  padding: 1rem;
  border: 1px solid #ccc;
  border-radius: 8px;
  background-color: #f9f9f9;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.form-group {
  margin-bottom: 1rem;
}

label {
  display: block;
  margin-bottom: 0.5rem;
  font-weight: bold;
  color: #333;
}

input,
select {
  width: 100%;
  padding: 0.5rem;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 4px;
}

.fees-section {
  margin-top: 1.5rem;
}

.fees-section h3 {
  margin-bottom: 0.5rem;
}

.fees-section ul {
  list-style-type: none;
  padding: 0;
}

.fees-section li {
  background-color: #fff;
  padding: 0.5rem;
  border: 1px solid #ccc;
  border-radius: 4px;
  margin-bottom: 0.5rem;
}

.total-cost {
  margin-top: 1.5rem;
  font-size: 1.25rem;
  font-weight: bold;
  text-align: center;
  color: #333;
}
</style>
