using PupaLib.Node.Nodes.Blocks;

namespace PupaLib.Node.Nodes;

public class SimpleNode<T>(INodeBlock parentBlock) : INode {
   public INodeBlock ParentBlock { get; } = parentBlock;
   public SimpleNode<T>? Parent { get; private set; }
   public T? Value { get; private set; }
   public bool HasValue { get; private set; }

   public void Reset() {
      Value = default;
      HasValue = false;
   }

   public bool IsConnected => Parent != null;
   public bool IsEmpty => Value == null;

   public void SetParent(SimpleNode<T>? parent) {
      Parent = parent;
   }

   public void SetValue(T value) {
      HasValue = true;
      Value = value;
   }

   public void Impulse() {
      ParentBlock.Impulse();
   }

   public T GetValue() {
      if (IsConnected) return Parent!.GetValue();


      if (HasValue == false || IsEmpty) Impulse();

      return Value!;
   }
}