using System;
using System.Collections.Generic;

namespace FindShortestPaths
{
    public class ProcessData
    {
        public static int vertexCount;    //记录顶点数 
        public static float[,] graphs;        //邻接矩阵
        public static Dictionary<string, int> dic;
        public static int ids;  //每个顶点标识映射成从0开始
        static Dictionary<string, string> edgeName;
        public static int m;
        static int[,] edges;      //定义边关系矩阵

        public Graph readJson(Root root, string point1, string point2)
        {
            m = 0;
            ids = 0;
            edgeName = new Dictionary<string, string>();
            vertexCount = 0;
            dic = new Dictionary<string, int>();
            foreach (var v in root.Vertex)
            {
                vertexCount++;
                var id = v.id;
                if (id != "")
                {
                    dic.Add(id, ids++);
                }
            }

            graphs = new float[vertexCount, vertexCount];
            //初始化邻接矩阵
            for (int i = 0; i < vertexCount; i++)
            {
                for (int j = 0; j < vertexCount; j++)
                {
                    graphs[i, j] = 10000;
                }
            }

            foreach (var e in root.Edge)
            {
                var id = e.id;
                var name = e.name;
                var pathLength = Convert.ToSingle(e.pathLength);
                var pathWidth = Convert.ToSingle(e.pathWidth);
                var pointId1 = e.pointId1;
                var pointId2 = e.pointId2;

                if (dic.ContainsKey(pointId1) && dic.ContainsKey(pointId2))
                {
                    if (!(edgeName.ContainsKey(pointId1 + "!" + pointId2)))
                    {
                        edgeName.Add(pointId1 + "!" + pointId2, id);
                    }
                    if (!(edgeName.ContainsKey(pointId2 + "!" + pointId1)))
                    {
                        edgeName.Add(pointId2 + "!" + pointId1, id);
                    }

                    graphs[dic[pointId1], dic[pointId2]] = pathLength;
                    graphs[dic[pointId2], dic[pointId1]] = pathLength;
                    m = m + 1;                  
                }
            }
            Graph g = buildVertexRelation();
            return g;
        }

        public Graph buildVertexRelation()
        {
            int h = 0;
            int[] vexs = new int[vertexCount];
            for (int i = 0; i < vertexCount; i++)
            {
                vexs[i] = i;
            }
            edges = new int[2 * m, 2];
            for (int j = 0; j < vertexCount; j++)
            {
                for (int k = 0; k < vertexCount; k++)
                {
                    if (graphs[j, k] != 10000)
                    {
                        edges[h, 0] = j;
                        edges[h, 1] = k;
                        h++;
                    }
                }
            }
            Graph graph = new Graph();
            graph.buildGraph(vexs, edges);
            return graph;
        }
    }
}
