export class UserIndex{
    constructor(
        public userId : number,
        public fullName: string,
        public email: string,
        public role: string = null,
        public imageFileName: string = null,
        public googleAuth: boolean=false
    ){}
}
