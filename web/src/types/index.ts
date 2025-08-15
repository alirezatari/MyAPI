// Auth Types
export interface LoginRequest {
  username: string;
  password?: string;
}

export interface LoginResponse {
  token: string;
}

// Product Types
export interface Product {
  id: number;
  name: string;
  price: number;
  createdAt: string;
}

export interface PaginatedResponse<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}
