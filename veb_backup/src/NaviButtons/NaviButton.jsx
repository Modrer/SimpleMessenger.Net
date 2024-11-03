import React from 'react'
import classes from './Navi.module.css'
import { Link } from 'react-router-dom';
// let text = {
//   short_text: "Видалення шаблону квитанції",
//   long_text: "Дозволяє видалити шаблон квитанції",
//   id: "button3",
//   link: "templatedelete.html"
// }
function NaviButton(props) {
  
  return (
    <Link to={props.button.link}Z>
      <button type="button" className={classes.btn}>{props.button.description} </button>
    </Link>


  )
}

export default NaviButton