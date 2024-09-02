import React, {useState, useEffect} from 'react';
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    TextField,
    Grid,
    MenuItem,
    Select,
    SelectChangeEvent
} from '@mui/material';
import {TaskToDo} from '../../../../Domain/Entities/TaskToDo.Entity';
import CategoryModal from "../../Category/Components/CategoryModal";
import {useCategory} from "../../../Proviners/Category.Proviner";

interface TaskModalProps {
    open: boolean;
    handleClose: () => void;
    handleSave: (task: TaskToDo) => void;
    initialData?: TaskToDo | null;
}

const TaskModal: React.FC<TaskModalProps> = function ({open, handleClose, handleSave, initialData}) {
    const categoryUseCase = useCategory();
    const [isCategoryModalOpen, setIsCategoryModalOpen] = useState(false);
    const [categories, setCategories] = useState<{ id: number, name: string }[]>([]);

    useEffect(() => {
        fetchCategories();
    }, [categoryUseCase]);

    useEffect(() => {
        if (initialData) {
            setTask(initialData);
        } else {
            setTask({
                id: null,
                title: '',
                description: '',
                isCompleted: false,
                categoryId: 0,
                userId: 0,
            });
        }
    }, [initialData]);

    /**
     * Initializes the `task` state with default values for a new task.
     * The task object has the following properties:
     * - `id`: `null` to indicate a new task
     * - `title`: an empty string
     * - `description`: an empty string
     * - `isCompleted`: `false` to indicate the task is not completed
     * - `categoryId`: `0` to indicate no category is selected
     * - `userId`: `0` to indicate no user is assigned
     */
    const [task, setTask] = useState<TaskToDo>({
        id: null,
        title: '',
        description: '',
        isCompleted: false,
        categoryId: 0,
        userId: 0,
    });

    /**
     * Fetches all categories from the categoryUseCase and updates the `categories` state.
     * This function is used to populate the list of categories in the TaskModal component.
     * 
     * @throws {Error} If there is an error fetching the categories.
     */
    const fetchCategories = async () => {
        try {
            const response = await categoryUseCase.getAll();
            setCategories(response.data.data);
        } catch (error) {
            console.error("Failed to fetch categories:", error);
        }
    };

    /**
     * Handles changes to the task object's properties.
     * 
     * @param e - The React change event containing the updated input value and name.
     * @returns A new task object with the updated property.
     */
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const {name, value} = e.target;
        setTask(prevTask => ({
            ...prevTask,
            [name]: value,
        }));
    };

    /**
     * Handles the change event for the category dropdown in the TaskModal component.
     * Updates the `categoryId` property of the `task` state with the selected category ID.
     *
     * @param event - The select change event containing the new category ID.
     */
    const handleCategoryChange = (event: SelectChangeEvent<number>) => {
        setTask(prevTask => ({
            ...prevTask,
            categoryId: event.target.value as number,
        }));
    };

    /**
     * Closes the category modal by setting the `isCategoryModalOpen` state to `false`.
     */
    const handleCategoryModalClose = () => {
        setIsCategoryModalOpen(false);
    };

    /**
     * Saves a new category by calling the `AddCategory` method of the `categoryUseCase`.
     * After the category is saved, it fetches the updated list of categories and closes the category modal.
     *
     * @param categoryName - The name of the new category to be saved.
     * @throws {Error} If there is an error saving the new category.
     */
    const handleCategorySave = async (categoryName: string) => {
        try {
            const response = await categoryUseCase.AddCategory(categoryName);
            fetchCategories();
            handleCategoryModalClose();
        } catch (error) {
            
        }
    };

    /**
     * Saves the current task and closes the task modal.
     */
    const onSave = () => {
        handleSave(task);
        handleClose();
    };

    return (
        <>
            <Dialog open={open} onClose={handleClose}>
                <DialogTitle>{task.id ? "Edit Task" : "Add Task"}</DialogTitle>
                <DialogContent>
                    <Grid container spacing={2}>
                        <Grid item xs={12}>
                            <TextField
                                label="Title"
                                name="title"
                                value={task.title}
                                onChange={handleChange}
                                fullWidth
                                inputProps={{maxLength: 50}}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <TextField
                                label="Description"
                                name="description"
                                value={task.description}
                                onChange={handleChange}
                                fullWidth
                                multiline
                                rows={4}
                                inputProps={{maxLength: 255}}
                            />
                        </Grid>
                        <Grid item xs={12}>
                            <Select
                                label="Category"
                                name="categoryId"
                                value={task.categoryId}
                                onChange={handleCategoryChange}
                                fullWidth
                            >
                                {categories.map((categoryId) => (
                                    <MenuItem key={categoryId.id} value={categoryId.id}>
                                        {categoryId.name}
                                    </MenuItem>
                                ))}
                            </Select>
                        </Grid>
                    </Grid>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setIsCategoryModalOpen(true)}>Add Category</Button>
                    <Button onClick={handleClose}>Cancel</Button>
                    <Button onClick={onSave} color="primary">Save</Button>
                </DialogActions>
            </Dialog>

            <CategoryModal
                open={isCategoryModalOpen}
                handleClose={handleCategoryModalClose}
                handleSave={handleCategorySave}
            />
        </>
    );
};

export default TaskModal;
