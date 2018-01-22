import React from 'react'
import ProductListItem from './ProductListItem'
import { shallow, configure, mount, render  } from 'enzyme';
import sinon from 'sinon';
import Adapter from 'enzyme-adapter-react-16';
import PropTypes from 'prop-types'

configure({ adapter: new Adapter() });

const product = { id: 1, name: "A product", price : "29.99", created: "2018-01-22"};

describe('product-list-item', () => {
  it('should render one product-list-item', () => {
    const wrapper = mount(<ProductListItem product={product} match={{url: "no"}} />);
    expect(wrapper.find('.product').length).toEqual(1);
    expect(wrapper.text()).toContain(product.name);
  })
})