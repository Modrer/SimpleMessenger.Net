import React from 'react'
import { MakeChat } from '../../Requests/Chats';
import classes from './Chat.module.css'

export default function ChatAdder() {

  const nameRef = React.useRef();

  function CreateChat(){
    MakeChat(nameRef.current.value);
  }


  return (
    <div className={classes.ChatAdder}>
      <label htmlFor="chatAdder" >Create new chat</label>
      <input ref={nameRef} id="chatAdder" type='text' placeholder='New chat name' ></input>
      <button onClick={CreateChat}> Create chat</button>
    </div>
  )
}
