import React from 'react'
import LoadinComponent from './Loading'
import { shallow, configure, mount, render  } from 'enzyme';
import sinon from 'sinon';
import Adapter from 'enzyme-adapter-react-16';
import PropTypes from 'prop-types'

configure({ adapter: new Adapter() });

describe('loading', () => {
  it('should render loading', () => {
    let message = "loading";
    const wrapper = render(<LoadinComponent text={message} />);
    expect(wrapper.text()).toContain(message);
  })
})