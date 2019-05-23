export class Menu{
    public displayName: string;
    public icon: string;
    public order: number;
    public submenues: Submenu[];

    constructor(){
        this.submenues =  new Array<Submenu>();
    }

}

 export class Submenu{
    displayName: string;
    url: string;
}