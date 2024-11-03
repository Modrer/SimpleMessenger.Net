import React, { useRef } from 'react'
import classes from './Search.module.css'

export default function Search({SetSearched, IsSerched,getSearched}) {
  const input = useRef();
  return (
    <div className={classes.search}>
        <input ref={input} placeholder='Search' 
         onClick={() => {SetSearched(true); getSearched(input.current.value) }} 
         onBeforeInput={() => {getSearched(input.current.value)  }}
         ></input>
        {
          IsSerched ?
          <i className="fa-sharp fa-solid fa-xmark" onClick={() => {SetSearched(false)}}></i>
          :
          null
        }
      </div>
  )
}
