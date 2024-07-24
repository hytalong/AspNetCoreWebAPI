import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Router } from '@angular/router';
import { Aluno } from 'src/app/models/Aluno';

@Component({
  selector: 'app-alunos-professores',
  templateUrl: './alunos-professores.component.html',
  styleUrls: ['./alunos-professores.component.scss']
})
export class AlunosProfessoresComponent implements OnInit {

  @Input() public alunos!: Aluno[];
  @Output() closeModal = new EventEmitter<void>();

  constructor(private router: Router) { }

  ngOnInit() {
  }

  alunoSelect(id: number) {
    this.closeModal.emit();
    this.router.navigate(['/alunos', id]);
  }

}
