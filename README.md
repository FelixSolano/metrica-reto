# Metrica Reto - Fullstack Senior (.NET + React)

Solución al reto técnico Fullstack implementada con **.NET 8** (Clean Architecture) y **React** (Vite + Tailwind).

## � Stack Tecnológico

La solución utiliza las siguientes versiones y librerías clave:

### Frontend
*   **React 19.2.3**: Biblioteca principal de UI (Última versión estable).
*   **Vite 7.3.1**: Build tool y entorno de desarrollo de alto rendimiento.
*   **Tailwind CSS 3.4.1**: Framework de estilos *utility-first*.
*   **React Router DOM 7.12.0**: Manejo de rutas y navegación.
*   **Axios 1.13.2**: Cliente HTTP para consumo de API.
*   **Lucide React 0.562.0**: Iconografía moderna y ligera.

### Backend
*   **ASP.NET Core 8.0**: Framework web de alto rendimiento.
*   **Entity Framework Core 8.0**: ORM para manejo de datos.
*   **PostgreSQL**: Motor de base de datos (con driver Npgsql).
*   **Serilog 8.0**: Logging estructurado.
*   **JWT Bearer 8.0**: Autenticación segura.
*   **Swashbuckle (Swagger) 6.6.2**: Documentación de API.

## �🚀 Características Principales

### Backend (.NET 8)
*   **Arquitectura Limpia**: Separación en Capas (Domain, Application, Infrastructure, API).
*   **Seguridad**: Autenticación JWT Bearer con expiración y Claims de Roles.
*   **Resiliencia**: 
    *   **Rate Limiting** (100 req/min).
    *   **Retry Policy** en conexión a base de datos (5 reintentos).
*   **Validaciones**: Reglas de negocio en capa de Servicio (Total > 0, Número Único).
*   **Manejo de Errores**: Middleware Global de Excepciones.
*   **Logging**: Integración con **Serilog**.
*   **Base de Datos**: PostgreSQL con Entity Framework Core.
*   **Borrado Lógico**: Implementado mediante columna `IsActive`.

### Frontend (React)
*   **Stack Moderno**: Vite, React 18, Tailwind CSS.
*   **UX/UI**: 
    *   Diseño **Responsive** con Menú Lateral (Sidebar).
    *   **Formularios Flotantes (Modales)** para Crear/Editar.
    *   Feedback visual de estados (Colores por estado).
*   **Seguridad**: Manejo de sesión con Context API y protección de rutas.

## 🛠️ Instrucciones de Ejecución

### Prerrequisitos
*   .NET 8 SDK
*   Node.js 18+
*   PostgreSQL

### 1. Configuración de Base de Datos
1.  Crear una base de datos vacía en PostgreSQL (ej: `db_metrica`).
2.  Ejecutar el script de inicialización ubicado en:
    `sql_scripts/init_db.sql`
3.  Actualizar la cadena de conexión en `back-end/src/MetricaReto.API/appsettings.json`.

### 2. Ejecutar Backend
```bash
cd back-end
dotnet run --project src/MetricaReto.API
```
*   El sistema sembrará automáticamente 2 usuarios si no existen:
    *   Admin: `admin@metrica.com` / `admin123`
    *   User: `user@email.com` / `123456`
*   URL API: `http://localhost:5082`
*   Swagger: `http://localhost:5082/swagger`

### 3. Ejecutar Frontend
```bash
cd frond-end
npm install
npm run dev
```
*   URL Web: `http://localhost:5173`

## 🧪 Endpoints Principales

*   **POST** `/auth/login`: Obtener Token JWT.
*   **GET** `/pedidos`: Listar pedidos activos.
*   **POST** `/pedidos`: Crear nuevo pedido (Requiere Auth).
*   **PUT** `/pedidos/{id}`: Actualizar pedido.
*   **DELETE** `/pedidos/{id}`: Borrado lógico.

---
**Desarrollado para el Reto Técnico Fullstack Senior.**
