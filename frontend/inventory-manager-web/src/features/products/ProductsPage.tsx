import { useQuery } from "@tanstack/react-query";
import { getProducts } from "../../api/productsApi";
export function ProductsPage() {
    const {
        data: products,
        isLoading,
        isError,
        error,
    } = useQuery({
        queryKey: ["products"],
        queryFn: getProducts,
    });

    if (isLoading){
        return <p>Loading products...</p>;
    }

    if (isError){
        return (
            <div>
                <h2>Something went wrong</h2>
                <p>{error instanceof Error ? error.message : "Unable to load products."}</p>
            </div>
        );
    }

    return (
        <section>
            <div className = "page-header">
                <div>
                    <h1>Products</h1>
                    <p>View all products currently stored in the inventory system.</p>
                </div>
            </div>

            <div className="card">
                <table>
                    <thead>
                        <tr>
                            <th>SKU</th>
                            <th>Name</th>
                            <th>Description</th>
                            <th>Price</th>
                            <th>Quantity</th>
                            <th>Category ID</th>
                            <th>Supplier ID</th>
                        </tr>
                    </thead>

                    <tbody>
                        {products?.map((product) => (
                            <tr key={product.id}>
                                <td>{product.sku}</td>
                                <td>{product.name}</td>
                                <td>{product.description ?? "-"}</td>
                                <td>{product.price.toFixed(2)}</td>
                                <td>{product.quantityInStock}</td>
                                <td>{product.categoryId}</td>
                                <td>{product.supplierId}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>

                {products?.length == 0 && <p>No products found.</p>}
            </div>
        </section>
    );
}