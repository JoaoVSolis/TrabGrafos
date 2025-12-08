using System;
using System.Collections.Generic;
using System.IO;

public class Graph{
    private int[,] adjacencyMatrix;
    private List<Edge> edgeList;
    private List<List<(int neighbor, int weight)>> adjacencyList;
    public int Size { get; private set; }

    public Graph(int size){
        Size = size;
        adjacencyMatrix = new int[size, size];
        edgeList = new List<Edge>();
        adjacencyList = new List<List<(int, int)>>();

        for (int i = 0; i < size; i++){
            adjacencyList.Add(new List<(int, int)>());
        }
    }
    public void AddEdge(int src, int dst, int weight){
        if (src < 0 || src >= Size || dst < 0 || dst >= Size)
            throw new ArgumentOutOfRangeException("Índices fora do intervalo do grafo.");
        // matriz
        adjacencyMatrix[src, dst] = weight;
        adjacencyMatrix[dst, src] = weight;

        // lista de arestas
        edgeList.Add(new Edge(src, dst, weight));

        // lista de adjacência
        adjacencyList[src].Add((dst, weight));
        adjacencyList[dst].Add((src, weight));
    }
    public void PrintMatrix(){
        Console.WriteLine("Matriz de Adjacência:");
        for (int i = 0; i < Size; i++){
            for (int j = 0; j < Size; j++){
                Console.Write(adjacencyMatrix[i, j] + " ");
            }
            Console.WriteLine();
        }
    }
    public void PrintEdgeList(){
        Console.WriteLine("Lista de Arestas:");
        foreach (var edge in edgeList){
            Console.WriteLine(edge);
        }
    }
    public void PrintAdjacencyList(){
        Console.WriteLine("Lista de Adjacência:");
        for (int i = 0; i < Size; i++){
            Console.Write($"[{i}] -> ");
            foreach (var (neighbor, weight) in adjacencyList[i]){
                Console.Write($"{neighbor}({weight}) ");
            }
            Console.WriteLine();
        }
    }
    public void RemoveEdge(int src, int dst){
    if (src < 0 || src >= Size || dst < 0 || dst >= Size)
        throw new ArgumentOutOfRangeException("Índices fora do intervalo do grafo.");

    // matriz
    adjacencyMatrix[src, dst] = 0;
    adjacencyMatrix[dst, src] = 0;

    // lista de arestas (remove todas as ocorrências da aresta src-dst ou dst-src)
    edgeList.RemoveAll(e => 
        (e.Src == src && e.Dst == dst) || (e.Src == dst && e.Dst == src));

    // lista de adjacência
    adjacencyList[src].RemoveAll(n => n.neighbor == dst);
    adjacencyList[dst].RemoveAll(n => n.neighbor == src);
}
public List<(int neighbor, int weight)> GetNeighbors(int node){
    if (node < 0 || node >= Size)
        throw new ArgumentOutOfRangeException("Índice fora do intervalo do grafo.");

    // Retorna uma cópia da lista de vizinhos para evitar modificações externas
    return new List<(int, int)>(adjacencyList[node]);
}
public List<Edge> GetEdges(){
    return new List<Edge>(edgeList);
}

public static Graph LoadFromFile(string filePath)
    {
        string[] lines = File.ReadAllLines(filePath);
        int size = int.Parse(lines[0]);
        Graph g = new Graph(size);
        for (int i = 1; i < lines.Length; i++)
        {
            string[] parts = lines[i].Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (parts.Length < 3) continue;

            int src = int.Parse(parts[0]);
            int dst = int.Parse(parts[1]);
            int weight = int.Parse(parts[2]);

            g.AddEdge(src, dst, weight);
        }
        return g;
    }

}