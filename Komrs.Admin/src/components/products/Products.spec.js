import React from 'react'
import Products from './Products'
import { shallow, configure, mount  } from 'enzyme';
import sinon from 'sinon';
import Adapter from 'enzyme-adapter-react-16';
import PropTypes from 'prop-types'

configure({ adapter: new Adapter() });

const products = [{ id: 1, name: "A product", price : "29.99", created: "2018-01-22"}, { id: 2, name: "A product", price : "29.99", created: "2018-01-22"}, { id: 3, name: "A product", price : "29.99", created: "2018-01-22"}];

describe('products', () => {
  it('should render one product', () => {
    const login = sinon.spy();
    const wrapper = mount(<Products products={[products[0]]} />);
    expect(wrapper.find('.product').length).toEqual(1);
  })

  it('should render three products', () => {
    const login = sinon.spy();
    const wrapper = mount(<Products products={products} />);
    expect(wrapper.find('.product').length).toEqual(3);
  })
})