import React from 'react'
import classes from './Footer.module.css'
import { Link } from 'react-router-dom'
export default function Footer() {
  return (
    <div className={classes.footer}>
          <Link to='/' className={classes.header_text}>
            SimpleMessenger
          </Link>
    </div>

  )
}
