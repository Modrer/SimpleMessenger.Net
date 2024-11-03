import React, { useState } from 'react'
import Tabs from './Tabs';
import { TabsEnum
 } from './TabsEnum.';
import UsersResult from './UsersResult';
import ContactsResult from './ContactsResult';
// import classes from './SearchResult.module.css';

export default function SearchPanel({pattern}) {

  const [resultPages, SetResultPage] = useState(TabsEnum.Users);
  
  const SetPanel = React.useCallback((tabsEnum ) => {
    SetResultPage(tabsEnum);
  }, []);


  return (
    <div>
      <Tabs SetPanel={SetPanel} ></Tabs>
      {
        resultPages === TabsEnum.Users ? (<UsersResult pattern={pattern}></UsersResult>) : null
      }
      {
        resultPages === TabsEnum.Contacts ? (<ContactsResult pattern={pattern}></ContactsResult>) : null
      }
    </div>
  )
}
