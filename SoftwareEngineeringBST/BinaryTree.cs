using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoftwareEngineeringBST
{
    public class BinaryTree<Tkey, Tvalue> where Tkey : IComparable
    {
        private Node root;

        public BinaryTree()
        {
            root = null;
        }

        private class Node
        {
            private Tkey key;           // sorted by key
            private Tvalue value;         // associated data
            private Node left, right;  // left and right subtrees

            public Node(Tkey key, Tvalue val)
            {
                this.key = key;
                value = val;
            }

            public Tvalue GetData()
            {
                return value;
            }

            public override string ToString()
            {
                return String.Format("Key: %s, Value: %s", key.ToString(), value.ToString());
            }
        }
    }
}