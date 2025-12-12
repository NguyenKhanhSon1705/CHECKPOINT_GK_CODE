using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

class Program
{
    static int N;
    static List<int>[] adj;
    static int[] parent;

    static void Main(string[] args)
    {
        Stream inputStream;
        string filePath = Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "test1.txt");
        filePath = Path.GetFullPath(filePath);

        if (File.Exists(filePath))
        {
            inputStream = File.OpenRead(filePath);
            Console.WriteLine($"Da tim thay file '{filePath}'. Dang doc du lieu...");
        }
        else
        {
            inputStream = Console.OpenStandardInput();
        }

        FastScanner scanner = new FastScanner(inputStream);

        StringBuilder output = new StringBuilder();

        try
        {
            N = scanner.NextInt();
        }
        catch
        {
            return;
        }

        if (N == 1)
        {
            Console.WriteLine("0");
            return;
        }

        adj = new List<int>[N];
        for (int i = 0; i < N; i++) adj[i] = new List<int>();

        //Console.WriteLine($"start");

        for (int i = 0; i < N - 1; i++)
        {
            int u = scanner.NextInt();
            int v = scanner.NextInt();
            //Console.WriteLine($"UUUU {u}");
            //Console.WriteLine($"VVVV {v}");

            adj[u].Add(v);
            adj[v].Add(u);
        }
        //Console.WriteLine($"end");

        int U = BfsFindFurthest(0, out _);

        parent = new int[N];
        int V = BfsFindFurthest(U, out _);

        List<int> path = new List<int>();
        int curr = V;
        path.Add(curr);

        while (curr != U)
        {
            curr = parent[curr];
            path.Add(curr);
        }

        List<int> centers = new List<int>();
        int k = path.Count;

        if (k % 2 != 0)
        {
            centers.Add(path[k / 2]);
        }
        else
        {
            centers.Add(path[(k / 2) - 1]);
            centers.Add(path[k / 2]);
        }

        centers.Sort();

        foreach (var center in centers)
        {
            output.AppendLine(center.ToString());
        }
        Console.Write(output);

        if (inputStream is FileStream) inputStream.Close();
    }

    static int BfsFindFurthest(int startNode, out int maxDist)
    {
        Queue<int> q = new Queue<int>();
        q.Enqueue(startNode);

        int[] dist = new int[N];
        for (int i = 0; i < N; i++) dist[i] = -1;

        dist[startNode] = 0;

        if (parent == null) parent = new int[N];
        parent[startNode] = -1;

        int furthestNode = startNode;
        maxDist = 0;

        while (q.Count > 0)
        {
            int u = q.Dequeue();

            if (dist[u] > maxDist)
            {
                maxDist = dist[u];
                furthestNode = u;
            }

            foreach (int v in adj[u])
            {
                if (dist[v] == -1)
                {
                    dist[v] = dist[u] + 1;
                    parent[v] = u;
                    q.Enqueue(v);
                }
            }
        }
        return furthestNode;
    }

    class FastScanner
    {
        Stream stream;
        byte[] buffer = new byte[1024];
        int ptr = 0;
        int len = 0;

        public FastScanner(Stream inputStream)
        {
            this.stream = inputStream;
        }

        byte Read()
        {
            if (ptr == len)
            {
                ptr = 0;
                len = stream.Read(buffer, 0, 1024);
                if (len == 0) return 0;
            }
            return buffer[ptr++];
        }

        public int NextInt()
        {
            byte c = Read();
            while (c <= ' ')
            {
                c = Read();
                if (c == 0) return 0;
            }

            bool negative = false;
            if (c == '-')
            {
                negative = true;
                c = Read();
            }

            int res = 0;
            while (c >= '0' && c <= '9')
            {
                res = res * 10 + c - '0';
                c = Read();
            }
            return negative ? -res : res;
        }
    }
}