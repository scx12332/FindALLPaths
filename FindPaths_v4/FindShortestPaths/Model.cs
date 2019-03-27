using System;
using System.Collections.Generic;

namespace FindShortestPaths
{
    public class VertexItem
    {
        ///<summary>
        ///自定义的顶点的编号
        ///</summary>
        public int data { get; set; }
        /// <summary>
        /// 每个顶点的邻接点
        /// </summary>
        public EdgeItem firstEdge { get; set; }

        /// <summary>
        /// 每个顶点的json文件中id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// 顶点的json文件的name
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 顶点目前人数
        /// </summary>
        public string currentPeople
        {
            get
            {
                return "0";
            }
        }
        /// <summary>
        /// 顶点可容纳人数
        /// </summary>
        public string admissiblePeople
        {
            get
            {
                return "100";
            }
        }
    }

    public class EdgeItem
    {
        /// <summary>
        /// 自定义边的编号
        /// </summary>
        public int adjvex { get; set; }
        /// <summary>
        /// 邻接表中每个点的下一条边
        /// </summary>
        public EdgeItem nextEdge { get; set; }
        /// <summary>
        /// json文件中边id
        /// </summary>
        public string id { get; set; }

        /// <summary>
        /// json文件中边name
        /// </summary>
        public string name { get; set; }


        /// <summary>
        /// 构成每条边的第一个点
        /// </summary>
        public string pointId1 { get; set; }
        /// <summary>
        /// 构成每条边的第二个点
        /// </summary>
        public string pointId2 { get; set; }
        /// <summary>
        /// 边长度
        /// </summary>
        public string pathLength
        {
            get
            {
                return "10";
            }
        }
        /// <summary>
        /// 边宽度
        /// </summary>
        public string pathWidth
        {
            get
            {
                return "10";
            }
        }
    }

    public class XLYZLocation
    {
        public string ID { get; set; }

        public string Name { get; set; }

        public double X { get; set; }

        public double Y { get; set; }

        public double Z { get; set; }

        public int PathSort { get; set; }

        public List<V2> Boundary {get; set;}
    }
    public class V2
    {
        public double X {get; set;}
        public double Y{get; set;}
    }
    public class Root
    {
        /// <summary>
        /// 顶点集合
        /// </summary>
        public List<VertexItem> Vertex { get; set; }
        /// <summary>
        /// 边集合
        /// </summary>
        public List<EdgeItem> Edge { get; set; }

        public List<XLYZLocation> XLYZ { get; set; }
        /// <summary>
        /// 路径集合
        /// </summary>
        public List<PathItem> Paths { get; set; }
    }

    public class PathItem
    {
        /// <summary>
        /// 每条路径
        /// </summary>
        public List<OnePathItem> OnePath { get; set; }
        /// <summary>
        /// 每条路径的总距离
        /// </summary>
        public string TotalLength { get; set; }

    }

    public class OnePathItem
    {
        /// <summary>
        /// 自定义的路径中每个顶点的编号
        /// </summary>
        public int VertexNumber { get; set; }
        /// <summary>
        /// 每个顶点的id
        /// </summary>
        public string VertexId { get; set; }
        /// <summary>
        /// 一条路径中每条边的长度
        /// </summary>
        public string EdgeLength{ get; set; }
    }
}

