import { describe, it, expect, beforeEach, vi } from 'vitest'
import { useVehicleForm } from '../useVehicleForm'
import axios from 'axios'

vi.mock('axios')

const mockedAxios = vi.mocked(axios, true)

describe('useVehicleForm', () => {
  let composable: ReturnType<typeof useVehicleForm>

  beforeEach(() => {
    composable = useVehicleForm()
  })

  it('should fetch vehicle types', async () => {
    const mockVehicleTypes = [
      { id: 1, name: 'Car' },
      { id: 2, name: 'Truck' }
    ]
    mockedAxios.get.mockResolvedValue({ data: mockVehicleTypes })

    await composable.fetchVehicleTypes()

    expect(composable.vehicleTypes.value).toEqual(mockVehicleTypes)
  })

  it('should calculate fees', async () => {
    const mockFees = {
      price: 398,
      fees: [{ id: 1, feeType: { name: 'Basic' }, calculatedFee: 39.8 }]
    }
    mockedAxios.post.mockResolvedValue({ data: mockFees })

    composable.basePrice.value = 398
    composable.vehicleType.value = '1'

    await composable.fetchFees()

    expect(composable.totalCost.value).toBe(398)
    expect(composable.fees.value).toEqual([{ id: 1, name: 'Basic', amount: 39.8 }])
  })
})
