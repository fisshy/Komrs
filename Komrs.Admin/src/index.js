import React from 'react'
import { render } from 'react-dom'
import { createStore, applyMiddleware } from 'redux'
import komrs from './reducers'
import Root from './components/Root'
import registerServiceWorker from './registerServiceWorker'
import thunkMiddleware from 'redux-thunk'


const store = createStore(
    komrs,
    applyMiddleware(
        thunkMiddleware
    )
)


render(
    <Root store={store} />,
    document.getElementById('root')
)

registerServiceWorker()
