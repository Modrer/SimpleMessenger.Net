import React from 'react'
import NaviButton from './NaviButton'
import classes from './Navi.module.css'

function NaviButtons() {

  return (
    <div className={classes.buttonsitem}>
      Меню
      <br></br>
      <div className={classes.buttonlist}>
        {
          buttons.map((button, index) =>{
            return <NaviButton button={button} key={index} ></NaviButton>
          })
        }
      </div>
      
    </div>
  )
}

export default NaviButtons

const buttons = [
  {
    description: "Створення нової квитанції",
    hint: "Дозволяє створити нову квитанцію по шаблону",
    link: "createreceive.html"
  },
  {
    description: "Створення нового шаблону квитанції",
    hint: "Дозволяє створити новийу шаблон квитанції",
    link: "templatecreate.html"
  },
  {
    description: "Видалення шаблону квитанції",
    hint: "Дозволяє видалити шаблон квитанції",
    link: "templatedelete.html"
  },
  {
    description: "Оновлення шаблону квитанції",
    hint: "Дозволяє оновити шаблон квитанції",
    link: "templateupdate.html"
  }
]