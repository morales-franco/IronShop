import { RoleMembership } from "../commons/membership/role.membership";

export class Register{
    constructor(
        public fullName : string,
        public email: string,
        public password: string,
        public role: string = RoleMembership.User
    ) {}
}