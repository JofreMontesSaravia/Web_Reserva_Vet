# Proyecto Web\_Vet\_Pet

## Requisitos

* **Visual Studio 2022**

  > *No se garantiza la compatibilidad con Visual Studio 2019. Si decides intentarlo y ocurre algún error, será bajo tu responsabilidad.*

### Extensiones necesarias:

Asegúrate de tener instaladas las siguientes extensiones:

* **Bootstrap 5**
* **Pomelo.EntityFrameworkCore.Tools** (v8.3)
* **Microsoft.EntityFrameworkCore.Tools** (v8.3)

El proyecto ya está configurado con estas extensiones. Sin embargo, si no te aparecen en el entorno, debes instalarlas localmente.

#### ¿Cómo verificar si están instaladas?

1. Haz clic derecho sobre el proyecto `Web_Vet_Pet`.
2. Ve a **Administrar paquetes NuGet**.
3. Revisa la pestaña **Instalado**. Ahí deberían aparecer las tres extensiones mencionadas.
4. Si no aparecen, ve a la pestaña **Examinar**, busca las extensiones e instálalas manualmente.

---

## Base de Datos

### ¿Qué motor usar?

Según la malla curricular no se especifica el motor de base de datos. En clase, el profesor mencionó el uso de **XAMPP**, lo cual implica utilizar **MySQL** desde **phpMyAdmin**.

> 💡 **Recomendación personal**: Usar directamente **MySQL Workbench**, pero si la mayoría prefiere XAMPP, podemos adaptarnos.

---

## Estado Actual del Proyecto

Actualmente, **no se ha implementado ninguna funcionalidad propia** (como interfaces o clases personalizadas). Si ejecutas el proyecto, se mostrará la página por defecto de ASP.NET.

### ¿Qué se ha implementado hasta ahora?

* Carpeta `Data` (para el contexto de base de datos)
* Carpeta `Migrations` (para migraciones de Entity Framework)

### Cómo crear y actualizar la base de datos

1. Crea la clase modelo en la carpeta `Models` (cada clase representa una tabla).
2. Asegúrate de instanciar el modelo en el contexto de base de datos (`Data`).
3. Abre la terminal de paquetes NuGet y ejecuta los siguientes comandos:

```bash
Add-Migration NombreDescriptivo
Update-Database
```

Esto generará la base de datos automáticamente y actualizará la estructura cada vez que hagas cambios.

> ✅ Esta es la forma más sencilla, ya que evita tener que exportar e importar scripts manualmente en cada equipo.

---

## Verificar que la BD se haya creado

1. Abre MySQL Workbench.
2. Confirma que se haya creado la base de datos (por ejemplo, `VeterinariaDB`) y que incluya la tabla correspondiente (por ejemplo, `Usuario`).

Si no se creó, revisa el archivo `Program.cs` y asegúrate de que la cadena de conexión esté bien configurada:

```csharp
options.UseMySql("server=localhost;database=VeterinariaDB;user=root;password=12345678;");
```

* `VeterinariaDB`: nombre de la base de datos
* `user`: usuario de MySQL que configuraste durante la instalación
* `password`: contraseña que colocaste

---

## ¿Problemas?

Si aún después de seguir los pasos tienes errores, avísame para organizar una reunión y resolverlo en equipo.
