import React from 'react'
import SearchPanel from './SearhResult/SearchPanel';
import ChatList from './ChatComponents/ChatList';
import classes from './Main.module.css';
import Search from './SearchComponents/Search';
import { useNavigate } from 'react-router';
import ChatAdder from './ChatComponents/ChatAdder';

export default function LeftPanel({SetActive, chats}) {
  const [IsSerched, SetSearched] = React.useState(false);
  const setSearched = React.useCallback((isSearched ) => {

    console.log("leftPanel");
    
    if(isSearched && !IsSerched){
      SetSearched(isSearched);
      return;
    }
    if(!isSearched){
      SetSearched(isSearched);
    }
  }, [IsSerched]);

  const [pattern, setPattern] = React.useState("");


  const navigate = useNavigate();

  React.useEffect(() => {
    // eslint-disable-next-line
  },[]);

  return (
    <div className={classes.LeftPanel}>
      <div className={classes.SearchUpPanel}>
      <button className={classes.LogOut} onClick={() => {
        window.localStorage.setItem("userInfo",null);
          navigate('/logIn/login');
        }} >
          LogOut
        </button>
        <Search IsSerched={IsSerched} SetSearched={setSearched} getSearched={setPattern}></Search>
      </div>
      
      {
        IsSerched ? 
        <SearchPanel pattern={pattern} >

        </SearchPanel>
        :
        <ChatList chats={chats} openChat={SetActive}>

        </ChatList>
      }
      {
        IsSerched ?
        null :
        <ChatAdder>

            </ChatAdder>
      }
    </div>
  )
}
