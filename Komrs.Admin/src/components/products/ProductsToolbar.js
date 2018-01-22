import React from 'react'
import PropTypes from 'prop-types'
import './ProductsToolbar.css';

let ProductsToolbar = () => {
    return (
    <div className="toolbar">
        <div>
            <a href="/admin/products/new">Add new product</a>
        </div>
    </div>
    )
}

ProductsToolbar.propTypes = {
}

export default ProductsToolbar