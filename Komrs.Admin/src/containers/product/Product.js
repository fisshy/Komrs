import React from 'react'
import { Switch, Route, Link } from 'react-router-dom'

import ProductsToolbar from '../../components/products/ProductsToolbar'
import ProductsContainer from './Products'
import AddProduct from './AddProduct'

const ProductContainer = ({match}) => {
    return (
        <div>
            <ProductsToolbar />
            <Switch>
                <Route exact path={`${match.url}`} component={ProductsContainer} />
                <Route path={`${match.url}/new`} component={AddProduct} />
            </Switch>
        </div>

    )
}

export default ProductContainer;