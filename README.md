# Fanti - A Gamified Flashcard Learning App

Fanti is a Unity-based flashcard application that combines spaced repetition learning with virtual pet gameplay. Users can create and study flashcards while taking care of their virtual companions (Fantis) that grow and level up as they learn.

## 🎮 Features

- **Virtual Pet System**: Fantis gain experience and level up as users study
- **Spaced Repetition**: Smart scheduling of cards based on user performance
- **Flexible Flashcards**: Support for both one-sided and double-sided cards
- **Progress Tracking**: Experience points, levels, streaks, and virtual currency
- **Customization**: Wearable items for Fantis to personalize their appearance

## 🛠 Prerequisites

- Unity 6.0 or higher
- .NET development environment
- Git
- Visual Studio Code or Cursor (recommended)

## 🚀 Getting Started

1. **Clone the Repository**
   ```bash
   git clone git@github.com:harrisoncparker/fanti-demo.git
   cd fanti-demo
   ```

2. **Open in Unity**
   - Open Unity Hub
   - Click "Add" and select the cloned project directory
   - Open the project with Unity 6.0 or higher

3. **Initial Setup**
   - Open the `BaseScene` in `Assets/Scenes/`
   - Ensure all required packages are imported (Unity will do this automatically)
   - If using the test data, ensure `_useTestData` is checked in the SaveDataManager

4. **IDE Setup**

   ### Option 1: Cursor (Recommended)
   1. Install Cursor IDE:
      - Download Cursor from [cursor.sh](https://cursor.sh)
      - Open Cursor and select "Open Folder"
      - Navigate to your cloned fanti-demo directory
   
   2. Install Unity Cursor Package:
      - In Unity, go to Window > Package Manager
      - Click "+" in the top-left corner
      - Select "Add package from git URL"
      - Enter: `https://github.com/boxqkrtm/com.unity.ide.cursor.git`
      - Click "Add"
   
   3. Configure Unity:
      - Go to Edit > Preferences > External Tools
      - Set 'External Script Editor' to Cursor
   
   After setup, Cursor will automatically:
   - Detect Unity project settings
   - Load project rules from `.cursor/rules/`
   - Set up intelligent code completion for Unity

   ### Option 2: Visual Studio Code
   1. Install Required Extensions:
      - C# Dev Kit
      - Unity Tools
      - Unity Debugger
   2. Install Unity VSCode Package:
      - In Unity, go to `Edit > Preferences > External Tools`
      - Set 'External Script Editor' to Visual Studio Code
      - Click 'Regenerate Project Files'
   3. Additional Setup:
      ```json
      // Recommended settings.json additions
      {
        "omnisharp.useModernNet": true,
        "omnisharp.enableRoslynAnalyzers": true,
        "files.exclude": {
          "**/*.meta": true,
          "Library/": true,
          "Temp/": true,
          "Obj/": true,
          "Logs/": true
        }
      }
      ```

## 📁 Project Structure

```
Assets/
├── Scenes/              # Unity scenes
│   ├── BaseScene       # Main scene with core managers
│   ├── Home           # Home environment for Fantis
│   ├── Shelter       # Fanti adoption area
│   └── Fanti         # Individual Fanti interaction
├── Scripts/           # C# scripts
│   ├── Models/       # Data models (PlayerModel, FantiModel, etc.)
│   ├── MonoBehaviours/ # Unity components
│   │   ├── Components/ # Reusable components
│   │   └── Managers/   # Singleton managers
│   └── ScriptableObjects/ # Unity ScriptableObjects
├── Prefabs/          # Reusable Unity prefabs
├── Art/              # Visual assets
└── UI Toolkit/       # UI definitions and styles
```

## 🏗 Architecture

### Model-View Separation
- Models (`PlayerModel`, `FantiModel`, etc.) contain serializable data
- MonoBehaviours handle Unity-specific functionality and user interactions
- Event-driven communication between components using `GameEvent` system

### Key Components
- **GameStateManager**: Manages global game state and current player/Fanti
- **SaveDataManager**: Handles data persistence using JSON serialization
- **PlayMenu**: Manages flashcard study sessions and rewards
- **Fanti**: Handles virtual pet behavior and progression

## 💾 Data Persistence

Eventually data will be stored in firebase but for development we're using a local file system.

Player data is saved locally in JSON format. The save file location is:
```
Application.dataPath/Data/testPlayerData.json
```

## 🎯 Development Guidelines

Please refer to the project rules in `.cursor/rules/` for detailed coding standards and patterns:
- `fanti-unity-rules.mdc`: Main project guidelines
- `unity-development.mdc`: Unity-specific development standards

### Key Conventions

1. **Naming**
   - Private fields: `_camelCase` with underscore prefix
   - Public properties: `PascalCase`
   - Constants: `c_ConstantName`
   - Static fields: `s_StaticName`

2. **Code Organization**
   - Use `#region` directives for logical grouping
   - Follow the established order of class members
   - Keep components focused and single-responsibility

3. **Event System**
   - Use `GameEvent` for component communication
   - Follow the Observer pattern
   - Avoid direct references between components

## 🧪 Testing

The project includes fake data generation for testing:
- Use `PlayerModel.Fake()` for test player data
- Enable `_useTestData` in SaveDataManager for development

## 🤝 Contributing

1. Follow the coding standards in `.cursor/rules/`
2. Use meaningful commit messages (see commit conventions below)
3. Test thoroughly before submitting changes
4. Document any new features or significant changes

### Commit Message Conventions

Format: `<scope> - <description>`

- **scope**: The area of the codebase being modified (lowercase)
- **description**: A brief description of the change (lowercase)
- Use hyphen + space (`" - "`) to separate scope from description

Examples:
```
play menu - notify player of level ups
navigation - display gold and scheduled cards
bugfix - close overlay menus when changing scene
```

Common scopes:
- `play menu`: Changes to the flashcard study interface
- `navigation`: UI navigation and scene management
- `bugfix`: Bug fixes and corrections
- `save data`: Data persistence related changes
- `fanti menu`: Fanti customization and stats UI
- `decks & cards`: Flashcard and deck management
- `home`: Home scene and environment
- Multiple words in scope are separated by spaces, not hyphens
