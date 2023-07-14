import { HttpClient, HttpHeaders } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";
import { environment } from "src/environments/environment";
import { TableOutputTag } from "../database-manager/database-manager.component";


@Injectable({
    providedIn: 'root'
  })
  export class TagService {

    constructor(private http: HttpClient) { }

    getAllOutputTagsDBManager(): Observable<TableOutputTag[]> {
        return this.http.get<any>(environment.apiHost + "/tag/output-dbm", {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        });
    }

    deleteDigitalOutput(id: number): Observable<any> {
        return this.http.delete<any>(environment.apiHost + "/tag/digital/" + id, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        });
    }

    deleteAnalogOutput(id: number): Observable<any> {
        return this.http.delete<any>(environment.apiHost + "/tag/analog/" + id, {
            headers: new HttpHeaders({
                'Content-Type': 'application/json',
            })
        });
    }
}