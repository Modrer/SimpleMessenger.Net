import React from 'react'
import { RemoveMember } from '../../Requests/Members'
import classes from './Member.module.css'

export default function Member({member, chatId,isOwner, refresh }) {
  return (
    <div className={classes.Member}> 
      <div>{member.name}</div>
      {
        isOwner ?
        <button onClick={ () => {RemoveMember(chatId,member.id); refresh()}}> Remove</button>
        :
        null
      }
    </div>
  )
}
