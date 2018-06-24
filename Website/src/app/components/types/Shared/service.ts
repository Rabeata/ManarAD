import { Injectable } from '@angular/core';
import { serverConfig } from '../../../config/api';
import {TypesData} from './model';
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
export class TypesService {
  api_url: string = serverConfig.apiUrl + 'Types'; // Just change this URL .....

    constructor(private http: HttpClient
    ) {}

    getAll(page = 1, itemfilter: TypesData = null, pageSize: number = 30): Observable<any> {
 let Params  = new HttpParams();
 Params = Params.append("page",page+"");
 Params = Params.append("pageSize", pageSize+"");
Object.keys(itemfilter).forEach(function (key) {
    Params = Params.append(key, itemfilter[key]);
});
 return this.http.get(this.api_url ,{ params:Params });
    }


    getForDDL(): Observable<any>{
        return this.http.get(this.api_url + '/ddl');
    }


    getOne(id):Observable<any> {
        return this.http.get(this.api_url + '/' + id);
    }

    addNewItem (item: TypesData): Observable<TypesData> {
        return this.http.post<TypesData>(this.api_url, item, httpOptions);

      }

      updateItem (item: TypesData): Observable<TypesData> {

        return this.http.put<TypesData>(this.api_url + '/' + item.id, item, httpOptions);

  }

  deleteItem (id: number): Observable<{}> {
    const url = `${this.api_url}/${id}`; // DELETE api/heroes/42
    return this.http.delete(url, httpOptions);

  }

}
