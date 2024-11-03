import React from 'react'
import { AddContact } from '../../Requests/Contacts'
import classes from './SearchResult.module.css';

export default function UserResult({user}) {
  function PutContact(){
    AddContact(user.id);
  }
  return (
    <div className={classes.UserResult}>
      <div>{user.name}</div>
      <button type='button' onClick={PutContact} > Add </button>
    </div>
  )
}
