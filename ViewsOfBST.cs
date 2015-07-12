using System;

namespace ConsoleApplication24
{
    using System.Collections.Generic;

    class Node
    {
        public int Data { get; set; }

        public Node Left { get; set; }

        public Node Right { get; set; }
    }

    class BinaryTree
    {
        public readonly Node Root;

        public BinaryTree(int data)
        {
            this.Root = new Node() { Data = data, Left = null, Right = null };
        }

        public void AddNode(int data)
        {
            Node node = new Node() { Data = data, Left = null, Right = null };
            this.AddNode(node, this.Root);
        }

        public void AddNode(Node node, Node root)
        {
            while (true)
            {
                if (node.Data >= root.Data)
                {
                    if (root.Right == null)
                    {
                        root.Right = node;
                        return;
                    }
                    root = root.Right;
                }
                else
                {
                    if (root.Left == null)
                    {
                        root.Left = node;
                        return;
                    }
                    root = root.Left;
                }
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinaryTree binaryTree = new BinaryTree(3);
            binaryTree.AddNode(5);
            binaryTree.AddNode(2);
            binaryTree.AddNode(1);
            binaryTree.AddNode(4);
            binaryTree.AddNode(6);
            binaryTree.AddNode(7);
            binaryTree.AddNode(9);
            binaryTree.AddNode(8);
            
            Console.WriteLine(TopView(binaryTree.Root, 0, 0, 0, string.Empty));
           
            Left_view(binaryTree.Root);

            Console.WriteLine(" ");

            Bottom_View(binaryTree.Root);

            Console.Read();
        }

        static string TopView(Node root, int level, int right, int left, string a)
        {
            if (left == 1)
            {
                a = root.Data + " " + a;
                if (root.Left != null)
                {
                    return TopView(root.Left, level + 1, 0, 1, a);
                }
                return a;
            }

            if (right == 1)
            {
                a = a + " " + root.Data;
                if (root.Right != null)
                {
                    return TopView(root.Right, level + 1, 1, 0, a);
                }
                return a;
            }

            if (level == 0)
            {
                string left_string = a + " " + root.Data;
                string right_string = a;
                return (TopView(root.Left, level + 1, 0, 1, left_string) + " " + TopView(root.Right, level + 1, 1, 0, right_string));
            }

            return String.Empty;
        }

        static void Left_view(Node Root)
        {
            int maxLevel = 0;
            var res = string.Empty;
            ////var lst = new Stack<int>();
            Left_view(Root, 1, ref maxLevel, ref res);
            Console.Write(res);
        }

        static void Left_view(Node Root, int level, ref int maxLevel, ref string res)
        {
            if (Root == null)
                return;

            if (level > maxLevel)
            {
                res = Root.Data + " " + res;
                maxLevel = level;
            }

            Left_view(Root.Left, level + 1, ref maxLevel, ref res);
            Left_view(Root.Right, level + 1, ref maxLevel, ref res);
        }

        static void Bottom_View(Node root)
        {
            var DistMap = new Dictionary<int, int>();
            int minKey = 0;
            int maxKey = 0;
            Bottom_View(root, ref DistMap, 0, ref minKey, ref maxKey);
            for (int i = minKey; i <= maxKey; i++)
            {
                var data = 0;
                if (DistMap.TryGetValue(i, out data))
                {
                    Console.Write(" " + data);
                }
            }
        }

        static void Bottom_View(Node root, ref Dictionary<int, int> DistMap, int dist, ref int minKey, ref int maxKey)
        {
            if (root == null)
                return;

            int val;
            if (DistMap.TryGetValue(dist, out val))
            {
                DistMap[dist] = root.Data;
            }
            else
            {
                DistMap.Add(dist, root.Data);
            }

            minKey = (dist < minKey) ? dist : minKey;
            maxKey = (dist > maxKey) ? dist : maxKey;

            Bottom_View(root.Left, ref DistMap, dist-1, ref minKey, ref maxKey);
            Bottom_View(root.Right, ref DistMap, dist+1, ref minKey, ref maxKey);
        }
    }
}
