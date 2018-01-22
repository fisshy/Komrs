import React from 'react'
import PropTypes from 'prop-types'
import './AddProduct.css';
import { Field } from 'redux-form'


let AddProduct = () => {
    return (
        <div className="add-product">
            <h2>Create new product</h2>
            <div>
                <form onSubmit={() => {}}>
                    <Field type="text" name="articleNumber" placeholder="Article number" component="input" />
                    <Field type="text" name="name" placeholder="Name" component="input" />
                    <Field type="text" name="description" placeholder="Description" component="input" />
                    <Field type="text" name="price" placeholder="Price" component="input" />
                    <Field type="text" name="currency" placeholder="Currency" component="input" />
                    <Field type="text" name="productInfo" placeholder="Product info" component="input" />
                    <Field type="text" name="height" placeholder="Height" component="input" />
                    <Field type="text" name="width" placeholder="Width" component="input"  />
                    <Field type="text" name="length" placeholder="Length" component="input" />
                    <Field type="text" name="weight" placeholder="Weight" component="input" />
                    <Field type="text" name="actualStock" placeholder="Stock" component="input" />
                    <Field type="text" name="supplier" placeholder="Supplier" component="input" />
                    <input type="submit" value="Create product" />
                </form>
            </div>
        </div>
    )
}

AddProduct.propTypes = {
}

export default AddProduct

