export class Product{
    constructor(
        public productId?: number,
        public category?: string,
        public price?: number,
        public title?: string,
        public description?: string,
        public imageFileName?: string
    ){}
}
