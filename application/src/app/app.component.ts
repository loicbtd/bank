import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';
import { CustomerEntity } from './entities/customer.entity';
import { environment } from '../environments/environment';
import { HttpClientModule, HttpClient } from '@angular/common/http';
import { CustomerWithDetailsModel } from './models/customer-with-details.models';

@Component({
  standalone: true,
  selector: 'application-root',
  imports: [CommonModule, HttpClientModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss',
})
export class AppComponent implements OnInit {
  customerEntities$: Observable<CustomerEntity[]>;

  customerWithDetails?: CustomerWithDetailsModel;

  constructor(private readonly _httpClient: HttpClient) { }

  ngOnInit(): void {
    this.customerEntities$ = this._httpClient.post<CustomerEntity[]>(`${environment.service}/Customer/FindAll`, null);
  }

  openCurrentAccount(customerId: string, initialCredit: string) {
    this._httpClient.post<CustomerEntity[]>(`${environment.service}/CurrentAccount/OpenNewCurrentAccount`, {
      customerId: customerId,
      initialCredit: Number(initialCredit)
    }).subscribe(() => {
      this.customerEntities$ = this._httpClient.post<CustomerEntity[]>(`${environment.service}/Customer/FindAll`, null);
    });
  }

  showDetails(customerId: string) {
    this._httpClient.post<CustomerWithDetailsModel>(`${environment.service}/Customer/FindOneWithDetails`, {customerId: customerId})
      .subscribe(data => {
        this.customerWithDetails = data;
      });
  }


  goBack() {
    this.customerWithDetails = undefined;
  }
}
