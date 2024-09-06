// src/composables/useVehicleForm.ts
import { ref, watch, computed } from 'vue'
import axios from 'axios'
import debounce from 'lodash/debounce'
import type { FeesResponse } from '@/types/feeTypes'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL

export const useVehicleForm = () => {
  const basePrice = ref(0)
  const vehicleType = ref('')
  const vehicleTypes = ref<{ id: number; name: string }[]>([])
  const fees = ref<{ id: number; name: string; amount: number }[]>([])
  const totalCost = ref(0)

  const fetchVehicleTypes = async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}/CarAuction/vehicleTypes`)
      vehicleTypes.value = response.data
    } catch (error) {
      console.error('Error fetching vehicle types:', error)
    }
  }

  const fetchFees = async () => {
    if (basePrice.value > 0 && vehicleType.value) {
      try {
        const response = await axios.post<FeesResponse>(`${API_BASE_URL}/CarAuction/calculate`, {
          vehiclePrice: basePrice.value,
          vehicleTypeId: vehicleType.value
        })
        totalCost.value = response.data.price
        fees.value = response.data.fees.map((fee) => ({
          id: fee.id,
          name: fee.feeType.name,
          amount: fee.calculatedFee
        }))
      } catch (error) {
        console.error('Error fetching fees:', error)
      }
    } else {
      totalCost.value = 0
      fees.value = []
    }
  }

  const debouncedFetchFees = debounce(fetchFees, 300)

  watch([basePrice, vehicleType], () => {
    debouncedFetchFees()
  })

  fetchVehicleTypes()

  const formatCurrency = (value: number) => {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD'
    }).format(value)
  }

  const formattedTotalCost = computed(() => formatCurrency(totalCost.value))
  const formattedFees = computed(() =>
    fees.value.map((fee) => ({
      ...fee,
      amount: formatCurrency(fee.amount)
    }))
  )

  return {
    basePrice,
    vehicleType,
    vehicleTypes,
    fees,
    totalCost,
    formattedTotalCost,
    formattedFees,
    fetchVehicleTypes,
    fetchFees
  }
}
