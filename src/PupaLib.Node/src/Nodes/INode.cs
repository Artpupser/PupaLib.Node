namespace PupaLib.Node.Nodes;

public interface INode {
   public bool IsConnected { get; }
   public bool IsEmpty { get; }
   public void Reset();
}