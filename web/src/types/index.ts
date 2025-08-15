// Auth Types
interface ILoginRequest {
  username: string;
  password?: string;
}

interface ILoginResponse {
  token: string;
}

// Product Types
interface IProduct {
  id: number;
  name: string;
  price: number;
  createdAt: string;
}

interface IPaginatedResponse<T> {
  items: T[];
  totalCount: number;
  pageNumber: number;
  pageSize: number;
}

export type {
    ILoginRequest as LoginRequest,
    ILoginResponse as LoginResponse,
    IProduct as Product,
    IPaginatedResponse as PaginatedResponse
};
