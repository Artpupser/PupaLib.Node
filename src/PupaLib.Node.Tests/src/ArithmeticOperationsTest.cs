using System.Numerics;

using PupaLib.Node.Nodes.Bundles;
using PupaLib.Node.Tests.Blocks;

using Xunit.Abstractions;

namespace PupaLib.Node.Tests;

[CollectionDefinition("Arithmetic operations test", DisableParallelization = false)]
public sealed class ArithmeticOperationsTest(ITestOutputHelper testOutputHelper) {
   private readonly ITestOutputHelper _testOutputHelper = testOutputHelper;

   [Theory(DisplayName = "Test A + B")]
   [InlineData(3, 5)]
   [InlineData(35, 51)]
   [InlineData(-14, 5)]
   [InlineData(24, -5)]
   [InlineData(0, 0)]
   public void NodeSum(int a, int b) {
      var bundle = new NodeBundle();
      var vector2Block = bundle.AddBlock(new NodeDataVector2Block());
      var operationBlock = bundle.AddBlock(new NodeActionFuncABBlock((x, y) => x + y));
      var flBlock = bundle.AddBlock(new NodeStaticFloatBlock());
      bundle.Init();

      vector2Block
         .OnData(new Vector2(a, b))
         .OnConnect<float>(operationBlock.XNode, "x")
         .OnConnect<float>(operationBlock.YNode, "y");
      operationBlock
         .OnConnect<float>(flBlock.ContentNode, "result");
      var result = flBlock
         .OnImpulse()
         .OnResult<int>();
      Assert.True(result == a + b);
   }
   
   [Theory(DisplayName = "Test A - B")]
   [InlineData(3, 5)]
   [InlineData(35, 51)]
   [InlineData(-14, 5)]
   [InlineData(24, -5)]
   [InlineData(0, 0)]
   public void NodeSubstraction(int a, int b) {
      var bundle = new NodeBundle();
      var vector2Block = bundle.AddBlock(new NodeDataVector2Block());
      var operationBlock = bundle.AddBlock(new NodeActionFuncABBlock((x, y) => x - y));
      var flBlock = bundle.AddBlock(new NodeStaticFloatBlock());
      bundle.Init();

      vector2Block
         .OnData(new Vector2(a, b))
         .OnConnect<float>(operationBlock.XNode, "x")
         .OnConnect<float>(operationBlock.YNode, "y");
      operationBlock
         .OnConnect<float>(flBlock.ContentNode, "result");
      var result = flBlock
         .OnImpulse()
         .OnResult<int>();
      Assert.True(result == a - b);
   }
   
   [Theory(DisplayName = "Test A * B")]
   [InlineData(3, 5)]
   [InlineData(35, 51)]
   [InlineData(-14, 5)]
   [InlineData(24, -5)]
   [InlineData(0, 0)]
   public void NodeMultiply(int a, int b) {
      var bundle = new NodeBundle();
      var vector2Block = bundle.AddBlock(new NodeDataVector2Block());
      var operationBlock = bundle.AddBlock(new NodeActionFuncABBlock((x, y) => x * y));
      var flBlock = bundle.AddBlock(new NodeStaticFloatBlock());
      bundle.Init();

      vector2Block
         .OnData(new Vector2(a, b))
         .OnConnect<float>(operationBlock.XNode, "x")
         .OnConnect<float>(operationBlock.YNode, "y");
      operationBlock
         .OnConnect<float>(flBlock.ContentNode, "result");
      var result = flBlock
         .OnImpulse()
         .OnResult<int>();
      Assert.True(result == a * b);
   }
   
   [Theory(DisplayName = "Test A / B")]
   [InlineData(3, 5)]
   [InlineData(35, 51)]
   [InlineData(-14, 5)]
   [InlineData(24, -5)]
   [InlineData(0, 0)]
   [InlineData(5, 0)]
   [InlineData(0, 5)]
   public void NodeDivide(int a, int b) {
      try {
         var bundle = new NodeBundle();
         var vector2Block = bundle.AddBlock(new NodeDataVector2Block());
         var operationBlock = bundle.AddBlock(new NodeActionFuncABBlock((x, y) => x / y));
         var flBlock = bundle.AddBlock(new NodeStaticFloatBlock());
         bundle.Init();

         vector2Block
            .OnData(new Vector2(a, b))
            .OnConnect<float>(operationBlock.XNode, "x")
            .OnConnect<float>(operationBlock.YNode, "y");
         operationBlock
            .OnConnect<float>(flBlock.ContentNode, "result");
         var result = flBlock
            .OnImpulse()
            .OnResult<float>();
         Assert.True(Math.Abs(result - a / b) < 1f);
      } catch (DivideByZeroException) {
         Assert.True(true);
      }
   }
}