/**
 * Represents a task to do, with properties for its id, title, description, completion status, category, and the user ID.
 */
export type TaskToDo = {
    id: number | null;
    title: string;
    description: string;
    isCompleted: boolean;
    categoryId: number;
    userId: number;
}

export type Task = {
    id: number | null;
    title: string;
    description?: string;
    isCompleted: boolean;
    currentTime?: string;
    categoryId: number;
};