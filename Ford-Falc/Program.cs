namespace Ford_Falc
{
    internal class FordFalc
    {
        static int len;

        int maxFlow(int[,] graph, int source, int sink)
        {

            int[,] residualGraph = new int[len, len];
            for (int i = 0; i < len; i++)
                for (int j = 0; j < len; j++)
                    residualGraph[i, j] = graph[i, j];

            int[] parent = new int[len];

            int maxFlow = 0;

            while (bfs(residualGraph, source, sink, parent))
            {
                int pathFlow = int.MaxValue;
                for (int j = sink; j != source; j = parent[j])
                {
                    int i = parent[j];
                    pathFlow = Math.Min(pathFlow, residualGraph[i, j]);
                }

                for (int j = sink; j != source; j = parent[j])
                {
                    int i = parent[j];
                    residualGraph[i, j] -= pathFlow;
                    residualGraph[j, i] += pathFlow;
                }

                maxFlow += pathFlow;
            }

            return maxFlow;
        }

        // Функция для поиска пути от источника до стока с помощью BFS
        bool bfs(int[,] residualGraph, int source, int sink, int[] parent)
        {
            bool[] visited = new bool[len];
            for (int i = 0; i < len; ++i)
                visited[i] = false;

            Queue<int> queueue = new Queue<int>();
            queueue.Enqueue(source);
            visited[source] = true;
            parent[source] = -1;

            while (queueue.Count != 0)
            {
                int i = queueue.Dequeue();

                for (int j = 0; j < len; j++)
                {
                    if (visited[j] == false && residualGraph[i, j] > 0)
                    {
                        queueue.Enqueue(j);
                        parent[j] = i;
                        visited[j] = true;
                    }
                }
            }

            // Возвращаем true, если достигли стока в ходе поиска пути
            return (visited[sink] == true);
        }

        // Пример использования
        static void Main()
        {
            // Исходный граф
            int[,] graph = new int[,] { { 0, 16, 13, 0, 0, 0 },
                                    { 0, 0, 10, 12, 0, 0 },
                                    { 0, 4, 0, 0, 14, 0 },
                                    { 0, 0, 9, 0, 0, 20 },
                                    { 0, 0, 0, 7, 0, 4 },
                                    { 0, 0, 0, 0, 0, 0 } };

            FordFalc m = new FordFalc();
            len = Convert.ToInt32(Math.Sqrt(graph.Length));
            Console.WriteLine("Максимальный поток: " + m.maxFlow(graph, 0, 5));

            Console.ReadKey();
        }
    }
}