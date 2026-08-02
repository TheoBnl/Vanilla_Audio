# Vanilla Audio 🎵 

**WIP - Still in developpement, all features are not implemented yet**

A desktop audio player built with **WPF (.NET / C#)**, allowing you to scan a local folder and play **MP3** and **FLAC** files, with metadata display (title, artist, duration, cover art).

## ✨ Features

- Select a music folder via a folder picker
- Automatic scan of `.mp3` and `.flac` files in the selected folder
- Metadata extraction (title, artist, duration, cover art) via **TagLibSharp**
- Library displayed as a list with cover art, title, and duration
- Persistence of the selected folder (automatically reloaded on startup)
- Audio playback via **NAudio**

## 🏗️ Architecture

The project is organized into several class libraries, following a layered architecture (separation of View / ViewModel / Business / Data access):

```
Vanilla_Audio.sln
├── Vanilla_Audio/          # WPF project (Views, code-behind, converters, entry point)
├── ViewModels/              # ViewModels (MVVM): MainWindowVM, SettingWindowVM
├── Business/                 
│   ├── Models/               # Business models (Song, SongFolder)
│   └── Data/                 # Business-level data logic (SongManager, TagReader)
├── Data/                     # Persistence (FolderPathManager, interfaces)
└── Tests/                    # xUnit unit tests
```

**Principles applied:**
- **MVVM**: Views only know their ViewModel via `DataContext` and binding; no business logic in code-behind (aside from UI orchestration such as opening windows/dialogs).
- **Dependency inversion**: every component depends on abstractions (`IFolderPathManager`, `ISongManager`, `ITagReader`), never on concrete implementations — enabling dependency injection and unit testing with mocks/fakes.
- **Separation of concerns**: building/reading the persistence format (`SongFolder` serialized to JSON) stays internal to the `Data` layer and is never exposed to upper layers.
- **Dependency injection**: handled via `Microsoft.Extensions.Hosting` and `Microsoft.Extensions.DependencyInjection`, configured in `App.xaml.cs`.

## 🔧 Prerequisites

- [.NET SDK](https://dotnet.microsoft.com/download) (version matching the project's `TargetFramework` in the `.csproj` files)
- Visual Studio 2022 (recommended) with the **.NET Desktop Development** workload, or any compatible editor (JetBrains Rider, VS Code + C# extensions)
- Windows (WPF is not cross-platform)

## 📦 Installation

1. Clone the repository:
   ```bash
   git clone <repo-url>
   cd Vanilla_Audio
   ```

2. Restore NuGet dependencies:
   ```bash
   dotnet restore
   ```

   The project notably uses the following packages:
   - `TagLibSharp` — reading audio metadata (title, artist, duration, cover art)
   - `NAudio` — audio playback
   - `Microsoft.Extensions.Hosting` / `Microsoft.Extensions.DependencyInjection` — dependency injection
   - `xunit` — unit tests (`Tests` project)

3. Build the solution:
   ```bash
   dotnet build
   ```

## ▶️ Running the app

From Visual Studio: set `Vanilla_Audio` as the startup project, then run with **F5** (or **Ctrl+F5** without debugging).

From the command line:
```bash
dotnet run --project Vanilla_Audio
```

On first launch, no music folder is configured yet: open the **Settings** window to select the folder containing your `.mp3`/`.flac` files.

## ✅ Running the tests

```bash
dotnet test
```

Unit tests (xUnit) cover in particular:
- `FolderPathManager` (saving/loading the folder path)
- `SongManager` (folder scanning and `Song` object creation, using a fake `ITagReader`)

## 📁 Persistence format

The music folder path is saved to a local JSON file (`folderPath.json`), in the form:

```json
{ "Path": "C:/Users/.../Music" }
```

## 🗺️ Roadmap

- Playback progress bar (duration retrieved dynamically via NAudio)
- Support for multiple folders / libraries
- Playlists
- Handling of audio files with invalid metadata (dedicated visual indicator instead of `00:00`)

## 📄 License

_To be completed based on the project's chosen license._
