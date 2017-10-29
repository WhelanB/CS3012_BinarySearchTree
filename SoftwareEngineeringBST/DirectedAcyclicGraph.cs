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
        private int[] marked;
        public DirectedAcyclicGraph(int V)
        {
            adj = new HashSet<int>[V];
            marked = new int[V];
            for (int v = 0; v < V; v++)
            {
                adj[v] = new HashSet<int>();
            }
        }

        public bool AddEdge(int v, int w)
        {
            if (v < 0 || v > adj.Length || w < 0 || w > adj.Length)
                return false;
            try
            {
                adj[v].Add(w);
                for (int i = 0; i < adj.Count(); i++)
                    marked[i] = 0;
                for (int n = 0; n < adj.Count(); n++)
                    if (marked[n] == 0)
                        DfsVisit(n);
            }
            catch (Exception e)
            {
                adj[v].Remove(w);
                return false;
            }
            return true;

        }

        public Boolean RemoveEdge(int v, int w)
        {
            if (adj[v].Contains(w))
            {
                adj[v].Remove(w);
                return true;
            }
            return false;
        }

        private void DfsVisit(int n)
        {
            if (marked[n] == 2)
                return;
            if (marked[n] == 1)
                throw new Exception("This graph contains a cycle, and as a result is not a directed acyclic graph");
            marked[n] = 1;
            foreach (int i in adj[n])
                DfsVisit(i);
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
