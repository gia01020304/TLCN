import { Injectable } from '@angular/core';
import { environment } from 'src/environments/environment';
import { HttpClient } from '@angular/common/http';
import { SystemConfigurate } from '../@models/system-configurate';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SystemConfigurateService {

  constructor(private http: HttpClient) {

  }
  baseUrl = environment.apiUrl + "/systemconfigure";
  getSystemConfigurate(): Observable<SystemConfigurate> {
    return this.http.get<SystemConfigurate>(this.baseUrl);
  }
  saveSystemConfigurate(model: SystemConfigurate): Observable<any> {
    return this.http.post(this.baseUrl + "/save", model,{ responseType:'text'});
  }
}
