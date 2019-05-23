import { Permission } from "./permission.model";

export class Profile{
    constructor(
        public userId : number,
        public fullName: string,
        public email: string,
        public roleId: number = null,
        public role: string = null,
        public imageFileName: string = null,
        public googleAuth: boolean=false,
        public permissions: Array<Permission> = null
    ){
        permissions = Array<Permission>();
    }
}
