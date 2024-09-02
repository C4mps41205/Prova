import ICrudDefault from "../../Domain/Repositories/ICrudDefault.Repository";

export default class TaskToDoUseCase {
    private taskToDoRepository: ICrudDefault;

    /// <summary>
    /// Initializes a new instance of the <see cref="TaskToDoUseCase"/> class.
    /// </summary>
    /// <param name="taskToDoRepository">The task to do repository.</param>
    constructor(taskToDoRepository: ICrudDefault) {
        this.taskToDoRepository = taskToDoRepository;
    }

    /**
     * Retrieves all tasks from the task repository, optionally filtered by the provided object.
     *
     * @param filter - An object containing the filter criteria to apply to the task retrieval.
     * @returns A Promise that resolves to an array of tasks matching the filter criteria.
     */
    async GetAll(filter: object): Promise<any> {
        return this.taskToDoRepository.GetAll(filter);
    }

    /**
     * Deletes a task from the task repository.
     *
     * @param id - The ID of the task to be deleted.
     * @returns A Promise that resolves when the task has been deleted.
     */
    async DeleteTask(id: number) {
        return this.taskToDoRepository.Delete(id);
    }

    /**
     * Adds a new task to the task repository.
     *
     * @param task - The task object to be added. All fields except "id" are required and cannot be null, undefined, or empty.
     * @returns A Promise that resolves to the newly created task.
     * @throws Error if any required field is missing or empty.
     */
    async AddTask(task: any) {
        for (const key in task) {
            if(key == "id") {
                continue;
            }
            
            if ([null, undefined, ''].includes(task[key])) {
                throw new Error('All fields are required');
            }
        }
        
        return this.taskToDoRepository.Create(task);
    }

    /**
     * Updates a task in the task repository.
     *
     * @param task - The task object to be updated. All fields except "user" are required and cannot be null, undefined, or empty.
     * @returns A Promise that resolves to the updated task.
     * @throws Error if any required field is missing or empty.
     */
    async UpdateTask(task: any) {
        console.log(task)
        for (const key in task) {
            if(["user", "createdAt", "updateddAt", "category"].includes(key)) {
                continue;
            }
            
            if ([null, undefined, ''].includes(task[key])) {
                throw new Error('All fields are required');
            }
        }
        
        return this.taskToDoRepository.Update(task);
    }
}
