import React from 'react'
import MemberList from '../MemberComponent/MemberList'
import { RemoveMember } from '../../Requests/Members'
// import classes from './RightPanelComponents.module.css'

export default function Members({chat, members, refresh}) {
  return (
    <div>
    {
      members.length > 0 ?
      <MemberList members={members} chat={chat} 
      userId={
        JSON.parse(window.localStorage.getItem("userInfo")).id
      } 
      refresh={refresh}
      ></MemberList>
      :
      null
    }
    <button type='button' onClick={() => {RemoveMember(
      chat.id,
      JSON.parse(window.localStorage.getItem("userInfo")).id
      )}} >
        Leave
      </button>
    
  </div>
  )
}
