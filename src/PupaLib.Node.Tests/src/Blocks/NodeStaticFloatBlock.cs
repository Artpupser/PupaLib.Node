using PupaLib.Node.Nodes;
using PupaLib.Node.Nodes.Attributes;
using PupaLib.Node.Nodes.Blocks;

namespace PupaLib.Node.Tests.Blocks;

public sealed class NodeStaticFloatBlock : NodeStaticBlock {
   [NodeInput("content")] public SimpleNode<float> ContentNode { get; private set; } = null!;

   public override Type ResultType => typeof(float);

   public override void Init() {
      ContentNode = new SimpleNode<float>(this);
      base.Init();
   }

   public override void Impulse() {
      Result = ContentNode.GetValue();
   }
}