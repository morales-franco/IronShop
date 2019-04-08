export class Profile{
    constructor(
        public userId : number,
        public fullName: string,
        public email: string,
        public roleId: number = null,
        public role: string = null,
        public imageFileName: string = null,
        public googleAuth: boolean=false
    ){}
}
