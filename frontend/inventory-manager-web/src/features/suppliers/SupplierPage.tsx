import { useQuery } from "@tanstack/react-query";
import { getSuppliers } from "../../api/suppliersApi";

export function SuppliersPage() {
    const {
        data: Suppliers,
        isLoading,
        isError,
        error,
    } = useQuery({
        queryKey: ["Suppliers"],
        queryFn: getSuppliers,
    });

    if (isLoading){
        return <p>Loading Suppliers...</p>;
    }

    if (isError){
        return (
            <div>
                <h2>Something went wrong</h2>
                <p>{error instanceof Error ? error.message : "Unable to load Suppliers."}</p>
            </div>
        );
    }

    return (
        <section>
            <div className = "page-header">
                <div>
                    <h1>Suppliers</h1>
                    <p>View all Suppliers currently stored in the inventory system.</p>
                </div>
            </div>

            <div className="card">
                <table>
                    <thead>
                        <tr>
                            <th>Name</th>
                            <th>Description</th>
                        </tr>
                    </thead>

                    <tbody>
                        {Suppliers?.map((Supplier) => (
                            <tr key={Supplier.id}>
                                <td>{Supplier.name}</td>
                                <td>{Supplier.description ?? "-"}</td>
                            </tr>
                        ))}
                    </tbody>
                </table>

                {Suppliers?.length == 0 && <p>No Suppliers found.</p>}
            </div>
        </section>
    );
}