# NetTemplate - Clean Architecture Template

Template de .NET para crear APIs basadas en **Clean Architecture** con capas:

- `API`
- `Application`
- `Domain`
- `Infrastructure` (incluye EF Core, DbContext y Migrations)

La idea es que puedas instalar el template una vez y luego generar nuevos proyectos con un solo comando.

---

## Requisitos

- [.NET SDK](https://dotnet.microsoft.com/) (7 u 8, segun use el template)
- SQL Server (local o remoto)
- Herramientas de Entity Framework Core:

```bash
dotnet tool install --global dotnet-ef
```

> Si ya tenes `dotnet-ef` instalado, podes saltear este paso.

---

## 1. Instalar el template localmente

Clona o descarga este repo (`netTemplate`) y desde la **carpeta raiz** donde esta `.template.config` ejecuta:

```bash
dotnet new install .
```

Esto registra el template en el CLI de .NET usando el `shortName` definido (`cleanarch`).

Podes verificar que este instalado con:

```bash
dotnet new list
```

Deberias ver algo similar a:

```text
cleanarch   NetTemplate - Clean Architecture Template   ...
```

---

## 2. Crear un nuevo proyecto a partir del template

Desde cualquier carpeta donde quieras crear tu solucion nueva:

```bash
dotnet new cleanarch -n MiNuevaApp
```

Esto va a crear una carpeta:

```text
MiNuevaApp/
```

con una estructura similar a:

```text
src/
  MiNuevaApp.API/
  MiNuevaApp.Application/
  MiNuevaApp.Domain/
  MiNuevaApp.Infrastructure/
```

Todos los namespaces y nombres de proyectos que en el template original eran `NetTemplate.*` se reemplazan por `MiNuevaApp.*`.

---

## 3. Configurar la base de datos

En el proyecto generado (`MiNuevaApp`), abri:

```text
src/MiNuevaApp.API/appsettings.Development.json
```

y ajusta la connection string:

```json
"ConnectionStrings": {
  "DefaultConnection": "Data Source=.;Initial Catalog=MiNuevaAppDb;Integrated Security=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
}
```

Puntos a revisar:

- `Data Source` -> instancia de SQL Server (ej: `.`, `(localdb)\\MSSQLLocalDB`, `Servidor\\Instancia`)
- `Initial Catalog` -> nombre de la base que quieras usar
- `TrustServerCertificate=True` -> util si tenes temas de SSL en dev

---

## 4. Crear la base y las tablas (migrations)

El template incluye:

- `AppDbContext` (en `Infrastructure`)
- Migrations iniciales en `Infrastructure/Persistence/Migrations`

Hay dos formas de aplicar las migrations.

### Opcion A - Automatico al iniciar la API (recomendado para dev)

El `Program.cs` del template esta preparado para aplicar `db.Database.Migrate()` al arrancar la API (en entorno `Development`).

En el proyecto generado, simplemente ejecuta:

```bash
cd MiNuevaApp

dotnet restore
dotnet run --project src/MiNuevaApp.API/MiNuevaApp.API.csproj
```

En el primer arranque, EF Core va a:

1. Crear la base de datos (si no existe).
2. Aplicar las migrations.
3. Crear las tablas (por ejemplo, `Products`, etc.).

### Opcion B - Manual con `dotnet ef`

Si preferis aplicar las migrations manualmente, en el root de la solución generada:

```bash
cd MiNuevaApp

dotnet ef database update \
  -p src/MiNuevaApp.Infrastructure \
  -s src/MiNuevaApp.API
```

- `-p` -> proyecto donde esta el `DbContext` + migrations (`Infrastructure`)
- `-s` -> proyecto startup que usa la configuracion (`API`)

---

## 5. Ejecutar la API

Una vez configurada la conexion y creadas las tablas:

```bash
cd MiNuevaApp

dotnet run --project src/MiNuevaApp.API/MiNuevaApp.API.csproj
```

Por defecto, en `Development`:

- Se aplican las migrations (si no las aplicaste manualmente antes).
- Se habilita Swagger en la URL similar a:
  - `https://localhost:5001/swagger`  
  - o la que indique el output de `dotnet run`.

---

## 6. Comandos utiles de EF Core

Desde la carpeta raiz del proyecto generado (`MiNuevaApp`):

### Agregar una nueva migration

```bash
dotnet ef migrations add NombreDeLaMigration \
  -p src/MiNuevaApp.Infrastructure \
  -s src/MiNuevaApp.API \
  -o Persistence/Migrations
```

### Aplicar migrations pendientes

```bash
dotnet ef database update \
  -p src/MiNuevaApp.Infrastructure \
  -s src/MiNuevaApp.API
```

---

## 7. Actualizar o desinstalar el template

### Actualizar el template (despues de cambiar el repo base)

Cada vez que modifiques el template original (`netTemplate`) y quieras que esos cambios se reflejen en nuevos proyectos:

```bash
cd netTemplate
dotnet new install .
```

### Desinstalar el template

Si alguna vez queres quitar este template del CLI:

```bash
cd netTemplate
dotnet new uninstall .
```

---

## 8. Resumen rapido

1. **Instalar template (una vez):**

   ```bash
   cd netTemplate
   dotnet new install .
   ```

2. **Crear nueva solucion:**

   ```bash
   dotnet new cleanarch -n MiNuevaApp
   ```

3. **Configurar connection string** en  
   `src/MiNuevaApp.API/appsettings.Development.json`.

4. **Aplicar migrations** (automatico al levantar la API o manual):

   ```bash
   dotnet ef database update \
     -p src/MiNuevaApp.Infrastructure \
     -s src/MiNuevaApp.API
   ```

5. **Levantar la API:**

   ```bash
   dotnet run --project src/MiNuevaApp.API/MiNuevaApp.API.csproj
   ```

Con eso, el template queda listo para usar como base de nuevos proyectos
