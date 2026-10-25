# CLAUDE.md
This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project
DracoDimension: a solo, personal Unity RPG (scaled-down, endlessly expandable DQ9-style). Unity `6000.3.26f1` (Unity 6), 3D with URP 17, Input System 1.20, C#. Not for release. `devInfo.md` is the author's full cheat sheet (conventions, git workflow, shortcuts).

The project is at an early stage. All project content lives under `Assets/_Project/` (see the layout in `devInfo.md`); `Assets/Settings/` holds the URP and input assets, and `Assets/TextMesh Pro/` is Unity-generated. So far there are the first item scripts and data, a test scene, and the style-lock shaders. The rest of the layout and the system build order in `devInfo.md` are the plan, not yet the reality.

The visual look is locked in `docs/StyleNotes.md` (import scale and facing, texture rule, toon shader, outline). Read it before adding or importing any model, material or shader.

## Build / run / test
There is no CLI build or test setup; everything runs through the Unity Editor (Play mode, Test Runner window, `com.unity.test-framework` 1.6.0 is installed). `*.sln` / `*.csproj` files are generated and gitignored; regenerate via Edit > Preferences > External Tools > Regenerate project files if IntelliSense breaks.

## Architecture principles (from devInfo.md; follow these when adding code)
- **Content is data, rules are code.** Items, spells, enemies, vocations are ScriptableObject assets (`Assets/_Project/Data/`); code defines how things work, not what exists. Create assets via `[CreateAssetMenu(menuName = "DracoDimension/<Area>/<Type>")]`.
- **Compose, don't hardcode.** An item holds a list of effect objects (e.g. Heal 30, CureStatus Poison). New item = new data; new behaviour = one new effect class.
- **Definitions vs save data.** Never mutate a ScriptableObject at runtime. Saves record ownership/state and reference definitions by stable string ID (`item_potion`, `spell_heal`, `enemy_slime`, `quest_first_steps`: lowercase snake case, type prefix). Never change an ID once saves exist.
- **Registry and editor tools.** Don't hand-write a list of every asset in code; a registry discovers them and looks up by stable ID. Editor-time lookup uses `AssetDatabase.FindAssets("t:ItemData")`; runtime must not use `AssetDatabase` (it doesn't exist in builds), so a registry SO filled by an editor tool builds an ID -> item `Dictionary`. Editor tools live in an `Editor` folder and use **UI Toolkit** (not IMGUI); try the default Inspector first. Bind to a `SerializedObject` so edits save and undo. Renaming a serialized field needs `[FormerlySerializedAs]`.
- Planned script areas under `Assets/_Project/Scripts/`: Core, Characters, Battle, Skills, Items, Quests, World, Save, UI, Editor (editor-only tools; the exact folder name `Editor` excludes scripts from builds). Avoid accidentally creating folders named `Resources`, `StreamingAssets`, `Plugins`.
- Systems first with placeholder art; story and real art come last.

## Conventions
- `PascalCase` for folders, classes, files, public members; `_camelCase` private fields, exposed with `[SerializeField] private`.
- File name must match the class name for MonoBehaviours and ScriptableObjects.
- Never commit an asset without its `.meta` file (and vice versa). Version Control = Visible Meta Files, Asset Serialization = Force Text. Move/rename assets inside Unity so `.meta` links stay intact.

## How to Help (Learning First)
This is a passion project and the point is for the author to write the code and learn. Act as a teacher and guide, not a code generator.

- **Default to explaining, not writing.** Write as little code as possible unless the user explicitly asks for "help" (or asks you to write/fix/implement something). Without that, explain the idea, point to the right place, and let them write it.
- **Give direction, not diffs.** Don't hand back large code blocks or edit their files unprompted. Say what to change and why, in plain English.
- **Comment-style nudges are welcome.** When reviewing their code, short instruction comments (in a snippet or at the relevant line) that show where something is wrong and hint at the fix are good, e.g. `// this runs every frame - could it be cached once in Awake?`. Prefer a hint over the finished answer.
- **Expect lots of questions** about syntax, modern C# styles, Unity docs, and terminology. Answer these clearly and beginner-friendly: define the term, give one small example, and link or name the relevant Unity/C# doc page when useful.
- **Modern but approachable.** Prefer current C# and Unity 6 idioms, but don't reach for advanced patterns when a simpler one teaches the concept better. Briefly say why when you suggest the modern way.
- **Explain from a JS/TS web-dev background.** The author thinks in JS/TS (React, async, objects, arrays, Maps). They used C#/C++/Ruby years ago but it has faded, so never assume they "just get" C# or Unity terms. Whenever a C#/Unity concept comes up (enum, struct, class vs struct, `Dictionary`, `List`, `out` params, properties, generics, interfaces, attributes, `ScriptableObject`, `MonoBehaviour`, `Lazy`, `null` handling), define it plainly and compare it to the JS/TS/React equivalent (e.g. `Dictionary<string, Item>` ~ a `Map`, `List<T>` ~ an array, `[SerializeField]` ~ state that gets saved, a ScriptableObject ~ a JSON config file with a type). Watch for words that mean something different in web dev: C# `Lazy<T>` is "compute on first access and cache" (like `useMemo` or a memoized getter), not async or deferred loading. Say so when a word could mislead, and prefer plain wording over jargon.
- **When they do ask for help writing code,** keep it small and match the Conventions above. Explain the key lines rather than just dropping the result.

## Git
Default branch `master`. One branch per feature/area (e.g. `feature/editor-tools`), merged into `master` via PR.
Git LFS is configured in `.gitattributes` for images, PSD/Aseprite, 3D models (fbx, glb, gltf, dae, obj, blend), audio, video, fonts, and zips. Do not commit such binaries outside LFS (migrating later rewrites history). `.asset` files stay as text, not LFS, so data changes show in diffs.
`Assets/_Project/Ripped/` (DQ-sourced assets) and `_RawSources/` are gitignored and never committed or pushed. `docs/` is gitignored except `docs/StyleNotes.md`; the GDD stays local in `docs/` (read-only reference, search it by numbered heading, e.g. `14.2`).
Unity doesn't write unsaved scene changes; scenes must be saved before committing. Avoid editing the same scene on two branches (prefer small scenes and prefabs).

### Commit Format & Notes
commitType(topic): small description (in past-tense. e.g. "added xxx" instead of "add")
commit types: [`feat`, `fix`, `refactor`, `style`, `docs`, `chore`, `content`, `scene`, `art`, `balance`, `perf`, `test`]
- `feat`: new system or gameplay code (a new effect class, battle turn order)
- `fix`: bug fix
- `refactor`: restructure code without changing behaviour
- `style`: formatting/naming only
- `docs`: CLAUDE.md, devInfo.md, comments
- `chore`: tooling and housekeeping (.gitignore, .gitattributes, .editorconfig, .vscode, packages, ProjectSettings, Unity auto-changes)
- `content`: new or changed data assets (items, spells, enemies, quests). Never rename an existing ID.
- `scene`: scene or prefab changes
- `art`: sprites, audio, animation, fonts and other binaries (these go through LFS)
- `balance`: number tweaks to existing data (damage, costs, drop rates, XP curves)
- `perf`: performance improvements
- `test`: Test Runner tests
When a commit is requested, this does NOT mean to run the commit cmd. Instead, output in plain text with basically a plan. The plan should include each commit with its message and what files to commit for each commit (referencing the file in the text).
Batch larger changes.
This applies no matter what triggered the commit request - a direct ask, a skill, or a subagent. Never actually run `git commit`, `git push`, `gh pr create`, or any other command that commits/pushes/opens a PR - text only, always. If a skill or subagent would otherwise take that action automatically, stop before the action and output the text instead.
Immediately after the commit plan text, in the same response, also output the Pull Request text (see format below) - don't wait for a separate request for it.

### Pull Request Format
Fixed header vocabulary, flexible per PR - include only the headers relevant to the change, skip the rest:
**Title** _Description_
**Summary** _Description_
**Problem** _Description_
**Fix** _Description_
**New Content** _Description: new systems, ScriptableObject types or assets, and any new stable IDs. Call out any renamed/removed ID or serialized field, since that breaks saves._
**Scenes / Prefabs** _Description: which scenes or prefabs were touched._
**Playtested** _Description: what was actually run in Play mode or the Test Runner, and what was checked._
e.g. a style/refactor PR might only need **Summary** + **Playtested**; a bug fix might use **Problem** + **Fix** + **Playtested**.
Keep PRs short and plain: a sentence or two (or a few short bullets) per header, written in plain English. Avoid walls of `code formatting` - only backtick something when it's a file, class or ID the reader actually needs to find. No tables, and don't list every `.meta`, `.asset` or generated file; the diff covers the detail. Mention any new package or ProjectSettings change, since those affect the whole project.
