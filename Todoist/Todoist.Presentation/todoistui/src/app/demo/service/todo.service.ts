import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ToDo } from '../api/todo';
import { environment } from 'src/environments/environment';

@Injectable()
export class ToDoService {

    constructor(private http: HttpClient) { }

    getAllTasks() {
        return this.http.get<ToDo[]>(`${environment.baseApiUrl}/Tasks`)
            .toPromise()
            .then(res => res)
            .then(data => data);
    }

    createTask(payload: any) {
        return this.http.post<any>(`${environment.baseApiUrl}/Tasks`, payload)
            .toPromise()
            .then(res => res)
            .then(data => data);
    }

}
