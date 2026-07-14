import { httpClient } from "./httpClient";
import type { Product } from "../features/products/productType";

export async function getProducts(): Promise<Product[]> {
    const response = await httpClient.get<Product[]>("/products");

    return response.data;
}