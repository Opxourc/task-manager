# Task Manager CLI

A small command-line task manager built with C# and .NET.

This was my first C# project, built primarily to get hands-on experience with the language by taking a small idea from implementation through to a functional application with persistent data.

## Features

- Create, view, complete, and delete tasks
- Interactive CLI menu
- JSON-based persistence
- Loads existing tasks on startup
- Saves changes to disk

## Tech Stack

- C#
- .NET
- `System.Text.Json`

## Data Persistence

Tasks are serialized to a local JSON file. The application loads the existing task data when launched and writes changes back to the file as actions are performed.

## Running

Requires the .NET SDK.

```bash
dotnet run
```

## Project Goals

The primary goal was learning through implementation rather than building a feature-complete task manager.

Areas explored in the project include:

- Classes and object-oriented design
- Collections
- Control flow and methods
- Console input/output
- File I/O
- JSON serialization/deserialization
- Basic state management
- Debugging and working with unfamiliar APIs

*Notably, the project doesn't make extensive use of LINQ. That wasn't a deliberate attempt to avoid it; the project was more about getting comfortable with the language and solving problems as they came up.*

## Status

Complete as a small learning project. Further changes would primarily be driven by experimenting with C# concepts rather than expanding it into a full-featured task manager.
