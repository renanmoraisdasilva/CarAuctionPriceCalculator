<script setup lang="ts">
import { useVehicleForm } from '@/composables/useVehicleForm'
import InputText from 'primevue/inputtext'
import IconField from 'primevue/iconfield'
import InputIcon from 'primevue/inputicon'
import Dropdown from 'primevue/dropdown'
import Toast from 'primevue/toast'
const { basePrice, selectedVehicleType, vehicleTypes, formattedTotalCost, formattedFees } =
  useVehicleForm()
</script>

<template>
  <Toast />
  <form class="vehicle-form">
    <div class="form-group">
      <label for="base-price">Vehicle Base Price:</label>
      <IconField>
        <InputIcon class="pi pi-dollar" />
        <InputText type="number" v-model="basePrice" placeholder="0.00" />
      </IconField>
    </div>
    <div class="form-group">
      <label for="vehicle-type">Vehicle Type:</label>
      <Dropdown id="vehicle-type" v-model="selectedVehicleType" :options="vehicleTypes" optionLabel="name" optionValue="id" placeholder="Select a Vehicle Type" />
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

input {
  width: 100%;
  padding: 0.5rem;
  font-size: 1rem;
  border: 1px solid #ccc;
  border-radius: 6px;
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
  border-radius: 6px;
  margin-bottom: 0.5rem;
  box-shadow: 0px 0px #0000,0px 0px #0000,0px 1px 2px 0px rgba(18,18,23,0.05);
}

.total-cost {
  margin-top: 1.5rem;
  font-size: 1.25rem;
  font-weight: bold;
  text-align: center;
  color: #333;
}
</style>
