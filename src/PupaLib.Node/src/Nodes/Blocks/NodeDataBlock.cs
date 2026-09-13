using System.Collections.Frozen;
using System.Reflection;

using PupaLib.Node.Nodes.Attributes;
using PupaLib.Node.Utils;

namespace PupaLib.Node.Nodes.Blocks;

public abstract class NodeDataBlock<T> : IOutputNodeBlock, INodeDataBlock {
   protected FrozenDictionary<string, INode> _outputNodes = null!;
   public bool IsEmpty => Value == null;
   protected T? Value { get; private set; }

   public void Reset() {
      foreach (var outputNode in _outputNodes) outputNode.Value.Reset();
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

   public abstract void Impulse();

   public virtual void Init() {
      _outputNodes = ReflectionUtils.GetFilteredByAttributeProperties<NodeOutputAttribute>(this)
         .ToFrozenDictionary(x => x.GetCustomAttribute<NodeOutputAttribute>()!.Id, x => (INode)x.GetValue(this)!);
   }

   public virtual void SetData(T value) {
      Value = value;
   }


   protected void ThrowIfValueNull() {
      if (Value == null) throw new Exception("Bad update nodes, value is null");
   }

   #region Builder

   public NodeDataBlock<T> OnData(T value) {
      SetData(value);
      return this;
   }

   public NodeDataBlock<T> OnConnect<O>(INode inputNode, string id) {
      ConnectTo<O>(inputNode, id);
      return this;
   }


   public NodeDataBlock<T> OnConnect<O>(INode inputNode, INode outputNode) {
      ConnectTo<O>(inputNode, outputNode);
      return this;
   }

   #endregion
}