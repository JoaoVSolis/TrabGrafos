using System;
using System.Collections.Generic;

public static class GraphAlgorithms{
    // DFS - Travessia em profundidade
    public static void DFS(Graph g, int start){
        bool[] visited = new bool[g.Size];
        Stack<int> stack = new Stack<int>();
        stack.Push(start);

        Console.WriteLine("DFS:");
        while (stack.Count > 0){
            int node = stack.Pop();
            if (!visited[node]){
                visited[node] = true;
                Console.Write(node + " ");

                foreach (var (neighbor, _) in g.GetNeighbors(node)){
                    if (!visited[neighbor])
                        stack.Push(neighbor);
                }
            }
        }
        Console.WriteLine();
    }

    // BFS - Travessia em amplitude
    public static void BFS(Graph g, int start){
        bool[] visited = new bool[g.Size];
        Queue<int> queue = new Queue<int>();
        queue.Enqueue(start);
        visited[start] = true;

        Console.WriteLine("BFS:");
        while (queue.Count > 0){
            int node = queue.Dequeue();
            Console.Write(node + " ");

            foreach (var (neighbor, _) in g.GetNeighbors(node)){
                if (!visited[neighbor]){
                    visited[neighbor] = true;
                    queue.Enqueue(neighbor);
                }
            }
        }
        Console.WriteLine();
    }

    // Dijkstra - menor caminho entre dois vértices
    public static void Dijkstra(Graph g, int start, int end){
        int[] dist = new int[g.Size];
        bool[] visited = new bool[g.Size];
        int[] prev = new int[g.Size];

        for (int i = 0; i < g.Size; i++){
            dist[i] = int.MaxValue;
            prev[i] = -1;
        }
        dist[start] = 0;

        for (int i = 0; i < g.Size; i++){
            int u = -1;
            for (int j = 0; j < g.Size; j++){
                if (!visited[j] && (u == -1 || dist[j] < dist[u]))
                    u = j;
            }

            if (dist[u] == int.MaxValue) break;
            visited[u] = true;

            foreach (var (neighbor, weight) in g.GetNeighbors(u)){
                if (dist[u] + weight < dist[neighbor])
                {
                    dist[neighbor] = dist[u] + weight;
                    prev[neighbor] = u;
                }
            }
        }

        Console.WriteLine($"Dijkstra: menor caminho de {start} até {end}");
        if (dist[end] == int.MaxValue){
            Console.WriteLine("Não existe caminho.");
            return;
        }

        // reconstruir caminho
        List<int> path = new List<int>();
        for (int at = end; at != -1; at = prev[at])
            path.Add(at);
        path.Reverse();
        Console.WriteLine("Caminho: " + string.Join(" -> ", path));
        Console.WriteLine("Distância: " + dist[end]);
    }

    // Prim - árvore geradora mínima
    public static void Prim(Graph g){
        bool[] inMST = new bool[g.Size];
        int[] key = new int[g.Size];
        int[] parent = new int[g.Size];

        for (int i = 0; i < g.Size; i++){
            key[i] = int.MaxValue;
            parent[i] = -1;
        }

        key[0] = 0;

        for (int count = 0; count < g.Size - 1; count++){
            int u = -1;
            for (int i = 0; i < g.Size; i++){
                if (!inMST[i] && (u == -1 || key[i] < key[u]))
                    u = i;
            }

            inMST[u] = true;

            foreach (var (neighbor, weight) in g.GetNeighbors(u)){
                if (!inMST[neighbor] && weight < key[neighbor]){
                    key[neighbor] = weight;
                    parent[neighbor] = u;
                }
            }
        }

        Console.WriteLine("Árvore Geradora Mínima (Prim):");
        for (int i = 1; i < g.Size; i++){
            Console.WriteLine($"{parent[i]} --({key[i]})-- {i}");
        }
    }

    // Kruskal - árvore geradora mínima
    public static void Kruskal(Graph g){
        var edges = new List<Edge>(g.GetEdges());
        edges.Sort((a, b) => a.Weight.CompareTo(b.Weight));

        int[] parent = new int[g.Size];
        for (int i = 0; i < g.Size; i++) parent[i] = i;

        Func<int, int> find = null;
        find = (x) => parent[x] == x ? x : parent[x] = find(parent[x]);

        Console.WriteLine("Árvore Geradora Mínima (Kruskal):");
        foreach (var e in edges){
            int rootSrc = find(e.Src);
            int rootDst = find(e.Dst);

            if (rootSrc != rootDst){
                Console.WriteLine($"{e.Src} --({e.Weight})-- {e.Dst}");
                parent[rootSrc] = rootDst;
            }
        }
    }
}