using NSpec;
using FluentAssertions;
using SoftwareEngineeringBST;

class DirectedAcyclicTest : nspec
{
    DirectedAcyclicGraph graph;

    void before_each()
    {
        graph = new DirectedAcyclicGraph(10);
    }

    void describe_toString()
    {
        context["When an empty graph is supplied"] = () =>
        {
            it["should return the empty string"] = () =>
            {
                graph.ToString().ShouldAllBeEquivalentTo("");
            };
        };
        context["When a constructed tree is provided"] = () =>
        {
            it["should return the constructed tree as an inorder string"] = () =>
            {
                graph.AddEdge(0, 1);
                graph.AddEdge(0, 3);
                graph.AddEdge(1, 2);
                graph.AddEdge(3, 9);
                graph.ToString().Should().Match("0: 1|3\n1: 2\n3: 9\n");
            };
        };
    }


}