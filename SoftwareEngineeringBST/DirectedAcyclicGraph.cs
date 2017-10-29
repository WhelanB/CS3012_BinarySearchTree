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
    }
}
