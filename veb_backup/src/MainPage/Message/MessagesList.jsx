import React from 'react'
import Message from './Message'
import classes from './Message.module.css'

export default function MessagesList({messages, setReaded}) {

  const listRef = React.useRef();

  function IsDown(e){
    
    const result = e.target.scrollHeight - Math.ceil(e.target.scrollTop) === e.target.clientHeight;
    console.log("IsDown",result);
    return result;

  }
  function HandleScroll(e){
    let message = messages.findLast(() => {return true});

    if(!message){
      return;
    }

    if(IsDown(e) && message && !message.isReaded){
      console.log('message',message);
      setReaded();
    }
  }
  React.useEffect(()=>{
    let list = listRef.current;
    const result = list.scrollHeight - Math.ceil(list.scrollTop) === list.clientHeight;
    let message = messages.findLast(() => {return true});
    if(result && message && !message.isReaded){
      setReaded();
    }

  },[JSON.stringify(messages)])
  return (
    <div ref={listRef} className={classes.MessagesList} onScroll={HandleScroll}>
      {
      messages.length > 0 ?
      messages.map((message) => {
        return <Message key={message.id} text={message.message} username={message.sender.name} ></Message>

      })
      :
      null
    }
    </div>
  )
}
