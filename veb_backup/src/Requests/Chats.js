const url = "http://localhost:5000/api/Chat";
export function GetChats(){

  const token = JSON.parse(window.localStorage.getItem("userInfo"))?.token;
    // let data = {
    //   login: nickname.current.value,
    //   password: password.current.value
    // }
    return fetch(url, {
      method: "GET", // *GET, POST, PUT, DELETE, etc.
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
        // 'Content-Type': 'application/x-www-form-urlencoded',
      }
       // body data type must match "Content-Type" header
    });
};
export function MakeChat(name){
  
  let data = {
    "name": name
  };
  const token = JSON.parse(window.localStorage.getItem("userInfo"))?.token;
    // let data = {
    //   login: nickname.current.value,
    //   password: password.current.value
    // }
    return fetch(url, {
      method: "PUT", // *GET, POST, PUT, DELETE, etc.
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: JSON.stringify(data), // body data type must match "Content-Type" header
    });
};

