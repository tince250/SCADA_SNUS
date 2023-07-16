import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AlarmService {

  constructor(private http: HttpClient) { }

  getAlarmsForTag(id: number): Observable<any> {
    return this.http.get<any>(environment.apiHost + "/alarm?tagId=" + id, {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
        })
    });
  }

  addAlarm(dto: AlarmDTO): Observable<any> {
    return this.http.post<any>(environment.apiHost + "/alarm", dto, {
        headers: new HttpHeaders({
            'Content-Type': 'application/json',
        })
    });
  }
}

export interface AlarmDTO {
  tagId: number,
  type: string,
  value: number,
  priority: string
}