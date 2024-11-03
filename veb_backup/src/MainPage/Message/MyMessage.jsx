import React from 'react'
import classes from './Message.module.css'

export default function MyMessage({username, text}) {

  return (
    <div className={classes.MyMessage}>
      <div>
        {username}
      </div>
      <div className={classes.MessageText}>
        {text}
      </div>
    </div>
  )
}