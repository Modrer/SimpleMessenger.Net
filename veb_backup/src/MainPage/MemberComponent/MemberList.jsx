import React from 'react'
import Member from './Member'
import classes from './Member.module.css'

export default function MemberList({members, chat, userId, refresh}) {
  function IsOwner(ownerId, memberId){
    if(ownerId !== userId){
      return false;
    }
    if(memberId === userId){
      return false;
    }
    return true;
  }
  return (
    <div className={classes.MemberList}>
      {
        members.map((member) => {
          return <Member key={member.id} 
          member={member} 
          chatId={chat.id} 
          
          refresh={refresh}
          isOwner={IsOwner(chat.ownerId, member.id)} ></Member>
        })
      }
    </div>
  )
}
