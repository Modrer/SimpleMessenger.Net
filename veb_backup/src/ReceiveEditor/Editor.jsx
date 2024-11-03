import React from 'react'
import classes from './Editor.module.css'
export default function Editor() {
  return (
    <div className={classes.body}>"efwdkjed
      <div id="heading" className={classes.heading} contenteditable="true"></div>
      <div id="content" className={classes.content} contenteditable="true"></div>
    </div>
  )
}
