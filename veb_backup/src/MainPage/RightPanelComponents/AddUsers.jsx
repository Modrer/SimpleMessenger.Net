import React from 'react'
import { GetContacts } from '../../Requests/Contacts';
import UserList from './UserList';
import { GetUserById } from '../../Requests/User';

export default function AddUsers({chatId, refresh, members}) {
  const [contacts, setContacts] = React.useState([]);

function SetUsers(){
  
  GetContacts()
  .then((response) => {return response.json()})
  .then((data) => 
  {
      Promise
      .all(
        data.map((contact) => {
        return GetUserById(contact.contactId)
      })
      )
      .then((promises) => {
        let users = promises.map((resp) => {
         return resp.json(); 
        })
        Promise.all(users)
        .then((data) => {
          let newContacts = data.filter((user) => {
            let mmbr = members.findLast((member) => {
              return member.id === user.id;
            });
            return !mmbr;
        
          })
          console.log("newContacts",newContacts);
          console.log("members",members);
          setContacts(newContacts);

        })
        
      })
  } );
}

 

 React.useEffect(() => {

  SetUsers();
},[JSON.stringify(members)]);
  // useEffect(() => {
  //   GetContacts().then((response) => {
  //     return response.json();
  //   }).then((cnts) => { 
  //     console.log(cnts);
  //     let newContacts = cnts.filter((user) => {
  //       let mmbr = members.findLast((member) => {
  //         return member.id === user.id;
  //       });
  //       return mmbr;

  //     })
     
  //     setContacts(newContacts);
      
  //   });
    
  // },[])
  
  return (
    <div>
      <UserList chatId={chatId} users={contacts} refresh={refresh} ></UserList>
    </div>
  )
}
