import { FormGroup } from '@angular/forms';
export class ValidatorReactive{

    public static areEquals(fieldName1: string , fieldName2: string){
        return (group: FormGroup) => {
              let value1 = group.controls[fieldName1].value;
              let value2 = group.controls[fieldName2].value; 

              if(value1 === value2)
              return null;

              return{
                  areEquals : true
              };

        }
    }

}