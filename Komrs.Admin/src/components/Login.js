import React from 'react'
import PropTypes from 'prop-types'
import { Form, Input } from './Form';
import './Login.css'

let LoginForm = ({ login, isAuthenticated }) => {

    let username = null;
    let password = null;

    return (
        <div className="login-wrapper">
            <div className="login-container">
                <h2 className="active"> Sign In </h2>
                <Form onSubmit={() => { login(username.value, password.value); }}>
                    <Input type="text" id="username" placeholder="Enter your email" inputRef={node => {
                        username = node
                    }} />
                    <Input type="password" id="password" placeholder="Password" inputRef={node => {
                        password = node
                    }} />
                    <input type="submit" value="Login" />
                </Form>
                <div className="footer">
                    <a className="underlineHover">Forgot Password?</a>
                </div>
            </div>
            <ul className="bg-bubbles">
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
                <li></li>
            </ul>
        </div>
    )
}

LoginForm.propTypes = {
    login: PropTypes.func.isRequired,
    isAuthenticated: PropTypes.bool.isRequired,
    errorMessage: PropTypes.string
}

export default LoginForm