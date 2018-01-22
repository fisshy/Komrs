import React from 'react'
import Portal from '../components/Portal'
import { connect } from 'react-redux'
import { Redirect } from 'react-router'

const mapStateToProps = (state) => ({
    isAuthenticated: state.auth.isAuthenticated,
});

let PortalContainer = ({ isAuthenticated, ...rest }) => {
    return !isAuthenticated ? 
        <Redirect to="/login" /> :  
        <Portal {...rest} /> 
}

PortalContainer = connect(mapStateToProps)(PortalContainer)

export default PortalContainer