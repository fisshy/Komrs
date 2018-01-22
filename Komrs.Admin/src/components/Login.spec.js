import React from 'react'
import LoginForm from './Login'
import { shallow, configure, mount  } from 'enzyme';
import sinon from 'sinon';
import Adapter from 'enzyme-adapter-react-16';

configure({ adapter: new Adapter() });

describe('login', () => {
  it('submit should call login', () => {
    const login = sinon.spy();
    const wrapper = mount(<LoginForm login={login} isAuthenticated={false} />);
    wrapper.find('input#username').simulate('change', {target: {value: 'asdf'}});
    wrapper.find('input#password').simulate('change', {target: {value: 'asdf'}});
    wrapper.find('form').simulate('submit');
    expect(login.calledOnce).toEqual(true);
  })
})