using System;
using System.Collections.Generic;

namespace LevelWiseFriendList_BFS
{

    /// Input format :- 
    /// A: B,C,D
    /// D: E,F
    /// C: G,H
    /// G: I
    /// 
    /// Output format:-
    /// Level1 : B,C,D
    /// Level2: E,F,G,H
    /// Level3: I
    /// 

    class LevelWiseFriendList_BFS
    {
        static void Main(string[] args)
        {
            FriendGraph friendGraph = new FriendGraph();

            var readLine = Console.ReadLine();
            while (readLine != String.Empty)
            {
                readLine = readLine.Trim();
                var spliter = readLine.IndexOf(":", StringComparison.Ordinal);
                var person = readLine.Substring(0, spliter);
                
                var friends = readLine.Substring(spliter + 2, readLine.Length - 3).Split(',');
                //friends = friends.Split(',').ToString();
                
                if (friendGraph.PersonGraph.Count == 0)
                {
                    var rootPerson = new Person(person);
                    foreach (var friend in friends)
                    {
                        var friendNode = new Person(friend.ToString());
                        friendNode.IsFriendOf(rootPerson);
                        friendGraph.PersonGraph.Add(friend.ToString(), friendNode);
                    }
                    friendGraph.PersonGraph.Add(person, rootPerson);
                }

                else
                {
                    Person rootPerson;
                    if (!friendGraph.PersonGraph.TryGetValue(person, out rootPerson))
                    {
                        rootPerson = new Person(person);
                    }

                    foreach (var friend in friends)
                    {
                        Person friendPerson;
                        if (!friendGraph.PersonGraph.TryGetValue(friend.ToString(), out friendPerson))
                        {
                            friendPerson = new Person(friend.ToString());
                            friendPerson.IsFriendOf(rootPerson);
                            friendGraph.PersonGraph.Add(friend.ToString(), friendPerson);
                        }
                        else
                        {
                            friendPerson.IsFriendOf(rootPerson);
                            ////friendGraph.PersonGraph.Add(friend.ToString(), friendPerson);
                        }
                    }
                    friendGraph.PersonGraph[person] = rootPerson;
                }

                readLine = Console.ReadLine();
            }

            friendGraph.Bfs("A");
            Console.Read();
        }
    }

    class Person
    {
        public string Name;

        public List<Person> Friends = new List<Person>();

        public Person()
        {}

        public Person(string name)
        {
            this.Name = name;
        }

        public void IsFriendOf(Person friend)
        {
            this.Friends.Add(friend);
            friend.Friends.Add(this);
        }

        public bool IsVisited = false;
    }

    class FriendGraph
    {
        public Dictionary<string, Person> PersonGraph = new Dictionary<string, Person>();

        public void Bfs(string name)
        {
            Person rootperson;
            if (!this.PersonGraph.TryGetValue(name, out rootperson))
            {
                return;
            }

            Queue<Person> q = new Queue<Person>();
            q.Enqueue(rootperson);
            rootperson.IsVisited = true;
            int level = 1;
            int currentLevelCount = rootperson.Friends.Count;;
            string print = "Level1 :";
            int nextLevelCount = -currentLevelCount;

            while (q.Count != 0)
            {
                var tempPerson = q.Dequeue();
                if (!tempPerson.Name.Equals(name))
                {
                    currentLevelCount--;
                    print = string.Concat(print, tempPerson.Name + ",");
                }
                //Console.WriteLine( level + ":" + tempPerson.Name + " ");
                foreach (var friend in tempPerson.Friends)
                {
                    if (!friend.IsVisited)
                    {
                        friend.IsVisited = true;
                        q.Enqueue(friend);
                        nextLevelCount++;
                    }
                }

                if (currentLevelCount == 0)
                {
                    level++;
                    currentLevelCount = nextLevelCount;
                    Console.WriteLine(print);
                    print = "Level" + level + ": ";
                    nextLevelCount = 0;
                }
            }

        }
    }
}
