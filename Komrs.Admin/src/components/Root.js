import React from 'react'
import PropTypes from 'prop-types'
import { Provider } from 'react-redux'
import { BrowserRouter as Router, Route, Switch } from "react-router-dom";

import PortalContainer from '../containers/Portal'

import Login from '../containers/Login'

const Root = ({ store }) => (
    <Provider store={store}>
        <Router>
            <div style={{height: "100%"}}>
                <Route path='/admin' component={PortalContainer} />
                <Route path='/login' component={Login} />
            </div>
        </Router>
    </Provider>
)

Root.propTypes = {
    store: PropTypes.oneOfType([
        PropTypes.func.isRequired,
        PropTypes.object.isRequired,
    ]).isRequired,
};

export default Root