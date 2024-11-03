const url = "http://localhost:5000/api/Members";
export function GetMembers(id){

  const token = JSON.parse(window.localStorage.getItem("userInfo")).token;
    let data = {
      id: id
    };
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
export function RemoveMember(chatId, memberId){

  const token = JSON.parse(window.localStorage.getItem("userInfo")).token;
    let data = {
      "chatId": chatId,
      "memberId": memberId
    };
    return fetch(url, {
      method: "DELETE", // *GET, POST, PUT, DELETE, etc.
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: JSON.stringify(data), // body data type must match "Content-Type" header
    });
};
export function AddMember(chatId, memberId){

  const token = JSON.parse(window.localStorage.getItem("userInfo")).token;
    let data = {
      "chatId": chatId,
      "memberId": memberId
    };
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

export function SetReadedMessage(chatId, messageId){

  const token = JSON.parse(window.localStorage.getItem("userInfo")).token;
    let data = {
      "chatId": chatId,
      "messageId": messageId
    };
    return fetch(url, {
      method: "PATCH", // *GET, POST, PUT, DELETE, etc.
      headers: {
        "Content-Type": "application/json",
        "Authorization": `Bearer ${token}`
        // 'Content-Type': 'application/x-www-form-urlencoded',
      },
      body: JSON.stringify(data), // body data type must match "Content-Type" header
    });
};