var body = document.getElementsByTagName('body')[0];
var data = [];
let theme = "";


body.childNodes.forEach((node) => {
  if(! node.innerText){
    return;
  }
  if(node.tagName === 'H3'){
    theme = node.innerText;
    return;
  }

  let id = node.getElementsByTagName("a")[0]?.href?.substring(56,58);

  let text = node.innerText;

    
let flag = true;
let authors = "";
    let regex = /^[^.,\n]{3,20}(\s.{1,2}[.]{1}){1,3}([ |,|\n]|$)/;
    while(flag){
        let found = text.match(regex);
        if(!found){
            flag = false;
            continue;
        }
        authors += found[0];
        text = text.replace(found[0],"");
    }

  
    
  let name = text;

  let info = {
    id: id,
    name: name,
    authors: authors,
    theme: theme
  }
  data.push(info)
})
data.shift();
console.log(data);




data = [];
for(let i = 0; i < dataEn.length; i++){
    let id = dataEn[i].id;
    let info = {
        id: id,
        nameEn: dataEn[i].name,
        nameRu: dataRu[i].name,
        theme: dataRu[i].theme,
        authors: dataRu[i].authors,
        
    }
    data.push(info);
}


"".concat(...data.map((elem) => {
    
  return 'INSERT INTO `practic`.`stattya` (`idStattya`, `NameRu`, `NameUa`, `NameEn`, `Authors`, `Theme`) VALUES' + " " +
  `('${elem.id}', '${elem.nameRu}', "", '${elem.nameEn}', '${elem.authors}', '${elem.theme}');`
}))