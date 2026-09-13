using PupaLib.Node.Nodes;
using PupaLib.Node.Nodes.Attributes;
using PupaLib.Node.Nodes.Blocks;

namespace PupaLib.Node.Tests.Blocks;

public sealed class NodeActionFuncABBlock(Func<float, float, float> func) : NodeActionBlock {
   [NodeInput("x")] public SimpleNode<float> XNode { get; private set; } = null!;

   [NodeInput("y")] public SimpleNode<float> YNode { get; private set; } = null!;

   [NodeOutput("result")] public SimpleNode<float> ResultNode { get; private set; } = null!;

   public override void Init() {
      XNode = new SimpleNode<float>(this);
      YNode = new SimpleNode<float>(this);
      ResultNode = new SimpleNode<float>(this);
      base.Init();
   }

   public override void Impulse() {
      ResultNode.SetValue(func(XNode.GetValue(), YNode.GetValue()));
   }
}