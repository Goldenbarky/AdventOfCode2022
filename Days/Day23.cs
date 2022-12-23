class Day23 {

    public static void Part1(StreamReader sr) {
        (int, int) xSize = (int.MaxValue, int.MinValue);
        (int, int) ySize = (int.MaxValue, int.MinValue);
        List<(int, int)> map = new List<(int, int)>();

        int row = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            for(int col = 0; col < line.Length; col++) {
                if(line[col] == '#') map.Add((col, row));
                else continue;

                if(col > xSize.Item2) xSize.Item2 = col;
                if(row > ySize.Item2) ySize.Item2 = row;
                if(col < xSize.Item1) xSize.Item1 = col;
                if(row < ySize.Item1) ySize.Item1 = row;
            }
            row++;
        }

        Queue<char> directions = new Queue<char>();
        foreach(char dir in new char[] {'N', 'S', 'W', 'E'}) directions.Enqueue(dir);

        Console.WriteLine("Initial");
        PrintMap(map, 0, 0, xSize.Item2, ySize.Item2);

        for(int i = 0; i < 10; i++) {
            Dictionary<(int, int), List<(int, int)>> proposals = new Dictionary<(int, int), List<(int, int)>>();

            foreach((int, int) elf in map) {

                if(IsAlone(elf, map)) continue;

                foreach(char move in directions) {
                    int x = elf.Item1;
                    int y = elf.Item2;

                    (int, int)[] checks = new (int, int)[3];

                    switch(move) {
                        case 'N':
                            y--;
                            checks[0] = (x-1, y);
                            checks[1] = (x, y);
                            checks[2] = (x+1, y);

                            break;
                        case 'S':
                            y++;
                            checks[0] = (x-1, y);
                            checks[1] = (x, y);
                            checks[2] = (x+1, y);

                            break;
                        case 'W':
                            x--;
                            checks[0] = (x, y-1);
                            checks[1] = (x, y);
                            checks[2] = (x, y+1);

                            break;
                        case 'E':
                            x++;
                            checks[0] = (x, y-1);
                            checks[1] = (x, y);
                            checks[2] = (x, y+1);
                            break;
                    }

                    (int, int) spot = (x, y);

                    if(checks.All(x => !map.Contains(x))) {
                        if(!proposals.ContainsKey(spot)) {
                            proposals.Add(spot, new List<(int, int)>());
                        }

                        proposals[spot].Add(elf);
                        break;
                    }
                }
            }

            foreach((int, int) spot in proposals.Keys) {
                if(proposals[spot].Count == 1) {
                    (int, int) elf = proposals[spot][0];
                    
                    map.Remove(elf);
                    map.Add(spot);

                    if(spot.Item1 < xSize.Item1) xSize.Item1 = spot.Item1;
                    else if(spot.Item1 > xSize.Item2) xSize.Item2 = spot.Item1;
                    else if(spot.Item2 < ySize.Item1) ySize.Item1 = spot.Item2;
                    else if(spot.Item2 > ySize.Item2) ySize.Item2 = spot.Item2;
                }
            }

            directions.Enqueue(directions.Dequeue());
            
            //Console.WriteLine("Round " + (i + 1));
            //PrintMap(map, xSize.Item1, ySize.Item1, xSize.Item2, ySize.Item2);
        }

        int xMax = int.MinValue, yMax = int.MinValue;
        int xMin = int.MaxValue, yMin = int.MaxValue;

        foreach((int, int) point in map) {
            int x = point.Item1;
            int y = point.Item2;

            if(point.Item1 < xMin) xMin = point.Item1;
            if(point.Item1 > xMax) xMax = point.Item1;
            if(point.Item2 < yMin) yMin = point.Item2;
            if(point.Item2 > yMax) yMax = point.Item2;
        }

        //Console.WriteLine("Final");
        //PrintMap(map, xMin, yMin, xMax, yMax);

        int width = xMax - xMin + 1;
        int height = yMax - yMin + 1;

        Console.WriteLine((width * height) - map.Count);
    }

    public static void Part2(StreamReader sr) {
        (int, int) xSize = (int.MaxValue, int.MinValue);
        (int, int) ySize = (int.MaxValue, int.MinValue);
        List<(int, int)> map = new List<(int, int)>();

        int row = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            for(int col = 0; col < line.Length; col++) {
                if(line[col] == '#') map.Add((col, row));
                else continue;

                if(col > xSize.Item2) xSize.Item2 = col;
                if(row > ySize.Item2) ySize.Item2 = row;
                if(col < xSize.Item1) xSize.Item1 = col;
                if(row < ySize.Item1) ySize.Item1 = row;
            }
            row++;
        }

        Queue<char> directions = new Queue<char>();
        foreach(char dir in new char[] {'N', 'S', 'W', 'E'}) directions.Enqueue(dir);

        Console.WriteLine("Initial");
        PrintMap(map, 0, 0, xSize.Item2, ySize.Item2);

        int round = 1;
        while(true) {
            Dictionary<(int, int), List<(int, int)>> proposals = new Dictionary<(int, int), List<(int, int)>>();

            foreach((int, int) elf in map) {

                if(IsAlone(elf, map)) continue;

                foreach(char move in directions) {
                    int x = elf.Item1;
                    int y = elf.Item2;

                    (int, int)[] checks = new (int, int)[3];

                    switch(move) {
                        case 'N':
                            y--;
                            checks[0] = (x-1, y);
                            checks[1] = (x, y);
                            checks[2] = (x+1, y);

                            break;
                        case 'S':
                            y++;
                            checks[0] = (x-1, y);
                            checks[1] = (x, y);
                            checks[2] = (x+1, y);

                            break;
                        case 'W':
                            x--;
                            checks[0] = (x, y-1);
                            checks[1] = (x, y);
                            checks[2] = (x, y+1);

                            break;
                        case 'E':
                            x++;
                            checks[0] = (x, y-1);
                            checks[1] = (x, y);
                            checks[2] = (x, y+1);
                            break;
                    }

                    (int, int) spot = (x, y);

                    if(checks.All(x => !map.Contains(x))) {
                        if(!proposals.ContainsKey(spot)) {
                            proposals.Add(spot, new List<(int, int)>());
                        }

                        proposals[spot].Add(elf);
                        break;
                    }
                }
            }

            foreach((int, int) spot in proposals.Keys) {
                if(proposals[spot].Count == 1) {
                    (int, int) elf = proposals[spot][0];
                    
                    map.Remove(elf);
                    map.Add(spot);

                    if(spot.Item1 < xSize.Item1) xSize.Item1 = spot.Item1;
                    else if(spot.Item1 > xSize.Item2) xSize.Item2 = spot.Item1;
                    else if(spot.Item2 < ySize.Item1) ySize.Item1 = spot.Item2;
                    else if(spot.Item2 > ySize.Item2) ySize.Item2 = spot.Item2;
                }
            }

            directions.Enqueue(directions.Dequeue());
            
            //Console.WriteLine("Round " + (i + 1));
            //PrintMap(map, xSize.Item1, ySize.Item1, xSize.Item2, ySize.Item2);

            if(proposals.Count == 0) break;
            round++;
        }

        Console.WriteLine("Final");
        PrintMap(map, xSize.Item1, ySize.Item1, xSize.Item2, ySize.Item2);

        Console.WriteLine(round);
    }

    public static bool IsAlone((int, int) elf, List<(int, int)> map) {
        int x = elf.Item1;
        int y = elf.Item2;

        for(int row = -1; row <= 1; row++) {
            for(int col = -1; col <= 1; col++) {
                if(row == 0 && col == 0) continue;

                if(map.Contains((x + col, y + row))) return false;
            }
        }

        return true;
    }

    public static void PrintMap(List<(int, int)> map, int xMin, int yMin, int xMax, int yMax) {
        for(int j = yMin; j <= yMax; j++) {
            for(int i = xMin; i <= xMax; i++) {
                if(map.Contains((i, j))) Console.Write("#");
                else Console.Write(".");
            }
            Console.WriteLine();
        }
    }

}