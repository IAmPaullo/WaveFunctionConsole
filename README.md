
# Wave Function Collapse - Grid Simulation

This is a simple project implementing the **Wave Function Collapse (WFC)** algorithm using C#. It was designed as an educational exercise to explore how WFC works with basic features and some custom extensions like **state weights** and **directional rules**.

## 📖 About the Algorithm

Wave Function Collapse is an algorithm widely used for **procedural content generation**, such as game maps, visual patterns, and textures. The WFC operates based on these principles:
1. **Superposition of States:** Each cell begins with multiple possible states.
2. **Cell Collapse:** A cell is "collapsed" into a single state.
3. **Constraint Propagation:** After collapsing, constraints are propagated to neighboring cells, reducing their possible states.
4. **Adjacency Rules:** Determine which states can coexist between neighboring cells.

[Here's some more Info](https://en.wikipedia.org/wiki/Model_synthesis)

In this project, we use a grid where each cell represents different types of terrain:
- **`S`**: Sea
- **`L`**: Land
- **`C`**: Coast

## 🚀 Features

- **Configurable grid:** Customizable grid sizes (e.g., 3x3, 5x5, etc.).
- **Directional adjacency rules:** Different states are allowed based on their direction (`Up`, `Down`, `Left`, `Right`).
- **Weighted states:** Certain states are more or less likely to be chosen during collapse.
- **Console visualization:** The generated grid is displayed in the console.
- **Modular structure:** Code is structured for easy maintenance and extension.

### Configuration
- You can change the grid size in the `GridManager.cs` file:
  ```csharp
  var gridManager = new GridManager(5, 5); // Example: 5x5 grid
  ```
- Weights and adjacency rules can also be customized in the code:
  - **Weights:** Adjust the `_stateWeights` dictionary in `GridManager.cs` to modify the likelihood of each state.
  - **Directional rules:** Modify the rules in `DirectionalRules.cs`.
 
I'll implement better way to customize the grid eventually 🤣.

## 📂 Project Structure

```
WaveFunctionCollapse/
│
├── Program.cs                  # Entry point of the program
├── Grid/
│   ├── GridManager.cs          # Manages the grid and constraint propagation
│   ├── Cell.cs                 # Represents a single cell in the grid
│
├── Rules/
│   ├── DirectionalRules.cs     # Directional adjacency rules
│
├── Utils/
│   ├── NeighborHelper.cs       # Functions for calculating neighbors
│
└── WaveFunctionCollapse.csproj # Project configuration file
```

## 🖼️ Example Console Output

For a **5x5 grid** with adjusted weights, you might see output like this:

```plaintext
[S] [C] [C] [S] [S]
[C] [S] [L] [C] [S]
[L] [L] [C] [S] [C]
[C] [L] [C] [C] [L]
[S] [C] [L] [L] [C]
```

### Interpretation:
- **`S`**: Represents sea.
- **`C`**: Represents coast.
- **`L`**: Represents land.

## 📦 Future Extensions

Planned improvements include:
1. **Image export:** Save the grid as a PNG for visualization.
2. **Custom input:** Allow rules and states to be defined based on input (e.g., an image or map).
3. **Dynamic larger grids:** Optimize for larger grids, such as 50x50 or higher.
