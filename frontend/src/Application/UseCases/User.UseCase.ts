import ICrudDefault from "../../Domain/Repositories/ICrudDefault.Repository";

export default class UserUseCase {
    private userRepository: ICrudDefault;

    /**
     * Initializes a new instance of the `UserUseCase` class with the provided `userRepository`.
     *
     * @param userRepository - The `ICrudDefault` repository to be used for user-related operations.
     */
    constructor(userRepository: ICrudDefault) {
        this.userRepository = userRepository;
    }

    /**
     * Creates a new user with the provided name and password.
     *
     * @param param - An object containing the name and password of the new user.
     * @param param.name - The name of the new user.
     * @param param.password - The password of the new user.
     * @returns A Promise that resolves with the result of creating the new user.
     * @throws {Error} If the name or password is undefined, null, or an empty string.
     */
    async CreateUser(param: { password: string; name: string }): Promise<any> {
        if([undefined, null, ''].includes(param.name) || [undefined, null, ''].includes(param.password)) {
            throw new Error('Name and password are required');
        }
        
        return this.userRepository.Create(param);
    }
}
