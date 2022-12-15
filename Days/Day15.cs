class Day15 {

    public static void Part1(StreamReader sr) {
        Dictionary<(int, int), (int, int)> beaconPair = new Dictionary<(int, int), (int, int)>();
        HashSet<(int, int)> beacons = new HashSet<(int, int)>();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] values = line.Split(":");

            string sensor = values[0];
            string beacon = values[1];

            (int, int) sensorCoords = (0,0);

            string value = sensor.Split(",")[0];
            sensorCoords.Item1 = int.Parse(value.Substring(value.IndexOf("x=") + 2));
            value = sensor.Split(",")[1];
            sensorCoords.Item2 = int.Parse(value.Substring(value.IndexOf("y=") + 2));

            (int, int) beaconCoords = (0,0);

            value = beacon.Split(",")[0];
            beaconCoords.Item1 = int.Parse(value.Substring(value.IndexOf("x=") + 2));
            value = beacon.Split(",")[1];
            beaconCoords.Item2 = int.Parse(value.Substring(value.IndexOf("y=") + 2));

            beaconPair.Add(sensorCoords, beaconCoords);
            beacons.Add(beaconCoords);
        }

        Dictionary<int, HashSet<int>> beaconMap = new Dictionary<int, HashSet<int>>();
        beaconMap.Add(2000000, new HashSet<int>());

        foreach((int, int) sensor in beaconPair.Keys) {
            markImpossible(sensor, beaconPair[sensor], beaconMap, beacons, 2000000);
        }

        Console.WriteLine(beaconMap[2000000].Count);
    }

    public static int getManDistance((int, int) coord1, (int, int) coord2) {
        int distance = 0;
        distance += Math.Abs(coord1.Item1 - coord2.Item1);
        distance += Math.Abs(coord1.Item2 - coord2.Item2);

        return distance;
    }

    public static void markImpossible((int, int) sensor, (int, int) beacon, Dictionary<int, HashSet<int>> map, HashSet<(int, int)> beacons, int row) {
        int distance = getManDistance(sensor, beacon);

        int sensorX = sensor.Item1;
        int sensorY = sensor.Item2;

        if(row < sensorY + distance && row > sensorY - distance) {
            int distanceDiff = Math.Abs(row - sensorY);

            for(int x = 0; x <= (distance - distanceDiff); x++) {
                if(!beacons.Contains((sensorX + x, row)))
                    map[row].Add(sensorX + x);
                if(!beacons.Contains((sensorX - x, row)))
                    map[row].Add(sensorX - x);
            }
        }

        return;
    }

    public static void Part2(StreamReader sr) {
        Dictionary<(int, int), (int, int)> beaconPair = new Dictionary<(int, int), (int, int)>();
        HashSet<(int, int)> beacons = new HashSet<(int, int)>();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] values = line.Split(":");

            string sensor = values[0];
            string beacon = values[1];

            (int, int) sensorCoords = (0,0);

            string value = sensor.Split(",")[0];
            sensorCoords.Item1 = int.Parse(value.Substring(value.IndexOf("x=") + 2));
            value = sensor.Split(",")[1];
            sensorCoords.Item2 = int.Parse(value.Substring(value.IndexOf("y=") + 2));

            (int, int) beaconCoords = (0,0);

            value = beacon.Split(",")[0];
            beaconCoords.Item1 = int.Parse(value.Substring(value.IndexOf("x=") + 2));
            value = beacon.Split(",")[1];
            beaconCoords.Item2 = int.Parse(value.Substring(value.IndexOf("y=") + 2));

            beaconPair.Add(sensorCoords, beaconCoords);
            beacons.Add(beaconCoords);
        }

        (int, int) distressBeacon = findDistressBeacon(beaconPair, beacons, 20);

        Console.WriteLine("(" + distressBeacon.Item1 + ", " + distressBeacon.Item2 + ")");
        Console.WriteLine(((Int64) 4000000 * distressBeacon.Item1) + distressBeacon.Item2);
    }

    public static PriorityQueue<(int, int), int> sortScannerPriority(int x, int y, List<(int, int)> scanners) {
        PriorityQueue<(int, int), int> sortedScanners = new PriorityQueue<(int, int), int>();

        foreach((int, int) scanner in scanners) {
            int distance = getManDistance((x, y), scanner);
            sortedScanners.Enqueue(scanner, distance);
        }

        return sortedScanners;
    }

    public static (int, int) findDistressBeacon(Dictionary<(int, int), (int, int)> beaconPair, HashSet<(int, int)> beacons, int beaconRange) {
        for(int j = 0; j <= beaconRange; j++) {
            for(int i = 0; i <= beaconRange; i++) {
                PriorityQueue<(int, int), int> sortedScanners = sortScannerPriority(i, j, beaconPair.Keys.ToList());

                bool distressLoc = true;
                while(sortedScanners.Count > 0 && distressLoc) {
                    (int, int) scanner = sortedScanners.Dequeue();
                    (int, int) beacon = beaconPair[scanner];

                    int range = getManDistance(scanner, beacon);

                    if(range >= getManDistance((i, j), scanner)) {
                        distressLoc = false;

                        int distanceScanner = Math.Abs(i - scanner.Item1);
                        int distanceDiff = Math.Abs(Math.Abs(j - scanner.Item2) - range);

                        int iSkip = distanceDiff + distanceScanner;
                        i += iSkip;
                    }
                }

                if(distressLoc) {
                    return (i, j);
                }
            }
        }

        return (0, 0);
    }
}