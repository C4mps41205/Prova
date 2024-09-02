import React, { useState } from 'react';
import {
    Button,
    Dialog,
    DialogActions,
    DialogContent,
    DialogTitle,
    TextField,
} from '@mui/material';

interface CategoryModalProps {
    open: boolean;
    handleClose: () => void;
    handleSave: (categoryName: string) => void;
}

const CategoryModal: React.FC<CategoryModalProps> = ({ open, handleClose, handleSave }) => {
    const [categoryName, setCategoryName] = useState('');

    /**
     * Handles the change event for the category name input field.
     * @param e - The React change event object containing the new input value.
     */
    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        setCategoryName(e.target.value);
    };

    /**
     * Handles the save operation for the category modal.
     * - Calls the `handleSave` callback with the current category name.
     * - Clears the category name input field.
     * - Closes the modal by calling the `handleClose` callback.
     */
    const onSave = () => {
        handleSave(categoryName);
        setCategoryName('');
        handleClose();
    };

    return (
        <Dialog open={open} onClose={handleClose}>
            <DialogTitle>Add Category</DialogTitle>
            <DialogContent>
                <TextField
                    label="Category Name"
                    value={categoryName}
                    onChange={handleChange}
                    fullWidth
                />
            </DialogContent>
            <DialogActions>
                <Button onClick={handleClose}>Cancel</Button>
                <Button onClick={onSave} color="primary">Save</Button>
            </DialogActions>
        </Dialog>
    );
};

export default CategoryModal;
