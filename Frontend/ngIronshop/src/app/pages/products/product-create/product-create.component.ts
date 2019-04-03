import { Component, OnInit } from '@angular/core';
import { Product } from './../../../models/product.model'
import { NgForm } from '@angular/forms';
import { ProductService } from './../../../services/product/product.service'
import { Router } from '@angular/router';

@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styles: []
})
export class ProductCreateComponent implements OnInit {

  picture: File;
  pictureTemp: string;
  product : Product;

  constructor(private _productService : ProductService,
    private _route : Router) {
    this.product = new Product();
    this.picture = null;
   }

  ngOnInit() {

  }

  onSelectPicture(picture: File){
    if(!picture){
      this.picture = null;
      return;
    }

    if(picture.type.indexOf("image") < 0){
      swal("Error", "Can upload only images!", "error");
      this.picture = null;
      return;
    }

    this.picture = picture;
    let reader = new FileReader();
    reader.readAsDataURL(picture);

    reader.onloadend = () =>this.pictureTemp = reader.result.toString(); 
  }

  create(productForm : NgForm){
    if(productForm.invalid){
      swal("Error", "Please, complete required fields.", "error");
      return;
    }

    this._productService.create(productForm.value)
      .subscribe(r => this.onProductCreated(r), 
      error => {
        swal("Error", "Can not create product!", "error");
      });


  }

  onProductCreated(product : Product){
    swal("Success", "Product was created successfully!", "success");

    if(this.picture == null){
      this._route.navigate(['/products'])
      return;
    }

    this._productService.updateImage(this.picture, product.productId)
      .then(r => this._route.navigate(['/products']))
      .catch(r => swal("Error", "Can not create product!", "error"))
  }

  back(){
    this._route.navigate(['/products']);
  }

}
