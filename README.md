# VR-Escape-Room
# 🔧 VR Escape Room: The Engineering Deck

> **"Warning. Core containment failing. Manual override required."**

Welcome to **The Engineering Deck**, a fully interactive, 3-floor Virtual Reality escape room built in Unity! Players are trapped in a failing sci-fi facility and must physically interact with their environment, solve logic puzzles, and route power back to the ship before the reactor reaches critical mass.

Will you stabilize the core, or go down with the ship?

## ✨ Features & Gameplay
This project leverages the **XR Interaction Toolkit** to create satisfying, physics-based VR interactions. There are no laser-pointers here—you must use your hands to grab, pull, plug, and assemble your way to victory across three distinct levels.

### 🏢 Floor 1: System Boot
* **The Keypad:** A fully functional, physical VR keypad requiring a hidden passcode to initialize the deck's systems.
* **The Override Lever:** A physics-based hinge joint lever to manually crank the power grid online.

### ⚡ Floor 2: Power Routing
* **Simon Says Sequence:** A short-term memory puzzle featuring a 4-color glowing button matrix.
* **The Fuse Box:** A strict shape-and-color matching puzzle. Players must find scattered Red, Blue, and Green capsule fuses and slot them into their exact corresponding sockets to bypass the blown circuits.

### ☢️ Floor 3: The Reactor Core
* **The Greek Matrix:** The grand finale. A 4-slot permutation puzzle using glowing `Alpha`, `Beta`, `Gamma`, and `Delta` fuel blocks. Every block fits into every socket, preventing brute-force guessing. Players must rely on environmental clues to deduce the exact sequence (`Gamma ➔ Delta ➔ Alpha ➔ Beta`).
* **The Master Switch:** Solving the core reveals a hidden spotlight and the final Emergency Stop button, which freezes all lower-deck systems and triggers the victory sequence.

## 🛠️ Tech Stack & Mechanics
This game was built using **Unity 3D** and the **XR Interaction Toolkit (XRIT)**. Key technical achievements include:
* **Custom Interaction Layer Masks:** Used in the Floor 2 Fuse Puzzle to strictly enforce which objects can enter which `XRSocketInteractors`, preventing player errors.
* **Attach Transforms:** Custom rotation and positional anchors to ensure items (like the Greek Blocks) snap perfectly upright into sockets for readability.
* **Global State Management:** A master script (`ReactorCoreManager.cs`) that continuously validates socket contents and communicates with prior puzzle scripts to freeze their states upon game completion.
* **Material Swapping & Emission:** Dynamic glowing neon textures that respond to player success states.

## 🚀 How to Play
1. Download the latest `EngineeringDeck_Final.apk` from the Releases tab.
2. Connect your Meta Quest (or compatible Android-based standalone VR headset) to your PC.
3. Use [SideQuest](https://sidequestvr.com/) or the Meta Quest Developer Hub to sideload the `.apk` onto your headset.
4. Put on your headset, navigate to **Unknown Sources**, and launch the game.
5. Save the ship!

---
*Developed as a deep-dive into VR puzzle logic and XR Interaction Toolkit mechanics.*
