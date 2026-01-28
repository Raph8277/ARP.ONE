# Copilot / AI Agent Instructions for ARP.ONE

This file contains concise, project-specific guidance for AI coding agents working in this repository.

Summary
- Architecture: layered .NET solution with a Blazor server host (`ARP.ONE.BlazorApp`) that also references an interactive WASM client (`ARP.ONE.BlazorApp.Client`). Data models are generated under `ARP.ONE.Generation`.
- UI framework: MudBlazor is used in both server and client projects (see service registration in Program.cs).

Quick pointers (most important files)
- Server startup and DI: [ARP.ONE.BlazorApp/ARP.ONE.BlazorApp/Program.cs](ARP.ONE.BlazorApp/ARP.ONE.BlazorApp/Program.cs#L1-L80) — registers Razor Components, interactive render modes, and `AddMudServices()`.
- Client startup: [ARP.ONE.BlazorApp/ARP.ONE.BlazorApp.Client/Program.cs](ARP.ONE.BlazorApp/ARP.ONE.BlazorApp.Client/Program.cs#L1-L40) — lightweight WASM host, also calls `AddMudServices()`.
- Generated EF models & context: [ARP.ONE.Generation/Models](ARP.ONE.Generation/Models) — contains scaffolded entity classes and the generated DbContext (see the long-named dbContext file in that folder).
- EF generation config: [ARP.ONE.Generation/efpt.config.json](ARP.ONE.Generation/efpt.config.json) — used to regenerate DB models.

Why things are structured this way
- The solution separates API/host, client, domain, contracts, and infrastructure to keep responsibilities clear. UI-specific dependencies (MudBlazor) are registered per-host (server and client). Generated DB code is isolated in `Generation` so regeneration does not mix with hand-authored domain code.

Build / run / developer workflows
- Build solution: `dotnet build ARP.ONE.BlazorApp.sln` from repo root.
- Run the app (hosts client + server): `dotnet run --project ARP.ONE.BlazorApp/ARP.ONE.BlazorApp.csproj` from repo root. This starts the server that serves the Razor Components and static assets.
- Regenerate DB models: edit or review `ARP.ONE.Generation/efpt.config.json` and run the generation tool you use locally (config present but the repo does not include the generator binary). Inspect `ARP.ONE.Generation/Models` after generation.

Project-specific conventions and patterns
- Layered projects: `Contracts` (DTOs/interfaces), `Domain` (entities + business rules), `Infrastructure` (data persistence & context), `Services` (application services), `Generation` (auto-generated DB models). When adding new services, place interfaces in `Contracts` and implementations under `Services`.
- Minimal server Program.cs: server config relies on AddRazorComponents / MapRazorComponents with both server and WASM render modes. When adding UI assemblies, include them via `AddAdditionalAssemblies(...)` as seen in the server Program.cs.
- UI components: client-side pages and layouts live in `ARP.ONE.BlazorApp.Client/Pages` and `ARP.ONE.BlazorApp.Client/Layout`. Follow existing component patterns and use MudBlazor components (registered via `AddMudServices`).

Integration points & external dependencies
- MudBlazor: UI library added to both host and client. Example: `builder.Services.AddMudServices();` in both Program.cs files.
- EF / DB models: models are generated (scaffolded) into `ARP.ONE.Generation/Models` and controlled via `efpt.config.json` files. The codebase currently stores scaffolded context with a generated name — be careful when referencing its type (search the Models folder for the actual DbContext class name).

Guidance for AI edits
- Prefer minimal, targeted changes: update components inside `ARP.ONE.BlazorApp.Client/Pages` or `Components` and services under `Services` rather than moving project boundaries.
- When adding DI registrations, update the appropriate `Program.cs` (server vs client) depending on runtime: server-only services go in `ARP.ONE.BlazorApp`, browser services go in `ARP.ONE.BlazorApp.Client`.
- When touching data models, do not edit files under `ARP.ONE.Generation/Models` unless you're intentionally committing generated outputs. Instead, update generation config and regenerate.

Helpful examples
- To add a UI library to the client, run: `dotnet add ARP.ONE.BlazorApp/ARP.ONE.BlazorApp.Client/ARP.ONE.BlazorApp.Client.csproj package <PackageName>` and add `builder.Services.Add...` in the client Program.cs.
- To find where a service is registered, search for `AddTransient|AddScoped|AddSingleton|AddMudServices` across the solution.

What I couldn't infer
- No test projects were found in the repository root; if unit/integration tests exist elsewhere, point the agent to them.
- The EF model generation tool invocation is not included; regeneration commands are environment-dependent. Check local dev tooling for EFPowerTools/Scaffold usage.

If anything above looks incomplete or you want examples added (e.g., PR checklist, commit message style, or test commands), tell me which sections to expand.
