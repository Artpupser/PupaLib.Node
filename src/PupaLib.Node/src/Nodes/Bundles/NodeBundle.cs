using PupaLib.Node.Nodes.Blocks;
using PupaLib.Node.Static;

namespace PupaLib.Node.Nodes.Bundles;

public class NodeBundle {
   public List<INodeBlock> Blocks { get; } = [];

   public IEnumerable<NodeStaticBlock> GetStaticBlocks() {
      return Blocks.OfType<NodeStaticBlock>();
   }

   public IEnumerable<NodeDataBlock<T>> GetStaticBlocks<T>() {
      return Blocks.OfType<NodeDataBlock<T>>();
   }

   public IEnumerable<NodeActionBlock> GetActionBlocks() {
      return Blocks.OfType<NodeActionBlock>();
   }

   public T AddBlock<T>(T block) where T : INodeBlock {
      Blocks.Add(block);
      return block;
   }

   public void Reset() {
      Blocks.OfType<IOutputNodeBlock>().ResetAll();
   }

   public void Init() {
      Blocks.InitAll();
   }
}