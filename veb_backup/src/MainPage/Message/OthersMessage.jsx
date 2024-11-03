import React from 'react'
import classes from './Message.module.css'
export default function OthersMessage({username, text}) {

  return (
    <div className={classes.Message}>
      <div className={classes.MessageSender}>
        {username}
      </div>
      <div className={classes.MessageText}>
        {text}
      </div>
    </div>
  )
}
