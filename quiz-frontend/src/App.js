import { React, useState } from 'react'
import { Routes, Route } from 'react-router-dom'
import 'bootstrap/dist/css/bootstrap.min.css';
import './App.css';
import { HomePage } from './pages/HomePage'
import { RegisterPage } from './pages/RegisterPage'
import { LoginPage } from './pages/LoginPage'
import { ForgotPasswordPage } from './pages/ForgotPasswordPage'
import { NotFoundPage } from './pages/NotFoundPage'
import {Layout} from './components/Layout'
import {RegisteredPage} from './pages/RegisteredPage'
import SignedContext from './context'

function App() {
  const [signed, setSigned] = useState(false)
  return (
    <div>
      <SignedContext.Provider value={{ signed, setSigned }}>  
      <Routes>
        <Route path='/' element={<Layout/>}>
          <Route path="/" element={<HomePage />} />
          <Route path="/signup" element={<RegisterPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/forgot-password" element={<ForgotPasswordPage />} />
          <Route path="/login" element={<LoginPage />} />
          <Route path="/registered" element={<RegisteredPage/>}/>

          <Route path="*" element={<NotFoundPage />} />
        </Route>
      </Routes>
      </SignedContext.Provider>
    </div>
  );
}

export default App;
