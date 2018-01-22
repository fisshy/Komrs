import React from 'react'
import { Form, Input } from './Form'
import { shallow, configure, mount  } from 'enzyme';
import sinon from 'sinon';
import Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

describe('form', () => {
  it('should call submit', () => {
    const onSubmit = sinon.spy();
    const wrapper = mount(<Form onSubmit={onSubmit} />);
    wrapper.find('form').simulate('submit');
    expect(onSubmit.calledOnce).toEqual(true);
  })

  it('should render two inputs', () => {
    const onSubmit = sinon.spy();
    const wrapper = mount(
    <Form onSubmit={onSubmit}>
        <Input type="username" id="username" placeholder="Username" />
        <Input type="password" id="password" placeholder="Password" />
    </Form>);
    expect(wrapper.find('input').length).toEqual(2);
  })

})