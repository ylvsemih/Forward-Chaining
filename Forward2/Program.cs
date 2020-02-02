using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Forward2
{
    class Program
    {
        static List<Node> nodes = new List<Node>();
        static List<Rules> rules = new List<Rules>();
        static List<Rules> rulesUsed = new List<Rules>();
        static List<Rules> flag2 = new List<Rules>();
        static StreamWriter file;
        static List<Node> nodesBank = new List<Node>();
        static bool goal = true;
        static int nodesIhad = 0;
        static int iter = 1;
        static string[] eachLine;
        static void Main(string[] args)
        {
            
                Start();
            file.Close();
        }
        static public void Search(Char endChar, int endPoint)
        {
          
                while (!nodesBank.Contains(nodes[endPoint]))
                {
                    file.WriteLine("\titeration " + iter);
                    Rules usageR = PickRule();
                    if (usageR == null)
                    {
                        goal = false;
                        break;
                    }
                    rulesUsed.Add(usageR);
                    nodesBank.Add(usageR.getAim());
                    if (usageR.getRuleN() == 2)
                        file.Write("\t  R" + usageR.getRuleNo() + ": " + usageR.getNode().getName() + ", " + usageR.getNode2().getName() + "->" + usageR.getAim().getName()
                        + " apply. Raise flag1. ");
                    else
                    {
                        if (usageR.getRuleN() == 3)
                            file.Write("\t  R" + usageR.getRuleNo() + ": " + usageR.getNode().getName() + ", " + usageR.getNode2().getName() + ", " + usageR.getNode3().getName() + "->" + usageR.getAim().getName()
                        + " apply. Raise flag1. ");
                        else
                            file.Write("\t  R" + usageR.getRuleNo() + ": " + usageR.getNode().getName() + "->" + usageR.getAim().getName()
                                + " apply. Raise flag1. ");
                    }
                    initials();
                    iter++;
                }
                if (goal)
                    file.WriteLine("\t   Goal achieved.");
                file.WriteLine();
            
        }
        static public Rules PickRule()
        {
            foreach (var item in rules)
            {
                if ((item.getRuleN() == 3 && nodesBank.Contains(item.getNode2()) && nodesBank.Contains(item.getNode3()) && nodesBank.Contains(item.getNode()) && !nodesBank.Contains(item.getAim())) ||
                    (item.getRuleN() == 2 && nodesBank.Contains(item.getNode2()) && nodesBank.Contains(item.getNode()) && !nodesBank.Contains(item.getAim())) ||
                    (item.getRuleN() == 1 && nodesBank.Contains(item.getNode()) && !nodesBank.Contains(item.getAim())))
                    return item;
                else
                {
                    if (rulesUsed.Contains(item))
                    {
                        if (item.getRuleN() == 3)
                        {
                            file.WriteLine("\t  R" + item.getRuleNo() + ": " + item.getNode().getName() + ", " + item.getNode2().getName() + ", " + item.getNode3().getName() + "->" + item.getAim().getName()
                     + " skip, because flag1 raised. ");
                        }
                        else
                            if (item.getRuleN() == 2)
                        {
                            file.WriteLine("\t  R" + item.getRuleNo() + ": " + item.getNode().getName() + ", " + item.getNode2().getName() + "->" + item.getAim().getName()
                       + " skip, because flag1 raised. ");
                        }
                        else
                            file.WriteLine("\t  R" + item.getRuleNo() + ": " + item.getNode().getName() + "->" + item.getAim().getName()
                        + " skip, because flag1 raised. ");
                    }
                    else
                    {
                        if (item.getRuleN() == 2 && !nodesBank.Contains(item.getNode2()))
                        {
                            file.WriteLine("\t  R" + item.getRuleNo() + ": " + item.getNode().getName() + ", " + item.getNode2().getName() + "->" + item.getAim().getName()
                        + " not applied, because of lacking " + item.getNode2().getName() + ".");
                        }
                        else
                        if (item.getRuleN() == 3 && !nodesBank.Contains(item.getNode3()))
                        {
                            file.WriteLine("\t  R" + item.getRuleNo() + ": " + item.getNode().getName() + ", " + item.getNode2().getName() + ", " + item.getNode3().getName() + "->" + item.getAim().getName()
                        + " not applied, because of lacking " + item.getNode3().getName() + ".");
                        }
                        else
                        if (!nodesBank.Contains(item.getNode()))
                        {
                            if (item.getRuleN() == 2)
                                file.WriteLine("\t  R" + item.getRuleNo() + ": " + item.getNode().getName() + ", " + item.getNode2().getName() + "->" + item.getAim().getName()
                       + " not applied, because of lacking " + item.getNode().getName() + ".");
                            else
                                if (item.getRuleN() == 3)
                                file.WriteLine("\t  R" + item.getRuleNo() + ": " + item.getNode().getName() + ", " + item.getNode2().getName() + ", " + item.getNode3().getName() + "->" + item.getAim().getName()
                       + " not applied, because of lacking " + item.getNode().getName() + ".");
                            else
                                file.WriteLine("\t  R" + item.getRuleNo() + ": " + item.getNode().getName() + "->" + item.getAim().getName()
                                + " not applied, because of lacking " + item.getNode().getName() + ".");
                        }
                        else
                    if (nodesBank.Contains(item.getAim()))
                        {
                            if (flag2.Contains(item))
                            {
                                file.WriteLine("\t  R" + item.getRuleNo() + ": " + item.getNode().getName() + "->" + item.getAim().getName()
                          + " skip, because flag2 raised.");
                            }
                            else
                            {
                                file.WriteLine("\t  R" + item.getRuleNo() + ": " + item.getNode().getName() + "->" + item.getAim().getName()
                          + " not applied, because RHS in facts. Raise flag2.");
                                flag2.Add(item);
                            }
                        }
                    }
                }
            }
            return null;
        }
        static public void Start()
        {

            Console.Write("Enter Test No : ");
            int Test = Convert.ToInt32(Console.Read())-48;
            int x = 0, e
                = 0; char endNodes = ' ';
            eachLine = File.ReadAllLines("Rules -"+Test+".txt");
            file = new StreamWriter(@"C:\Users\ASUS\Desktop\Forward2\Forward2\bin\Debug\out"+Test+".txt");
            file.WriteLine("Semih Yoltan. Riga Technical University, Mathematic and Informatics");
            file.WriteLine("Test Number: " + Test);
            file.WriteLine("PART 1. Data\n");
            createNodes();
            endNodes = createRules();
            file.WriteLine();
            file.WriteLine("\t3) Goal");
            file.WriteLine("\t "+endNodes+"");
            foreach (var item in nodes)
            {if (item.getName() == endNodes)
                { x = e; }  else
                    e++;    }
            if (nodesBank.Contains(nodes[x]))
            {
                file.WriteLine("PART 2. Trace\n");
                file.WriteLine("PART 3. Results\n\t   Goal " + nodes[x].getName() + " in facts. Empty path.");
            }
            else
            {
                file.WriteLine("PART 2. Trace\n");
                Search(endNodes, x);
                file.WriteLine("\nPART 3. Results\n");
                if (goal)
                {
                    file.WriteLine("\t1) Goal " + endNodes + " achieved");
                    file.Write("\n\t2) Path: ");

                }

                else
                {
                    file.WriteLine("\t1) No Path" );
                    file.Write("\n\t2) Rules: ");
                }
                file.WriteLine();
            }
        


        }
        static public void createNodes()
        {
            nodes.Add(new Node('A'));//0
            nodes.Add(new Node('B'));//1
            nodes.Add(new Node('C'));//2
            nodes.Add(new Node('D'));//3
            nodes.Add(new Node('G'));//4
            nodes.Add(new Node('L'));//4
            nodes.Add(new Node('K'));//5
            nodes.Add(new Node('M'));//7
            nodes.Add(new Node('Z'));//8
            nodes.Add(new Node('F'));//9
            nodes.Add(new Node('X'));//10
            nodes.Add(new Node('Y'));//11
            nodes.Add(new Node('N'));//12
            nodes.Add(new Node('E'));//12
        }
        static public char createRules()
        {
            int ruleNo = 1;
            bool takeRule = false;
            bool takeFacts = false;
            bool quit = false;
            string splittedItem;
            foreach (var item in eachLine)
            {
                if (takeFacts) {
                    file.WriteLine("\t1) Rules");
                    printRules(); file.WriteLine();
                    file.WriteLine("\t2) Facts"); startNodes(item);takeFacts = false; }
                if (quit) return item[0];
                if (item == "3) Goal") quit = true;
                if (item == "2) Facts") takeFacts = true;
               
                if (item.Length < 2)takeRule = false;

                if (item.Contains('\t'))
                    splittedItem = item.Split('\t')[0];
                else
                    splittedItem = item;
                if (takeRule)
                {
                    if (splittedItem.Length < 4)
                    {
                        foreach (var item2 in nodes)
                        {
                            foreach (var item3 in nodes)
                            {
                                if (item2.getName() == splittedItem.Split(' ')[0][0] && item3.getName() == splittedItem.Split(' ')[1][0])
                                {
                                    rules.Add(new Rules(item3, item2, ruleNo));
                                    ruleNo++;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (splittedItem.Length > 5)
                        {
                            foreach (var item2 in nodes)
                            {
                                foreach (var item3 in nodes)
                                {
                                    foreach (var item4 in nodes)
                                    {
                                        foreach (var item5 in nodes)
                                        {
                                            if (item2.getName() == splittedItem.Split(' ')[0][0] && item3.getName() == splittedItem.Split(' ')[1][0]
                                                && item4.getName() == splittedItem.Split(' ')[2][0] && item5.getName() == splittedItem.Split(' ')[3][0])
                                            {
                                                rules.Add(new Rules(item3, item4, item5, item2, ruleNo));
                                                ruleNo++;
                                            }
                                        }
                                    }
                                }

                            }
                        }
                        else
                            foreach (var item2 in nodes)
                            {
                                foreach (var item3 in nodes)
                                {
                                    foreach (var item4 in nodes)
                                    {
                                        if (item2.getName() == splittedItem.Split(' ')[0][0] && item3.getName() == splittedItem.Split(' ')[1][0]
                                            && item4.getName() == splittedItem.Split(' ')[2][0])
                                        {
                                            rules.Add(new Rules(item3, item4, item2, ruleNo));
                                            ruleNo++;
                                        }
                                    }
                                }

                            }
                    }
                }
                if (item == "1) Rules") takeRule = true;
                
            }
            return ' ';
        }
        static public void startNodes(string x)
        {
            file.Write("\t   ");
            file.WriteLine(x);
            int nodeCounter = 0;
            foreach (var item in x.Split(' '))
            {
                foreach (var item2 in nodes)
                {
                    if (item[0] != ' ')
                    {
                        if (item2.getName() == item[0])
                        {
                            nodesBank.Add(item2);
                            nodeCounter++;
                        }
                    }
                    else
                    {
                        if (item2.getName() == item[1])
                        {
                            nodesBank.Add(item2);
                            nodeCounter++;
                        }
                    }

                }
               // if (item == "2) Facts") takeRule = true;
            }
           
            nodesIhad = nodesBank.Count;
        }
        static public void initials()
        {
            var index = 1;
            file.Write("Facts ");
            foreach (var item in nodesBank)
            {
                file.Write(item.getName());
                if (index < nodesBank.Count) file.Write(", ");
                index++;
                if (index == nodesIhad + 1) file.Write(" and ");
            }
            file.WriteLine();
        }
        static public void printNodes()
        {
            var index = 1;
            foreach (var item in nodes)
            {
                file.Write(item.getName());
                if (index < nodes.Count) file.Write(", ");
                index++;
            }
        }
        static public void usedRules()
        {
            var index = 1;
            foreach (var item in rulesUsed)
            {
                file.Write("R" + item.getRuleNo());
                if (index < rulesUsed.Count) file.Write(", ");
                index++;
            }
            file.Write(".");
        }
        static public void printRules()
        {
            var index = 0;
            foreach (var item in rules)
            {
                if (item.getRuleN() == 1)
                {
                    file.Write("\t   R" + item.getRuleNo() + ": " + item.getNode().getName());
                    if (index < rules.Count) file.Write(" -> ");
                    file.Write(item.getAim().getName() + "\n");
                    index++;
                }
                else
                {
                    if (item.getRuleN() == 3)
                    {
                        file.Write("\t   R" + item.getRuleNo() + ": " + item.getNode().getName() + ", " + item.getNode2().getName()
                            + ", " + item.getNode3().getName());
                        if (index < rules.Count) file.Write(" -> ");
                        file.Write(item.getAim().getName() + "\n");
                        index++;
                    }
                    else
                    {
                        file.Write("\t   R" + item.getRuleNo() + ": " + item.getNode().getName() + ", " + item.getNode2().getName());
                        if (index < rules.Count) file.Write(" -> ");
                        file.Write(item.getAim().getName() + "\n");
                        index++;
                    }
                }
            }
        }
    }
    class Node
    {
        char name;
        public Node(char name)
        {
            this.name = name;
        }
        public char getName()
        {
            return name;
        }
    }
    class Rules
    {
        int RuleNo, rulElem;
        Node aimNode, Node, Node2, Node3;
        public Rules(Node Node, Node aimNode, int RuleNo)
        {
            rulElem = 1;
            this.Node = Node;
            this.aimNode = aimNode;
            this.RuleNo = RuleNo;
        }
        public int getRuleN()
        {
            return rulElem;
        }
        public Rules(Node Node, Node Node2, Node aimNode, int RuleNo)
        {
            rulElem = 2;
            this.Node = Node;
            this.Node2 = Node2;
            this.aimNode = aimNode;
            this.RuleNo = RuleNo;
        }
        public Rules(Node Node, Node Node2, Node Node3, Node aimNode, int RuleNo)
        {
            rulElem = 3;
            this.Node = Node;
            this.Node2 = Node2;
            this.Node3 = Node3;
            this.aimNode = aimNode;
            this.RuleNo = RuleNo;
        }
        public Node getAim()
        {
            return aimNode;
        }
        public Node getNode()
        {
            return Node;
        }
        public Node getNode2()
        {
            return Node2;
        }
        public Node getNode3()
        {
            return Node3;
        }
        public int getRuleNo()
        {
            return RuleNo;
        }

    }
}