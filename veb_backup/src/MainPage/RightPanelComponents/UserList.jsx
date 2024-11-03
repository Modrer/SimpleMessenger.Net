import React from 'react'
// import { GetUserById } from '../../Requests/User';
import UserItem from './UserItem';
// import { GetContacts } from '../../Requests/Contacts';
// import classes from './RightPanelComponents.module.css'

export default function UserList({chatId, refresh, users }) {
  // const [users, setUsers] = React.useState([]);
  
  // function PutUser(user){
  //   let arr = users;
  //   arr.push(user);
  //   // console.log(arr);
  //   setUsers(arr);
  // }
  
  // React.useEffect(() => {
  //   GetContacts().then((response) => {return response.json()})
  // .then((data) => {
  //     Promise.all(data.map((contact) => {
  //       return GetUserById(contact.contactId)
  //     })).then((promises) => {
  //       promises.forEach((resp) => {
  //         resp.json().then((data) => {
  //           PutUser(data)
  //         })
          
  //       })
  //     })
  // } );
  // // eslint-disable-next-line
  // }, [])
  return (

    <div>
      {
        users.length > 0 ?
        
        users.map((user) => {
          return  <UserItem key={user.id} user={user} chatId={chatId} refresh={refresh}></UserItem>
        })
        
        :
        null
      }
    </div>
  )
}
