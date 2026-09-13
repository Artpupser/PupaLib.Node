using System.Collections.Frozen;
using System.Reflection;

using PupaLib.Node.Nodes.Attributes;
using PupaLib.Node.Utils;

namespace PupaLib.Node.Nodes.Blocks;

public abstract class NodeStaticBlock : IInputNodeBlock, INodeStaticBlock {
   protected FrozenDictionary<string, INode> _inputNodes = null!;
   public object? Result { get; protected set; } = null!;
   public bool IsResults => ResultType != typeof(void);
   public bool IsEmpty => IsResults && Result != null;
   public virtual Type ResultType { get; } = typeof(void);

   public abstract void Impulse();

   public virtual void Init() {
      _inputNodes = ReflectionUtils.GetFilteredByAttributeProperties<NodeInputAttribute>(this)
         .ToFrozenDictionary(x => x.GetCustomAttribute<NodeInputAttribute>()!.Id, x => (INode)x.GetValue(this)!);
   }

   protected void ThrowIfAnyInputEmpty() {
      if (_inputNodes.Any(x => x.Value.IsConnected == false)) throw new Exception("Any inputs not connected");
   }


   #region Builder

   public NodeStaticBlock OnImpulse() {
      Impulse();
      return this;
   }

   public T OnResult<T>() {
      return (T)Convert.ChangeType(Result!, typeof(T));
   }

   public object? OnResult() {
      return Result;
   }

   #endregion
}