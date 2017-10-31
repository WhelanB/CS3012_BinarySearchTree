using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineeringBST
{
    public class DirectedAcyclicGraph
    {

        private HashSet<int>[] adj;
        private HashSet<int>[] rev;
        private int[] marked;
        private int[] colours;
        public DirectedAcyclicGraph(int V)
        {
            adj = new HashSet<int>[V];
            rev = new HashSet<int>[V];
            marked = new int[V];
            colours = new int[V];
            for (int v = 0; v < V; v++)
            {
                adj[v] = new HashSet<int>();
                rev[v] = new HashSet<int>();
            }
        }

        //Find LCA of x,y
        public bool LCA(int x, int y, out HashSet<int> result)
        {
            if (x < 0 || x > adj.Length || y < 0 || y > adj.Length)
            {
                result = new HashSet<int>();
                return false;
            }
            HashSet<int> lcas = new HashSet<int>();
            if (x > 0 && x < adj.Length && y > 0 && y < adj.Length)
            
                if (adj[x].Contains(y))
                {
                    lcas.Add(x);
                }
                if (adj[y].Contains(x))
                {
                    lcas.Add(y);
                }
                colourBlue(x);
                colourBlack(y);
                HashSet<int>[] a = new HashSet<int>[adj.Length];
                for (int i = 0; i < adj.Length; i++)
                {
                    a[i] = new HashSet<int>();
                    a[i].UnionWith(adj[i]);
                    if (a[i].Contains(x))
                        a[i].Remove(x);
                    if (a[i].Contains(y))
                        a[i].Remove(y);
                }
                
                lcas.UnionWith(this.FindLCAFromGraph(a, colours));
                result = lcas;
                if (result.Count == 0)
                    return false;
                else
                    return true;
        }

        //Finds and returns nodes in the graph that are coloured black and have an out degree of zero (LCAs) excluding edges to x/y
        private HashSet<int> FindLCAFromGraph(HashSet<int>[] adj, int[] colours)
        {
            HashSet<int> result = new HashSet<int>();
            for(int i = 0; i < adj.Length; i++)
            {
                if (colours[i] == 2 && adj[i].Count == 0)
                    result.Add(i);
            }
            return result;
        }

        //Colour all ancestors of x blue
        private void colourBlue(int x)
        {
            colours[x] = 1;
            foreach(int node in rev[x])
            {
                colourBlue(node);
            }
        }

        //Colour all blue ancestors of x black
        private void colourBlack(int x)
        {
            foreach (int node in rev[x])
            {
                if (colours[node] == 1)
                {
                    colours[node] = 2;
                    colourBlack(node);
                }
            }
        }

        //Add an edge between nodes v, w - will not be added if a cycle is created
        public bool AddEdge(int v, int w)
        {
            if (v < 0 || v > adj.Length || w < 0 || w > adj.Length)
                return false;
            try
            {
                adj[v].Add(w);
                rev[w].Add(v);
                for (int i = 0; i < adj.Count(); i++)
                    marked[i] = 0;
                for (int n = 0; n < adj.Count(); n++)
                    if (marked[n] == 0)
                        FindCycles(n);
            }
            catch (Exception e)
            {
                adj[v].Remove(w);
                rev[w].Remove(v);
                return false;
            }
            return true;

        }

        //Remove an edge between nodes v, w
        public Boolean RemoveEdge(int v, int w)
        {
            if (v < 0 || v > adj.Length || w < 0 || w > adj.Length)
                return false;
            if (adj[v].Contains(w))
            {
                adj[v].Remove(w);
                rev[w].Remove(v);
                return true;
            }
            return false;
        }

        //Find Cycles in the graph via DFS
        private void FindCycles(int n)
        {
            if (marked[n] == 2)
                return;
            if (marked[n] == 1)
                throw new Exception("This graph contains a cycle, and as a result is not a directed acyclic graph");
            marked[n] = 1;
            foreach (int i in adj[n])
                FindCycles(i);
            marked[n] = 2;
        }

        public override String ToString()
        {
            String result = "";
            for (int v = 0; v < adj.Length; v++)
            {
                if (adj[v].Count != 0)
                {
                    result += v + ": " + string.Join("|", adj[v]) + "\n";
                }
            }
            return result;
        }
    }
}
