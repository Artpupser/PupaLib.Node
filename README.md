<div align="center">

# 🧩 PupaLib.Node

![PupaLib.Data](https://img.shields.io/badge/PupaLib.Data-black?style=for-the-badge&logo=PupaLib.Data&logoColor=white)
![License](https://img.shields.io/badge/MIT-black?style=for-the-badge)
![Dotnet](https://img.shields.io/badge/.NET-black?style=for-the-badge&logo=dotnet&logoColor=white)
![Nuget](https://img.shields.io/badge/NuGet-black?style=for-the-badge&logo=nuget&logoColor=white)
![Github]( https://img.shields.io/badge/GitHub-black?style=for-the-badge&logo=github&logoColor=white)
![License](https://img.shields.io/badge/MIT-black?style=for-the-badge)
![C#](https://img.shields.io/badge/C%23-black.svg?style=for-the-badge&logo=csharp&logoColor=white)


![NuGet](https://img.shields.io/nuget/v/PupaLib.FileIO.svg?style=for-the-badge)
![.NET](https://img.shields.io/badge/.NET-10.0-blue?style=for-the-badge)

<!-- ![.NET](https://img.shields.io/badge/.NET-10.0-blue?style=for-the-badge) -->
<!-- ![.Version](https://img.shields.io/github/v/release/Artpupser/PupaLib.Node?style=for-the-badge) -->


#### [PupaLib.Node](https://github.com/Artpupser/PupaLib.Node) is a lightweight node-based execution system for building modular data flows and logic graphs. 🎯

<img src="https://github.com/Artpupser/PupaLib.Node/blob/main/assets/banner.jpg" style="border-radius: 20px; max-height: 500px">

</div>

---
## 📎 Navigation

- [✨ Features](#-features)
- [🧵 Usage](#-usage)
- [👀 Preview](#-usage)
- [📦 Dependencies](#-dependencies)
- [🗃️ Devlog](#devlog)
- [⚖️️ License](#-license)


## ✨️ Features

<div align="center">

| 🏆 Feature                  | 📝 Description                                                  |
| --------------------------- | --------------------------------------------------------------- |
| **Node-based Architecture** | Build logic using connected nodes and blocks                    |
| **Typed Data Flow**         | Strongly-typed connections via `SimpleNode<T>`                  |
| **Reflection-driven Setup** | Auto-detect inputs/outputs via attributes                       |
| **Flexible Blocks**         | Supports `Data`, `Action`, and `Static` blocks                  |
| **Connection System**       | Connect nodes by id or reference                                |
| **Lazy Execution**          | Values are computed on demand (`GetValue()` triggers execution) |
| **Bundle System**           | Group blocks into `NodeBundle` for easier lifecycle management  |
| **Builder Pattern**         | Fluent API for chaining (`OnData`, `OnConnect`, `OnImpulse`)    |

</div>

## 🚀 Installation

You can install the package via NuGet:

```bash
dotnet add package PupaLib.Node
```

or

```bash
Install-Package PupaLib.Node
```

## 🧵 Usage

### 📌 Define a custom block

```csharp
public class AddBlock : NodeDataBlock<int>
{
    [NodeInput("a")] public SimpleNode<int> A { get; set; } = new(null!);
    [NodeInput("b")] public SimpleNode<int> B { get; set; } = new(null!);
    
    [NodeOutput("result")] public SimpleNode<int> Result { get; set; } = new(null!);

    public override void Impulse()
    {
        ThrowIfValueNull();
        var result = A.GetValue() + B.GetValue();
        Result.SetValue(result);
    }
}
```

---

### 📌 Create and connect nodes

```csharp
var bundle = new NodeBundle();

var add = bundle.AddBlock(new AddBlock());
var inputA = bundle.AddBlock(new SomeInputBlock(5));
var inputB = bundle.AddBlock(new SomeInputBlock(10));

bundle.Init();

// Connect inputs to add block
inputA.OnConnect<int>(add.A, "result");
inputB.OnConnect<int>(add.B, "result");

// Execute
add.Impulse();

var result = add.Result.GetValue();
Console.WriteLine(result); // 15
```

---

### 📌 Using builder pattern

```csharp
add
    .OnData(0)
    .OnConnect<int>(add.A, "a")
    .OnConnect<int>(add.B, "b");
```

---

### 📌 Working with NodeBundle

```csharp
bundle.Init();   // Initialize all blocks
bundle.Reset();  // Reset all outputs
```

---

### 📌 Lazy execution

```csharp
var value = add.Result.GetValue(); // triggers Impulse automatically if needed
```

## 📦 Dependencies

None

## 🗃️ Devlog

### 0.0.3

* Initial implementation of node system
* Added `NodeDataBlock`, `NodeActionBlock`, `NodeStaticBlock`
* Reflection-based input/output binding
* Introduced `SimpleNode<T>` for value propagation
* Added `NodeBundle` for grouping and lifecycle control


## ⚖️ License

This project is licensed under the **MIT License**.
