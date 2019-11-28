import { required } from '@rxweb/reactive-form-validators';

export class SocialConfiguration {
    @required()
    appId: string;
    @required()
    appSecret: string;
    @required()
    appType: AppType;
    @required()
    token: string;
}
export enum AppType {
    Facebook
}
