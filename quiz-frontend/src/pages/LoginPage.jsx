import React, { useRef } from 'react'
import { useContext } from 'react';
import { Link, useNavigate } from 'react-router-dom'
import SignedContext from '../context';
import './Page.css'

const LoginPage = () => {
    const emailRef = useRef();
    const passwordRef = useRef();
    const navigate = useNavigate();
    const { signed, setSigned } = useContext(SignedContext);

    async function onLogin() {
        console.log("1")
        if (validateEmail(emailRef.current.value) === null) {
            alert("Wrong email!")
            return
        }

        if (passwordRef.current.value === "") {
            alert("Empty password!")
            return
        }

        console.log("2")
        let response = await fetch("https://localhost:7178/api/account/login",
            {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "POST",
                body: JSON.stringify({
                    email: emailRef.current.value,
                    password: passwordRef.current.value,
                })
            })
            .catch(function (res) { alert("Error in fetching post") });

        console.log("3")
        if (response.ok) {
            alert("logined")
            let data = await response.json();

            alert("1")
            console.log(data.access_token)
            localStorage.setItem("JWT", data.access_token)
            setSigned(true);
            alert("2")

            navigate('/registered')


            return;
        }

        console.log("4")
        if (response.status === 400) {
            response.json()
                .then((data) => {
                    console.log(data.errors)
                    alert("Couldn't login new user");
                })

            return;
        }
    }

    const validateEmail = (email) => {
        return String(email)
            .match(
                /^(([^<>()[\]\\.,;:\s@"]+(\.[^<>()[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/
            );
    };

    return (
        <>
            <div className="content">
                <div className="header-text">Login Form</div>
                <div>{signed}</div>
                <div>
                    <div className="field">
                        <span className="fa fa-user"></span>
                        <input ref={emailRef} type="text" placeholder="Email" required />
                    </div>
                    <div className="field field-another">
                        <span className="fa fa-user"></span>
                        <input ref={passwordRef} type="password" placeholder="Password" required />
                    </div>
                    <div className="forgot-pass"><Link to="/forgot-password">Forgot Password?</Link></div>
                    <button onClick={onLogin}>Sign In</button>
                    <div className="signup">Not a member? <Link to="/signup">Signup now</Link></div>
                </div>
            </div>
        </>
    )
}

export { LoginPage }