import { LIST_PRODUCTS_SUCCESS, LIST_PRODUCTS_ERROR, LIST_PRODUCTS_REQUEST } from '../constants'

import { listProductsApi } from '../api';

export const recieveProducts = (products) => ({
    type: LIST_PRODUCTS_SUCCESS,
    products: products,
    isLoading: false
})

export const requestProducts = (message) => ({
    type: LIST_PRODUCTS_REQUEST,
    isLoading: true
})

export const listProductsError = (message) => ({
    type: LIST_PRODUCTS_ERROR,
    isLoading: false
})

export const listProducts = () => {

    return async dispatch => {

        dispatch(requestProducts());

        try {
            let products = await listProductsApi();

            if (!products) {
                return dispatch(listProductsError())
            }

            console.log("listProducts", products);
            
            return dispatch(recieveProducts(products))
        }
        catch (err) {
            console.log("error", err);
            return dispatch(listProductsError('failed to list products'))
        }
    }
}

