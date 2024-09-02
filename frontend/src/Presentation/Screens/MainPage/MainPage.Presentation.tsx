import React, { useState, useEffect } from "react";
import {
    Box,
    Grid,
    Paper,
    TextField,
    Typography,
    Button,
    Snackbar,
    Alert, MenuItem, Select, SelectChangeEvent,
} from '@mui/material';
import { indigo } from "@mui/material/colors";
import { makeStyles } from '@mui/styles';
import TaskModal from "./Components/TaskModal";
import TaskList from "./Components/ItemToDoList";
import { useTaskToDo } from "../../Proviners/TaskToDo.Proviner";
import { TaskToDo } from '../../../Domain/Entities/TaskToDo.Entity';
import {useCategory} from "../../Proviners/Category.Proviner";

const useStyles = makeStyles((theme: any) => ({
    container: {
        margin: theme.spacing(3, 0, 2, 0),
        padding: theme.spacing(2),
    },
    formContainer: {
        padding: theme.spacing(2),
    },
    heading: {
        textAlign: "center",
        color: indigo[500],
        marginBottom: theme.spacing(3),
    },
    inputContainer: {
        display: 'flex',
        alignItems: 'center',
        gap: theme.spacing(1),
    },
    taskPaper: {
        padding: theme.spacing(2),
        margin: theme.spacing(1, 0),
        boxShadow: theme.shadows[3],
    },
}));

export default function MainPage() {
    /**
     * Initializes the state variables for the MainPage component, including the task list, task modal, and snackbar.
     * 
     * @param taskToDoUseCase - A custom hook that provides access to the task management use case.
     * @param classes - The CSS classes defined for the MainPage component.
     * @returns void
     */
    const taskToDoUseCase = useTaskToDo();
    const categoryUseCase = useCategory();
    const classes = useStyles();

    const [inputData, setInputData] = useState<string>("");
    const [remainingTaskList, setRemainingTaskList] = useState<TaskToDo[]>([]);
    const [completedTaskList, setCompletedTaskList] = useState<TaskToDo[]>([]);
    const [taskModalOpen, setTaskModalOpen] = useState(false);
    const [currentTask, setCurrentTask] = useState<TaskToDo | null>(null);
    const [snackbarOpen, setSnackbarOpen] = useState<boolean>(false);
    const [snackbarMessage, setSnackbarMessage] = useState<string>('');
    const [snackbarSeverity, setSnackbarSeverity] = useState<'success' | 'error'>('success');
    const [categories, setCategories] = useState<{ id: number, name: string }[]>([]);
    

    useEffect(() => {
        const token = localStorage.getItem('authToken');
        if (!token) {
            window.location.href = '/'
            return;
        }
        
        loadTasks();
        fetchCategories();
    }, []);

    const fetchCategories = async () => {
        try {
            const response = await categoryUseCase.getAll();
            setCategories(response.data.data);
        } catch (error) {
            console.error("Failed to fetch categories:", error);
        }
    };

    /**
     * Loads the tasks from the task management use case and updates the state with the remaining and completed tasks.
     *
     * @param filter - An optional filter object to pass to the GetAll method of the task management use case.
     * @returns void
     */
    const loadTasks = async (filter = {}) => {
        try {
            const response = await taskToDoUseCase.GetAll(filter);
            const tasks = response.data.data || [];

            setRemainingTaskList(tasks.filter((task: TaskToDo) => !task.isCompleted));
            setCompletedTaskList(tasks.filter((task: TaskToDo) => task.isCompleted));
            fetchCategories();  
            showSnackbar('Tasks loaded successfully', 'success');
        } catch (error) {
            handleError(error);
        }
    };

    /**
     * Handles errors that occur during the execution of the application.
     *
     * @param error - The error object that was encountered.
     * @returns void
     */
    const handleError = (error: unknown) => {
        const message = error instanceof Error ? error.message : 'Unexpected error';
        showSnackbar(message, 'error');
    };

    /**
     * Handles the change event of an input element and updates the `inputData` state with the new value.
     *
     * @returns void
     * @param event
     */
    const handleOnChange = (event: SelectChangeEvent<string>) => {
        setInputData(event.target.value);
    };


    /**
     * Marks a task as completed by updating its `isCompleted` property and then reloading the tasks.
     *
     * @param task - The task to be marked as completed.
     * @returns void
     */
    const handleCheck = async (task: TaskToDo) => {
        try {
            await taskToDoUseCase.UpdateTask({ ...task, isCompleted: true });
            loadTasks();
        } catch (error) {
            handleError(error);
        }
    };

    /**
     * Handles the saving of a task, either by updating an existing task or creating a new one.
     *
     * @param task - The task to be saved, either an existing task or a new one.
     * @returns void
     */
    const handleSaveTask = async (task: TaskToDo) => {
        const userData = localStorage.getItem('user');
        const user = userData ? JSON.parse(userData) : { id: 0 };

        task.userId = user.id;

        try {
            if (task.id !== null && task.id !== 0) {
                await taskToDoUseCase.UpdateTask(task);
            } else {
                await taskToDoUseCase.AddTask(task);
            }
            loadTasks();
        } catch (error) {
            handleError(error);
        }
        handleTaskModalClose();
    };

    /**
     * Deletes a task by its ID and then reloads the tasks.
     *
     * @param id - The ID of the task to be deleted.
     * @returns void
     */
    const handleDelete = async (id: number) => {
        try {
            await taskToDoUseCase.DeleteTask(id);
            loadTasks();
        } catch (error) {
            handleError(error);
        }
    };

    /**
     * Opens the task modal and sets the current task.
     *
     * @param task - The task to be edited, or null if creating a new task.
     * @returns void
     */
    const handleTaskModalOpen = (task: TaskToDo | null = null) => {
        const userData = localStorage.getItem('user');
        const user = userData ? JSON.parse(userData) : { id: 0 };

        setCurrentTask(task ? {
            id: task.id,
            title: task.title,
            description: task.description || '',
            isCompleted: task.isCompleted,
            categoryId: task.categoryId,
            userId: user.id,
        } : null);
        setTaskModalOpen(true);
    };

    /**
     * Closes the task modal and resets the current task.
     */
    const handleTaskModalClose = () => {
        setTaskModalOpen(false);
        setCurrentTask(null);
    };

    /**
     * Displays a snackbar message with the given severity.
     *
     * @param message - The message to be displayed in the snackbar.
     * @param severity - The severity of the message, either 'success' or 'error'.
     * @returns void
     */
    const showSnackbar = (message: string, severity: 'success' | 'error') => {
        setSnackbarMessage(message);
        setSnackbarSeverity(severity);
        setSnackbarOpen(true);
    };

    /**
     * Closes the snackbar message.
     */
    const handleCloseSnackbar = () => {
        setSnackbarOpen(false);
    };

    /**
     * Filters the todo list based on the input category.
     *
     * @param e - The form submission event.
     * @returns void
     */
    const getToDoListFilter = (e: React.FormEvent) => {
        e.preventDefault();
        loadTasks({ category: inputData });
    };

    return (
        <Box className={classes.container}>
            <Grid container>
                <Grid item xs={12}>
                    <Paper elevation={3}>
                        <form onSubmit={getToDoListFilter} className={classes.formContainer}>
                            <Typography variant='h5' className={classes.heading}>
                                Todo List
                            </Typography>
                            <Grid container justifyContent='center' alignItems="center" spacing={2}>
                                <Grid item xs={8} className={classes.inputContainer}>
                                    <Select
                                        label="Category"
                                        id='inputTaskField'
                                        name="categoryId" 
                                        value={inputData}
                                        onChange={handleOnChange}
                                        fullWidth
                                    >
                                        {categories.map((category) => (
                                            <MenuItem key={category.id} value={category.id}>
                                                {category.name}
                                            </MenuItem>
                                        ))}
                                    </Select>
                                    <Button variant="contained" color="primary" type="submit">
                                        Search
                                    </Button>
                                </Grid>
                                <Grid item>
                                    <Button variant="contained" color="primary"
                                            onClick={() => handleTaskModalOpen(null)}>
                                        Add Task
                                    </Button>
                                </Grid>
                            </Grid>
                        </form>
                    </Paper>
                </Grid>
            </Grid>

            <Grid container spacing={2}>
                <Grid item xs={12} sm={6} lg={6}>
                    <Paper className={classes.taskPaper} elevation={3}>
                        <TaskList
                            tasks={remainingTaskList}
                            title="Remaining Tasks"
                            handleCheck={handleCheck}
                            handleDelete={handleDelete}
                            handleEdit={handleTaskModalOpen}
                        />
                    </Paper>
                </Grid>
                <Grid item xs={12} sm={6} lg={6}>
                    <Paper className={classes.taskPaper} elevation={3}>
                        <TaskList
                            tasks={completedTaskList}
                            title="Completed Tasks"
                            handleCheck={handleCheck}
                            handleDelete={handleDelete}
                            handleEdit={handleTaskModalOpen}
                            isCompleted={true}
                        />
                    </Paper>
                </Grid>
            </Grid>

            <TaskModal
                open={taskModalOpen}
                handleClose={handleTaskModalClose}
                handleSave={handleSaveTask}
                initialData={currentTask}
            />
            <Snackbar open={snackbarOpen} autoHideDuration={6000} onClose={handleCloseSnackbar}>
                <Alert onClose={handleCloseSnackbar} severity={snackbarSeverity} sx={{ width: '100%' }}>
                    {snackbarMessage}
                </Alert>
            </Snackbar>
        </Box>
    );
}
