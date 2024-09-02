import IAuthDefault from '../../Domain/Repositories/IAuthDefault.Repository';

export default class AuthUseCase {
    private authRepository: IAuthDefault;

    constructor(authRepository: IAuthDefault) {
        this.authRepository = authRepository;
    }

    async Authenticate(name: string, password: string): Promise<string> {
        return await this.authRepository.Authenticate(name, password);
    }
}
