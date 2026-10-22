# DracoDimension - Dev Info

Personal Unity RPG (scaled-down, endlessly expandable DQ9-style). Solo project, not for release.
This file is my cheat sheet: structure, conventions, commands, and the rules I want to stick to.

- Engine: Unity 6 LTS, Universal 2D (URP) template
- Language: C# (Visual Studio Community)
- VCS: Git + Git LFS, default branch `master`, one branch per feature/area, merged into `master` via PR
- Design goals: data-driven content, systems before story/art, easy to keep adding levels/areas/gear

---

## 1. Core principles

1. **Content is data, rules are code.** Items, spells, enemies, vocations live in ScriptableObject assets. Code defines *how* things work, not *what exists*.
2. **Compose, don't hardcode.** An item holds a list of effects (Heal 30, CureStatus Poison). New item = new data. New behaviour = one new effect class.
3. **Definitions vs save data.** Never modify a ScriptableObject at runtime. The asset says what a Potion *is*; the save file says I own 5. Reference things by stable string ID (e.g. `item_potion`), never by name or asset path.
4. **Systems first, placeholders for art.** Combat, items, equipment, skills, party, quests all get built with boxes and text before any real art or story.
5. **Small commits, one system per branch.**

---

## 2. Folder structure

```
DracoDimension/                 <- git repo root (contains Assets/)
|- Assets/
|  |- Scenes/                   Bootstrap, Battle, Town, Dungeon, test scenes
|  |- Scripts/
|  |  |- Core/                  Game manager, event bus, service locator, utilities
|  |  |- Characters/            Stats, party, vocations, levelling
|  |  |- Battle/                Turn order, actions, targeting, damage formulas, enemy AI
|  |  |- Skills/                Skill points, abilities, battle spells, utility spells
|  |  |- Items/                 Item types, effects, inventory, equipment
|  |  |- Quests/                Quest definitions, tracking, flags
|  |  |- World/                 Maps, dungeons, NPCs, encounters
|  |  |- Save/                  Save/load, serialization, ID registry
|  |  |- UI/                    Menus, battle HUD, inventory screens
|  |  `- Editor/                Editor-only tools (item browser, validators)
|  |- Data/                     ScriptableObject assets (items, spells, enemies, vocations...)
|  |- Art/                      Sprites, tilesets, UI art
|  |- Audio/
|  |- Prefabs/
|  `- Settings/                 URP assets, input actions
|- Packages/                    Package manifest (committed)
|- ProjectSettings/             Project settings (committed)
|- .gitignore
|- .gitattributes               (Git LFS rules)
`- devInfo.md                   (this file)
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

## 5. Daily git workflow

```bash
git status                           # what changed
git switch master
git pull origin master               # always start from the latest master
git switch -c feature/combat         # new branch for a system
# ...work...
git add .
git commit -m "Add basic damage calculation"
git push -u origin feature/combat    # first push of a new branch (plain `git push` after that)
# open a PR on GitHub, look over the diff, merge
git switch master
git pull origin master               # pick up the merged work
git tag v0.1-combat                  # tag stable points on master
git push --tags
```

Continuing work on an existing area branch (e.g. `feature/combat`):

```bash
git switch feature/combat
git pull origin master               # bring the branch up to date with master first
```

### Branch lifetime

- **One-off branches:** delete after the PR merges.
- **Area branches** (combat, items, quests, ui): fine to keep and reuse, as long as I pull `master` into them before continuing.
- Deleting a merged branch loses nothing. The commits are already in `master`, and GitHub can restore a deleted branch from its PR page.
- If PRs are **squash-merged**, reusing the same branch can cause odd conflicts because `master` holds one squashed commit rather than the branch's own commits. Either use regular merge commits, or reset the branch to master before reusing it (only when it has nothing unmerged): `git switch feature/combat` then `git reset --hard origin/master`.
- Optional GitHub setting: Settings > General > "Automatically delete head branches" removes the remote branch on merge.

Habits:
- **Save the scene (Ctrl+S) before committing.** Unity doesn't write unsaved scene changes.
- Never commit an asset without its `.meta` file (and vice versa). Keep Edit > Project Settings > Editor > Version Control on **Visible Meta Files** and Asset Serialization on **Force Text**.
- One system per commit. Easier to roll back.
- Work from one machine at a time, or always pull before starting and push when stopping.

### Useful git commands

```bash
git log --oneline --graph --all  # history overview
git diff                         # unstaged changes
git diff --staged                # staged changes
git restore <file>               # discard changes to a file
git restore --staged <file>      # unstage a file
git stash / git stash pop        # shelve and reapply work in progress
git branch -d feature/combat     # delete a merged local branch
git push origin --delete feature/combat   # delete the remote branch
git fetch --prune                # drop local refs to remote branches that were deleted
git mv scripts Scripts           # fix a case-only rename (Windows)
```

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

(Add design notes, balance ideas, TODOs here.)

---

## 10. Editor tools and the item registry

Editor code lives in an `Editor` folder (e.g. `Assets/Scripts/Editor/`) so it is excluded from builds. Use **UI Toolkit** for new tools (IMGUI is legacy). Try the default Inspector first; only build a window when it stops being enough.

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
