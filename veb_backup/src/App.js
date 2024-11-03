import './App.css';
import React from 'react'
import {Routes, Route, useNavigate}
    from 'react-router-dom';
// import Header from './Header/Header';
import Editor from './ReceiveEditor/Editor';
// import Footer from './Footer/Footer';
import SignInUp from './SignInUp/SignInUp';
import MainPage from './MainPage/MainPage';

function App() {
  const navigate = useNavigate();

  React.useEffect(() => {
    console.log("UseEfect",!window.localStorage.getItem("userInfo"));
    if(!window.localStorage.getItem("userInfo")){
      console.log("navigate");
      
      navigate('/logIn/login');
    }
  }, []);
  return (
    <div className="App">
      {/* <Header></Header> */}

      {/* <Editor>  </Editor> */}

      <Routes>
        <Route path='/' element={<MainPage></MainPage>}></Route>
        <Route path='/logIn/*' element={<SignInUp></SignInUp>}></Route>
        <Route path='/receiveeditor' element={<Editor>  </Editor>}></Route>
      </Routes>



      {/* <Footer></Footer> */}
    </div>
  );
}

export default App;
