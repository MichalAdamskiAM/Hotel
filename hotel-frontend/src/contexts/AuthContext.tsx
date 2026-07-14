import { createContext, useContext, useState } from 'react'
import type { ReactNode } from 'react'

interface AuthContextType {
    token: string | null;
    saveToken: (token: string) => void;
    logout: () => void;
}

const AuthContext = createContext<AuthContextType | undefined> (undefined);

export function AuthProvider({ children }: { children: ReactNode }){
    const [token, setToken] = useState<string | null>(
        localStorage.getItem('token')
    );

    function saveToken(token: string){
        localStorage.setItem('token', token);
        setToken(token);
    }

    function logout(){
        localStorage.removeItem('token');
        setToken(null);
    }

    return (
        <AuthContext.Provider
            value={{
                token,
                saveToken,
                logout
            }}
        >
            {children}
        </AuthContext.Provider>
    )
}

export function useAuth(){
    const context = useContext(AuthContext);

    if(!context){
        throw new Error(
            "useAuth must be used inside AuthProvider"
        );
    }

    return context;
}