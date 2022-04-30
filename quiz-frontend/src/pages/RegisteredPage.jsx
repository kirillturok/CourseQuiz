import {Link} from 'react-router-dom'
import './Page.css'

const RegisteredPage = () => {
    return (
        <>
            <div className="content content-wide">
                <div className="header-text">You've been successfully registered
                <br/>Check your email</div>
                
                <div className="forgot-pass"><Link to="/">Home</Link></div>
                <div className="forgot-pass"><Link to="/login">Login</Link></div>

                
            </div>
        </>
    )
}

export { RegisteredPage }