using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication26
{
    
    class Node
    {
        public int data { get; set; }

        public Node Next { get; set; }

        public Node Child { get; set; }

        public bool IsVisited = false;

        public Node()
        {
            this.data = 0;
            this.Next = null;
            this.Child = null;
        }
    }

    class System
    {
        public Dictionary<int, List<Node>> LisNodes;

        public System()
        {
            this.LisNodes = new Dictionary<int, List<Node>>();
        }

        public void AddNodes(int level, List<int> nodes)
        {
            Node currentNode = null;
            List<Node> tmp = new List<Node>();
            foreach (var node in nodes)
            {
                if (currentNode == null)
                {
                    currentNode = new Node() {data = node};
                }
                else
                {
                    currentNode.Next = new Node(){data = node};
                    currentNode = currentNode.Next;
                }
                tmp.Add(currentNode);
            }

            if (this.LisNodes.ContainsKey(level))
            {
                this.LisNodes[level] = this.LisNodes[level].Union(tmp);
            }
            else
            {
                this.LisNodes.Add(level, tmp);
            }
        }

        public void AddChild(int parentLevel, int parent, int child)
        {
           this.LisNodes[parentLevel].Where(x => x.data == parent).First().Child 
               = this.LisNodes[parentLevel + 1].Where(x => x.data == child).First();
        }

        public void Flatten()
        {
            Node head = this.LisNodes[1][0];
            Queue<Node> queue = new Queue<Node>();
            while (true)
            {
                this.PrintLevel(head, ref queue);
                while (queue.Count != 0)
                {
                    this.PrintLevel(queue.Dequeue(), ref queue);
                }
                return;
            }
        }

        public void PrintLevel(Node head, ref Queue<Node> queue)
        {
            while (head != null && !head.IsVisited)
            {
                Console.Write(" " + head.data);
                head.IsVisited = true;
                if (head.Child != null)
                {
                    queue.Enqueue(head.Child);
                }
                head = head.Next;
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            System system = new System();
            
            system.AddNodes(1, new List<int>() {10, 5, 12, 7, 11});
            system.AddNodes(2, new List<int>() {4, 20, 13});
            system.AddNodes(2, new List<int>(){17,6});
            system.AddNodes(3, new List<int>(){2});
            system.AddNodes(3, new List<int>() {16});
            system.AddNodes(3, new List<int>() {9,8});
            system.AddNodes(4, new List<int>() {3});
            system.AddNodes(4, new List<int>() {19,15});

            system.AddChild(1,10,4);
            system.AddChild(1, 7, 17);
            system.AddChild(2, 20, 2);
            system.AddChild(2, 13, 16);
            system.AddChild(2, 17, 9);
            system.AddChild(3, 16, 3);
            system.AddChild(3, 9, 19);

            system.Flatten();

            Console.Read();
        }
    }
}
