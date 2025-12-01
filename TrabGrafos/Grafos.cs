using System;
using System.Collections.Generic;

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

}