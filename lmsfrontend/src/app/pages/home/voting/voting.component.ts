import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { VotingService,Candidate,Voter } from '../../../core/services/voting.service';

@Component({
  selector: 'app-voting', // ✅ FIXED
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './voting.component.html',
  styleUrl: './voting.component.css',
})
export class VotingComponent implements OnInit {

  voters: Voter[] = [];
  candidates: Candidate[] = [];

  selectedVoterId: number | null = null;
  selectedCandidateId: number | null = null;

  constructor(private votingService: VotingService) {}

  ngOnInit(): void {
    console.log('VotingComponent INIT'); 
    this.loadData();
  }

  loadData(): void {
    this.votingService.getVoters().subscribe({
      next: v => this.voters = v,
      error: e => console.error('Voters load failed', e)
    });

    this.votingService.getCandidates().subscribe({
      next: c => this.candidates = c,
      error: e => console.error('Candidates load failed', e)
    });
  }

  submitVote(): void {
    if (!this.selectedVoterId || !this.selectedCandidateId) {
      alert('Please select voter and candidate');
      return;
    }

    this.votingService
      .submitVote(this.selectedVoterId, this.selectedCandidateId)
      .subscribe({
        next: () => this.loadData(),
        error: err => alert(err?.error || 'Vote failed')
      });
  }
}
