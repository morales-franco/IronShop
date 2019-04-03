import { Component, OnInit } from '@angular/core';
import { Product } from './../../../models/product.model'
import { NgForm } from '@angular/forms';
import { ProductService } from './../../../services/product/product.service'
import { Router, ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-product-edit',
  templateUrl: './product-edit.component.html',
  styles: []
})

export class ProductEditComponent implements OnInit {
  picture: File;
  pictureTemp: string;
  product : Product;

  constructor(private _productService : ProductService,
    private _route : Router,
    private _activatedRoute :ActivatedRoute) { 
      this.picture = null;
      this.product = new Product();
    }

  ngOnInit() {
    this._activatedRoute.params.subscribe(params =>{
      this._productService.getById(+params['id'])
        .subscribe(product => {
          this.product = product;
        })
     });
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

  edit(productForm :NgForm){
    if(productForm.invalid){
      swal("Error", "Please, complete required fields.", "error");
      return;
    }
  
    this._productService.update(this.product)
      .subscribe(r => this.onProductUpdated(this.product), 
      error => {
        swal("Error", "Can not update product!", "error");
      });


  }

  onProductUpdated(product : Product){
    swal("Success", "Product was updated successfully!", "success");

    debugger;
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
