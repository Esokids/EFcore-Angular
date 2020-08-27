import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { ProductService } from '../api/product.service';
import { ProductModel } from '../api/ProductModel';
import { DetailModel } from '../api/DetailModel';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.css'],
})
export class ProductComponent implements OnInit {
  product: ProductModel;

  constructor(
    private route: ActivatedRoute,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    this.route.params.subscribe((params) => {
      this.productService
        .getProduct(params.Id)
        .subscribe(
          (data: {
            id: number;
            name: string;
            stock: number;
            detail: DetailModel;
          }) => {
            console.log(data);
            this.product = data;
            console.log(this.product.detail);
          }
        );
    });
  }
}
