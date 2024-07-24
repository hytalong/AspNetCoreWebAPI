import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { Aluno } from '../models/Aluno';
import { PaginatedResult } from '../models/Pagination';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class AlunoService {

  baseURL = `${environment.mainUrlAPI}aluno`;

  constructor(private http: HttpClient) { }

  getAll(page?: number, itemsPerPage?: number): Observable<PaginatedResult<Aluno[]>> {
    const paginatedResult: PaginatedResult<Aluno[]> = new PaginatedResult<Aluno[]>();

    let params = new HttpParams();

    if (page != null && itemsPerPage != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', itemsPerPage.toString());
    }

    return this.http.get<Aluno[]>(this.baseURL, { observe: 'response', params })
      .pipe(
        map(response => {
          paginatedResult.result = response.body ?? [];
          const paginationHeader = response.headers.get('Pagination');
          paginatedResult.pagination = paginationHeader ? JSON.parse(paginationHeader) : { totalItems: 0 };
          return paginatedResult;
        })
      );
  }



  getById(id: number): Observable<Aluno> {
    return this.http.get<Aluno>(`${this.baseURL}/${id}`);
  }

  getByDisciplinaId(id: number): Observable<Aluno[]> {
    return this.http.get<Aluno[]>(`${this.baseURL}/ByDisciplina/${id}`);
  }

  post(aluno: Aluno) {
    return this.http.post(this.baseURL, aluno);
  }

  put(aluno: Aluno) {
    return this.http.put(`${this.baseURL}/${aluno.id}`, aluno);
  }

  patch(aluno: Aluno) {
    return this.http.patch(`${this.baseURL}/${aluno.id}`, aluno);
  }

  trocarEstado(alunoId: number, ativo: boolean) {
    return this.http.patch(`${this.baseURL}/${alunoId}/trocarEstado`, {estado: ativo});
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }

}

