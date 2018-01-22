import React from 'react';
import { render } from 'react-dom'
import { createStore } from 'redux'
import Root from './Root';
import komrs from '../reducers'

it('renders without crashing', () => {
    let store = createStore(komrs);
    const div = document.createElement('div');
    render(<Root  store={store}/>, div);
});
