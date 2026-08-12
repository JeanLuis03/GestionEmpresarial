# CorePlus ERP

Sistema web de gestión empresarial desarrollado con ASP.NET MVC, .NET 10, Entity Framework Core y SQL Server.

## Descripción

CorePlus ERP es una aplicación web orientada a la gestión de información empresarial. El sistema permite administrar clientes, categorías, productos y usuarios, además de proporcionar un Dashboard con indicadores reales y un módulo de reportería integrado con SQL Server Reporting Services (SSRS).

El proyecto utiliza una arquitectura organizada basada en controladores, servicios, interfaces, modelos, ViewModels y acceso a datos mediante Entity Framework Core.

También cuenta con autenticación mediante Cookies, autorización basada en roles y permisos, validaciones frontend y backend, interfaz responsive, modo claro y oscuro y un sistema de loading durante las operaciones realizadas mediante Fetch.

## Módulos

### Autenticación

Permite a los usuarios iniciar sesión en el sistema mediante autenticación basada en Cookies.

El acceso a las diferentes funcionalidades está protegido mediante roles, Claims y permisos.

### Dashboard

Muestra información general del sistema mediante indicadores obtenidos directamente desde la base de datos.

Incluye:

* Clientes activos.
* Productos activos.
* Usuarios activos.
* Categorías activas.
* Gráfico de productos por categoría.
* Top 5 de productos con menor stock.

El gráfico de productos por categoría se implementa utilizando Chart.js mediante CDN.

### Clientes

Permite administrar la información de los clientes.

Funciones principales:

* Registrar clientes.
* Consultar clientes.
* Editar clientes.
* Cambiar estado.
* Validar información.
* Controlar registros duplicados.

### Categorías

Permite administrar las categorías utilizadas para clasificar los productos.

Funciones principales:

* Registrar categorías.
* Consultar categorías.
* Editar categorías.
* Cambiar estado.
* Validar información.
* Controlar registros duplicados.

### Productos

Permite administrar los productos registrados en el sistema.

Información principal:

* Código.
* Nombre.
* Marca.
* Modelo.
* Categoría.
* Precio unitario.
* Stock.

Funciones principales:

* Registrar productos.
* Consultar productos.
* Editar productos.
* Cambiar estado.
* Validar información.
* Controlar registros duplicados.

### Usuarios

Permite administrar las cuentas que tienen acceso al sistema.

Información principal:

* Usuario.
* Contraseña.
* Rol.
* Estado.

Los roles determinan los permisos que tendrá cada usuario dentro de la aplicación.

### Roles y permisos

El sistema utiliza un modelo de autorización basado en roles y permisos.

Los permisos están asociados a acciones específicas, como:

* Agregar.
* Consultar.
* Editar.
* Eliminar o cambiar estado.

La autorización se aplica tanto en la interfaz como en el servidor, evitando que un usuario pueda acceder directamente a funcionalidades para las cuales no posee permisos.

### Reportes

El sistema cuenta con integración con SQL Server Reporting Services (SSRS).

Actualmente se encuentra implementado el reporte:

```text
Productos.rdl
```

El reporte presenta información del inventario, incluyendo:

* Código.
* Nombre.
* Marca.
* Modelo.
* Categoría.
* Precio unitario.
* Stock.
* Total de productos.
* Valor total del inventario.
* Usuario que genera el reporte.
* Fecha y hora de generación.

Los reportes pueden ser exportados mediante las opciones proporcionadas por SSRS, incluyendo formatos como PDF, Excel, Word, CSV y XML.

### Interfaz

La interfaz utiliza Bootstrap y Google Material Symbols.

Incluye:

* Sidebar dinámico.
* Topbar.
* Modales.
* DataTables.
* SweetAlert2.
* Diseño responsive.
* Modo claro.
* Modo oscuro.
* Loading durante operaciones Fetch.

## Tecnologías utilizadas

* .NET 10
* ASP.NET MVC
* C#
* Entity Framework Core
* SQL Server
* SQL Server Reporting Services (SSRS)
* Microsoft Report Builder
* Bootstrap
* JavaScript
* Fetch API
* Chart.js
* DataTables
* SweetAlert2
* Google Material Symbols

## Requisitos

Para ejecutar el proyecto se necesita tener instalado:

* Visual Studio 2022 o superior.
* .NET 10 SDK.
* SQL Server.
* SQL Server Management Studio (SSMS).
* SQL Server Reporting Services (SSRS), únicamente si se desea utilizar la funcionalidad de reportes.
* Microsoft Report Builder, únicamente si se desea modificar los reportes.

## Clonar el proyecto

Clonar el repositorio utilizando Git:

```bash
git clone URL_DEL_REPOSITORIO
```

Entrar al directorio del proyecto:

```bash
cd GestionEmpresarial
```

Abrir la solución:

```text
GestionEmpresarial.sln
```

También se puede abrir directamente desde Visual Studio.

## Configuración de la base de datos

El proyecto utiliza SQL Server y la base de datos se denomina:

```text
dbGestionEmpresarial
```

Antes de ejecutar el proyecto, es necesario configurar la cadena de conexión.

Abrir:

```text
appsettings.json
```

y verificar la sección correspondiente a la conexión de SQL Server.

Ejemplo:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=SERVIDOR;Database=dbGestionEmpresarial;Trusted_Connection=True;TrustServerCertificate=True;"
}
```

Reemplazar `SERVIDOR` por el nombre de la instancia de SQL Server disponible en el equipo donde se ejecutará el proyecto.

Por ejemplo:

```text
LAPTOP-JEAN
```

o:

```text
localhost
```

dependiendo de la configuración local de SQL Server.

## Migrar la base de datos

El proyecto utiliza Entity Framework Core para administrar las migraciones de la base de datos.

Después de configurar la cadena de conexión, abrir la consola del Administrador de paquetes de Visual Studio:

```text
Tools
→ NuGet Package Manager
→ Package Manager Console
```

Ejecutar:

```powershell
Update-Database
```

Este comando aplicará las migraciones existentes y creará la base de datos:

```text
dbGestionEmpresarial
```

en la instancia configurada.

También puede utilizarse la CLI de .NET:

```bash
dotnet ef database update
```

Si el proyecto aún no tiene creada la herramienta de Entity Framework, puede instalarse mediante:

```bash
dotnet tool install --global dotnet-ef
```

## Seeders

El proyecto utiliza Seeders para insertar información inicial necesaria para el funcionamiento del sistema.

Entre ellos se encuentran los relacionados con:

* Roles.
* Permisos.
* Relación entre roles y permisos.
* Usuarios iniciales.

Los Seeders se ejecutan automáticamente durante el inicio de la aplicación siguiendo el orden de ejecución establecido en el proyecto.

Por esta razón, después de ejecutar las migraciones se recomienda iniciar la aplicación para que se genere la información inicial correspondiente.

## Ejecutar el proyecto

Una vez configurada la conexión a SQL Server y aplicada la migración:

1. Abrir la solución en Visual Studio.
2. Verificar la cadena de conexión.
3. Ejecutar las migraciones.
4. Ejecutar el proyecto desde Visual Studio.
5. Abrir la URL proporcionada por ASP.NET.

También puede ejecutarse desde la terminal:

```bash
dotnet run
```

La aplicación mostrará en la consola la dirección local donde se encuentra disponible.

## Configuración de SSRS

La funcionalidad de reportes depende de SQL Server Reporting Services.

El proyecto utiliza un servidor de reportes configurado mediante:

```json
"Reportes": {
  "Servidor": "http://SERVIDOR/ReportServer"
}
```

Esta configuración permite que la aplicación genere dinámicamente las URL de los reportes sin tener que escribir directamente la dirección del servidor dentro del código JavaScript.

Si se desea utilizar la funcionalidad de reportería en otro equipo, es necesario:

1. Tener SSRS instalado y configurado.
2. Tener disponible un Report Server.
3. Crear o configurar el DataSource compartido.
4. Configurar la conexión a `dbGestionEmpresarial`.
5. Publicar los archivos `.rdl`.
6. Configurar la URL correspondiente en `appsettings.json`.

## Reporte de productos

El reporte principal del proyecto se encuentra en:

```text
Reportes/Productos.rdl
```

El archivo puede abrirse y modificarse utilizando Microsoft Report Builder.

El reporte utiliza el DataSource compartido de la base de datos:

```text
dbGestionEmpresarial
```

Para utilizarlo correctamente, el servidor SSRS debe tener configurado dicho DataSource.

## Estructura general del proyecto

La solución utiliza una estructura organizada para separar las responsabilidades:

```text
GestionEmpresarial/
│
├── Controllers/
├── DBContext/
├── Helpers/
├── Interfaces/
├── Mappings/
├── Models/
├── Reportes/
├── Seed/
├── Services/
├── ViewModels/
├── Views/
│
├── wwwroot/
│   ├── css/
│   ├── js/
│   └── ...
│
├── appsettings.json
├── Program.cs
└── GestionEmpresarial.csproj
```

## Flujo general de funcionamiento

La aplicación sigue un flujo general basado en:

```text
Usuario
   |
   v
Login
   |
   v
Autenticación
   |
   v
Roles y permisos
   |
   v
Dashboard / Módulos
   |
   v
Controllers
   |
   v
Services
   |
   v
Entity Framework Core
   |
   v
SQL Server
```

Para la reportería:

```text
Usuario
   |
   v
Módulo Reportes
   |
   v
ReportesController
   |
   v
SSRS
   |
   v
Productos.rdl
   |
   v
SQL Server
   |
   v
Reporte generado
```

## Notas

La funcionalidad principal del sistema puede ejecutarse utilizando .NET 10, Entity Framework Core y SQL Server.

La integración con SSRS es necesaria únicamente para utilizar el módulo de reportería.

Los datos de conexión, credenciales y configuraciones específicas del entorno local no deben incluirse directamente en el repositorio. Se recomienda configurar estos valores de acuerdo con el entorno donde se vaya a ejecutar la aplicación.

## Estado del proyecto

El proyecto se encuentra finalizado con las funcionalidades principales implementadas:

* Autenticación.
* Roles y permisos.
* Clientes.
* Categorías.
* Productos.
* Usuarios.
* Dashboard.
* Reportería con SSRS.
* Validaciones.
* Diseño responsive.
* Modo claro y oscuro.
* Loading durante operaciones Fetch.
* Integración con Chart.js.
* DataTables.
* SweetAlert2.
