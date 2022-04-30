import { React, useContext } from 'react'
import { Link, Outlet } from 'react-router-dom'
import './Layout.css'
import SignedContext from '../context'

const Layout = () => {
    const { signed, setSigned } = useContext(SignedContext)

    function onLeave(){
        localStorage.removeItem("JWT");
        setSigned(false);
    }

    return (
        <>
            <header className="header">
                <nav className="navbar navbar-expand-lg fixed-top boxes">
                    <div className="container"><Link to="/" className="navbar-brand header-text text">Home</Link>

                        <div id="navbarSupportedContent" className="collapse navbar-collapse">
                            <ul className="navbar-nav ml-auto">
                                {(signed) ? (
                                    <>
                                        <li className="nav-item"><Link to="/profile" className="links-route">Profile</Link></li>
                                        <li className="nav-item"><Link onClick={onLeave} to="/" className="links-route">Leave</Link></li>
                                    </>
                                ) : (
                                    <>
                                        <li className="nav-item"><Link to="/login" className="links-route">Login</Link></li>
                                        <li className="nav-item"><Link to="/signup" className="links-route">Signup</Link></li>
                                    </>

                                )}

                            </ul>
                        </div>
                    </div>
                </nav>
            </header>

            <main>
                <Outlet />
            </main>
        </>
    )
}

export { Layout }