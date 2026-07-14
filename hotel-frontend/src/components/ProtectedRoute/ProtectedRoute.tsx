import { Navigate } from 'react-router-dom';
import type { ReactNode } from 'react';
import { useAuth } from '../../contexts/AuthContext';

interface Props{
    children: ReactNode;
}

export function ProtectedRoute({children}: Props){
    const {token} = useAuth();

    if(!token){
        return <Navigate to="/login" />;
    }

    return children;
}