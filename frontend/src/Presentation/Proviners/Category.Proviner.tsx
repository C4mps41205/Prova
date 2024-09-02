import React, { createContext, useContext, ReactNode } from 'react';
import CategoryAdapter from "../../Infra/Adapters/Category/Category.Adapter";
import CategoryUseCase from "../../Application/UseCases/Category.UseCase";

const CategoryContext = createContext<CategoryUseCase | undefined>(undefined);

export const CategoryProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
    const jwtAdapter = new CategoryAdapter();
    const categoryUseCase = new CategoryUseCase(jwtAdapter);

    return (
        <CategoryContext.Provider value={categoryUseCase}>
            {children}
        </CategoryContext.Provider>
    );
};

export const useCategory = () => {
    const context = useContext(CategoryContext);
    if (context === undefined) {
        throw new Error('');
    }
    return context;
};
