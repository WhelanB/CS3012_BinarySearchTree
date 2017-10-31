using NSpec;
using FluentAssertions;
using SoftwareEngineeringBST;
using System.Collections.Generic;

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
                graph.ToString().Should().Match("0: 3\n3: 4\n");
                graph.RemoveEdge(4, 5).ShouldBeEquivalentTo(false);
                graph.ToString().Should().Match("0: 3\n3: 4\n");
            };
        };
        context["When the edge is presnet in the graph"] = () =>
        {
            it["should remove the edge from the graph"] = () =>
            {
                graph.AddEdge(0, 3).ShouldBeEquivalentTo(true);
                graph.AddEdge(3, 4).ShouldBeEquivalentTo(true);
                graph.ToString().Should().Match("0: 3\n3: 4\n");
                graph.RemoveEdge(3, 4).ShouldBeEquivalentTo(true);
                graph.ToString().Should().Match("0: 3\n");
            };
        };
    }

    void describe_LCA()
    {
        HashSet<int> result;
        context["When there are no edges in the graph"] = () =>
        {
            it["should return false and out an empty collection"] = () =>
            {
                graph.LCA(0, 1, out result).ShouldBeEquivalentTo(false);
                result.Count.ShouldBeEquivalentTo(0);
            };
        };
        context["When there are edges in the graph"] = () =>
        {
            context["When either of the nodes supplied are not present in the graph"] = () =>
            {
                it["should return false and out an empty collection"] = () =>
                {
                    graph.LCA(100, 100, out result).ShouldBeEquivalentTo(false);
                    result.Count.ShouldBeEquivalentTo(0);
                };
            };

            context["When one of the nodes is a direct common ancestor of the other"] = () =>
            {
                it["should return said node as one of the lowest common ancestors"] = () =>
                {
                    graph.AddEdge(0, 1);
                    graph.LCA(0, 1, out result).ShouldBeEquivalentTo(true);
                    result.Contains(0).ShouldBeEquivalentTo(true);
                };
            };

            context["In all other cases"] = () =>
            {
                it["should return true and out a HashSet containing the lowest common ancestors"] = () =>
                {
                    graph.AddEdge(0, 3);
                    graph.AddEdge(1, 3);
                    graph.AddEdge(1, 4);
                    graph.AddEdge(2, 5);
                    graph.AddEdge(2, 6);
                    graph.AddEdge(3, 5);
                    graph.AddEdge(3, 6);
                    graph.AddEdge(4, 6);
                    graph.LCA(5, 6, out result).ShouldBeEquivalentTo(true);
                    result.Contains(2).ShouldBeEquivalentTo(true);
                    result.Contains(3).ShouldBeEquivalentTo(true);
                    

                };
            };

        };
    }

    void describe_AddEdge()
    {
        context["When the graph has no edges"] = () =>
        {
            it["add an edge between the two supplied nodes"] = () =>
            {
                graph.ToString().Should().Match("");
                graph.AddEdge(0, 1).ShouldBeEquivalentTo(true);
                graph.ToString().Should().Match("0: 1\n");
            };
        };
        context["When edges are present in the graph"] = () =>
        {
            context["if the edge will not create a cycle"] = () =>
            {
                it["should return true and add the edge to the graph"] = () =>
                {
                    graph.ToString().Should().Match("");
                    graph.AddEdge(0, 3).ShouldBeEquivalentTo(true);
                    graph.ToString().Should().Match("0: 3\n");
                    graph.AddEdge(3, 4).ShouldBeEquivalentTo(true);
                    graph.ToString().Should().Match("0: 3\n3: 4\n");

                };
            };
            context["if the edge will create a cycle"] = () =>
            {
                it["should return false and not add the edge to the graph"] = () =>
                {
                    graph.AddEdge(0, 3).ShouldBeEquivalentTo(true);
                    graph.AddEdge(3, 4).ShouldBeEquivalentTo(true);
                    graph.AddEdge(4, 5).ShouldBeEquivalentTo(true);
                    graph.ToString().Should().Match("0: 3\n3: 4\n4: 5\n");
                    graph.AddEdge(5, 0).ShouldBeEquivalentTo(false);
                    graph.ToString().Should().Match("0: 3\n3: 4\n4: 5\n");
                };
            };
            
        };
        context["If the node is not present in the graph"] = () =>
        {
            it["should return false"] = () =>
            {
                graph.AddEdge(0, 1000).ShouldBeEquivalentTo(false);
                graph.AddEdge(-1, 0).ShouldBeEquivalentTo(false);
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