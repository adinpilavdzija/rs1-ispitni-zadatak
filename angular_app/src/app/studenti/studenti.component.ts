import {Component, OnInit} from '@angular/core';
import {HttpClient, HttpHeaders} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";

declare function porukaSuccess(a: string): any;

declare function porukaError(a: string): any;

@Component({
  selector: 'app-studenti',
  templateUrl: './studenti.component.html',
  styleUrls: ['./studenti.component.css']
})
export class StudentiComponent implements OnInit {

  title: string = 'angularFIT2';
  ime_prezime: string = '';
  opstina: string = '';
  studentPodaci: any;
  filter_ime_prezime: boolean;
  filter_opstina: boolean;
  naslov: string;
  opstine: any;
  showModal: boolean;
  selectedStudent: any;
  defaultnaOpstina: number;


  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  testirajWebApi(): void {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Student/GetAll", MojConfig.http_opcije()).subscribe((x: any) => {
      this.studentPodaci = x;
      this.defaultnaOpstina = x[0].opstina_rodjenja_id;
    });
  }

  getOpstine(): void {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Opstina/GetByAll", MojConfig.http_opcije()).subscribe(x => {
      this.opstine = x;
    });
  }

  ngOnInit(): void {
    this.testirajWebApi();
    this.getOpstine();
  }

  getFiltered() {
    if (!this.filter_ime_prezime && !this.filter_opstina) {
      this.testirajWebApi();
    } else {
      this.httpKlijent.get(MojConfig.adresa_servera + "/Student/GetAll?ime_prezime=" + this.ime_prezime, MojConfig.http_opcije()).subscribe(x => {
        this.studentPodaci = x;

        if (this.filter_opstina)
          this.studentPodaci = this.studentPodaci.filter((s: any) => s.opstina_rodjenja.description.toLowerCase().startsWith(this.opstina.toLowerCase()));
      });
    }
  }

  dodajStudenta() {
    this.selectedStudent = {
      id: 0,
      ime: this.filter_ime_prezime ? this.ime_prezime[0].toUpperCase() + this.ime_prezime.substring(1).toLowerCase() : '',
      prezime: '',
      opstina_rodjenja_id: this.defaultnaOpstina
    }
    this.showModal = true;
    this.naslov = "Dodaj";
  }

  urediStudenta(s: any) {
    this.selectedStudent = s;
    this.showModal = true;
    this.naslov = "Uredi";
  }

  obrisiStudenta(id: number) {
    this.httpKlijent.delete(MojConfig.adresa_servera + "/Student/Delete?id=" + id, MojConfig.http_opcije()).subscribe(x => {
      this.getFiltered();
    });
  }

  goToMaticna(id: number) {
    this.router.navigate(['student-maticnaknjiga', id]);
  }

  saveChanges() {
    this.httpKlijent.put(MojConfig.adresa_servera + "/Student/SaveChanges", this.selectedStudent, MojConfig.http_opcije()).subscribe(x => {
      this.getFiltered();
    });
    this.showModal = false;
  }
}
