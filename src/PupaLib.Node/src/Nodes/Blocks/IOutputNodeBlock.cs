using PupaLib.Node.Nodes.Attributes;

namespace PupaLib.Node.Nodes.Blocks;

public interface IOutputNodeBlock : INodeBlock {
   public void Reset();

   public bool ConnectTo<O>(INode inputNode, NodeOutputAttribute attribute) {
      return ConnectTo<O>(inputNode, attribute.Id);
   }

   public bool ConnectTo<O>(INode inputNode, string id);
   public bool ConnectTo<O>(INode inputNode, INode outputNode);
}