import React, { useEffect } from 'react'
import classes from './Main.module.css'
// import { GetMembers } from '../Requests/Members';
import Members from './RightPanelComponents/Members';
import AddUsers from './RightPanelComponents/AddUsers';

export default function RigthPanel({chats, activeChatId, active, setActive}) {
  let chat = chats.findLast((item) => {
    return item.id === activeChatId;
  });

  if(!chat){
    chat = {
      "id": 31,
      "name": "new Chat",
      "ownerId": 4,
      "members": []
  }
  };

  const ref = React.useRef();
  function Hide(){
    setActive(false);
    ref.current.style.display = "none";
  }

  const members = chat.members;

  function refresh(){
    console.log("called refresh");
      //setMembers(chat.members);
  }

  useEffect(() => {
    if(active){
      ref.current.style.display = "flex";
    }
    if(!active){
      Hide();
    }
  })

  useEffect(() => {
  },[chat.id])

  return (
    <div ref={ref} className={classes.right}>
      <div className={classes.flexLine}>

        <i onClick={Hide}  className={"fa-sharp fa-solid fa-xmark" + " " + classes.cancelButton}></i>

        <span className={classes.chatInfo}>{chat.name}</span>
        
      </div>
      <Members chat={chat} members={members} refresh={refresh}></Members>
      <AddUsers chatId={chat.id} members={members} refresh={refresh}></AddUsers>
    </div>
  )
}
