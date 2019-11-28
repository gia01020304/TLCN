import { prop, elementClass, required, model } from "@rxweb/reactive-form-validators"

export class SystemConfigurate {
    @required()
    fbToken: string;
    @required()
    fbUrlApi: string;
    @required()
    msEndPoint: string;
    @required()
    msKey: string;
    @required()
    smtpSenderName: string;
    @required()
    smtpSender: string;
    @required()
    smtpPassword: string;
    @prop()
    smtpPort: number;
    @required()
    mailServer: string;
}
