import ICrudDefault from "../../Domain/Repositories/ICrudDefault.Repository";
import {TaskToDo} from "../../Domain/Entities/TaskToDo.Entity";

export default class CategoryUseCase {
    private categoryRepository: ICrudDefault;

    /**
     * Initializes a new instance of the `CateogryUseCase` class with the provided `categoryRepository`.
     *
     * @param categoryRepository - The `ICrudDefault` repository to be used for user-related operations.
     */
    constructor(categoryRepository: ICrudDefault) {
        this.categoryRepository = categoryRepository;
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
    async CreateCategory(param: { name: string }): Promise<any> {
        if([undefined, null, ''].includes(param.name)) {
            throw new Error('Name are required');
        }

        return this.categoryRepository.Create(param);
    }

    async getAll() {
        return this.categoryRepository.GetAll({});
    }

    async AddCategory(categoryName: string) {
        return this.categoryRepository.Create({name: categoryName});
    }
}
