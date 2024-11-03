import React from 'react'
import classes from './Message.module.css'
import { SendMessage } from '../../Requests/Messages';

export default function MessageSender({chatId, update}) {
  const message = React.useRef();

  function Send(){
    if(!chatId){
      return;
    }
    let data = {
     ChatId: chatId,
     Message: message.current.value
    }
    SendMessage(data).then((data) => {
      //update();
    })
    message.current.value = "";
  }
  return (
    <div className={classes.Sender}>
      <input ref={message} placeholder='Write you`r message' 
      onKeyUp={(e) => {
        if(e.key === "Enter"){
          Send();
          message.current.value = "";
        }
        
      }
      } >
      </input>
      <button onClick={Send}  type="button">
        Send
      </button>
    </div>
  )
}
