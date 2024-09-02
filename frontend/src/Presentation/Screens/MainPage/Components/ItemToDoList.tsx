import React from 'react';
import {
    List,
    Typography,
    IconButton,
    ListItem,
    ListItemText,
} from '@mui/material';
import {indigo, green, red} from '@mui/material/colors';
import DoneOutlineOutlinedIcon from '@mui/icons-material/DoneOutlineOutlined';
import DeleteForeverIcon from '@mui/icons-material/DeleteForever';
import EditIcon from '@mui/icons-material/Edit';
import {TaskToDo} from '../../../../Domain/Entities/TaskToDo.Entity';

/**
 * Renders a list of tasks with options to check, edit, and delete them.
 *
 * @param tasks - An array of `TaskToDo` objects representing the tasks to be displayed.
 * @param title - The title to be displayed above the task list.
 * @param handleCheck - A function to be called when a task is checked/unchecked.
 * @param handleDelete - A function to be called when a task is deleted.
 * @param handleEdit - A function to be called when a task is edited.
 * @param isCompleted - An optional boolean indicating whether the task list is for completed tasks.
 */
const TaskList = ({
                      tasks,
                      title,
                      handleCheck,
                      handleDelete,
                      handleEdit,
                      isCompleted,
                  }: {
    tasks: TaskToDo[];
    title: string;
    handleCheck: (task: TaskToDo) => void;
    handleDelete: (id: number) => void;
    handleEdit: (task: TaskToDo | null) => void;
    isCompleted?: boolean;
}) => {
    return (
        <>
            <Typography variant='h5' style={{padding: '16px', color: indigo[500]}}>
                {title}
            </Typography>
            <List dense>
                {tasks.length > 0 ? (
                    tasks.map((item: TaskToDo) => (
                        <ListItem key={item.id}>
                            <ListItemText
                                primary={item.title}
                            />
                            {!isCompleted && (
                                <>
                                    <IconButton
                                        style={{color: green[500]}}
                                        onClick={() => handleCheck(item)}
                                    >
                                        <DoneOutlineOutlinedIcon/>
                                    </IconButton>
                                    <IconButton
                                        style={{color: indigo[500]}}
                                        onClick={() => handleEdit(item)}
                                    >
                                        <EditIcon/>
                                    </IconButton>
                                </>
                            )}
                            <IconButton
                                style={{color: red[600]}}
                                onClick={() => {
                                    if (item.id !== null) {
                                        handleDelete(item.id);
                                    }
                                }}
                            >
                                <DeleteForeverIcon/>
                            </IconButton>
                        </ListItem>
                    ))
                ) : (
                    <Typography style={{padding: '16px', textAlign: 'center'}}>
                        No tasks added yet!...
                    </Typography>
                )}
            </List>
        </>
    );
};

export default TaskList;
