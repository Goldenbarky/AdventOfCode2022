class Day18 {

    public static void Part1(StreamReader sr) {
        Lava lava = new Lava();

        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] lineSplit = line.Split(",");

            int x = int.Parse(lineSplit[0]);
            int y = int.Parse(lineSplit[1]);
            int z = int.Parse(lineSplit[2]);

            lava.AddDropplet(new Dropplet(x, y, z));
        }

        int exposedSides = 0;
        foreach(Dropplet dropplet in lava.Dropplets()) {
            exposedSides += 6 - dropplet.GetConnected().Count;
        }

        Console.WriteLine(exposedSides);
    }

    public static void Part2(StreamReader sr) {
        Lava lava = new Lava();

        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] lineSplit = line.Split(",");

            int x = int.Parse(lineSplit[0]);
            int y = int.Parse(lineSplit[1]);
            int z = int.Parse(lineSplit[2]);

            lava.AddDropplet(new Dropplet(x, y, z));
        }

        FillAirPockets(lava);        

        int exposedSides = 0;

        foreach(Dropplet dropplet in lava.Dropplets()) {
            exposedSides += 6 - dropplet.GetConnected().Count;
        }

        Console.WriteLine(exposedSides);
    }
    
    public static void AddDropplet(int x, int y, int z, Dictionary<(int, int, int), int> lava) {
        lava.Add((x, y, z), 6);

            if(lava.ContainsKey((x-1, y, z))) {
                lava[(x-1, y, z)]--;
                lava[(x, y, z)]--;
            }
            if(lava.ContainsKey((x+1, y, z))) {
                lava[(x+1, y, z)]--;
                lava[(x, y, z)]--;
            }
            if(lava.ContainsKey((x, y-1, z))) {
                lava[(x, y-1, z)]--;
                lava[(x, y, z)]--;
            }
            if(lava.ContainsKey((x, y+1, z))) {
                lava[(x, y+1, z)]--;
                lava[(x, y, z)]--;
            }
            if(lava.ContainsKey((x, y, z-1))) {
                lava[(x, y, z-1)]--;
                lava[(x, y, z)]--;
            }
            if(lava.ContainsKey((x, y, z+1))) {
                lava[(x, y, z+1)]--;
                lava[(x, y, z)]--;
            }
    }

    public static void FillAirPockets(Lava lava) {
        int xMax = int.MinValue;
        int xMin = int.MaxValue;
        int yMax = int.MinValue;
        int yMin = int.MaxValue;
        int zMax = int.MinValue;
        int zMin = int.MaxValue;

        foreach((int, int, int) dropplet in lava.lava.Keys) {
            int x = dropplet.Item1;
            int y = dropplet.Item2;
            int z = dropplet.Item3;
            
            xMax = Math.Max(x, xMax);
            xMin = Math.Min(x, xMin);
            yMax = Math.Max(y, yMax);
            yMin = Math.Min(y, yMin);
            zMax = Math.Max(z, zMax);
            zMin = Math.Min(z, zMin);
        }

        for(int x = xMin + 1; x < xMax; x++) {
            for(int y = yMin + 1; y < yMax; y++) {
                for(int z = zMin + 1; z < zMax; z++) {
                    if(Unchecked(x, y, z, lava) && !FindEdge((x, y, z), lava, xMin, xMax, yMin, yMax, zMin, zMax)) {
                        TurnToAir((x, y, z), lava);
                    }
                }
            }
        }
    }

    public static bool FindEdge((int, int, int) start, Lava lava, int xMin, int xMax, int yMin, int yMax, int zMin, int zMax) {
        Queue<(int, int, int)> nodes = new Queue<(int, int, int)>();
        List<(int, int, int)> searched = new List<(int, int, int)>();
        nodes.Enqueue(start);
        searched.Add(start);

        while(nodes.Count > 0) {
            (int x, int y, int z) = nodes.Dequeue();

            if(lava.edgeAir.Contains((x, y, z)) || x <= xMin || x >= xMax || y <= yMin || y >= yMax || z <= zMin || z >= zMax) {
                foreach((int, int, int) position in searched) {
                    lava.edgeAir.Add(position);
                }
                return true;
            } 

            foreach((int, int, int) next in new List<(int, int, int)>() {
            (x+1, y, z),
            (x-1, y, z),
            (x, y+1, z),
            (x, y-1, z),
            (x, y, z+1),
            (x, y, z-1)}
            ) {
                if(!lava.lava.ContainsKey(next)) {
                    if(!searched.Contains(next)) {
                        nodes.Enqueue(next);
                        searched.Add(next);
                    }
                }
            }
        }
        return false;
    }

    public static void TurnToAir((int, int, int) coord, Lava lava) {
        int x = coord.Item1;
        int y = coord.Item2;
        int z = coord.Item3;

        Dropplet target = new Dropplet(x, y, z);
        lava.AddDropplet(target);

        foreach((int, int, int) next in new List<(int, int, int)>() {
            (x+1, y, z),
            (x-1, y, z),
            (x, y+1, z),
            (x, y-1, z),
            (x, y, z+1),
            (x, y, z-1)}
        ) {
            if(!lava.lava.ContainsKey(next)) {
                TurnToAir(next, lava);
            }
        }
    }

    public static bool Unchecked(int x, int y, int z, Lava lava) {
        return !lava.lava.ContainsKey((x, y, z)) && !lava.edgeAir.Contains((x, y, z));
    }
}

class Dropplet {
    public int x;
    public int y;
    public int z;

    List<Dropplet> connected;

    public Dropplet(int x, int y, int z) {
        this.x = x;
        this.y = y;
        this.z = z;

        connected = new List<Dropplet>();
    }

    public void AddConnected(Dropplet neighbor) {
        connected.Add(neighbor);
    }

    public List<Dropplet> GetConnected() {
        return connected;
    }
}

class Lava {
    public Dictionary<(int, int, int), Dropplet> lava;
    public List<(int, int, int)> edgeAir;

    public Lava() {
        lava = new Dictionary<(int, int, int), Dropplet>();
        edgeAir = new List<(int, int, int)>();
    }

    public Dropplet GetDropplet(int x, int y, int z){
        return lava[(x, y, z)];
    }

    public void AddDropplet(Dropplet dropplet) {
        int x = dropplet.x;
        int y = dropplet.y;
        int z = dropplet.z;

        lava.Add((x, y, z), dropplet);

        foreach((int, int, int) next in new List<(int, int, int)>() {
            (x+1, y, z),
            (x-1, y, z),
            (x, y+1, z),
            (x, y-1, z),
            (x, y, z+1),
            (x, y, z-1)}
        ) {
            if(lava.ContainsKey(next)) {
                lava[next].AddConnected(dropplet);
                dropplet.AddConnected(lava[next]);
            }
        }
    }

    public List<Dropplet> Dropplets() {
        return lava.Values.ToList();
    }
}