using System.Numerics;

using PupaLib.Node.Nodes;
using PupaLib.Node.Nodes.Attributes;
using PupaLib.Node.Nodes.Blocks;

namespace PupaLib.Node.Tests.Blocks;

public sealed class NodeDataVector2Block : NodeDataBlock<Vector2> {
   [NodeOutput("x")] public SimpleNode<float> XNode { get; private set; } = null!;

   [NodeOutput("y")] public SimpleNode<float> YNode { get; private set; } = null!;

   public override void Init() {
      XNode = new SimpleNode<float>(this);
      YNode = new SimpleNode<float>(this);
      base.Init();
   }

   public override void Impulse() {
      ThrowIfValueNull();
      XNode.SetValue(Value.X);
      YNode.SetValue(Value.Y);
   }
}