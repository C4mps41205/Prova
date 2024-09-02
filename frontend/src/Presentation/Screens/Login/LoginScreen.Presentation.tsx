import React, { useState } from 'react';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import Typography from '@mui/material/Typography';
import Container from '@mui/material/Container';
import Snackbar from '@mui/material/Snackbar';
import Alert from '@mui/material/Alert';
import { useAuth } from "../../Proviners/AuthProviner";
import { useUser } from "../../Proviners/User.Proviner";

function LoginScreen() {
    /**
     * Manages the state of the login screen, including the user's name and password, as well as the state of a snackbar for displaying success or error messages.
     *
     * @param name - The user's name, stored in state.
     * @param password - The user's password, stored in state.
     * @param snackbarOpen - A boolean indicating whether the snackbar is currently open.
     * @param snackbarMessage - The message to be displayed in the snackbar.
     * @param snackbarSeverity - The severity of the snackbar message, either 'success' or 'error'.
     */
    const [name, setName] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [snackbarOpen, setSnackbarOpen] = useState<boolean>(false);
    const [snackbarMessage, setSnackbarMessage] = useState<string>('');
    const [snackbarSeverity, setSnackbarSeverity] = useState<'success' | 'error'>('success');

    const authUseCase = useAuth();
    const userUseCase = useUser();

    /**
     * Shows a snackbar with a given message and severity.
     *
     * @param message - The message to display in the snackbar.
     * @param severity - The severity of the message, either 'success' or 'error'.
     */
    const showSnackbar = (message: string, severity: 'success' | 'error') => {
        setSnackbarMessage(message);
        setSnackbarSeverity(severity);
        setSnackbarOpen(true);
    };

    /**
     * Closes the snackbar by setting the `snackbarOpen` state to `false`.
     */
    const handleCloseSnackbar = () => {
        setSnackbarOpen(false);
    };

    /**
     * Handles the form submission for the login screen.
     *
     * @param event - The form submission event.
     * @returns Promise<void> - A promise that resolves when the login process is complete.
     * @throws Error - If there is an error during the login process.
     */
    const handleSubmit = async (event: React.FormEvent) => {
        event.preventDefault();
        try {
            const response = await authUseCase.Authenticate(name, password);
            showSnackbar(response, 'success');
            window.location.href = '/home';
        } catch (error) {
            handleError(error);
        }
    };

    /**
     * Creates a new user with the provided name and password.
     *
     * @param name - The name of the user to create.
     * @param password - The password for the new user.
     * @returns A promise that resolves with the response data, which includes a success message.
     * @throws Error - If there is an error creating the user.
     */
    const createUser = async () => {
        try {
            const response = await userUseCase.CreateUser({ name, password });
            showSnackbar(response.data.message, 'success');
        } catch (error) {
            handleError(error);
        }
    };

    /**
     * Handles an error that occurred during the login process.
     *
     * @param error - The error that occurred.
     * @returns void
     */
    const handleError = (error: unknown) => {
        const message = error instanceof Error ? error.message : 'Unexpected error';
        showSnackbar(message, 'error');
    };

    return (
        <Container component="main" maxWidth="xs">
            <CssBaseline />
            <Paper elevation={3} sx={{ mt: 8, p: 4 }}>
                <Box sx={{ display: 'flex', flexDirection: 'column', alignItems: 'center' }}>
                    <Avatar sx={{ m: 1, bgcolor: 'secondary.main' }} />
                    <Typography component="h1" variant="h5">Login</Typography>
                    <Box component="form" onSubmit={handleSubmit} sx={{ mt: 1 }}>
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            id="name"
                            label="Name"
                            name="name"
                            autoComplete="name"
                            autoFocus
                            value={name}
                            onChange={(e) => setName(e.target.value)}
                        />
                        <TextField
                            variant="outlined"
                            margin="normal"
                            required
                            fullWidth
                            name="password"
                            label="Password"
                            type="password"
                            id="password"
                            autoComplete="current-password"
                            value={password}
                            onChange={(e) => setPassword(e.target.value)}
                        />
                        <Button
                            type="submit"
                            fullWidth
                            variant="contained"
                            color="primary"
                            sx={{ mt: 3, mb: 2 }}
                        >
                            Sign In
                        </Button>
                        <Button
                            type="button"
                            fullWidth
                            variant="contained"
                            color="primary"
                            sx={{ mt: 3, mb: 2 }}
                            onClick={createUser}
                        >
                            Create User
                        </Button>
                    </Box>
                </Box>
            </Paper>
            <Snackbar open={snackbarOpen} autoHideDuration={6000} onClose={handleCloseSnackbar}>
                <Alert onClose={handleCloseSnackbar} severity={snackbarSeverity} sx={{ width: '100%' }}>
                    {snackbarMessage}
                </Alert>
            </Snackbar>
        </Container>
    );
}

export default LoginScreen;
