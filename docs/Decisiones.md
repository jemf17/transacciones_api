### Tecnologías

##### Front
Se realizara con react vite en typescript, tailwind para soportar mejor velocidad de renderizado y agilidad a la hora de ingresar y cargar
###### **Librerías**:
* i18next
##### Black
Se realizara la api en C# con .net core para tener mejor soporte a la hora de despliegue en servidores linux, las automatizaciones se realizaran en go, debido a su compilación eficaz programados por github actions, en caso de realizar actividades que requieran un acceso a la bd se deberán conectar a un proxy (pronto a configurar y realizar con [traefik](https://traefik.io/)) , la autenticación del mismo proxy se deberá especificar dentro de las variables de entorno de github actions.

	La administracion de usuarios sera organizada por el momento por supabase (no se puede restaurar, descargue el backup que me proporcionaron ellos)
###### **Librerías**:
* ... 

##### BDs
Se realizara por postgres 16, con un factor de encryptacion de sha256
*Explica que es cada esquema y funciones*

Seguridad:

para el control de los servicios se utilizara [Wazuh](https://wazuh.com/)