# gmd2-snake

Source code for session **03 Snake** of the Game Architecture (GAR) course.

The game is built up in steps. Each step is a separate project that builds on the previous
one, so you can follow the code's evolution one concept at a time. Compare two neighbouring
steps (e.g. with a diff tool) to see exactly what changed.

| Step | Topic | What's new |
| --- | --- | --- |
| `Snake0` | Starting point | Loads the atlas image and draws parts of it with hardcoded source rectangles |
| `Snake1` | Texture atlas | `TextureAtlas` loads named regions from `atlas-definition.xml` |
| `Snake2` | Sprites | `Sprite` wraps a region with color, rotation, scale and origin |
| `Snake3` | Animation | `AnimatedSprite` plays animations defined in the atlas |
| `Snake4` | Input | A `Snake` class that moves on the grid, polling the keyboard directly |
| `Snake5` | Command pattern | Buttons are bound to `ICommand` objects; `R` reverses the controls by swapping commands |
| `Snake6` | Undo & redo | Commands can `Undo()`; a `CommandInvoker` keeps the history |
| `Snake7` | Collision detection | A bouncing `Bat` with circle collision; the snake eats the bat |
| `Snake8` | Tilemap | The room is drawn from `tilemap-definition.xml` and the walls limit movement |

All steps share the **GMDCore** library, which contains the final versions of the reusable
classes (`TextureAtlas`, `Sprite`, `AnimatedSprite`, `Tilemap`, `Circle`, input, …).

## Content

All steps also share the same assets and the same **content builder** (MonoGame 3.8.5+):

```text
Content/
├── Assets/                  # The raw assets: images and XML definitions
├── Builder/Builder.cs       # The rules for building the assets, in C#
├── BuildContent.targets     # Runs the builder when a game project builds
└── Content.csproj
```

There is no `.mgcb` file and no MGCB Editor. `Builder.cs` decides how each asset is
processed: PNG images are built into textures, and the XML files are copied as they are.
Each step project imports `BuildContent.targets`, so building a step also builds the content
into its output folder, where `Content.Load` finds it.

To add an asset, put it in `Content/Assets` and, if no existing rule matches it, add a rule
in `Builder.cs`.

## Controls

| Key | Action |
| --- | --- |
| `W` `A` `S` `D` | Move (from `Snake4`) |
| `R` | Reverse the controls (from `Snake5`) |
| `Q` / `E` | Undo / redo (from `Snake6`) |
| `Esc` | Quit |

## Running a step

Requires the [.NET 10 SDK](https://dotnet.microsoft.com/download).

```sh
dotnet run --project Snake8
```

Or open `Snake.slnx` and choose the step to run.
