export type Product = {
    id : number;
    sku : string;
    name: string;
    description: string | null;
    price: number;
    quantityInStock: number;
    categoryId: number;
    supplierId: number;
};