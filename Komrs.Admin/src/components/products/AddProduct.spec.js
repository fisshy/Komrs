import React from 'react'
import AddProduct from './AddProduct'
import { shallow, configure, mount  } from 'enzyme';
import sinon from 'sinon';
import Adapter from 'enzyme-adapter-react-16';
import PropTypes from 'prop-types'

configure({ adapter: new Adapter() });

describe('add-product', () => {
  it('should render', () => {
    const login = sinon.spy();
    const wrapper = mount(<AddProduct />);
  })

})