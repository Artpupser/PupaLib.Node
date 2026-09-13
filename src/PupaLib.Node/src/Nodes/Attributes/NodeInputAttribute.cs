namespace PupaLib.Node.Nodes.Attributes;

[AttributeUsage(AttributeTargets.Property)]
public class NodeInputAttribute(string id) : NodeAttribute(id);