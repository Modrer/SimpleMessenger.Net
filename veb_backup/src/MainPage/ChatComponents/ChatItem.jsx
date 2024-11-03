import React from 'react'
import classes from './Chat.module.css'


export default function ChatItem({chat, openChat }) {

  let lastMessage = React.useRef("");
  let unreadedMessages = React.useRef(0);

  let messages = chat.messages;

  if(messages){
    lastMessage.current = messages[messages.length - 1]?.message;
  }

    if(!lastMessage.current){
      lastMessage.current = '';
    }
    //console.log(chat);
    unreadedMessages.current = chat.messages.reduce(
      (accumulator, currentValue) => {
        if(!currentValue.isReaded){
          return accumulator  + 1 
        }
         return accumulator},
      0
    );

  return (
    <div onClick={ () => {openChat(chat)}} className={classes.chatItem}>
      <div className={classes.chatItemName}>{chat.name}</div>
      <div className={classes.chatItemDown}>
        <div>
          {
          lastMessage && lastMessage.current.length > 15 
          ?
          lastMessage.current.slice(0, 10) + "..."
          :
          lastMessage.current
          }
        </div>
        <div>
          {
          unreadedMessages.current > 50 ?
          "50+" :
          unreadedMessages.current
          }
        </div>
      </div>
      
    </div>
  )
}
