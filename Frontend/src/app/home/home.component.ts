import { Component, OnInit } from '@angular/core';
import { ProductService } from '../api/product.service';
import { ProductModel } from '../api/ProductModel';
import { DetailModel } from '../api/DetailModel';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  products: ProductModel;

  constructor(private productService: ProductService) {}

  ngOnInit(): void {
    this.productService
      .getProducts()
      .subscribe(
        (data: {
          id: number;
          name: string;
          stock: number;
          detail: DetailModel;
        }) => {
          console.log(data);
          this.products = data;
        }
      );
  }

  getSumSellOut(details: Array<DetailModel>): number {
    let sum = 0;
    details.forEach((detail) => {
      sum += detail.sellOut;
    });
    return sum;
  }
}
