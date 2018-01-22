import React from 'react'
import PropTypes from 'prop-types'
import ProductListItem from './ProductListItem'

let Products = ({ products }) => {
    let list = products.map(p => <ProductListItem key={p.id}  product={p} />)
    return (<div>{list}</div>)
}

Products.propTypes = {
    products: PropTypes.array.isRequired
}

export default Products