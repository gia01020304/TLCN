import { NbPasswordStrategyMessage, getDeepFromObject, NbPasswordStrategyModule, NbAuthStrategyOptions, NbPasswordStrategyToken, NbAuthJWTToken, NbPasswordStrategyReset } from '@nebular/auth';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../../environments/environment'
export class NbPasswordAuthStrategyOptions extends NbAuthStrategyOptions {
  /**
   *
   */
  name: string = "email";
  body: {};
  baseEndpoint?= environment.apiUrl;
  login?: boolean | NbPasswordStrategyModule = {
    endpoint: '/auth/login',
    method: 'post',
    requireValidToken: true,
    redirect: {
      success: '/',
      failure: null,
    },
    defaultErrors: ['Login/Email combination is not correct, please try again.'],
    defaultMessages: ['You have been successfully logged in.'],
  };
  logout?: boolean | NbPasswordStrategyReset = {
    alwaysFail: false,
    method: 'null',
    endpoint: '/',
    redirect: {
      success: '/auth',
      failure: '/auth',
    },
    defaultErrors: ['Something went wrong, please try again.'],
    defaultMessages: ['You have been successfully logged out.'],
  };
  token?: NbPasswordStrategyToken = {
    class: NbAuthJWTToken,
    key: 'token',
    getter: (module: string, res: HttpResponse<Object>, options: NbPasswordAuthStrategyOptions) => {
      return getDeepFromObject(
        res.body,
        options.token.key,
      )
    },
  };
  errors?: NbPasswordStrategyMessage = {
    key: 'data.errors',
    getter: (module: string, res: HttpErrorResponse, options: NbPasswordAuthStrategyOptions) => {
      return getDeepFromObject(
        res.error,
        options.errors.key,
        options[module].defaultErrors,
      )
    },
  };
  messages?: NbPasswordStrategyMessage = {
    key: 'data.messages',
    getter: (module: string, res: HttpResponse<Object>, options: NbPasswordAuthStrategyOptions) => {
      return getDeepFromObject(
        res.body,
        options.messages.key,
        options[module].defaultMessages,
      )
    },
  };

  validation?: {
    password?: {
      required?: boolean;
      minLength?: number | null;
      maxLength?: number | null;
      regexp?: string | null;
    };
    email?: {
      required?: boolean;
      regexp?: string | null;
    };
    fullName?: {
      required?: boolean;
      minLength?: number | null;
      maxLength?: number | null;
      regexp?: string | null;
    };
  };
}