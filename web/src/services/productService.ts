import api from '../api/api';
import { Product, PaginatedResponse } from '../types';

interface GetProductsParams {
  pageNumber: number;
  pageSize: number;
}

const getProducts = async ({ pageNumber, pageSize }: GetProductsParams): Promise<PaginatedResponse<Product>> => {
  const response = await api.get<PaginatedResponse<Product>>('/api/product', {
    params: { pageNumber, pageSize },
  });
  return response.data;
};

const createProduct = async (productData: { name: string; price: number }): Promise<Product> => {
  const response = await api.post<Product>('/api/product', productData);
  return response.data;
};

const updateProduct = async (id: number, productData: { name: string; price: number, id: number }): Promise<void> => {
  await api.put(`/api/product/${id}`, productData);
};

const deleteProduct = async (id: number): Promise<void> => {
  await api.delete(`/api/product/${id}`);
};

export const productService = {
  getProducts,
  createProduct,
  updateProduct,
  deleteProduct,
};
