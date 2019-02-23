import { Profile } from './profile.model';
export class IronToken{

    constructor(
        public token: string,
        public expiration: Date,
        public profile: Profile
    ){}
}
