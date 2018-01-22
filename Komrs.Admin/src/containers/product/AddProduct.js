import React from 'react'
import { connect } from 'react-redux'
import { compose, lifecycle } from 'recompose';
import { Redirect } from 'react-router'
import { reduxForm } from 'redux-form'

import AddProduct from '../../components/products/AddProduct'
import Loading from '../../components/Loading'
import ProductsToolbar from '../../components/products/ProductsToolbar'

const mapStateToProps = (state) => ({
    isLoading: false
})

const mapDispatchToProps = dispatch => ({
    saveProduct: () => {
        dispatch(() => {});
    }
})

let AddProductForm = reduxForm({
    form: 'addProduct'
})(AddProduct)
  
const AddProductContainer = ({isLoading}) => {
    return isLoading ? 
        <Loading text="Loading products" /> : <AddProductForm />
}

export default  compose(
                    connect(mapStateToProps, mapDispatchToProps),
                    lifecycle({
                        componentDidMount() {
                        }
                    })
                )(AddProductContainer);