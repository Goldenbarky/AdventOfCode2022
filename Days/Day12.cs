class Day12 {

    public static void Part1(StreamReader sr) {
        char[,] map;
        string line = sr.ReadLine();

        map = new char[line.Length, line.Length];
        sr.DiscardBufferedData();
        sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin); 
        int lineIndex = 0;
        for(line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            for(int i = 0; i < line.Length; i++) {
                map[lineIndex, i] = line[i];
            }

            lineIndex++;
        }

        (int, int)[] points = findPoints(map);

        (int, int) start = points[0];
        (int, int) end = points[1];

        Console.WriteLine(aStar(start, end, map));
    }

    public static (int, int)[] findPoints(char[,] map) {
        (int, int)[] point = new (int, int)[2];
        int set = 0;

        for(int i = 0; i < map.GetLength(0); i++) {
            for(int j = 0; j < map.GetLength(1); j++) {
                if(map[i, j] == 'S') {
                    map[i, j] = 'a';
                    point[0] = (i, j);
                    set++;
                } else if(map[i,j] == 'E') {
                    map[i, j] = 'z';
                    point[1] = (i, j);
                    set++;
                }

                if(set == 2) return point;
            }
        }

        return point;
    }

    public static int aStar((int, int) start, (int, int) end, char[,] map) {
        int[,] costs = new int[map.GetLength(0), map.GetLength(1)];

        for(int i = 0; i < costs.GetLength(0); i++) {
            for(int j = 0; j < costs.GetLength(1); j++) {
                costs[i,j] = int.MaxValue;
            }
        }

        costs[start.Item1, start.Item2] = 0;

        Queue<(int, int)> moves = new Queue<(int, int)>();
        moves.Enqueue(start);

        while(moves.Count > 0) {
            (int, int) move = moves.Dequeue();

            if(move == end) {
                return costs[move.Item1, move.Item2];
            } 

            int x = move.Item1;
            int y = move.Item2;
            char currPos = map[x, y];
            int currCost = costs[x, y];

            //left
            if(x - 1 >= 0 && map[x-1, y] - currPos <= 1 && costs[x-1, y] == int.MaxValue) {
                costs[x-1, y] = currCost + 1;
                moves.Enqueue((x-1, y));
            }
            //right
            if(x + 1 < map.GetLength(0) && map[x+1, y] - currPos <= 1 && costs[x+1, y] == int.MaxValue) {
                costs[x+1, y] = currCost + 1;
                moves.Enqueue((x+1, y));
            }
            //down
            if(y - 1 >= 0 && map[x, y-1] - currPos <= 1 && costs[x, y-1] == int.MaxValue) {
                costs[x, y-1] = currCost + 1;
                moves.Enqueue((x, y-1));
            }
            //up
            if(y + 1 < map.GetLength(0) && map[x, y+1] - currPos <= 1 && costs[x, y+1] == int.MaxValue) {
                costs[x, y+1] = currCost + 1;
                moves.Enqueue((x, y+1));
            }
        }

        return int.MaxValue;
    }

    public static List<(int, int)> findStarts(char[,] map) {
        List<(int, int)> starts = new List<(int, int)>();

        for(int i = 0; i < map.GetLength(0); i++) {
            for(int j = 0; j < map.GetLength(1); j++) {
                if(map[i, j] == 'S' || map[i,j] == 'a') {
                    map[i, j] = 'a';
                    starts.Add((i, j));
                }
            }
        }

        return starts;
    }

    public static (int, int) findEnd(char[,] map) {
        for(int i = 0; i < map.GetLength(0); i++) {
            for(int j = 0; j < map.GetLength(1); j++) {
                if(map[i, j] == 'E') {
                    map[i, j] = 'z';
                    return (i, j);
                }
            }
        }

        return (-1, -1);
    }

    public static void Part2(StreamReader sr) {
        char[,] map;
        string line = sr.ReadLine();

        map = new char[line.Length, line.Length];
        sr.DiscardBufferedData();
        sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin); 
        int lineIndex = 0;
        for(line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            for(int i = 0; i < line.Length; i++) {
                map[lineIndex, i] = line[i];
            }

            lineIndex++;
        }

        List<(int, int)> starts = findStarts(map);
        (int, int) end = findEnd(map);

        int minSolution = int.MaxValue;

        foreach((int, int) start in starts) {
            int solution = aStar(start, end, map);

            if(solution < minSolution) minSolution = solution;
        }

        Console.WriteLine(minSolution);
    }
}