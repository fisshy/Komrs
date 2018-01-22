import React from 'react'
import ProductsToolbar from './ProductsToolbar'
import { shallow, configure, mount  } from 'enzyme';
import sinon from 'sinon';
import Adapter from 'enzyme-adapter-react-16';
import PropTypes from 'prop-types'

configure({ adapter: new Adapter() });

describe('products-toolbar', () => {
  it('should have atleast one action', () => {
    const login = sinon.spy();
    const wrapper = mount(<ProductsToolbar />);
    expect(wrapper.text()).toContain("Add new product");
  })

})