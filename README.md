# 🎮 Tic-Tac-Toe Game – WinForms (.NET Framework)

A fully functional desktop implementation of the classic Tic-Tac-Toe game built using **C# and Windows Forms (.NET Framework 4.8)**.

This project focuses on structured game-state management, clean event-driven design, and an index-based winner detection algorithm that separates UI representation from logical board evaluation.

---

## 🧠 Architecture & Game Logic Design

### 1️⃣ Strong State Modeling Using Enums

The game flow is controlled using clearly defined enums:

```csharp
enum enPlayers { Player1, Player2 };
enum enWinner { Player1 = 1, Player2, Draw, None };
```

This removes magic numbers and creates readable, maintainable game transitions.

---

### 2️⃣ Board Abstraction (UI → Logical Mapping)

Instead of checking buttons directly for wins, the board is converted into a structured integer array:

```csharp
int[] board = new int[9];
```

Each button's `Tag` is mapped to:

- 1 → Player 1 (X)
- 2 → Player 2 (O)
- 0 → Empty cell

This approach separates presentation from logic.

The array is then reversed to align logical indexing with UI layout orientation.

---

### 3️⃣ Index-Based Winner Detection (Core Innovation)

Winner detection is handled by:

```csharp
private (bool isWinner, int[] winIndexs) check_number_winner(int number, int[] board)
```

Instead of nested UI condition checks, the algorithm:

• Evaluates all 8 possible win combinations  
• Works purely on numeric board state  
• Returns both win status and the exact winning indexes  

This enables:

- Clean separation of logic
- Accurate win detection
- Dynamic UI highlighting
- Reusable validation structure

---

### 4️⃣ Dynamic Winning Highlight

Winning cells are highlighted using the returned winning indexes:

```csharp
ctrl.BackColor = Color.FromArgb(255, 135, 208, 135);
```

No button references are hardcoded — highlighting is driven purely by logical index results.

---

### 5️⃣ Custom Grid Rendering with GDI+

Instead of using layout controls for borders, grid lines are manually drawn inside the `Paint` event:

```csharp
e.Graphics.DrawLine(...)
```

This demonstrates:

- Understanding of GDI+ drawing
- Control over UI rendering
- Custom visual precision

---

## 🎨 UI & Design Decisions

• Transparent flat buttons for smooth UX  
• Resource-based images (X, O, Question state)  
• Dynamic turn indicator  
• Dynamic winner display  
• Restart system that resets:
  - Player state
  - UI elements
  - Button tags
  - Board state
• Double-buffered form to reduce flickering  
• Visual hierarchy using custom fonts and color contrast  

---

## 🔁 Game Flow

1. Player 1 starts.
2. On button click:
   - Symbol is applied.
   - Winner check executes.
   - If no winner → turn switches.
3. On win:
   - Winning cells are highlighted.
   - Game disables interaction.
   - Winner message appears.
4. On draw:
   - Game locks.
   - Draw message appears.
5. Restart fully resets the game state.

---

## 🛠 Technologies Used

- C#
- Windows Forms
- .NET Framework 4.8
- GDI+ (System.Drawing)
- Enum-based state modeling
- Index-driven win detection algorithm

---

## 🚀 How to Run

1. Open the solution in Visual Studio.
2. Ensure target framework is .NET Framework 4.8.
3. Build and run the project.

---

## 📌 Engineering Highlights

This project demonstrates:

✔ Logical abstraction from UI  
✔ Index-based board evaluation  
✔ Event-driven programming  
✔ Structured win validation  
✔ Clean state transitions  
✔ Custom rendering inside WinForms  

This is not just a visual game implementation — it reflects structured algorithmic thinking within a desktop application architecture.

---

## 👨‍💻 Author

Hamza Nasr