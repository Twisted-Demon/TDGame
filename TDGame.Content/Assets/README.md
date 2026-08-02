# Asset organization

Gameplay assets are grouped by feature so each entity's blueprint, definition,
visuals, animation, and audio can evolve together. Global audio and reusable
visuals live in `audio` and `shared` respectively.

Engine-owned effects and fonts live in the sibling `Dreambit.Content` project.
This directory contains only TDGame-owned source assets.

## Naming

Use lowercase kebab-case for directories and descriptive names. Asset filenames
follow this pattern:

`descriptive-name.asset-type.source-extension`

Examples:

- `space-diver-idle.animation.json`
- `railgun.sprite-sheet.json`
- `railgun.texture.png`
- `railgun-fire.sound-cue.json`
- `railgun-fire.audio.wav`

Runtime references keep the asset-type suffix and omit only the source
extension. For example, `space-diver-idle.animation.json` is referenced as
`space-diver-idle.animation`. The baker then produces the corresponding baked
extension.
