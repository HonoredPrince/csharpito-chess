# Simple chess game developed in C# for playing on the command line
---
- _Install dotnet SDK 5.0 and run "dotnet run" on the Program.cs directory to start the game_

## Application Layers Structure
1. Application Layer --> Console/Command Line
2. Game Logic Layer
3. Board Layer

## Code Folder Structure
1. ChessBoard => Code related to Board components
2. Match => Stores and administrate all information of running match
3. Pieces => Derivates from the generic Piece component to all chess pieces types
4. View => Console Renderer and Command Line prompts for showing data and handling inputs
