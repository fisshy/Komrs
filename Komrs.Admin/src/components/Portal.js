import React from 'react';
import { Switch, Route, Link } from 'react-router-dom'
import HomeContainer from '../containers/Home'

import ProductContainer from '../containers/product/Product'

import './Portal.css';


const Portal = ({match}) => (
  <div style={{height: "100%"}}>
      <div className="menu">
        <Link to={`${match.url}/products`}>Products</Link>
        <Link to={`${match.url}/products`}>Suppliers</Link>
        <Link to={`${match.url}/products`}>Categories</Link>
        <Link to={`${match.url}/products`}>Orders</Link>
        <Link to={`${match.url}/products`}>Freigth</Link>
        <Link to={`${match.url}/products`}>Payments</Link>
      </div>
      <div className="portal">
        <Route path={`${match.url}/products`} component={ProductContainer} />
        <Route exact path={`${match.url}`} component={HomeContainer}/>
      </div>

    </div>
);

export default Portal;
