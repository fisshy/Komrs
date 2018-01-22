import React from 'react'
import { connect } from 'react-redux'
import { loginUser } from '../actions'
import LoginForm from '../components/Login'
import { Redirect } from 'react-router'


const mapStateToProps = (state) => ({
    isAuthenticated: state.auth.isAuthenticated,
    isFetching: state.auth.isFetching
})

const mapDispatchToProps = dispatch => ({
    login: (username, password) => {
        dispatch(loginUser(username, password))
    }
})

let Login = ({ login, isAuthenticated, isFetching}) => {
    return isAuthenticated ? 
        <Redirect to="/admin" /> : <LoginForm login={login} isAuthenticated={isAuthenticated} />
}

Login = connect(mapStateToProps, mapDispatchToProps)(Login)

export default Login