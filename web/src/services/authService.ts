import api from '../api/api';
import { type LoginRequest, type LoginResponse } from '../types';

const login = async (credentials: LoginRequest): Promise<string> => {
  const response = await api.post<LoginResponse>('/api/auth/login', credentials);
  const token = response.data.token;

  if (token) {
    localStorage.setItem('token', token);
  }

  return token;
};

const logout = () => {
  localStorage.removeItem('token');
};

const getToken = (): string | null => {
  return localStorage.getItem('token');
};

export const authService = {
  login,
  logout,
  getToken,
};
