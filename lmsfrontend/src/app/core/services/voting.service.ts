import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface Voter {
  id: number;
  name: string;
  hasVoted: boolean;
}

export interface Candidate {
  id: number;
  name: string;
  voteCount: number;
}

@Injectable({
  providedIn: 'root'
})
export class VotingService {

  private apiBase = 'https://localhost:7216/api';

  constructor(private http: HttpClient) {}

  // 🔹 Get all voters
  getVoters(): Observable<Voter[]> {
    return this.http.get<Voter[]>(`${this.apiBase}/getvoters`);
  }

  // 🔹 Get all candidates
  getCandidates(): Observable<Candidate[]> {
    return this.http.get<Candidate[]>(`${this.apiBase}/getcandidates`);
  }

  // 🔹 Submit vote
  submitVote(voterId: number, candidateId: number): Observable<any> {
    return this.http.post(`${this.apiBase}/votes`, {
      voterId,
      candidateId
    });
  }
}
