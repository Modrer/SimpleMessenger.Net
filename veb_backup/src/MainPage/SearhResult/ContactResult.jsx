import React from 'react'
import classes from './SearchResult.module.css'

export default function ContactResult({user, removeContact}) {
console.log(user);
  return (
    <div className={classes.UserResult}>
      <div>
        {user.name}
      </div>
      <button onClick={ () => {removeContact(user.id)}}>
        Remove
      </button>
    </div>
  )
}
