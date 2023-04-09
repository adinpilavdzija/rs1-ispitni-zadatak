import {Component, OnInit} from '@angular/core';
import {ActivatedRoute} from "@angular/router";
import {MojConfig} from "../moj-config";
import {HttpClient} from "@angular/common/http";

declare function porukaSuccess(a: string): any;

declare function porukaError(a: string): any;

@Component({
  selector: 'app-student-maticnaknjiga',
  templateUrl: './student-maticnaknjiga.component.html',
  styleUrls: ['./student-maticnaknjiga.component.css']
})
export class StudentMaticnaknjigaComponent implements OnInit {
  id: number;
  selectedStudent: any;
  showModal: boolean;
  akGodine: any;
  upisGodine: any;
  upisi: any;
  showOvjeraModal: boolean;

  constructor(private httpKlijent: HttpClient, private route: ActivatedRoute) {
  }


  ngOnInit(): void {
    this.route.params.subscribe((s: any) => {
      this.id = +s["id"];
      this.getStudent();
    })
    this.getAkGodine();
    this.getUpisi();
  }

  getStudent() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Student/GetById?id=" + this.id, MojConfig.http_opcije()).subscribe(x => {
      this.selectedStudent = x;
    });
  }

  getUpisi() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/MaticnaKnjiga/GetById?id=" + this.id, MojConfig.http_opcije()).subscribe(x => {
      this.upisi = x;
    });
  }

  getAkGodine() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/AkademskeGodine/GetAll_ForCmb", MojConfig.http_opcije()).subscribe(x => {
      this.akGodine = x;
    });
  }

  dodajUpis() {
    this.upisGodine = {
      id: 0,
      datumUpisa: '',
      godinaStudija: '',
      cijenaSkolarine: '',
      isObnova: false,
      studentId: this.id,
      akGodinaId: 2
    }
    this.showModal = true;
  }

  saveChanges() {
    this.httpKlijent.put(MojConfig.adresa_servera + "/MaticnaKnjiga/SaveChanges", this.upisGodine, MojConfig.http_opcije()).subscribe(x => {
      this.getUpisi();
    });
    this.showModal = false;
    this.showOvjeraModal = false;
  }

  dodajOvjeru(u: any) {
    this.upisGodine = {
      id: u.id,
      datumOvjere: new Date().toISOString().split('T')[0],
      //datumOvjere: new Date().toISOString().substring(0,10),
      ovjeraNapomena: ''
    }
    this.showOvjeraModal = true;
  }
}
