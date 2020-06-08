import { SelectItem } from './select-item';

export class Tools {

  static EnumToArray(theEnum): SelectItem[] {
    return Object.keys(theEnum)
      .map((key: any) => {
        return {
          id: theEnum[key],
          text: key
        } as SelectItem;
      });
  }

}
