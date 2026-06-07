# Semana-4---Tarea-1-S6

# Agenda Viva Eventos

Estudiante: NOMBRE COMPLETO DEL ESTUDIANTE

Sistema ASP.NET Core MVC creado como ejercicio de scaffolding, CRUD y migraciones con Entity Framework Core.

## Entidades

El sistema tiene 5 entidades. Cada entidad mantiene 5 atributos escalares:

- Cliente: `ClienteId`, `Nombre`, `Telefono`, `Correo`, `FechaRegistro`
- Lugar: `LugarId`, `Nombre`, `Ciudad`, `Capacidad`, `Tipo`
- Organizador: `OrganizadorId`, `Nombre`, `Especialidad`, `Telefono`, `Activo`
- Evento: `EventoId`, `Titulo`, `FechaEvento`, `LugarId`, `OrganizadorId`
- Reserva: `ReservaId`, `FechaReserva`, `Estado`, `ClienteId`, `EventoId`

## Relaciones

- Un `Lugar` tiene muchos `Eventos`.
- Un `Organizador` tiene muchos `Eventos`.
- Un `Evento` pertenece a un `Lugar` y a un `Organizador`.
- Un `Cliente` tiene muchas `Reservas`.
- Una `Reserva` pertenece a un `Cliente` y a un `Evento`.

## Migraciones

El proyecto conserva Identity y agrega la migracion `CrearTablasEventos` para crear las tablas del sistema.

Comandos utiles:

```powershell
dotnet restore
dotnet build
dotnet ef database update
dotnet run --urls http://localhost:5147
```

La cadena de conexion esta en `appsettings.json` y usa SQLite: `Data Source=agenda_viva_eventos.db`.
Al ejecutar el sistema, la base se crea automaticamente con migraciones si todavia no existe.

## Datos de ejemplo

Al iniciar por primera vez, el sistema carga datos de prueba automaticamente:

- 5 lugares
- 5 organizadores
- 10 clientes
- 6 eventos
- 30 reservas de ejemplo

