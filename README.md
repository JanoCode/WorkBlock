# WorkBlock

Temporizador de enfoque para Windows diseñado para iniciar rápidamente bloques de trabajo de 30, 45 o 60 minutos, seguidos por un descanso de 15 minutos.

[![Latest release](https://img.shields.io/github/v/release/JanoCode/WorkBlock?label=versi%C3%B3n)](https://github.com/JanoCode/WorkBlock/releases/latest)
[![Platform](https://img.shields.io/badge/plataforma-Windows-0078D4)](https://github.com/JanoCode/WorkBlock/releases/latest)

## Funcionalidades

- Bloques de enfoque de 30, 45 o 60 minutos.
- Descansos automáticos de 15 minutos.
- Iniciar, pausar, continuar, reiniciar u omitir el descanso.
- Notificaciones nativas de Windows al comenzar y terminar cada bloque.
- Avisos de audio.
- Ventana compacta, movible y con opción de mantenerse siempre visible.
- Instalador y desinstalador para Windows x64.

## Descargar

La forma más simple de usar la aplicación es descargar el instalador desde [Releases](https://github.com/JanoCode/WorkBlock/releases/latest).

1. Descarga `WorkBlock-Setup.exe` desde la versión más reciente.
2. Ejecuta el instalador.
3. Abre WorkBlock desde el menú Inicio o desde el acceso directo opcional.

## Tecnologías

- C# y WPF
- .NET `10.0` para Windows
- Windows App SDK `2.3.1`
- Notificaciones mediante `Microsoft.Windows.AppNotifications`
- Inno Setup para crear el instalador

## Ejecutar desde el código

```powershell
git clone https://github.com/JanoCode/WorkBlock.git
cd WorkBlock
dotnet restore
dotnet run --project WorkBlock/WorkBlock.csproj
```

Requisitos de desarrollo:

- Windows 10/11 x64
- SDK de .NET 10

## Estructura

```text
WorkBlock/
├── WorkBlock/           # aplicación WPF
│   ├── MainWindow.xaml  # interfaz
│   └── MainWindow.xaml.cs
└── Installer/           # script de Inno Setup
```

## Estado

La versión estable actual es `v1.0.2`. El historial de cambios y los instaladores están disponibles en la sección de releases.

## Próximas mejoras

- Preferencias persistentes.
- Personalización de la duración del descanso.
- Historial básico de sesiones.
- Pruebas automatizadas para la lógica del temporizador.

---

**English summary:** A compact Windows focus timer built with C# and WPF. It supports 30/45/60-minute work blocks, automatic 15-minute breaks, native notifications, audio feedback and a downloadable installer.

