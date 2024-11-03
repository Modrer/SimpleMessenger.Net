import React, { useEffect } from 'react'
import { GetUserById } from '../../Requests/User';
import MessageSender from '../Message/MessageSender';
import classes from './Chat.module.css';
import MessagesList from '../Message/MessagesList';

export default function ChatPanel({chats, activeChatId, GetChatInfo, SetReaded}) {

  let chat = chats?.findLast((element) => element.id === activeChatId);

  const [messages, SetMessages] = React.useState([]);

  function UpdateMessages(){
    if(!chat || ! chat.messages){
      return;
    }
      let mappedMessages = chat.messages.map(async (array) => {

        array.sender = await GetUserById( array.senderId).then((data) => {
          return  data.json();
        }).then((data) => {
          return data
        });
        return array;
      });
      
      Promise.all(mappedMessages).then((mm) => {
        SetMessages(mm)
      })
      
      
    
  }
  // function mapMessages(messages){

  // }
  function SetReadedChat(){
    SetReaded(chat.id);
  }
  useEffect(() => {
    UpdateMessages();
    // eslint-disable-next-line
  }, [chats, activeChatId]);

  return (
    <div className={classes.ChatPanel}>
      <div className={classes.ChatName} onClick={GetChatInfo}>
        {
        chat ?
        chat.name
        :
        "Select chat"
        }
      </div>

    <MessagesList messages={messages} setReaded={SetReadedChat}>

    </MessagesList>
    {
      chat ?
      <MessageSender chatId={chat.id} update={UpdateMessages} ></MessageSender>
      :
      null
    }
    

    </div>
  )
}
