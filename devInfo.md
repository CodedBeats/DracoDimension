# DracoDimension - Dev Info

Personal Unity RPG (scaled-down, endlessly expandable DQ9-style). Solo project, not for release.

- Engine: Unity 6 LTS, 3D with the Universal Render Pipeline (URP). Switched from the 2D template; render settings live in `Assets/Settings/URP_3D*.asset`
- Look: low-poly 3D with a shared toon shader and screen-space outline (see `docs/StyleNotes.md`)
- Language: C# (using VS Code cause fuck Visual Stuio)
- Version Control: Git + Git LFS, default branch `master`, one branch per feature/area, merged into `master` via PR
- Design goals: data-driven content, systems before story/art, easy to keep adding levels/areas/gear

---

## 1. Core principles

1. **Content is data, rules are code.** Items, spells, enemies, vocations live in ScriptableObject assets. Code defines *how* things work, not *what exists*.
2. **Compose, don't hardcode.** An item holds a list of effects (Heal 30, CureStatus Poison). New item = new data. New behaviour = one new effect class.
3. **Definitions vs save data.** Never modify a ScriptableObject at runtime. The asset says what a Potion *is*; the save file says I own 5. Reference things by stable string ID (e.g. `item_potion`), never by name or asset path.
4. **Systems first, placeholders for art.** Combat, items, equipment, skills, party, quests all get built with boxes and text before any real art or story.

---

## 2. Folder structure

```
DracoDimension/                     # git repo root (contains Assets/)
│
├── Assets/
│   ├── _Project/                   # everything I make (underscore sorts it to the top)
│   │   ├── Scenes/                 # Bootstrap, Battle, Town, Dungeon, test scenes
│   │   │
│   │   ├── Scripts/
│   │   │   ├── Core/               # game manager, event bus, service locator, utilities
│   │   │   ├── Characters/         # stats, party, vocations, levelling
│   │   │   ├── Battle/             # turn order, actions, targeting, damage formulas, enemy AI
│   │   │   ├── Skills/             # skill points, abilities, battle spells, utility spells
│   │   │   ├── Items/              # item types, effects, inventory, equipment
│   │   │   ├── Dimension/          # idk, but didn't know where else to put it, hold all dimension stoof
│   │   │   ├── Quests/             # quest definitions, tracking, flags
│   │   │   ├── World/              # maps, dungeons, NPCs, encounters
│   │   │   ├── Save/               # save/load, serialization, ID registry
│   │   │   ├── UI/                 # menus, battle HUD, inventory screens
│   │   │   ├── Sandbox/            # throwaway test scripts (ItemTester, FloatingSpeech)
│   │   │   └── Editor/             # editor-only tools (item browser, validators)
│   │   │
│   │   ├── Data/                   # ScriptableObject assets, grouped by type
│   │   │   └── Items/              # item assets (Potion.asset ...); spells, enemies, vocations follow
│   │   │
│   │   ├── Art/                    # models, textures, materials, UI art
│   │   │   └── Shaders/            # toon shaders and their .hlsl files
│   │   │
│   │   ├── Animation/
│   │   ├── VFX/
│   │   ├── Audio/
│   │   ├── Prefabs/
│   │   │
│   │   ├── ThirdParty/             # CC0 packs (Quaternius, KayKit), left untouched
│   │   └── Ripped/                 # DQ-sourced assets. Gitignored, never pushed
│   │
│   ├── Settings/                   # URP assets, input actions
│   └── TextMesh Pro/               # Unity-generated TMP essentials, leave where it is
│
├── Packages/                       # package manifest (committed)
├── ProjectSettings/                # project settings (committed)
├── docs/                           # GDD (gitignored, local) and StyleNotes.md (committed)
├── _RawSources/                    # raw downloads before import (gitignored)
│
├── .gitignore
├── .gitattributes                  # Git LFS rules
└── devInfo.md                      # this file
```

Notes:
- A folder named exactly `Editor` is special: its scripts compile into the editor only and are excluded from builds. Keep dev-only tools there.
- Other exact-name folders Unity treats specially: `Resources`, `StreamingAssets`, `Plugins`. Avoid these names by accident.
- Not committed (regenerated): `Library/`, `Temp/`, `Obj/`, `Logs/`, `UserSettings/`, `.vs/`, `*.sln`, `*.csproj`.

---

## 3. Naming and code conventions

- Folders, classes, public members, files: `PascalCase`
- Private fields: `_camelCase`; expose to the Inspector with `[SerializeField] private`
- **File name must match the class name** for MonoBehaviours and ScriptableObjects, or Unity won't attach them
- Stable IDs: lowercase snake case with a type prefix, e.g. `item_potion`, `spell_heal`, `enemy_slime`, `quest_first_steps`. Never change an ID once saves exist
- Assets: name after the thing (`Potion.asset`), grouped in subfolders by type
- Use `[CreateAssetMenu(menuName = "DracoDimension/Items/Item")]` so data assets are created from the right-click menu

---

## 4. Planned systems (build order)

1. Core loop + data foundation (ID registry, ItemData, ScriptableObject pipeline)
2. Basic turn-based combat (turn order, actions, damage, win/lose)
3. Items + effects, inventory, equipment
4. Stats, levelling, party
5. Skills: skill points, vocations, battle spells vs utility spells
6. Quest system, flags
7. Dungeons / areas / encounters
8. Save/load
9. Dev tools (item browser window, validators)
10. Story and real art last

---

## 5. GIT
### Active branches
- docs
- feature/editor-tools
- feature/item-data

### Branches to/can delete
- feature/style-lock

### Helpful cmds
```bash
git switch -c feature/combat        # new branch for a system
git switch master   

git mv scripts Scripts              # fix a case-only rename (Windows)
git restore <file>                  # discard changes to a file
git restore --staged <file>         # unstage a file

git tag v0.1-combat                 # tag stable points on master
git push --tags
```

### Habits:
- Never commit an asset without its `.meta` file (and vice versa).

### Git LFS
```bash
git lfs install                  # once per machine
git lfs track                    # list tracked patterns
git lfs ls-files                 # files currently stored in LFS
git lfs status
git lfs version
```
If an image was committed before LFS rules existed, convert it with:
`git lfs migrate import --include="*.png"` (rewrites history, so do it early or not at all).

---

## 6. Unity shortcuts and tips
| Action | Shortcut |
|---|---|
| Play / Pause / Step | Ctrl+P / Ctrl+Shift+P / Ctrl+Alt+P |
| Save scene | Ctrl+S |
| Move / Rotate / Scale / Rect tools | W / E / R / T |
| Hand tool | Q |
| Frame selected in Scene view | F |
| Duplicate | Ctrl+D |
| New empty GameObject | Ctrl+Shift+N |
| Open Console | Ctrl+Shift+C |

- Changes made in Play Mode are **lost** when you stop (including ScriptableObject values you edit in the Inspector while playing, unless the asset is edited outside play).
- Rename and move assets inside Unity's Project window so `.meta` files stay linked.
- Project files not syncing in Visual Studio: Edit > Preferences > External Tools > **Regenerate project files**, or Assets > Open C# Project.
- Domain reload after every script change is normal; Enter Play Mode Options can speed it up later.


## 8. Troubleshooting
- **Pink/missing references after a pull:** a `.meta` file was missed or an asset moved outside Unity. Check `git status` for untracked `.meta` files.
- **IntelliSense broken:** regenerate project files (see above), then restart VS.
- **Huge repo / slow pushes:** a binary was committed outside LFS. Check `git lfs ls-files` and `.gitattributes`.
- **Scene merge conflicts:** avoid editing the same scene on two branches. Prefer small scenes and prefabs.

---

## 9. Notes / ideas
(design notes, balance ideas, TODOs here)

---

## 10. Editor tools and the item registry
Editor code lives in an `Editor` folder (e.g. `Assets/_Project/Scripts/Editor/`) so it is excluded from builds. Use **UI Toolkit** for new tools (IMGUI is legacy). Try the default Inspector first; only build a window when it stops being enough.

### Registry (better lookup)
- Don't hand-write a list of every item in code. The registry should *discover* the assets.
- Look things up by stable string ID (`item_potion`), never by file name or asset path.
- Two options:
  - **Editor-time:** `AssetDatabase.FindAssets("t:ItemData")` finds every `ItemData` asset. Good for tool windows (browser, search, validation).
  - **Runtime:** a `ScriptableObject` registry holds a list of item references, filled by an editor tool, and builds an ID -> item `Dictionary` on load. Don't use `AssetDatabase` at runtime, it doesn't exist in builds.
- Make the tool warn about duplicate or empty IDs, since saves depend on them never changing.

### UI Toolkit tool for making SOs
- Add a class inheriting `EditorWindow` in an `Editor` folder, with a `[MenuItem("DracoDimension/...")]` entry to open it.
- Build the UI in `CreateGUI()`. Structure goes in UXML, styling in USS, logic in C#; or build the elements in code to start.
- Create an asset with `ScriptableObject.CreateInstance<ItemData>()`, then `AssetDatabase.CreateAsset(item, path)` and `AssetDatabase.SaveAssets()`.
- Edit an asset with a `SerializedObject` and bind the fields to it (e.g. `rootVisualElement.Bind(serializedObject)`), so edits save and support undo with no manual copying.
- Fields on the SO should stay `[SerializeField] private`; the `SerializedObject` reads them by field name, so renaming one later needs `[FormerlySerializedAs]`.
- Unity docs: "UI Toolkit" > "Create a custom Editor window".
