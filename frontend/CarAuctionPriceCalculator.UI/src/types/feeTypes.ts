export interface FeeType {
  id: number
  name: string
}

export interface Fee {
  id: number
  feeType: FeeType
  calculatedFee: number
}

export interface FeesResponse {
  price: number
  fees: Fee[]
}
