using PupaLib.Node.Nodes;
using PupaLib.Node.Nodes.Blocks;

namespace PupaLib.Node.Static;

public static class NodeBlockExtensions {
   public static void InitAll(this IEnumerable<INodeBlock> nodeBlocks) {
      foreach (var nodeBlock in nodeBlocks) nodeBlock.Init();
   }

   public static void ResetAll(this IEnumerable<IOutputNodeBlock> nodeBlocks) {
      foreach (var nodeBlock in nodeBlocks) nodeBlock.Reset();
   }

   public static bool ConnectFromInput<O>(this IInputNodeBlock inputNodeBlock, IOutputNodeBlock outputNodeBlock,
      INode inputNode, string outputId) {
      return outputNodeBlock.ConnectTo<O>(inputNode, outputId);
   }

   public static bool ConnectFromInput<O>(this IInputNodeBlock inputNodeBlock, IOutputNodeBlock outputNodeBlock,
      INode inputNode, INode outputNode) {
      return outputNodeBlock.ConnectTo<O>(inputNode, outputNode);
   }
}