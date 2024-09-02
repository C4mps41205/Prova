import React from 'react';
import LoginScreen from "./Presentation/Screens/Login/LoginScreen.Presentation";
import { blue, red } from "@mui/material/colors";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import MainPage from "./Presentation/Screens/MainPage/MainPage.Presentation";
import Navbar from "./Presentation/Shared/Navbar";

const theme = createTheme({
    palette: {
        primary: {
            main: blue[500],
        },
        secondary: {
            main: red[500],
        },
    },
    typography: {
        fontFamily: 'Roboto, Arial, sans-serif',
        h5: {
            fontWeight: 600,
        },
    },
});

const App: React.FC = () => {
    return (
        <ThemeProvider theme={theme}>
            <Router>
                <Routes>
                    <Route path="/" element={<LoginScreen />} />
                    <Route path="/home" element={<><Navbar/><MainPage /></>} />
                </Routes>
            </Router>
        </ThemeProvider>
    );
}

export default App;
