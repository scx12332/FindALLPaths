using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FindShortestPathsTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region 将json文件处理成类的形式
            List<FindShortestPaths.VertexItem> vertexList = new List<FindShortestPaths.VertexItem>();
            List<FindShortestPaths.EdgeItem> edgeList = new List<FindShortestPaths.EdgeItem>();
            FindShortestPaths.Root root = new FindShortestPaths.Root();
            string jsonfile = "F://demo.json";
            //string jsonfile=@"C:\Users\孙传翔\Desktop\FindShortestPaths\FindAllPath2.0\2018_12_25\test_30_1.json"; 
            using (System.IO.StreamReader file = System.IO.File.OpenText(jsonfile))
            {
                using (JsonTextReader reader = new JsonTextReader(file))
                {
                    JObject o = (JObject)JToken.ReadFrom(reader);
                    var vertex = o["Vertex"];
                    var edge = o["Edge"];

                    foreach (JObject v in vertex)
                    {
                        FindShortestPaths.VertexItem vi = new FindShortestPaths.VertexItem();
                        vi.id = Convert.ToString(v["id"]);
                        vi.name = Convert.ToString(v["name"]);
                        vertexList.Add(vi);

                    }
                    root.Vertex = vertexList;

                    foreach (JObject e in edge)
                    {
                        FindShortestPaths.EdgeItem ei = new FindShortestPaths.EdgeItem();
                        ei.id = Convert.ToString(e["id"]);
                        ei.name = Convert.ToString(e["name"]);
                        ei.pointId1 = Convert.ToString(e["pointId1"]);
                        ei.pointId2 = Convert.ToString(e["pointId2"]);
                        edgeList.Add(ei);
                    }
                    root.Edge = edgeList;
                }
            }
            #endregion

            List<string> source = new List<string>() { "5e7a46a3-6fbd-4d5e-9e25-8ea0878acf98-0004ef96" };  //输入源点
            List<string> destinate = new List<string>() { "5e7a46a3-6fbd-4d5e-9e25-8ea0878acf98-0004ef99"};  //输入终点
            FindShortestPaths.FindPaths findPaths = new FindShortestPaths.FindPaths();

            // 调用Visit方法获取源点到终点的所有路径。
            var paths = findPaths.Visit(root, destinate[0], source[0]);
            try
            {
                foreach (var i in paths)
                {
                    Console.WriteLine("路径长度为：" + i.TotalLength + "，路径如下：");
                    foreach (var p in i.OnePath)
                    {
                        Console.WriteLine("编号：" + p.VertexNumber + "   顶点id：" + p.VertexId + "   边长：" + p.EdgeLength);
                    }
                    Console.WriteLine();
                }
            }
            catch { }

            Console.WriteLine("-----------------------------------");

            //调用FindShortestPath方法获取源点到终点的最短路径。
            var shortestPaths = findPaths.FindShortestPath(root, destinate[0], source[0]);
            try
            {
                Console.WriteLine("最短路径为：" + shortestPaths[0].TotalLength.ToString() + "，路径如下：");
                foreach (var i in shortestPaths)
                {
                    foreach (var j in i.OnePath)
                    {
                        Console.WriteLine("编号：" + j.VertexNumber + "   顶点id：" + j.VertexId + "   边长：" + j.EdgeLength);
                    }
                    Console.WriteLine();
                }
                Console.ReadKey();
            }
            catch { }
        }
    }
}

