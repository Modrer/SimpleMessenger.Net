import React from 'react'
import MyMessage from './MyMessage';
import OthersMessage from './OthersMessage';

export default function Message({username, text}) {
  const userName = JSON.parse(window.localStorage.getItem("userInfo")).name;

  if(userName === username){
    return (
      <MyMessage text={text} ></MyMessage>
    );
  }
  else{
    return (
      <OthersMessage text={text} username={username} ></OthersMessage>
    );
  }

  // return (
  //   <div>
  //   {
  //     userName === username ?
  //     <MyMessage text={text} ></MyMessage>
  //     :
  //     <OthersMessage text={text} username={username} ></OthersMessage>
  //   }
  //   </div>
    
  // )
}
