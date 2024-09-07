import { ref, watch, computed } from 'vue'
import axios, { AxiosError } from 'axios'
import debounce from 'lodash/debounce'
import type { FeesResponse } from '@/types/feeTypes'
import { useToast } from 'primevue/usetoast'

const API_BASE_URL = import.meta.env.VITE_API_BASE_URL

export const useVehicleForm = () => {
  const basePrice = ref(0)
  const selectedVehicleType = ref('')
  const vehicleTypes = ref<{ id: number; name: string }[]>([])
  const fees = ref<{ id: number; name: string; amount: number }[]>([])
  const totalCost = ref(0)
  const errorMessages = ref<string[]>([])
  const toast = useToast()

  const fetchVehicleTypes = async () => {
    try {
      const response = await axios.get(`${API_BASE_URL}/CarAuction/vehicleTypes`)
      vehicleTypes.value = response.data
    } catch (error) {
      handleError(error, 'Error fetching vehicle types')
    }
  }

  const fetchFees = async () => {
    if (basePrice.value > 0 && selectedVehicleType.value) {
      try {
        const response = await axios.post<FeesResponse>(`${API_BASE_URL}/CarAuction/calculate`, {
          vehiclePrice: basePrice.value,
          vehicleTypeId: selectedVehicleType.value
        })
        updateFees(response.data)
        errorMessages.value = []
      } catch (error) {
        handleError(error, 'Error fetching fees')
      }
    } else {
      resetFees()
    }
  }

  const updateFees = (data: FeesResponse) => {
    totalCost.value = data.price
    fees.value = data.fees.map((fee) => ({
      id: fee.id,
      name: fee.feeType.name,
      amount: fee.calculatedFee
    }))
  }

  const resetFees = () => {
    totalCost.value = 0
    fees.value = []
  }

  const handleError = (error: unknown, defaultMessage: string) => {
    console.error(defaultMessage, error)
    if (error instanceof AxiosError && error.response?.data?.errors) {
      errorMessages.value = Object.values(error.response.data.errors).flat() as string[]
      errorMessages.value.forEach((message) => {
        toast.add({ severity: 'error', summary: 'Error', detail: message, life: 3000 })
      })
    }
  }

  const debouncedFetchFees = debounce(fetchFees, 300)

  watch([basePrice, selectedVehicleType], () => {
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
    selectedVehicleType,
    vehicleTypes,
    fees,
    totalCost,
    formattedTotalCost,
    formattedFees,
    fetchVehicleTypes,
    fetchFees,
    errorMessages
  }
}
