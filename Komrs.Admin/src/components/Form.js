import React from 'react'
import PropTypes from 'prop-types'
import './Form.css';

let Input = ({inputRef, ...rest}) => {
    return (
        <input {...rest} ref={inputRef} />
    )       
}

Input.propTypes = {
    id: PropTypes.string.isRequired,
    type: PropTypes.string.isRequired,
    inputRef: PropTypes.func
}

let Form = ({ onSubmit, children }) => {
    return (
        <form className="form" onSubmit={(e) => { e.preventDefault(); onSubmit(); }}>
            {children}
        </form>
    )
}

Form.propTypes = {
    onSubmit: PropTypes.func.isRequired
}

export { Form, Input }