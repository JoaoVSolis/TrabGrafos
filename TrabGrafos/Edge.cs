public class Edge
{
    public int Src { get; }
    public int Dst { get; }
    public int Weight { get; }
    public Edge(int src, int dst, int weight){
        Src = src;
        Dst = dst;
        Weight = weight;
    }
    public override string ToString(){
        return $"{Src} --({Weight})-- {Dst}";
    }
}