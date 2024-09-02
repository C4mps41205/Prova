import axios from "axios";
import ICrudDefault from "../../../Domain/Repositories/ICrudDefault.Repository";
import api from "../../../Presentation/Api/Api.Service";

export default class UserAdapter implements ICrudDefault {
    /**
     * Creates a new user in the system.
     *
     * @param entity - The user entity to be created, containing the name and password.
     * @returns A Promise that resolves to the created user data.
     * @throws {Error} If there is an unexpected error during the creation process.
     */
    async Create(entity: any): Promise<any> {
        try {
            return await api.post<any>('/user', {
                Name: entity.name,
                PasswordHash: entity.password,
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

    async GetAll(filter: object): Promise<any> {
        // Todo
        return Promise.resolve(undefined);
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
    