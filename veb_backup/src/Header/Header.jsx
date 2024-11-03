import React from 'react'
import classes from './Header.module.css'
import { Link } from 'react-router-dom'
export default function Header() {
  return (
    <div className={classes.header}>
      <Link to='/' className={classes.header_text}>
        <div>
        SimpleMessenger
        </div>
      </Link>
      <div></div>
    </div>
  )
}
