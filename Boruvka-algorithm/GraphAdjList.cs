using System.Text.Json;

namespace Boruvka_Algorithm;

public class GraphAdjList
{
   private Dictionary<int, List<(int, int)>> adjList;
   public int Vertices { get; }

   public GraphAdjList(int vertices)
   {
      Vertices = vertices;
      adjList = new Dictionary<int, List<(int, int)>>();
      for (int i = 0; i < vertices; i++)
      {
         adjList[i] = new List<(int, int)>();
      }
   }
   
   public void AddEdge(int u, int v, int weight)
   {
      adjList[u].Add((v, weight));
      adjList[v].Add((u, weight));
   }

   public void PrintGraphAdjList()
   {
      foreach (var i in adjList )
      {
         Console.Write($"{i.Key} ->");
         foreach (var j in i.Value)
         {
            Console.Write($"(direction:{j.Item1}, weight:{j.Item2})");
         }
         Console.WriteLine();
      }
   }
}