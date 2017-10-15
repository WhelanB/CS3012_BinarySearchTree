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

        public BinaryTree(Tkey key, Tvalue val)
        {
            root = new Node(key, val);
        }

        public override String ToString()
        {
            if (root == null) return "()";
            return "(" + PrintKeys(root) + ")";
        }

        private String PrintKeys(Node node)
        {
            if (node != null)
            {
                return "(" + PrintKeys(node.left) + ")" + node.key + "(" + PrintKeys(node.right) + ")";
            }
            return "";

        }
        public bool contains(Tkey key)
        {
            Tvalue res;
            return get(key, out res);
        }

        public void insert(Tkey key, Tvalue val)
        {
            if (val == null) { delete(key); return; }
            root = insert(root, key, val);
        }

        private Node insert(Node x, Tkey key, Tvalue val)
        {
            if (x == null) return new Node(key, val);
            int cmp = key.CompareTo(x.key);
            if (cmp < 0) x.left = insert(x.left, key, val);
            else if (cmp > 0) x.right = insert(x.right, key, val);
            else x.value = val;
            return x;
        }

        public Boolean get(Tkey key, out Tvalue result)
        {
            return get(root, key, out result);   
        }

        private Boolean get(Node x, Tkey key, out Tvalue result)
        {
            if (x == null)
            {
                result = default(Tvalue);
                return false;
            }
            int cmp = key.CompareTo(x.key);
            if (cmp < 0)
            {
                return get(x.left, key, out result);
            }
            else if (cmp > 0)
            {
                return get(x.right, key, out result);
            }
            else
            {
                result = x.value;
                return true;
            }
        }
        public Boolean delete(Tkey key)
        {
            Tvalue result;
            if (get(key, out result)!=false)
            {
               return delete(root, root, key);
            }
            return false;
        }

        private Boolean delete(Node parent, Node n, Tkey key)
        {
            if (parent == n && parent.left == null && parent.right == null)
            {
                root = null;
                return true;
            }
            int cmp = key.CompareTo(n.key);
            if (cmp < 0) { delete(n, n.left, key); }
            if (cmp > 0) { delete(n, n.right, key); }
            else if (cmp == 0)
            {

                if (n.left == null && n.right == null)
                {
                    if (parent.left != null && parent.left == n)
                    {
                        parent.left = null;
                        return true;
                    }
                    else if (parent.right != null && parent.right == n)
                    {
                        parent.right = null;
                        return true;
                    }
                }

                if ((n.left != null) != (n.right != null))
                {
                    Node b = (n.left == null) ? n.right : n.left;
                    if (parent.left != null && parent.left == n)
                    {
                        parent.left = b;
                        return true;
                    }
                    else if (parent.right != null && parent.right == n)
                    {
                        parent.right = b;
                        return true;
                    }
                }

                if ((n.left != null) && (n.right != null))
                {
                    Node pred = n.left;
                    while (pred.right != null)
                    {
                        pred = pred.right;
                    }
                }
            }
            return false;
        }

        public Boolean lowestCommonAncestor(Tkey a, Tkey b, out Tkey result)
        {
            if (!(contains(a) && contains(b)))
            {
                result = default(Tkey);
                return false;
            }
            else
            {
                return lowestCommonAncestor(root, a, b, out result);
            }
        }

        private Boolean lowestCommonAncestor(Node root, Tkey a, Tkey b, out Tkey result)
        {
            if (root == null)
            {
                result = default(Tkey);
                return false;
            }
            if (root.key.CompareTo(a) < 0 && root.key.CompareTo(b) < 0)
            {
                return lowestCommonAncestor(root.left, a, b, out result);
            }
            if (root.key.CompareTo(a) < 0 && root.key.CompareTo(b) < 0)
            {
                return lowestCommonAncestor(root.right, a, b, out result);
            }
            result = root.key;
            return true;

        }
        private class Node
        {
            public Tkey key;           // sorted by key
            public Tvalue value;         // associated data
            public Node left, right;  // left and right subtrees

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