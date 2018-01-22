import PropTypes from 'prop-types'
import React from 'react'
import { Link } from 'react-router-dom'

import './ProductListItem.css'

let ProductLstItem = ({ product, match }) => {
    return <div className="product">
        <div className="articleNumber"><span >{product.articleNumber}</span></div>
        <div className="name"><span >{product.name}</span></div>
        <div className="supplierName"><span >{product.supplierName}</span></div>
        <div className="price"><span >{product.price}</span></div>
        <div><a href={`/admin/products/${product.id}`}>edit</a></div>
    </div>
}

ProductLstItem.propTypes = {
    product: PropTypes.object.isRequired
}

export default ProductLstItem;