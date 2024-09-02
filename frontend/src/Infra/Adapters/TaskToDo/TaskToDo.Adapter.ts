import axios from "axios";
import ICrudDefault from "../../../Domain/Repositories/ICrudDefault.Repository";
import api from "../../../Presentation/Api/Api.Service";

export default class TaskToDoAdapter implements ICrudDefault {
    /**
     * Creates a new task.
     *
     * @param entity - The task entity to create.
     * @returns A Promise that resolves to the created task.
     * @throws {Error} If the user is not found in local storage.
     * @throws {Error} If an unexpected error occurs during the create operation.
     */
    async Create(entity: any): Promise<any> {
        try {
            const userData: string | null = localStorage.getItem('user');
            let user: any = userData ? JSON.parse(userData) : null;

            if (user === null || user === undefined) {
                throw new Error('User not found');
            }

            return await api.post<any>('/tasktodo/', entity);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                const message = error.response?.data?.message || 'Unexpected error';
                throw new Error(message);
            } else {
                throw new Error('Unexpected error');
            }
        }
    }

    /**
     * Deletes a task by the specified ID.
     * 
     * @param id - The ID of the task to delete.
     * @returns A Promise that resolves to the result of the delete operation.
     * @throws {Error} If the user is not found in local storage.
     * @throws {Error} If an unexpected error occurs during the delete operation.
     */
    async Delete(id: number): Promise<any> {
        try {
            const userData: string | null = localStorage.getItem('user');
            let user: any = userData ? JSON.parse(userData) : null;

            if (user === null || user === undefined) {
                throw new Error('User not found');
            }

            return await api.delete<any>('/tasktodo/' + id);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                const message = error.response?.data?.message || 'Unexpected error';
                throw new Error(message);
            } else {
                throw new Error('Unexpected error');
            }
        }
    }

    /**
     * Retrieves all tasks for the authenticated user.
     * 
     * @param filter - An optional object containing filters to apply to the task list.
     * @returns A Promise that resolves to the list of tasks for the authenticated user.
     * @throws {Error} If the user is not found in local storage.
     * @throws {Error} If an unexpected error occurs during the retrieval operation.
     */
    async GetAll(filter: object): Promise<any> {
        try {
            const userData: string | null = localStorage.getItem('user');
            let user: any = userData ? JSON.parse(userData) : null;

            if (user === null || user === undefined) {
                throw new Error('User not found');
            }

            const params = new URLSearchParams(filter as any).toString();

            return await api.get<any>(`/tasktodo/${user.id}?${params}`);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                const message = error.response?.data?.message || 'Unexpected error';
                throw new Error(message);
            } else {
                throw new Error('Unexpected error');
            }
        }
    }

    /**
     * Retrieves a task by its ID for the authenticated user.
     * 
     * @param id - The ID of the task to retrieve.
     * @returns A Promise that resolves to the task with the specified ID.
     * @throws {Error} If the user is not found in local storage.
     * @throws {Error} If an unexpected error occurs during the retrieval operation.
     */
    async GetById(id: number): Promise<any> {
        try {
            const userData: string | null = localStorage.getItem('user');
            let user: any = userData ? JSON.parse(userData) : null;

            if (user === null || user === undefined) {
                throw new Error('User not found');
            }

            return await api.post<any>('/tasktodo/', id);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                const message = error.response?.data?.message || 'Unexpected error';
                throw new Error(message);
            } else {
                throw new Error('Unexpected error');
            }
        }
    }

    /**
     * Updates a task in the task management system.
     * 
     * @param entity - The task entity to be updated.
     * @returns A Promise that resolves to the updated task.
     * @throws {Error} If the userId is not provided.
     * @throws {Error} If an unexpected error occurs during the update operation.
     */
    async Update(entity: any): Promise<any> {
        try {
            const id = entity.userId || entity.UserId;
            if (!id) {
                throw new Error('UserId is required');
            }

            return await api.put<any>(`/tasktodo/${id}`, entity);
        } catch (error) {
            if (axios.isAxiosError(error)) {
                const message = error.response?.data?.message || 'Unexpected error';
                throw new Error(message);
            } else {
                throw new Error('Unexpected error');
            }
        }
    }
}
    