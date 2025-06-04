Guía de Instalación y Configuración
Este documento proporciona las instrucciones necesarias para configurar y ejecutar el proyecto desde cero, incluyendo la instalación de dependencias y herramientas, así como la configuración de la base de datos y la ejecución del proyecto en un entorno de desarrollo.

Requisitos previos
Antes de comenzar, asegúrate de tener instalados los siguientes requisitos en tu máquina:

Docker (para ejecutar el contenedor de la aplicación).

SQLite (para gestionar la base de datos localmente).

Visual Studio Code (como editor de código).

Paso 1: Instalación de SQLite
SQLite se utiliza como la base de datos en este proyecto. Sigue estos pasos para instalar SQLite:

Descargar SQLite:

Visita la página oficial de SQLite: Descargar SQLite.

Descarga el archivo adecuado para tu sistema operativo (Windows, macOS, Linux).

Instalar SQLite:

Para Windows, descomprime el archivo ZIP y coloca los ejecutables sqlite3.exe en una carpeta accesible. Asegúrate de que esta carpeta esté en tu PATH para poder ejecutar SQLite desde la terminal.

Verificar instalación:

Abre una terminal o línea de comandos y ejecuta:

bash
Copiar
sqlite3 --version
Si ves la versión de SQLite, la instalación fue exitosa.

Paso 2: Configuración de la base de datos con migraciones
Inicia la base de datos:

En tu terminal, navega a la carpeta de tu proyecto donde se encuentra la solución .NET Core.

Para crear la base de datos SQLite, asegúrate de que tu proyecto WebApiRedArbor esté configurado correctamente en el archivo Program.cs (como se mencionó previamente).

Aplicar migraciones:

Ejecuta las migraciones para crear las tablas y la base de datos usando Entity Framework Core. Asegúrate de tener la conexión correcta configurada en tu archivo appsettings.json (o en el código de configuración de la base de datos).

Si tu proyecto usa migraciones con Entity Framework Core, ejecuta los siguientes comandos desde la terminal en la carpeta del proyecto WebApiRedArbor:
"RedArborApi\WebApiRedArbor\WebApiRedArbor" // en esta ruta final se debe abrir un cmd y ejecutar los siguientes comandos

bash
Copiar
dotnet ef migrations add InitialCreate
dotnet ef database update
El comando dotnet ef migrations add InitialCreate generará las migraciones para la base de datos, y dotnet ef database update aplicará estas migraciones para crear las tablas en SQLite.

Verificar la base de datos:

La base de datos debe estar creada en employees.db. Puedes verificar esto usando DB Browser for SQLite o cualquier herramienta similar.

Paso 3: Configuración del Puerto de Docker
Modificar el puerto en el script utilidades.js:

Docker asignará un puerto aleatorio al contenedor cuando se ejecute, por lo que debes asegurarte de actualizar el script utilidades.js para que apunte al puerto generado por Docker.

En el archivo utilidades.js, asegúrate de que la variable path apunte al puerto correcto. 

javascript
Copiar
const path = "https://localhost:5000/api/";  // Aquí se remplaza el puerto pro el que genere Docker
Cambia el puerto de acuerdo con el puerto que tu contenedor está usando. Si estás usando Docker directamente y no docker-compose, puedes obtener el puerto mapeado desde el contenedor de Docker.

Para obtener el puerto expuesto en Docker, ejecuta el siguiente comando para ver los puertos mapeados:

bash
Copiar
docker ps
Esto te mostrará la lista de contenedores en ejecución y los puertos expuestos. Encuentra tu contenedor y asegúrate de usar el puerto correspondiente en la variable path.

Paso 4: Instalación de la Extensión "Live Server" en Visual Studio Code
Para poder previsualizar tu proyecto frontend (HTML + jQuery) en tiempo real mientras desarrollas, utilizaremos la extensión "Live Server" de Visual Studio Code.

Instalar "Live Server":
Abre Visual Studio Code.

En la barra lateral izquierda, haz clic en el ícono de extensiones (el cuadrado con una flecha).

En el campo de búsqueda, escribe "Live Server".

Selecciona la extensión Live Server de Ritwick Dey y haz clic en Instalar.

Una vez instalada, abre tu proyecto frontend (donde están tus archivos index.html, utilidades.js, etc.).

Haz clic derecho en el archivo index.html y selecciona "Open with Live Server".

Acceder a tu aplicación frontend:
Ahora, podrás ver tu página HTML servida en tiempo real en http://localhost:5500 (o el puerto que Live Server te asigne).