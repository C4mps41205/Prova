import React from 'react';
import {AppBar, Toolbar, Typography, Button} from '@mui/material';

const Navbar: React.FC = () => {
    /**
     * Handles the logout process by removing the user's authentication token and user data from local storage, and redirecting the user to the home page.
     */
    const handleLogout = () => {
        localStorage.removeItem('authToken');
        localStorage.removeItem('user');

        window.location.href = '/';
    };

    return (
        <AppBar position="static">
            <Toolbar>
                <Typography variant="h6" style={{flexGrow: 1}}>
                    Avaliação
                </Typography>
                <Button color="inherit" onClick={handleLogout}>
                    Exit
                </Button>
            </Toolbar>
        </AppBar>
    );
};

export default Navbar;
