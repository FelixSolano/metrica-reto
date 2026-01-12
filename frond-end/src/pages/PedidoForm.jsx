import { useEffect, useState } from 'react';
import api from '../api/axios';
import { Save } from 'lucide-react';

export default function PedidoForm({ pedidoId, onSuccess, onCancel }) {
    const isEditing = !!pedidoId;

    const [formData, setFormData] = useState({
        numeroPedido: '',
        cliente: '',
        total: '',
        estado: 'Registrado'
    });
    const [loading, setLoading] = useState(isEditing);
    const [error, setError] = useState('');

    useEffect(() => {
        if (isEditing) {
            const fetchPedido = async () => {
                try {
                    const response = await api.get(`/pedidos/${pedidoId}`);
                    const { numeroPedido, cliente, total, estado } = response.data;
                    setFormData({ numeroPedido, cliente, total, estado });
                } catch (err) {
                    setError('Error al cargar el pedido.');
                } finally {
                    setLoading(false);
                }
            };
            fetchPedido();
        } else {
            // Reset form when opening for new pedido
            setFormData({
                numeroPedido: '',
                cliente: '',
                total: '',
                estado: 'Registrado'
            });
            setLoading(false);
        }
    }, [pedidoId, isEditing]);

    const handleChange = (e) => {
        setFormData({ ...formData, [e.target.name]: e.target.value });
    };

    const handleSubmit = async (e) => {
        e.preventDefault();
        setError('');
        try {
            const payload = {
                ...formData,
                total: parseFloat(formData.total)
            };

            if (isEditing) {
                await api.put(`/pedidos/${pedidoId}`, payload);
            } else {
                await api.post('/pedidos', payload);
            }
            if (onSuccess) onSuccess();
        } catch (err) {
            setError(err.response?.data?.message || 'Error al guardar el pedido.');
        }
    };

    if (loading) return <div className="p-4 text-center">Cargando datos...</div>;

    return (
        <form onSubmit={handleSubmit} className="space-y-4">
            {error && <div className="bg-red-50 text-red-700 p-3 rounded text-sm">{error}</div>}

            {!isEditing && (
                <div>
                    <label className="block text-sm font-medium text-gray-700">Número de Pedido</label>
                    <input
                        type="text"
                        name="numeroPedido"
                        required
                        value={formData.numeroPedido}
                        onChange={handleChange}
                        className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm border p-2"
                        placeholder="#12345"
                    />
                </div>
            )}

            <div>
                <label className="block text-sm font-medium text-gray-700">Cliente</label>
                <input
                    type="text"
                    name="cliente"
                    required
                    value={formData.cliente}
                    onChange={handleChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm border p-2"
                    placeholder="Nombre del cliente"
                />
            </div>

            <div>
                <label className="block text-sm font-medium text-gray-700">Total</label>
                <input
                    type="number"
                    step="0.01"
                    name="total"
                    required
                    value={formData.total}
                    onChange={handleChange}
                    className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm border p-2"
                    placeholder="0.00"
                />
            </div>

            {isEditing && (
                <div>
                    <label className="block text-sm font-medium text-gray-700">Estado</label>
                    <select
                        name="estado"
                        value={formData.estado}
                        onChange={handleChange}
                        className="mt-1 block w-full rounded-md border-gray-300 shadow-sm focus:border-blue-500 focus:ring-blue-500 sm:text-sm border p-2"
                    >
                        <option>Registrado</option>
                        <option>Procesado</option>
                        <option>Entregado</option>
                        <option>Cancelado</option>
                    </select>
                </div>
            )}

            <div className="flex justify-end space-x-3 pt-4 border-t mt-6">
                <button
                    type="button"
                    onClick={onCancel}
                    className="inline-flex items-center px-4 py-2 border border-gray-300 shadow-sm text-sm font-medium rounded-md text-gray-700 bg-white hover:bg-gray-50 focus:outline-none"
                >
                    Cancelar
                </button>
                <button
                    type="submit"
                    className="inline-flex items-center px-4 py-2 border border-transparent shadow-sm text-sm font-medium rounded-md text-white bg-blue-600 hover:bg-blue-700 focus:outline-none"
                >
                    <Save className="-ml-1 mr-2 h-4 w-4" />
                    Guardar
                </button>
            </div>
        </form>
    );
}
