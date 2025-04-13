using System.Text.Json;

namespace Boruvka_Algorithm;

public class GraphAdjList
{
   public int Vertices { get; }
   public Dictionary<int, List<(int, int)>> AdjList { get; }


   public GraphAdjList(int vertices)
   {
      Vertices = vertices;
      AdjList = new Dictionary<int, List<(int, int)>>();
      for (int i = 0; i < vertices; i++)
      {
         AdjList[i] = new List<(int, int)>();
      }
   }
   
   public void AddEdge(int u, int v, int weight)
   {
      AdjList[u].Add((v, weight));
      AdjList[v].Add((u, weight));
   }

   public void PrintGraphAdjList()
   {
      foreach (var i in AdjList )
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