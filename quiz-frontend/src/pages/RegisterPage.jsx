import React, { useRef } from 'react'
import {useNavigate} from 'react-router-dom'
import './Page.css'

const RegisterPage = function (props) {
    const emailRef = useRef();
    const passwordRef = useRef();
    const passwordConfirmRef = useRef();
    const navigate = useNavigate();

    async function onRegister() {
        if (validateEmail(emailRef.current.value) === null) {
            alert("Wrong email!")
            return
        }

        if (passwordRef.current.value === "") {
            alert("Empty password!")
            return
        }

        if (passwordRef.current.value !== passwordConfirmRef.current.value) {
            alert("Passwords don't match")
            return
        }

        const response = await fetch("https://localhost:7178/api/account/register",
            {
                headers: {
                    'Accept': 'application/json',
                    'Content-Type': 'application/json'
                },
                method: "POST",
                body: JSON.stringify({
                    email: emailRef.current.value,
                    password: passwordRef.current.value,
                    passwordConfirm: passwordConfirmRef.current.value
                })
            })
            .then(function (res) { 
                if(res.status === 400){
                    alert("Couldn't register new user"); 
                    return;
                } 

                if(res.status === 200){
                    
                    navigate(`/registered`)
                    return;
                }

                alert("fetched with code "+res.status); 
            })
            .catch( function (res) { alert("Error in fetching post") })
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
                <div className="header-text">Register Form</div>
                <form action="#">
                    <div className="field">
                        <span className="fa fa-user"></span>
                        <input ref={emailRef} type="text" placeholder="Email" required />
                    </div>
                    <div className="field field-another">
                        <span className="fa fa-user"></span>
                        <input ref={passwordRef} type="text" placeholder="Password" required />
                    </div>
                    <div className="field field-another">
                        <span className="fa fa-user"></span>
                        <input ref={passwordConfirmRef} type="text" placeholder="Password" required />
                    </div>
                    <button onClick={onRegister}>Sign Up</button>
                </form>
            </div>
        </>
    )
}

export { RegisterPage }