import axios from "axios";
import ICrudDefault from "../../../Domain/Repositories/ICrudDefault.Repository";
import api from "../../../Presentation/Api/Api.Service";

export default class CategoryAdapter implements ICrudDefault {
    /**
     * Creates a new category in the system.
     * @param entity - The category entity to create.
     * @returns A Promise that resolves to the created category.
     * @throws Error - If there is an unexpected error during the creation process.
     */
    async Create(entity: any): Promise<any> {
        const userData = localStorage.getItem('user');
        const user = userData ? JSON.parse(userData) : { id: 0 };
        try {
            return await api.post<any>('/category', {
                Name: entity.name,
                UserId: user.id
            });
        } catch (error) {
            if (axios.isAxiosError(error)) {
                const message = error.response?.data?.message || 'Unexpected error';
                throw new Error(message);
            } else {
                throw new Error('Unexpected error');
            }
        }
    }

    Delete(id: number): Promise<any> {
        // Todo 
        return Promise.resolve(undefined);
    }

    /**
     * Retrieves all categories from the system.
     * @param filter - An optional object containing filters to apply to the category list.
     * @returns A Promise that resolves to an array of category objects.
     * @throws Error - If there is an unexpected error during the retrieval process.
     */
    async GetAll(filter: object): Promise<any> {
        try {
            const userData = localStorage.getItem('user');
            const user = userData ? JSON.parse(userData) : { id: 0 };
            
            return await api.get<any>('/category/' + user.id, {});
        } catch (error) {
            if (axios.isAxiosError(error)) {
                const message = error.response?.data?.message || 'Unexpected error';
                throw new Error(message);
            } else {
                throw new Error('Unexpected error');
            }
        }
    }

    GetById(id: number): Promise<any> {
        // Todo
        return Promise.resolve(undefined);
    }

    Update(entity: any): Promise<any> {
        // Todo
        return Promise.resolve(undefined);
    }

}
    