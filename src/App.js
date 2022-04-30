import {React} from 'react'
import {Routes,Route,Link} from 'react-router-dom'
import './App.css';
import {HomePage} from './pages/HomePage'
import {RegisterPage} from './pages/RegisterPage'
import {NotFoundPage} from './pages/NotFoundPage'

function App() {
  return (
    <div className="App">
      <header className="App-header">
        <a href="/">Home</a>
        <a href="/login">Login</a>
        <a href="/signup">signup</a>
      </header>
      <Routes>
        <Route path="/" element={<HomePage/>}/>
        <Route path="/signup" element={<RegisterPage/>}/>
        <Route path="*" element={<NotFoundPage/>}/>
      </Routes>
    </div>
  );
}

export default App;
