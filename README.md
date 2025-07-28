# Lights Out Puzzle Game 🔦

A minimalist yet challenging puzzle game built with **Unity**, featuring clean pixel-art backgrounds, performance optimizations, and a stats panel to track your progress.  
The goal: turn off all the lights by clicking on the tiles — each click toggles the selected tile and its neighbors.

---

## 🎮 Gameplay

- Click on any tile to toggle it and its adjacent tiles (up, down, left, right).
- Solve the puzzle by turning **all tiles off** (dark).
- Each game starts with a randomized board configuration.

---

## 📊 Features

- **Responsive UI** that adapts to screen sizes (including WebGL).
- **Randomized pixel-art backgrounds** on each session (loaded via Addressables).
- **Statistics panel**:
  - Total games played
  - Win rate (in %)
  - Average time to solve
- **Game Over panel** with restart option, layered correctly even in WebGL.
- **Cross-platform save system** using JSON file instead of PlayerPrefs for WebGL compatibility.

---

## 🌐 Play Online
Experience the web version of TileVania on [itch.io](https://noychen.itch.io/light-out) and start your adventure today!

---

## 🧩 Technical Overview

- **Frameworks used**:
  - [Zenject](https://github.com/modesttree/Zenject) for dependency injection
  - [UniTask](https://github.com/Cysharp/UniTask) for async operations
  - Unity Addressables for background asset management

- **Structure**:
  - `GamePlay`: Game logic, grid, tiles, and tile factory with object pooling
  - `UI`: Panels, transitions, and stat display
  - `Infrastructure`: Background manager, save system
  - `Data`: Serializable save data
  - `Events`: Global event dispatcher for game win / loss / button clicks

---

## 💾 Save System

All game statistics are saved to a JSON file:

- 📁 Location: `Application.persistentDataPath`
- 📄 File: `stats.json`

This ensures your stats are **preserved** across sessions, including on **WebGL builds** where `PlayerPrefs` can be unreliable.

---

## 📦 Build Info

- Target platforms: **WebGL**, **PC**, **Mobile**
- Optimized UI layout for WebGL to avoid input issues.
- Addressables are prebuilt for runtime loading of randomized backgrounds.

---

## 🖼️ Assets

- All backgrounds and UI icons (like stats, close, etc.) are generated and optimized for square layout and minimalistic aesthetics.
- Vector icons are used with transparency or solid background as needed.

---

## 🚀 How to Play (on Web or PC)

1. Press **Play**.
2. Click tiles to toggle them.
3. Solve the puzzle to win.
4. Click the **stats button** to view your performance.
5. Click the **restart** button to try a new random puzzle.

---
