using System;

namespace FindShortestPaths
{
    public class Graph
    {
        public VertexItem[] vexsarray;

        public void linkLast(EdgeItem target, EdgeItem node)
        {
            while (target.nextEdge != null)
            {
                target = target.nextEdge;
            }
            target.nextEdge = node;
        }

        public int getPosition(int data)
        {
            for (int i = 0; i < vexsarray.Length; i++)
            {
                if (data == vexsarray[i].data)
                {
                    return i;
                }
            }
            return -1;
        }

        public void buildGraph(int[] vexs, int[,] edges)
        {
            int vLen = vexs.Length;
            int eLen = edges.Length;
            vexsarray = new VertexItem[vLen];

            for (int i = 0; i < vLen; i++)
            {
                vexsarray[i] = new VertexItem();
                vexsarray[i].data = vexs[i];
                vexsarray[i].firstEdge = null;
            }

            for (int i = 0; i < eLen / 2; i++)
            {

                int a = edges[i, 0];
                int b = edges[i, 1];

                int start = getPosition(a);
                int end = getPosition(b);

                EdgeItem edgeNode = new EdgeItem();
                edgeNode.adjvex = end;

                if (vexsarray[start].firstEdge == null)
                {
                    vexsarray[start].firstEdge = edgeNode;
                }
                else
                {
                    linkLast(vexsarray[start].firstEdge, edgeNode);
                }
            }
        }
    }
}
