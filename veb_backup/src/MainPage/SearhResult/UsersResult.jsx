import React from 'react'
import { SearchUser } from '../../Requests/User';
import UserResult from './UserResult';

export default function UsersResult({pattern}) {
  const [users, setUsers] = React.useState([]);

  SearchUser(pattern).then((data) => {
    return data.json();
  }).then((result) => {
    setUsers(result.filter((user) => {
      return user.id !== JSON.parse(window.localStorage.getItem("userInfo")).id;
    }))
  });

  return (
    <div>
      {
        users.map((user) => {
          return <UserResult key={user.id} user={user}></UserResult>
        })
      }
    </div>
  )
}
