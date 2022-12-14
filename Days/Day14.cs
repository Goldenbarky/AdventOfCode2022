class Day14 {

    public static void Part1(StreamReader sr) {
        HashSet<(int, int)> map = new HashSet<(int, int)>();
        int bottom = 0;

        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] values = line.Split(" -> ");

            for(int i = 1; i < values.Length; i++) {
                (int, int) coord1 = (int.Parse(values[i-1].Split(',')[0]), int.Parse(values[i-1].Split(',')[1]));
                (int, int) coord2 = (int.Parse(values[i].Split(',')[0]), int.Parse(values[i].Split(',')[1]));

                if(coord1.Item1 - coord2.Item1 != 0) {
                    int xMax = Math.Max(coord1.Item1, coord2.Item1);
                    int xMin = Math.Min(coord1.Item1, coord2.Item1);
                    for(int x = xMin; x <= xMax; x++) {
                        map.Add((x, coord1.Item2));
                    }

                    if(coord1.Item2 > bottom) bottom = coord1.Item2;
                } else {
                    int yMax = Math.Max(coord1.Item2, coord2.Item2);
                    int yMin = Math.Min(coord1.Item2, coord2.Item2);
                    for(int y = yMin; y <= yMax; y++) {
                        map.Add((coord1.Item1, y));
                    }

                    if(yMax > bottom) bottom = yMax;
                }
            }
        }

        int sand = 0;
        while(true) {
            int x = 500;
            int y = 0;

            while(!map.Contains((x, y))) {
                y++;
            }

            y = y-1;

            if(!stepDown1(x, y, map, bottom)) {
                break;
            } else sand++;
        }

        Console.WriteLine(sand);
    }

    public static bool stepDown1(int x, int y, HashSet<(int, int)> map, int bottom) {
        if(y > bottom) return false;

        if(!map.Contains((x, y+1))) {
            return stepDown1(x, y+1, map, bottom);
        }

        if(!map.Contains((x-1, y+1))) {
            return stepDown1(x-1, y+1, map, bottom);
        }

        if(!map.Contains((x+1, y+1))) {
            return stepDown1(x+1, y+1, map, bottom);
        }

        map.Add((x, y));
        return true;
    }

    public static void stepDown2(int x, int y, HashSet<(int, int)> map, int bottom) {
        if(y+1 == bottom + 2) {
            map.Add((x, y));
            return;
        }

        if(!map.Contains((x, y+1))) {
            stepDown2(x, y+1, map, bottom);
            return;
        }

        if(!map.Contains((x-1, y+1))) {
            stepDown2(x-1, y+1, map, bottom);
            return;
        }

        if(!map.Contains((x+1, y+1))) {
            stepDown2(x+1, y+1, map, bottom);
            return;
        }

        map.Add((x, y));
    }

    public static void Part2(StreamReader sr) {
        HashSet<(int, int)> map = new HashSet<(int, int)>();
        int bottom = 0;

        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] values = line.Split(" -> ");

            for(int i = 1; i < values.Length; i++) {
                (int, int) coord1 = (int.Parse(values[i-1].Split(',')[0]), int.Parse(values[i-1].Split(',')[1]));
                (int, int) coord2 = (int.Parse(values[i].Split(',')[0]), int.Parse(values[i].Split(',')[1]));

                if(coord1.Item1 - coord2.Item1 != 0) {
                    int xMax = Math.Max(coord1.Item1, coord2.Item1);
                    int xMin = Math.Min(coord1.Item1, coord2.Item1);
                    for(int x = xMin; x <= xMax; x++) {
                        map.Add((x, coord1.Item2));
                    }

                    if(coord1.Item2 > bottom) bottom = coord1.Item2;
                } else {
                    int yMax = Math.Max(coord1.Item2, coord2.Item2);
                    int yMin = Math.Min(coord1.Item2, coord2.Item2);
                    for(int y = yMin; y <= yMax; y++) {
                        map.Add((coord1.Item1, y));
                    }

                    if(yMax > bottom) bottom = yMax;
                }
            }
        }

        int sand = 0;
        while(true) {
            int x = 500;
            int y = 0;

            if(map.Contains((x, y))) break;

            while(!map.Contains((x, y))) {
                y++;
            }

            y = y-1;

            stepDown2(x, y, map, bottom);
                
            sand++;
        }

        Console.WriteLine(sand);
    }
}