namespace PupaLib.Node.Nodes.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class NodeOutputAttribute(string id) : NodeAttribute(id);