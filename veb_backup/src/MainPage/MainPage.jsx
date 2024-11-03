import React from 'react'
import classes from './Main.module.css'
import RigthPanel from './RigthPanel'
// import ChatList from './ChatList'
import LeftPanel from './LeftPanel'
import ChatPanel from './ChatComponents/ChatPanel';
import useWebSocket from 'react-use-websocket';
import { GetChats } from '../Requests/Chats';
import { GetMessages } from '../Requests/Messages';
import { GetMembers, SetReadedMessage } from '../Requests/Members';
import { GetUserById } from '../Requests/User';



export default function MainPage() {
  

  async function MapChat(chat){
    let newChat = chat;
      
    let messages = GetMessages(chat.id).then((result) => {
        return result.json();
      });

    let members = GetMembers(chat.id).then((result) => {
      return result.json()
    });

    await Promise.all([messages, members]).then(([messags, membrs]) => {
      newChat.messages = messags;
      newChat.members = membrs;
    })
    
    return newChat;
  }

  const [activeChat, setActiveChat] = React.useState({});

  const [activeRightPanel, setActiveRightPanes] = React.useState(false);

  const [chats, SetChats] = React.useState([]);

  function OnMessage(message){
  console.log("onMEssage", message);

  let newChats = chats;
  newChats = newChats.map((chat) => {
    //console.log(chat.id + "  " +message.chatId );
    
    if(chat.id === message.chatId){
      console.log(chat);
      chat.messages.push(message);
      console.log(chat);
    }
    return chat;
  })
  SetChats(newChats);
  }

  function OnRemoveChat(id){
    console.log("OnRemoveChat", id);
  
    let newChats = chats;
    newChats = newChats.filter((chat) => {
      return chat.id !== id;
    });
  
    SetChats(newChats);
    }
  function OnAddChat(id){
    console.log("OnAddChat", id); 
    GetChats().then((data) =>{
      return data.json();
    }).then((chats) => {
      return  chats.findLast((chat) => {
        return chat.id === id;
      })
    }).then((chat) => {
      MapChat(chat).then((chat) =>{
        let n = [...chats, chat];
        console.log(n);
        
        SetChats([...chats, chat]);
      } )
    })
  }
 async function OnAddMember(chatId, memberId){
    let user;    
    console.log(chatId, memberId);
    await GetUserById(memberId).then((user) => {
      return user.json();
    }).then((data) => {
      user = data;
    })
    Promise.all(chats.map( async(chat) => {
      if(chat.id === chatId){
        
        chat.members.push(user);
      }
      return chat;
      
    })).then((newChats) => {
      // console.log("newChats",newChats);
      SetChats(newChats);
    })
    
    
  }
  function OnRemoveMember(chatId, memberId){
    console.log("Remove", chatId,memberId);
    let newChats = chats.map( (chat) => {
      if(chat.id === chatId){
        chat.members = chat.members.filter((member) => {
          return member.id !== memberId;
        })
      }
      return chat;
      
    })
    console.log(newChats);
    SetChats(newChats);
    
  }
  function OnReceive(messageEvent){
      let data = JSON.parse(messageEvent.data)
      let type = data.type;
      console.log(data);
      switch(type){
        case 'message':  
        console.log("before", data.data.message);
        OnMessage(data.data); break;
        case 'remove chat':
          OnRemoveChat(data.data);
          break;
        case 'add chat' :
          OnAddChat(data.data);
          break;
        case 'remove member' :
          OnRemoveMember(data.data.chatId,data.data.memberId);
          break;
        case 'add member' :
          console.log(data);
          OnAddMember(data.data.chatId,data.data.memberId);
          break;
        default: break;
      }
  }

  const {
    sendMessage,
    sendJsonMessage,
    lastMessage,
    lastJsonMessage,
    readyState,
    getWebSocket,
  }  = useWebSocket("ws://localhost:5000/ws", {
  
  queryParams : {
    token : JSON.parse(window.localStorage.getItem("userInfo"))?.token
  },
  
    onMessage : (messageEvent) => {
      OnReceive(messageEvent);
      console.log('This has type "message": ', JSON.parse(messageEvent.data));
      
    },
    onClose : (event) => {
      console.log('This close: ', event);
    },
    onError: (event) => {
      console.log('This eror: ', event);
    },
    onOpen: (event) => {
      console.log("Connected");
    }

    
  });

function UpdateChats(){
  GetChats().then((response) => {
    return response.json();
  })
  .then((data) => {
    console.log("Chats",data);
    Promise.all(
      data.map((chat) =>{
      //return MapChat(chat);
      let messages = GetMessages(chat.id).then((result) => {
        return result.json();
      });

    let members = GetMembers(chat.id).then((result) => {
      return result.json()
    });

    
    chat.messages = messages;
    chat.members = members;

    return chat;
    }
    )
    ).then((inf) => {
      
      let newChats =  inf.map(async(chat) => {
        return Promise.all([chat.messages, chat.members])
        .then(([messages, members]) => {
          chat.messages = messages;
          chat.members = members;
        }).then(() => {
          return chat;
        })
        
      });
      Promise.all(newChats)
      .then((newChats) => {
        console.log("Chats",newChats);
        SetChats(newChats);
      })
      
      
    })
  })
}
  React.useEffect(() => {
    UpdateChats();

  
  }, [])

  const SetActive = React.useCallback((chat ) => {
    setActiveChat(chat);
  }, []);

  const GetChatInfo = () => {
    //console.log(activeChat);
    if(!activeChat.id){
      return;
    }
    setActiveRightPanes(true)
  };

  function SetReaded(chatId){
    console.log("setReaded");
    const chat = chats.findLast((element) => {
      return element.id === chatId;
    });

    const lastMessage = chat.messages.findLast(() => {
      return true;
    });
console.log(lastMessage);

    if(!lastMessage){
      return;
    }

    SetReadedMessage(chatId,lastMessage.id).then((result) => {
      if(result.ok){
        let newChats = chats.map((oldChat) => {
          if(oldChat.id === chat.id){
            oldChat.messages = oldChat.messages.map((message) => {
              message.isReaded = true;
              return message;
            })
          }
          return oldChat;
        } )
        SetChats(newChats);
      }
    })

  }

 

  return (
    <div className={classes.main}>

      <LeftPanel SetActive={SetActive} chats={chats}></LeftPanel>
      {/* <ChatList></ChatList> */}
      {
        activeChat ?
        <ChatPanel chats={chats} activeChatId={activeChat.id} GetChatInfo={GetChatInfo} SetReaded={SetReaded} ></ChatPanel>
        
        :
        null
      }
      <RigthPanel chats={chats} activeChatId={activeChat.id} active={activeRightPanel} setActive={setActiveRightPanes} ></RigthPanel>
    </div>
  )
}
