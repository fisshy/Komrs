import React from 'react'
import { connect } from 'react-redux'
import { listProducts } from '../../actions'
import { compose, lifecycle } from 'recompose';
import { Redirect } from 'react-router'

import Products from '../../components/products/Products'
import Loading from '../../components/Loading'

const mapStateToProps = (state) => ({
    products: state.products.products,
    isLoading: state.products.isLoading
})

const mapDispatchToProps = dispatch => ({
    listProducts: () => {
        dispatch(listProducts())
    }
})

const ProductsContainer = ({products, isLoading}) => {
    return isLoading ? 
        <Loading text="Loading products" /> : 
        <Products products={products || []}/>
        
}

export default
    compose(
        connect(mapStateToProps, mapDispatchToProps),
        lifecycle({
            componentDidMount() {
                const { listProducts } = this.props;
                listProducts();
            }
        }))(ProductsContainer);