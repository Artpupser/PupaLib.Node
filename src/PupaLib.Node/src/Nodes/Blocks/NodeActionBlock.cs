using System.Collections.Frozen;
using System.Reflection;

using PupaLib.Node.Nodes.Attributes;
using PupaLib.Node.Utils;

namespace PupaLib.Node.Nodes.Blocks;

public abstract class NodeActionBlock : IInputNodeBlock, IOutputNodeBlock {
   protected FrozenDictionary<string, INode> _inputNodes = null!;
   protected FrozenDictionary<string, INode> _outputNodes = null!;

   public abstract void Impulse();

   public virtual void Init() {
      _outputNodes = ReflectionUtils.GetFilteredByAttributeProperties<NodeOutputAttribute>(this)
         .ToFrozenDictionary(x => x.GetCustomAttribute<NodeOutputAttribute>()!.Id, x => (INode)x.GetValue(this)!);
      _inputNodes = ReflectionUtils.GetFilteredByAttributeProperties<NodeInputAttribute>(this)
         .ToFrozenDictionary(x => x.GetCustomAttribute<NodeInputAttribute>()!.Id, x => (INode)x.GetValue(this)!);
   }

   public void Reset() {
      foreach (var valuePair in _outputNodes) valuePair.Value.Reset();
      foreach (var valuePair in _inputNodes) valuePair.Value.Reset();
   }

   public bool ConnectTo<O>(INode inputNode, string id) {
      if (_outputNodes.TryGetValue(id, out var outputNode) && inputNode is SimpleNode<O> inputSimpleNode &&
          outputNode is SimpleNode<O> outputSimpleNode) {
         inputSimpleNode.SetParent(outputSimpleNode);
         return true;
      }

      return false;
   }

   public bool ConnectTo<O>(INode inputNode, INode outputNode) {
      if (_outputNodes.Values.Any(x => x == outputNode) && inputNode is SimpleNode<O> inputSimpleNode &&
          outputNode is SimpleNode<O> outputSimpleNode) {
         inputSimpleNode.SetParent(outputSimpleNode);
         return true;
      }

      return false;
   }

   protected void ThrowIfAnyInputEmpty() {
      if (_inputNodes.Any(x => x.Value.IsConnected == false)) throw new Exception("Any inputs not connected");
   }

   #region Builder

   public NodeActionBlock OnConnect<O>(INode inputNode, string id) {
      ConnectTo<O>(inputNode, id);
      return this;
   }

   public NodeActionBlock OnConnect<O>(INode inputNode, INode outputNode) {
      ConnectTo<O>(inputNode, outputNode);
      return this;
   }

   #endregion
}