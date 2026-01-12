import { useEffect, useState } from 'react';
import api from '../api/axios';
import { Plus, Pencil, Trash2 } from 'lucide-react';
import Modal from '../components/Modal';
import PedidoForm from './PedidoForm';

export default function PedidosList() {
    const [pedidos, setPedidos] = useState([]);
    const [loading, setLoading] = useState(true);
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [editingId, setEditingId] = useState(null);

    const fetchPedidos = async () => {
        try {
            const response = await api.get('/pedidos');
            setPedidos(response.data);
        } catch (error) {
            console.error("Error fetching pedidos", error);
        } finally {
            setLoading(false);
        }
    };

    useEffect(() => {
        fetchPedidos();
    }, []);

    const handleDelete = async (id) => {
        if (!window.confirm('¿Estás seguro de eliminar este pedido?')) return;
        try {
            await api.delete(`/pedidos/${id}`);
            fetchPedidos();
        } catch (error) {
            console.error("Error deleting pedido", error);
            alert("No se pudo eliminar el pedido.");
        }
    };

    const handleCreate = () => {
        setEditingId(null);
        setIsModalOpen(true);
    };

    const handleEdit = (id) => {
        setEditingId(id);
        setIsModalOpen(true);
    };

    const handleModalClose = () => {
        setIsModalOpen(false);
        setEditingId(null);
    };

    const handleSuccess = () => {
        handleModalClose();
        fetchPedidos();
    };

    if (loading) return <div className="text-center pt-10">Cargando...</div>;

    return (
        <div className="space-y-6">
            <div className="flex flex-col sm:flex-row sm:items-center sm:justify-between">
                <div>
                    <h1 className="text-2xl font-bold leading-7 text-gray-900 sm:truncate sm:text-3xl sm:tracking-tight">
                        Pedidos
                    </h1>
                    <p className="mt-1 text-sm text-gray-500">Gestión de pedidos registrados en el sistema.</p>
                </div>
                <div className="mt-4 sm:ml-4 sm:mt-0">
                    <button
                        onClick={handleCreate}
                        className="inline-flex items-center rounded-md bg-blue-600 px-3 py-2 text-sm font-semibold text-white shadow-sm hover:bg-blue-500 focus-visible:outline focus-visible:outline-2 focus-visible:outline-offset-2 focus-visible:outline-blue-600"
                    >
                        <Plus className="-ml-0.5 mr-1.5 h-5 w-5" aria-hidden="true" />
                        Nuevo Pedido
                    </button>
                </div>
            </div>

            <div className="overflow-hidden rounded-lg bg-white shadow ring-1 ring-black ring-opacity-5">
                <table className="min-w-full divide-y divide-gray-300">
                    <thead className="bg-gray-50">
                        <tr>
                            <th scope="col" className="py-3.5 pl-4 pr-3 text-left text-sm font-semibold text-gray-900 sm:pl-6">ID / Número</th>
                            <th scope="col" className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">Cliente</th>
                            <th scope="col" className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">Fecha</th>
                            <th scope="col" className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">Total</th>
                            <th scope="col" className="px-3 py-3.5 text-left text-sm font-semibold text-gray-900">Estado</th>
                            <th scope="col" className="relative py-3.5 pl-3 pr-4 sm:pr-6">
                                <span className="sr-only">Acciones</span>
                            </th>
                        </tr>
                    </thead>
                    <tbody className="divide-y divide-gray-200 bg-white">
                        {pedidos.map((pedido) => (
                            <tr key={pedido.id}>
                                <td className="whitespace-nowrap py-4 pl-4 pr-3 text-sm font-medium text-gray-900 sm:pl-6">
                                    {pedido.numeroPedido}
                                    <div className="text-xs text-gray-500">ID: {pedido.id}</div>
                                </td>
                                <td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">{pedido.cliente}</td>
                                <td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">{new Date(pedido.fecha).toLocaleDateString()}</td>
                                <td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">${pedido.total.toFixed(2)}</td>
                                <td className="whitespace-nowrap px-3 py-4 text-sm text-gray-500">
                                    <span className={`inline-flex items-center rounded-md px-2 py-1 text-xs font-medium ring-1 ring-inset ${pedido.estado === 'Entregado' ? 'bg-green-50 text-green-700 ring-green-600/20' :
                                            pedido.estado === 'Cancelado' ? 'bg-red-50 text-red-700 ring-red-600/20' :
                                                pedido.estado === 'Procesado' ? 'bg-blue-50 text-blue-700 ring-blue-600/20' :
                                                    'bg-yellow-50 text-yellow-800 ring-yellow-600/20'
                                        }`}>
                                        {pedido.estado}
                                    </span>
                                </td>
                                <td className="relative whitespace-nowrap py-4 pl-3 pr-4 text-right text-sm font-medium sm:pr-6">
                                    <button
                                        onClick={() => handleEdit(pedido.id)}
                                        className="text-blue-600 hover:text-blue-900 mr-4"
                                    >
                                        <Pencil className="h-4 w-4 inline" />
                                    </button>
                                    <button onClick={() => handleDelete(pedido.id)} className="text-red-600 hover:text-red-900">
                                        <Trash2 className="h-4 w-4 inline" />
                                    </button>
                                </td>
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>

            <Modal
                isOpen={isModalOpen}
                onClose={handleModalClose}
                title={editingId ? 'Editar Pedido' : 'Nuevo Pedido'}
            >
                <PedidoForm
                    pedidoId={editingId}
                    onSuccess={handleSuccess}
                    onCancel={handleModalClose}
                />
            </Modal>
        </div>
    );
}
