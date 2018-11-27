export interface IMenu{
    title: string,
    icon: string,
    submenu: ISubmenu[]    
}

 interface ISubmenu{
    title: string,
    url: string
}