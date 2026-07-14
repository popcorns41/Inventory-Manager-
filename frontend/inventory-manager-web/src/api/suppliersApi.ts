import { httpClient } from "./httpClient";
import type { Supplier } from "../features/suppliers/supplierType";

export async function getSuppliers() : Promise<Supplier[]> {
    const response = await httpClient.get<Supplier[]>("/suppliers");

    return response.data;
}