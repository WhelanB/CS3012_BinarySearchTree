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

    void describe_RemoveEdge()
    {
        context["When the edge is not present in the graph"] = () =>
        {
            it["should return false and not modify the graph"] = () =>
            {
                graph.AddEdge(0, 3).ShouldBeEquivalentTo(true);
                graph.AddEdge(3, 4).ShouldBeEquivalentTo(true);
                graph.RemoveEdge(4, 5).ShouldBeEquivalentTo(false);
            };
        };
        context["When the edge is presnet in the graph"] = () =>
        {
            it["should remove the edge from the graph"] = () =>
            {
                graph.AddEdge(0, 3).ShouldBeEquivalentTo(true);
                graph.AddEdge(3, 4).ShouldBeEquivalentTo(true);
                graph.RemoveEdge(3, 4).ShouldBeEquivalentTo(true);
            };
        };
    }

    void describe_AddEdge()
    {
        context["When the graph has no edges"] = () =>
        {
            it["add an edge between the two supplied nodes"] = () =>
            {
                graph.AddEdge(0, 1).ShouldBeEquivalentTo(true);
            };
        };
        context["When edges are present in the graph"] = () =>
        {
            context["if the edge will not create a cycle"] = () =>
            {
                it["should return true and add the edge to the graph"] = () =>
                {
                    graph.AddEdge(0, 3).ShouldBeEquivalentTo(true);
                    graph.AddEdge(3, 4).ShouldBeEquivalentTo(true);

                };
            };
            context["if the edge will create a cycle"] = () =>
            {
                it["should return false and not add the edge to the graph"] = () =>
                {
                    graph.AddEdge(0, 3).ShouldBeEquivalentTo(true);
                    graph.AddEdge(3, 4).ShouldBeEquivalentTo(true);
                    graph.AddEdge(4, 5).ShouldBeEquivalentTo(true);
                    graph.AddEdge(5, 0).ShouldBeEquivalentTo(false);
                };
            };
            
        };
    }

    void describe_ToString()
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