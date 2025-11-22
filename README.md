# AuthAdmin

AuthAdmin es una API de autenticación construida con **.NET 10** que permite registrar usuarios, autenticar mediante **JWT** y acceder a endpoints protegidos.

---

## Características

* Registro e inicio de sesión
* Generación de tokens JWT
* Endpoints protegidos
* Manejo de configuración con User Secrets

---

## Tecnologías

* .NET 10
* ASP.NET Core
* Entity Framework Core
* SQL Server
* JWT Authentication

---

## Instalación

```bash
git clone https://github.com/midnight-ar19/AuthAdmin.git
cd AuthAdmin
```

Configurar User Secrets:

```bash
dotnet user-secrets init
dotnet user-secrets set "ConnectionStrings:DefaultConnection" "TU_CONEXION"
dotnet user-secrets set "Jwt:Key" "TU_CLAVE_SECRETA"
dotnet user-secrets set "Jwt:Issuer" "tu_issuer"
dotnet user-secrets set "Jwt:Audience" "tu_audience"
dotnet user-secrets set "Jwt:ExpireMinutes" "60"
```

---

## Endpoints

* **POST** `/api/auth/register` — Registro
* **POST** `/api/auth/login` — Inicio de sesión
* **GET** `/api/usuarios` — Listado protegido

---

## 🧭 Autor

**Alan Alvarez** — Backend Developer
