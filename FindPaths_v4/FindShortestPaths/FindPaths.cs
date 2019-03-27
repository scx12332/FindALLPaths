using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FindShortestPaths
{
    public class FindPaths
    {
        public static int count = 0;
        public static List<string> shortestPaths;     //最短路径集合

        public static Dictionary<string, float> paths;     //包括所有路径和距离的字典     
        public Dictionary<int, bool> states;   //代表某节点是否在stack中，避免产生回路         
        public Stack<int> stack;      //存放节点的stack
        Root r;  //存放输入输出的类
        List<PathItem> allPaths;

        //打印stack中信息，即路径信息
        public void PrintPath(float[,] graphs, Dictionary<string, int> dic)
        {
            int bianhao = 1;
            count++;
            int number = -1;
            float sum = 0;
            PathItem path = new PathItem();
            List<OnePathItem> onePath = new List<OnePathItem>();
            foreach (var i in stack)
            {           
                OnePathItem op = new OnePathItem();
                op.EdgeLength = "0";
                op.VertexNumber = bianhao++;
                if (number != -1)
                {
                    sum = sum + graphs[number, i];
                    op.EdgeLength = graphs[number, i].ToString() ;
                }
                string key = dic.FirstOrDefault(q => q.Value == i).Key;
                op.VertexId = key;
                onePath.Add(op);               
                number = i;
            }
            path.OnePath = onePath;
            path.TotalLength = Convert.ToString(sum);
            allPaths.Add(path);
            r.Paths = allPaths;
        }

        //得到x的邻接点为y的后一个邻接点位置,为-1说明没有找到 
        public int GetNextNode(Graph graph, int x, int y)
        {
            int next_node = -1;
            EdgeItem edge = graph.vexsarray[x].firstEdge;
            while (null != edge && y == -1)
            {
                int n = edge.adjvex;
                //元素还不在stack中  
                if (!states[n])
                    return n;
                if (edge.nextEdge != null)
                {
                    edge = edge.nextEdge;
                }
                else
                {
                    return -1;
                }
            }
            while (null != edge)
            {
                //节点未访问  
                if (edge.adjvex == y)
                {
                    if (null != edge.nextEdge)
                    {
                        next_node = edge.nextEdge.adjvex;

                        if (!states[next_node])
                            return next_node;
                        y = next_node;
                    }
                    else
                        return -1;
                }
                edge = edge.nextEdge;
            }
            return -1;
        }

        public List<PathItem> Visit(Root root, string point1, string point2)
        {
            ProcessData processData = new ProcessData();
            var graph = processData.readJson(root,point1,point2);           
            PreVisit(ProcessData.graphs, ProcessData.dic, graph, ProcessData.dic[point1], ProcessData.dic[point2]);
            return r.Paths;                   
        }

        public void PreVisit(float[,] graphs, Dictionary<string, int> dic, Graph graph, int x, int y)
        {
            r = new Root();
            allPaths = new List<PathItem>();
            paths = new Dictionary<string, float>();
            states = new Dictionary<int, bool>();
            stack = new Stack<int>();
            //初始化所有节点在states中的情况  
            for (int i = 0; i < graph.vexsarray.Length; i++)
            {
                states.Add(i, false);
            }
            //stack top元素  
            int top_node;
            //存放当前top元素已经访问过的邻接点,若不存在则置-1,此时代表访问该top元素的第一个邻接点  
            int adjvex_node = -1;
            int next_node;
            stack.Push(x);
            states[x] = true;

            while (stack.Count != 0)
            {
                top_node = stack.Peek();
                //找到需要访问的节点  
                if (top_node == y)
                {
                    //打印该路径  
                    PrintPath(graphs, dic);
                    adjvex_node = stack.Pop();
                    states[adjvex_node] = false;
                }
                else
                {
                    //访问top_node的第advex_node个邻接点  
                    next_node = GetNextNode(graph, top_node, adjvex_node);
                    if (next_node != -1)
                    {
                        stack.Push(next_node);
                        //置当前节点访问状态为已在stack中  
                        states[next_node] = true;
                        //临接点重置  
                        adjvex_node = -1;
                    }
                    //不存在临接点，将stack top元素退出   
                    else
                    {
                        //当前已经访问过了top_node的第adjvex_node邻接点  
                        adjvex_node = stack.Pop();
                        //不在stack中  
                        states[adjvex_node] = false;
                    }
                }
            }
        }

        public List<PathItem> FindShortestPath(Root root, string point1, string point2)
        {           
            var res=Visit(root,point1,point2);
            List<PathItem> shortestPathsList = new List<PathItem>();
            shortestPaths = new List<string>();
            int minValue = -1;

            if (res != null)
            {
                foreach (var i in res)
                {
                    if (minValue == -1)
                    {
                        minValue = Convert.ToInt32(i.TotalLength);
                    }
                    if (Convert.ToInt32(i.TotalLength) < minValue)
                    {
                        minValue = Convert.ToInt32(i.TotalLength);
                    }
                }

                foreach (var d in res)
                {
                    if (d.TotalLength == Convert.ToString(minValue))
                    {
                        PathItem shortestPath = new PathItem();
                        shortestPath.OnePath = d.OnePath;
                        shortestPath.TotalLength = d.TotalLength;
                        shortestPathsList.Add(shortestPath);
                    }
                }
                res = shortestPathsList;
                return res;
            }
            else
            {
                return null;
            }
        }
    }
}

