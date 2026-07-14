import { api } from './api';
import type { LoginRequest, LoginResponse } from '../types/User';

export async function login(data: LoginRequest) : Promise<LoginResponse> {
    const response = await api.post<LoginResponse>(
        '/Users/login',
        data
    );
    
    return response.data;
}