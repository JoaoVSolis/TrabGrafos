using System;
using System.Collections.Generic;
using System.Linq;

namespace TrabGrafos{
    class Program{
        static void Main(string[] args){
            Console.Clear();
            Console.WriteLine("Qual o tamanho do grafo?");
            int graphSize = int.Parse(Console.ReadLine());
            Graph grafos = new Graph(graphSize);
            int option = 99;
            do
            {
                Console.Clear();
                Console.WriteLine("-------------------------");
                Console.WriteLine("O que fazer com o grafo?");
                Console.WriteLine("-------------------------");
                Console.WriteLine("1 - Definir Arestas");
                Console.WriteLine("2 - Remover Aresta");
                Console.WriteLine("3 - Representar Grafo");
                Console.WriteLine("4 - Executar Algoritmo (futuro)");
                Console.WriteLine("0 - Sair");
                Console.WriteLine("-------------------------");
                Console.Write("Escolha uma opção: ");
                option = int.Parse(Console.ReadLine());
                switch (option){
                    case 1:
                        Console.WriteLine("Definir nova aresta:");
                        Console.Write("Origem: ");
                        int src = int.Parse(Console.ReadLine());
                        Console.Write("Destino: ");
                        int dst = int.Parse(Console.ReadLine());
                        Console.Write("Peso: ");
                        int weight = int.Parse(Console.ReadLine());
                        grafos.AddEdge(src, dst, weight);
                        Console.WriteLine("Aresta adicionada!");
                        Console.ReadKey();
                        break;
                    case 2:
                        Console.WriteLine("Remover aresta:");
                        Console.Write("Origem: ");
                        int srcR = int.Parse(Console.ReadLine());
                        Console.Write("Destino: ");
                        int dstR = int.Parse(Console.ReadLine());
                        grafos.RemoveEdge(srcR, dstR);
                        Console.WriteLine("Aresta removida!");
                        Console.ReadKey();
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("Como deseja representar?");
                        Console.WriteLine("1 - Matriz de Adjacência");
                        Console.WriteLine("2 - Lista de Arestas");
                        Console.WriteLine("3 - Lista de Adjacência");
                        int rep = int.Parse(Console.ReadLine());
                        switch (rep){
                            case 1:
                                grafos.PrintMatrix();
                                break;
                            case 2:
                                grafos.PrintEdgeList();
                                break;
                            case 3:
                                grafos.PrintAdjacencyList();
                                break;
                        }
                        Console.ReadKey();
                        break;
                    case 4:
                        Console.WriteLine("Aqui você pode implementar BFS, DFS ou Dijkstra futuramente.");
                        Console.ReadKey();
                        break;
                    case 0:
                        Console.WriteLine("Encerrando...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        Console.ReadKey();
                        break;
                }
            } while (option != 0);
        }
    }
}