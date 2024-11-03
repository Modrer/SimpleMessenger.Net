import React from 'react'
import { GetContacts, RemoveContact } from '../../Requests/Contacts'
import ContactResult from './ContactResult';
import { GetUserById } from '../../Requests/User';

export default function ContactsResult({pattern}) {
  const [contacts, setContacts] = React.useState([]);

  function remove(contactId){
    let arr = contacts;
    console.log(contactId);
    RemoveContact(contactId);

    return setContacts(arr.filter((contact) => {
      return contact.id !== contactId;
    }))
  }

  function PutContact(user){
    let arr = contacts;
    arr.push(user);
    setContacts( arr);
  }

  React.useEffect(() => {
    //setContacts([]);

    GetContacts().then((response) => {return response.json()})
    .then((data) => {
        Promise.all(data.map((contact) => {
          return GetUserById(contact.contactId)
        })).then((promises) => {
          promises.forEach((resp) => {
            resp.json().then((data) => {
              PutContact(data)
            })
            
          })
        })
    } );
    // eslint-disable-next-line
  },[])
  return (
    <div>
      {
        contacts.filter((contact) => {
          console.log(contact);
          console.log(contact.name.includes(pattern));
          return contact.name.includes(pattern)

        }).map((contact) => {
          console.log(contacts);
          return <ContactResult key={contact.id} user={contact} removeContact={remove}></ContactResult>
        })
        
      }
    </div>
  )
}
