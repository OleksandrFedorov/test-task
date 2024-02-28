import "./App.css";
import { BrowserRouter, Routes, Route } from "react-router-dom";
import { UserProvider } from "./components/context/UserContext";
import FileSharePage from "./components/file/FileSharePage";
import Login from "./components/auth/Login";
import Register from "./components/auth/Register";
import Files from "./components/file/Files";

export default function App() {
  return (
    <>
      <BrowserRouter>
        <UserProvider>
          <Routes>
            <Route path="/" element={<Files />} />
            <Route path="/shares/:id" element={<FileSharePage />} />
            <Route path="/login" element={<Login />} />
            <Route path="/register" element={<Register />} />
          </Routes>
        </UserProvider>
      </BrowserRouter>
    </>
  );
}
