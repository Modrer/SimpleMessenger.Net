import React from 'react'
import { TabsEnum } from './TabsEnum.'
import classes from './SearchResult.module.css'

export default function Tabs({SetPanel}) {
  const SetUsers = () =>{
    SetPanel(TabsEnum.Users);
  }
  const SetContacts = () =>{
    SetPanel(TabsEnum.Contacts);
  }


  return (
    <div className={classes.Tabs} >
      <button onClick={SetUsers}>
        Users
      </button>
      <button onClick={SetContacts}>
        Contacts
      </button>
    </div>
  )
}
