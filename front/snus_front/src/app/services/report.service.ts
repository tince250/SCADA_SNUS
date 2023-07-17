import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";


@Injectable({
    providedIn: 'root'
  })
  export class ReportService {

    constructor(private http: HttpClient) { }

    getAllTags(): Observable<any> {
        return this.http.get<any>(environment.apiHost + "/tag", {
            headers: new HttpHeaders({
              'Content-Type': 'application/json',
            })
          });
    }

    getAllAITags(): Observable<any> {
        return this.http.get<any>(environment.apiHost + "/tag/AI", {
            headers: new HttpHeaders({
              'Content-Type': 'application/json',
            })
          });
    }

    getAllDITags(): Observable<any> {
        return this.http.get<any>(environment.apiHost + "/tag/DI", {
            headers: new HttpHeaders({
              'Content-Type': 'application/json',
            })
          });
    }

    getAllTagsByAddress(address: string): Observable<any> {
        return this.http.get<any>(environment.apiHost + "/tag/" + address, {
            headers: new HttpHeaders({
              'Content-Type': 'application/json',
            })
          });
    }

    getAlarmsBetweenDates(start?: string, end?: string): Observable<any> {
        let dateRange = {
            StartTime: start,
            EndTime: end
        }
        return this.http.post<any>(environment.apiHost + "/alarm/between/dates", dateRange, {
            headers: new HttpHeaders({
              'Content-Type': 'application/json',
            })
          });
    }

    getAlarmsByPriority(priority: string): Observable<any> {
        return this.http.get<any>(environment.apiHost + "/alarm/" + priority, {
            headers: new HttpHeaders({
              'Content-Type': 'application/json',
            })
          });
    }

  }


  export interface TagRecordDTO {
    Id: number,
    Timestamp: string,
    Value: number,
    Description: string,
    IOAddress: string
  }

  export interface ALarmDTO {
    Id: number,
    Timestamp: string,
    Value: number,
    Priority: string,
    IOAddress: string
  }

  export interface AnalogInputDTO {
    Id: number,
    Value: number,
    Description: string,
    IOAddress: string,
    LowLimit: number,
    HighLimit: number,  
    Unit: string
  }

  export interface DigitalInputDTO {
    Id: number,
    Value: number,
    Description: string,
    IOAddress: string,
  }
  