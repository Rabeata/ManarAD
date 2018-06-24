import { Injectable } from '@angular/core';
import { serverConfig } from '../../../config/api';
import {PrintsData} from './model';
import { Observable } from 'rxjs';
import { of } from 'rxjs';
import { catchError } from 'rxjs/operators';
//import { Response,Http } from '@angular/http';
import { HttpClient ,HttpParams} from '@angular/common/http';
import { HttpHeaders } from '@angular/common/http';

//import { HttpErrorHandler,HandleError } from '../../../shared/http-error-handler.service';


const httpOptions = {
    headers: new HttpHeaders({
      'Content-Type':  'application/json'
    })
  };

@Injectable()
export class PrintsService {
  api_url: string = serverConfig.apiUrl + 'Prints'; // Just change this URL .....

    constructor(private http: HttpClient
    ) {}

    getAll(page = 1, itemfilter: PrintsData = new PrintsData(), pageSize: number = 30): Observable<any> {
      return this.http.post(this.api_url + "/get?page=" + page + "&pageSize=" + pageSize, itemfilter);
    }

    exportXls(itemfilter: PrintsData = new PrintsData()): Observable<any> {
      let Params = new HttpParams();
      Object.keys(itemfilter).forEach(function (key) {
        Params = Params.append(key, itemfilter[key]);
      });
    
      return this.http.get(this.api_url + "/Export", { params: Params });
    }



    getForDDL(): Observable<any>{
        return this.http.get(this.api_url + '/ddl');
    }


    getOne(id):Observable<any> {
        return this.http.get(this.api_url + '/' + id);
    }

    addNewItem (item: PrintsData): Observable<PrintsData> {
        return this.http.post<PrintsData>(this.api_url, item, httpOptions);

      }

      updateItem (item: PrintsData): Observable<PrintsData> {

        return this.http.put<PrintsData>(this.api_url + '/' + item.id, item, httpOptions);

  }

  deleteItem (id: number): Observable<{}> {
    const url = `${this.api_url}/${id}`; // DELETE api/heroes/42
    return this.http.delete(url, httpOptions);

  }

}
