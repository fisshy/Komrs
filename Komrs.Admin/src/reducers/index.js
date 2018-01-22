import { combineReducers } from 'redux'
import auth from './auth'
import products from './products'
import { reducer as formReducer } from 'redux-form'

const komrs = combineReducers({
    auth,
    products,
    form: formReducer
});

export default komrs;