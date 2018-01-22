import { LOGIN_ERROR, LOGIN_REQUEST, LOGIN_SUCCESS, LOGOUT_REQUEST, LOGOUT_SUCCESS } from '../constants'
import { authenticate } from '../api';

export const requestLogout = () => ({
    type: LOGOUT_REQUEST,
    isFetching: true,
    isAuthenticated: true
})

export const requestLogin = (username, password) => ({
    type: LOGIN_REQUEST,
    isFetching: true,
    isAuthenticated: false,
    username,
    password
})

export const receiveLogout = () => ({
    type: LOGOUT_SUCCESS,
    isFetching: false,
    isAuthenticated: false
})

export const receiveLogin = (user) => ({
    type: LOGIN_SUCCESS,
    isFetching: false,
    isAuthenticated: true
})

export const loginError = (message) => ({
    type: LOGIN_ERROR,
    isFetching: false,
    isAuthenticated: false,
    message
})

export const loginUser = (username, password) => {

    return async dispatch => {
        // We dispatch requestLogin to kickoff the call to the API
        dispatch(requestLogin(username, password))

        try {
            let user = await authenticate(username, password);

            if (!user.ok) {
                return dispatch(loginError(user.message))
            }

            localStorage.setItem('access_token', user.access_token)

            return dispatch(receiveLogin(user))
        }
        catch (err) {
            console.log("Error: ", err)
            return dispatch(loginError(err))
        }
    }
}

// Logs the user out
export const logoutUser = () => {
    return dispatch => {
        dispatch(requestLogout())
        localStorage.removeItem('id_token')
        localStorage.removeItem('access_token')
        dispatch(receiveLogout())
    }
}