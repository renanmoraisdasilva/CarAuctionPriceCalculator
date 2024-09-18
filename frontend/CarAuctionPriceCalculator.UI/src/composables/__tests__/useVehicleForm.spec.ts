import { describe, it, expect, beforeEach, vi } from 'vitest'
import { useVehicleForm } from '../useVehicleForm'
import axios from 'axios'

vi.mock('axios')
vi.mock('primevue/usetoast', () => ({
  useToast: vi.fn(() => ({
    add: vi.fn()
  }))
}))

const mockedAxios = vi.mocked(axios, true)

describe('useVehicleForm', () => {
  let composable: ReturnType<typeof useVehicleForm>

  beforeEach(() => {
    composable = useVehicleForm()
  })

  it('should fetch vehicle types', async () => {
    mockedAxios.get.mockResolvedValue({ data: mockVehicleTypes })

    await composable.fetchVehicleTypes()

    expect(composable.vehicleTypes.value).toEqual(mockVehicleTypes)
  })

  it('should calculate fees', async () => {
    mockedAxios.post.mockResolvedValue({ data: mockFees })

    composable.basePrice.value = 398
    composable.selectedVehicleType.value = '1'

    await composable.fetchFees()

    expect(composable.totalCost.value).toBe(550.76)
    expect(composable.fees.value).toEqual([
      {
        id: 1,
        name: 'Buyer',
        amount: 39.8
      },
      {
        id: 3,
        name: 'Seller',
        amount: 7.96
      },
      {
        id: 5,
        name: 'Association',
        amount: 5
      },
      {
        id: 9,
        name: 'Storage',
        amount: 100
      }
    ])
  })
})

const mockVehicleTypes = [
  { id: 1, name: 'Car' },
  { id: 2, name: 'Truck' }
]

const mockFees = {
  price: 550.76,
  fees: [
    {
      id: 1,
      feeType: {
        id: 1,
        name: 'Buyer'
      },
      calculatedFee: 39.8
    },
    {
      id: 3,
      feeType: {
        id: 2,
        name: 'Seller'
      },
      calculatedFee: 7.96
    },
    {
      id: 5,
      feeType: {
        id: 3,
        name: 'Association'
      },
      calculatedFee: 5
    },
    {
      id: 9,
      feeType: {
        id: 4,
        name: 'Storage'
      },
      calculatedFee: 100
    }
  ]
}
