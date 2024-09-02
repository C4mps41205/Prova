import React, { createContext, useContext, ReactNode } from 'react';
import AuthUseCase from "../../Application/UseCases/Auth.UseCase";
import JwtAdapter from "../../Infra/Adapters/Auth/Jwt/Jwt.Adapter";

const AuthContext = createContext<AuthUseCase | undefined>(undefined);

export const AuthProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const jwtAdapter = new JwtAdapter();
    const authUseCase = new AuthUseCase(jwtAdapter);

    return (
        <AuthContext.Provider value={authUseCase}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => {
    const context = useContext(AuthContext);
    if (context === undefined) {
        throw new Error('');
    }
    return context;
};
