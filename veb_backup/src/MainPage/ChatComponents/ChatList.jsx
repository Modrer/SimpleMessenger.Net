import React from 'react'
import classes from './Chat.module.css'
import ChatItem from './ChatItem'
//import ChatAdder from './ChatAdder';

export default function ChatList({chats, openChat}) {
console.log("updateChatList");
console.log(chats);
  return (
    <div className={classes.chatlist}>
      
      {
        chats.length === 0 ?
        "Nothing here" :

        chats.map( (chat) => {
          return <ChatItem key={chat.id}
          chat={chat}
          openChat={openChat}
          ></ChatItem>
        })
      }
    </div>
  )
}
