import React, { createContext, useContext, ReactNode } from 'react';
import UserUseCase from "../../Application/UseCases/User.UseCase";
import UserAdapter from "../../Infra/Adapters/User/User.Adapter";

const UserContext = createContext<UserUseCase | undefined>(undefined);

export const UserProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const jwtAdapter = new UserAdapter();
    const userUseCase = new UserUseCase(jwtAdapter);

    return (
        <UserContext.Provider value={userUseCase}>
            {children}
        </UserContext.Provider>
    );
};

export const useUser = () => {
    const context = useContext(UserContext);
    if (context === undefined) {
        throw new Error('');
    }
    return context;
};
