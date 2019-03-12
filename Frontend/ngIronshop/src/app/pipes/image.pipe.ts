import { Pipe, PipeTransform } from '@angular/core';
import { environment } from '../../environments/environment';
import { GeneralConstants } from '../commons/constants/general.constants';

@Pipe({
  name: 'image'
})
export class ImagePipe implements PipeTransform {

  transform(fileNameImage: string): any {
    let url = `${environment.WEBAPI_ENDPOINT}/image/${ fileNameImage == null || fileNameImage === "" ? GeneralConstants.DefaultImage:  fileNameImage}`;
    return url;
  }

}
