import IAuthDefault from "../../../../Domain/Repositories/IAuthDefault.Repository";
import api from "../../../../Presentation/Api/Api.Service";
import axios from "axios";

export default class JwtAdapter implements IAuthDefault {

    /**
     * Authenticates a user with the provided name and password.
     *
     * @param name - The name of the user.
     * @param password - The password of the user.
     * @returns A promise that resolves with the authentication message if successful, or throws an error if there is an issue.
     */
    async Authenticate(name: string, password: string): Promise<any> {
        try {
            const response = await api.post<any>('/auth', {
                Name: name,
                Password: password,
            });

            const token = response.data.data.token;
            const user = response.data.data.user;

            if (user) {
                localStorage.setItem('user', JSON.stringify(user));
            }

            if (token) {
                localStorage.setItem('authToken', token);
            }

            api.defaults.headers.common['Authorization'] = `Bearer ${token}`;
            return response.data.message;
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
    