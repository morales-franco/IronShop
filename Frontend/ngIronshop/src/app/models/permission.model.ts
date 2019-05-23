export class Permission{
    constructor(
        public permissionId : number,
        public displayName: string = null,
        public display: boolean = false,
        public url: string,
        public menuId: number = null,
        public menuDisplayName: string= null,
        public menuIcon: string= null
    ){}
}