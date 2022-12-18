class Day17 {

    public static void Part1(StreamReader sr) {
        List<bool[]> cave = new List<bool[]>();
        Queue<List<(int, int)>> shapes = new Queue<List<(int, int)>>();
        Queue<char> air = new Queue<char>();

        shapes.Enqueue(new List<(int, int)> {
            (0, 0), (1, 0), (2, 0), (3, 0)
        });

        shapes.Enqueue(new List<(int, int)> {
            (0, 0), (1, 0), (2, 0), (1, -1), (1, 1)
        });

        shapes.Enqueue(new List<(int, int)> {
            (0, 0), (1, 0), (2, 0), (2, 1), (2, 2)
        });

        shapes.Enqueue(new List<(int, int)> {
            (0, 0), (0, 1), (0, 2), (0, 3)
        });

        shapes.Enqueue(new List<(int, int)> {
            (0, 0), (0, 1), (1, 0), (1, 1)
        });

        string line = sr.ReadLine();
        foreach(char ch in line) {
            air.Enqueue(ch);
        }

        int trimmed = 0;

        for(int i = 0; i < 2022; i++) {
            List<(int, int)> shape = shapes.Dequeue();
            shapes.Enqueue(shape);

            //get shape height
            int shapeMax = int.MinValue;
            int shapeMin = int.MaxValue;
            shape.ForEach(x => shapeMax = Math.Max(shapeMax, x.Item2));
            shape.ForEach(x => shapeMin = Math.Min(shapeMin, x.Item2));

            int shapeHeight = shapeMax - shapeMin + 1;

            int height = cave.Count + 3 - shapeMin;
            int xCoord = 2;

            while(height >= 0) {
                while(cave.Count <= height + shapeMax) {
                    cave.Add(new bool[7]);
                }

                char wind = air.Dequeue();
                air.Enqueue(wind);

                int direction = (wind == '<') ? -1 : 1;

                if(shape.All(x => (x.Item1 + xCoord + direction >= 0) && (xCoord + x.Item1 + direction < 7) && !cave[height + x.Item2][x.Item1 + xCoord + direction])) {
                    xCoord += direction;
                }

                if(shape.All(x => (height + x.Item2 - 1) >= 0 && !cave[height + x.Item2 - 1][x.Item1 + xCoord])) {
                    height--;
                } else {
                    break;
                }
            }

            shape.ForEach(x => cave[height + x.Item2][x.Item1 + xCoord] = true);

            TrimEmpty(cave);
            trimmed += TrimBottom(cave);
        }

        Console.WriteLine(cave.Count + trimmed);
    }

    public static void Part2(StreamReader sr) {
        List<bool[]> cave = new List<bool[]>();
        Queue<List<(int, int)>> shapes = new Queue<List<(int, int)>>();
        Queue<char> air = new Queue<char>();
        Queue<List<(int, int)>> shapesCopy = new Queue<List<(int, int)>>();
        Queue<char> airCopy = new Queue<char>();

        shapes.Enqueue(new List<(int, int)> {
            (0, 0), (1, 0), (2, 0), (3, 0)
        });

        shapes.Enqueue(new List<(int, int)> {
            (0, 0), (1, 0), (2, 0), (1, -1), (1, 1)
        });

        shapes.Enqueue(new List<(int, int)> {
            (0, 0), (1, 0), (2, 0), (2, 1), (2, 2)
        });

        shapes.Enqueue(new List<(int, int)> {
            (0, 0), (0, 1), (0, 2), (0, 3)
        });

        shapes.Enqueue(new List<(int, int)> {
            (0, 0), (0, 1), (1, 0), (1, 1)
        });

        shapesCopy.Enqueue(new List<(int, int)> {
            (0, 0), (1, 0), (2, 0), (3, 0)
        });

        shapesCopy.Enqueue(new List<(int, int)> {
            (0, 0), (1, 0), (2, 0), (1, -1), (1, 1)
        });

        shapesCopy.Enqueue(new List<(int, int)> {
            (0, 0), (1, 0), (2, 0), (2, 1), (2, 2)
        });

        shapesCopy.Enqueue(new List<(int, int)> {
            (0, 0), (0, 1), (0, 2), (0, 3)
        });

        shapesCopy.Enqueue(new List<(int, int)> {
            (0, 0), (0, 1), (1, 0), (1, 1)
        });

        string line = sr.ReadLine();
        foreach(char ch in line) {
            air.Enqueue(ch);
            airCopy.Enqueue(ch);
        }

        long globalMod = air.Count * shapes.Count;
        long trimmed = DropRocks(cave, globalMod, air, shapes);
        long previous = trimmed + cave.Count;

        long rocksDropped = globalMod;
        long testAmount = 1000000000000;

        List<int> shifts = new List<int>();
        List<int> shiftPattern = null;

        while(shiftPattern == null) {
            trimmed += DropRocks(cave, globalMod, air, shapes);
            rocksDropped += globalMod;

            shifts.Add((int) ((trimmed + cave.Count) - previous));

            if(shifts.Count % 2 == 0) {
                List<int> one = new List<int>();
                List<int> two = new List<int>();

                one = shifts.GetRange(0, shifts.Count / 2);
                two = shifts.GetRange(shifts.Count / 2, shifts.Count / 2);

                bool same = true;
                for(int i = 0; i < one.Count && same; i++) {
                    same = one[i] == two[i];
                }

                if(same)
                    shiftPattern = one;
            }

            previous = cave.Count + trimmed;
        }

        Console.WriteLine("Found cycle after dropping " + rocksDropped);

        long projection = 0;
        int numSets = shiftPattern.Count;
        int cycleAmount = shiftPattern.Sum();
        while(rocksDropped + (globalMod * numSets) <= testAmount) {
            rocksDropped += (globalMod * numSets);
            projection += cycleAmount;
        }

        long rocksLeft = testAmount - rocksDropped;
        Console.WriteLine("Finished cycle after dropping " + rocksDropped + ", dropping last " + rocksLeft);
        trimmed += DropRocks(cave, rocksLeft, air, shapes);

        Console.WriteLine("Cave height of " + (projection));

        Console.WriteLine("Simulating actual now for comparison...");

        //List<bool[]> cave2 = new List<bool[]>();
        //int calculatedHeight = DropRocks(cave2, testAmount, airCopy, shapesCopy) + cave2.Count;

        //Console.WriteLine("Actual simulation after " + testAmount + " rocks is " + calculatedHeight);
        Console.WriteLine("Cached simulation after " + testAmount + " rocks is " + (projection + trimmed + cave.Count));
    }

    public static void TrimEmpty(List<bool[]> cave) {
        while(cave[^1].All(x => !x)) {
            cave.RemoveAt(cave.Count - 1);
        }
    }

    public static int TrimBottom(List<bool[]> cave) {
        if(cave.Count <= 100) return 0;

        int trimmed = cave.Count - 100;
        cave.RemoveRange(0, trimmed);

        return trimmed;
    }

    public static int DropRocks(List<bool[]> cave, long rockLimit, Queue<char> air, Queue<List<(int, int)>> shapes) {
        int trimmed = 0;

        for(Int64 i = 0; i < rockLimit; i++) {
            List<(int, int)> shape = shapes.Dequeue();
            shapes.Enqueue(shape);

            //get shape height
            int shapeMax = int.MinValue;
            int shapeMin = int.MaxValue;
            shape.ForEach(x => shapeMax = Math.Max(shapeMax, x.Item2));
            shape.ForEach(x => shapeMin = Math.Min(shapeMin, x.Item2));

            int shapeHeight = shapeMax - shapeMin + 1;

            int height = cave.Count + 3 - shapeMin;
            int xCoord = 2;

            while(height >= 0) {
                while(cave.Count <= height + shapeMax) {
                    cave.Add(new bool[7]);
                }

                char wind = air.Dequeue();
                air.Enqueue(wind);

                int direction = (wind == '<') ? -1 : 1;

                if(shape.All(x => (x.Item1 + xCoord + direction >= 0) && (xCoord + x.Item1 + direction < 7) && !cave[height + x.Item2][x.Item1 + xCoord + direction])) {
                    xCoord += direction;
                }

                if(shape.All(x => (height + x.Item2 - 1) >= 0 && !cave[height + x.Item2 - 1][x.Item1 + xCoord])) {
                    height--;
                } else {
                    break;
                }
            }

            shape.ForEach(x => cave[height + x.Item2][x.Item1 + xCoord] = true);

            TrimEmpty(cave);
            
            trimmed += TrimBottom(cave);
        }

        return trimmed;
    }

    public static long DropRocksCached(List<bool[]> cave, long rockLimit, Queue<char> air, Queue<List<(int, int)>> shapes) {
        long trimmed = 0;
        long globalMod = air.Count * shapes.Count;
        long droppedRocks = 0;

        Dictionary<bool[][], (int, bool[][])> cache = new Dictionary<bool[][], (int, bool[][])>();

        while(droppedRocks + globalMod <= rockLimit) {
            bool[][] initialCaveState = cave.ToArray();


            foreach(bool[][] state in cache.Keys) {
                if(ArraysAreEqual(state, initialCaveState)) {
                    initialCaveState = state;
                    break;
                }
            }

            if(cache.ContainsKey(initialCaveState)) {
                (int difference, bool[][] temp) = cache[initialCaveState];
                trimmed += difference;
                cave = temp.ToList();
            } else {
                int heightAdded = DropRocks(cave, globalMod, air, shapes);
                cache.Add(initialCaveState, (heightAdded, cave.ToArray()));
                trimmed += heightAdded;
            }

            droppedRocks += globalMod;
        }

        if(droppedRocks < rockLimit) {
            trimmed += DropRocks(cave, rockLimit - droppedRocks, air, shapes);
        }

        return trimmed;
    }

    public static bool ArraysAreEqual(bool[][] a, bool[][] b) {
        for(int i = 0; i < a.Length; i++) {
            for(int j = 0; j < a[i].Length; j++) {
                if(a[i][j] != b[i][j]) return false;
            }
        }
        
        return true;
    }
}