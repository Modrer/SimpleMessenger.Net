const url = "http://localhost:5000/api/User";
export function GetUserById(id){

  let data = {
    id:id
  };
  const token = JSON.parse(window.localStorage.getItem("userInfo")).token;
    // let data = {
    //   login: nickname.current.value,
    //   password: password.current.value
    // }
    return fetch(url, {
      method: "POST", // *GET, POST, PUT, DELETE, etc.
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: JSON.stringify(data), // body data type must match "Content-Type" header
    });
};
export function SearchUser(name){

  let data = {
    Name:name
  };
  const token = JSON.parse(window.localStorage.getItem("userInfo")).token;
    // let data = {
    //   login: nickname.current.value,
    //   password: password.current.value
    // }
    return fetch(`${url}/Search`, {
      method: "POST", // *GET, POST, PUT, DELETE, etc.
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: JSON.stringify(data), // body data type must match "Content-Type" header
    });
};