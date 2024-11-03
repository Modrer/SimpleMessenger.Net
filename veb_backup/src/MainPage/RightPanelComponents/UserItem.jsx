import React from 'react'
import { AddMember } from '../../Requests/Members'
import classes from './RightPanelComponents.module.css'

export default function UserItem({user, chatId, refresh}) {
  return (
    <div className={classes.UserItem}>
      <div>
        {user.name}
      </div>
      <button type='button' onClick={() => {AddMember(chatId, user.id).then(() =>  refresh())}}>
        Add
      </button>
    </div>
  )
}
