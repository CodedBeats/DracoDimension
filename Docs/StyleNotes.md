# Style Notes

How every model in DracoDimension is imported and rendered, so later imports match. Written after the style-lock test (Goresby-Purrvis from DQ9, a Quaternius dragon, a KayKit knight).

## Look in one line
Low-poly 3D models, point-filtered textures, hard two-band toon lighting, thin black screen-space outline.

## Folders
- `Assets/_Project/Ripped/`: DQ-sourced assets. Gitignored, never pushed. Each monster gets its own subfolder.
- `Assets/_Project/ThirdParty/`: CC0 packs (Quaternius, KayKit), left untouched. Keep each pack in its own folder.
- `Assets/_Project/Art/Shaders/`: the toon shaders and their `.hlsl` files.
- `_RawSources/` (project root, gitignored): raw downloads before import.

## Import rules (model)
- **Convention:** 1 Unity unit = 1 metre. An adult human is about 1.8 units tall.
- **Facing:** models face +Z (Unity's forward). Rotate the camera to see the front, never the model.
- **Scale:** fix it in the model's import settings (Scale Factor), not on the GameObject.
  - DQ9 rips have no unit info and are huge. Goresby-Purrvis uses Scale Factor `0.08` with Convert Units off.
  - Quaternius and KayKit models already come in at the right size (Scale Factor `1`).
- **Materials:** extract them (Materials tab > Extract Materials...) into a `Materials` folder next to the model, then set each one's shader to `Shader Graphs/Toon`.
- **Textures:** assign the PNG to the material's **Base Map** slot. Leave Base Color white for textured models. Flat-colour models (the dragon) set Base Color and leave Base Map empty.

## Texture rule
- Keep original textures, **Filter Mode: Point**, **Compression: None**.
- Only upscale (4x) an individual texture that looks rough. Don't upscale by default.

## Toon shader (`Toon.shadergraph`)
Unlit Shader Graph that does its own lighting.
- Lighting amount = dot(world normal, main light direction), from the Custom Function `ToonMainLight` (`ToonLighting.hlsl`).
- Smoothstep makes hard bands, then Lerp between `Shadow Color` (dark side) and white (lit side).
- Final colour = bands x (Base Map x Base Color) x light colour.
- Properties: `Base Map`, `Base Color` (white), `Shadow Color` (`#6A5F8A`), `Threshold` (`0`), `Softness` (`0.02`).
- Not yet done: it ignores shadow maps and ambient light, so keep shadows off for now.

## Outline (`ToonOutline.shadergraph`)
Screen-space depth edge detection, applied once to the whole screen. No per-model setup.
- Fullscreen Pass Renderer Feature on `URP_3D_Renderer`, material `ToonOutline_Mat`.
- Injection Point: Before Rendering Post Processing. Requirements: **Depth** must be ticked, otherwise nothing draws.
- `Thickness` `1.5`, `Threshold` `0.3`, `Outline Color` black.
- Check it in the Game view, not the Scene view.
- Limit: depth-only, so it draws silhouettes and overlaps, not inner creases.
- Limit: transparent sprites (icons, speech bubbles) don't write depth, so they get no outline. That's intended for overlay-style sprites. If a world sprite ever needs one, use an Opaque + Alpha Clipping material.

## Lighting and camera
- One Directional Light as the main light. Shadows off for now.
- TODO: record the final light rotation and camera angle/FOV here once they're settled.
