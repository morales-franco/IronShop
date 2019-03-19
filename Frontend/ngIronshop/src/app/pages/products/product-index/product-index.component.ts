import { Component, OnInit } from '@angular/core';
import { ProductIndex } from '../../../models/product.index.model';
import { ProductService } from '../../../services/product/product.service';
import { ModalImageUploadService } from '../../../components/modal-image-upload/modal-image-upload.service';


@Component({
  selector: 'app-product-index',
  templateUrl: './product-index.component.html'
})
export class ProductIndexComponent implements OnInit {
  products: ProductIndex[] = [];
  totalRows: number = 0;
  pageNumber: number = 1;
  rowsPerPage: number = 5;
  loading: boolean = false;

  constructor(private _productService: ProductService,
    private _modalImageUploadService: ModalImageUploadService) { }

  ngOnInit() {
    this.loadGrid();

    this._modalImageUploadService.uploadEvent.subscribe(
      r => this.onUploadPictureSuccess(r),
      error => {
        swal("Error", "Internal Error, the picture was not updated.", "error");
      });
  }

  private onUploadPictureSuccess( product :any){
    swal("Success", "Operation successfully.", "success");
    this.loadGrid();
  }

  private loadGrid(title: string = null, category: string = null) {
    this.loading = true;
    this._productService
      .getList(this.rowsPerPage, this.pageNumber, "ASC", "title", title, category)
      .subscribe((u: any) => {
        this.totalRows = u.totalRows;
        this.products = u.rows;
        this.loading = false;
      });
  }

  goToPreviousPage() {
    if (this.pageNumber <= 1) {
      return;
    }

    this.pageNumber--;
    this.loadGrid();
  }

  goToNextPage() {
    let nextPage: number = this.pageNumber + 1;
    let maxPage: number = Math.ceil(this.totalRows / this.rowsPerPage);

    if (maxPage < nextPage) {
      return;
    }

    this.pageNumber++;
    this.loadGrid();
  }

  search(title: string, category: string) {
    if (title.length == 0 && category.length == 0) {
      this.loadGrid();
      return;
    }

    console.log(title);
    console.log(category);
    this.loadGrid(title, category);
  }

  showModalUploadPicture(product: ProductIndex){
    this._modalImageUploadService.showModal(product.productId, "PRODUCT");
  }

}
