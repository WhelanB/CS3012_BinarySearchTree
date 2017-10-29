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
        public DirectedAcyclicGraph(int V)
        {
            adj = new HashSet<int>[V];
         
            for (int v = 0; v < V; v++)
            {
                adj[v] = new HashSet<int>();
            }
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
