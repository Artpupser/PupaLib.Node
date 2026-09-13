namespace PupaLib.Node.Nodes.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public abstract class NodeAttribute(string id) : Attribute {
   public string Id { get; } = id;
}