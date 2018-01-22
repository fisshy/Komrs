import {LIST_PRODUCTS_SUCCESS, LIST_PRODUCTS_ERROR, LIST_PRODUCTS_REQUEST, } from '../constants'


const products = (state = {
  isLoading: false
}, action) => {
  switch (action.type) {
    case LIST_PRODUCTS_SUCCESS:
    console.log("state", state);
    console.log("action", action)
      return {
        ...state,
        products: action.products,
        isLoading: action.isLoading
      }
    case LIST_PRODUCTS_ERROR:
      return {
        ...state,
        isLoading: action.isLoading
      }
    case LIST_PRODUCTS_REQUEST:
      return {
        ...state,
        isLoading: action.isLoading
      }
    default:
      return state
  }
  
}

export default products;