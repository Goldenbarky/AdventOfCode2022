class Day24 {
    public static char[] directions = new char[4] {'^', 'v', '>', '<'};
    public static (int, int)[] moves = new (int, int)[5] {(1, 0), (-1, 0), (0, 1), (0, -1), (0, 0)};

    public static void Part1(StreamReader sr) {
        List<(int, int, char)> blizzards = new List<(int, int, char)>();

        int row = 0;
        int lineSize = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            for(int col = 0; col < line.Length; col++) {
                if(directions.Contains(line[col])) blizzards.Add((col, row, line[col]));
            }

            row++;
            lineSize = line.Length;
        }

        (int, int) end = (lineSize - 2, row - 1);
        BlizzardTracker tracker = new BlizzardTracker(blizzards, lineSize - 1, row - 1);

        //Queue of each dijkstras state, currPos, blizzard pos, step#
        PriorityQueue<((int, int), int), int> nodes = new PriorityQueue<((int, int), int), int>();
        nodes.Enqueue(((1, 0), 0), 0);

        int cost = 0;
        while(true) {
            ((int, int) pos, int num) = nodes.Dequeue();

            if(pos == end) {
                cost = num;
                break;
            }

            List<(int, int, char)> nextStorm = tracker.GetMapState(num + 1);

            int movesMade = 0;
            foreach((int, int) move in moves) {
                int priority = 0;
                (int, int) next = (pos.Item1 + move.Item1, pos.Item2 + move.Item2);

                if((next.Item1 <= 0 || next.Item1 >= lineSize - 1 || next.Item2 <= 0 || next.Item2 >= row) && next != (1, 0)) continue;
                if(move == (0, 0) && movesMade > 0) break;

                bool open = true;
                foreach((int, int, char) blizzard in nextStorm) {
                    if(blizzard.Item1 == next.Item1 && blizzard.Item2 == next.Item2) {
                        open = false;
                        break;
                    }
                }

                if(open) {
                    priority = getManDistance(next, end);
                    priority += num;

                    nodes.Enqueue((next, num + 1), priority);
                    movesMade++;
                }
            }
        }

        Console.WriteLine(cost);
    }

    public static void Part2(StreamReader sr) {
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            
        }
    }

    public static int getManDistance((int, int) coord1, (int, int) coord2) {
        int distance = 0;
        distance += Math.Abs(coord1.Item1 - coord2.Item1);
        distance += Math.Abs(coord1.Item2 - coord2.Item2);

        return distance;
    }
}

class BlizzardTracker {
    Dictionary<int, (int, int, char)[]> states;
    List<(int, int, char)> initial;
    int boundsX;
    int boundsY;

    public BlizzardTracker(List<(int, int, char)> initial, int boundsX, int boundsY) {
        this.initial = initial;
        this.boundsX = boundsX;
        this.boundsY = boundsY;
        states = new Dictionary<int, (int, int, char)[]>();

        states.Add(0, initial.ToArray());
    }

    public List<(int, int, char)> GetMapState(int time) {
        if(states.ContainsKey(time)) return states[time].ToList();

        List<(int, int, char)> nextMap = GetMapState(time - 1);

        for(int i = 0; i < nextMap.Count; i++) {
            nextMap[i] = MoveBlizzard(nextMap[i], boundsX, boundsY);
        }

        states.Add(time, nextMap.ToArray());

        return nextMap;
    }

    public static (int, int, char) MoveBlizzard((int, int, char) blizzard, int boundsX, int boundsY) {
        int x = blizzard.Item1;
        int y = blizzard.Item2;
        char dir = blizzard.Item3;

        switch(dir) {
            case '>':
                x++;
                break;
            case '<':
                x--;
                break;
            case '^':
                y--;
                break;
            case 'v':
                y++;
                break;
        }

        if(x <= 0) x = boundsX - 1;
        else if(x >= boundsX) x = 1;
        else if(y <= 0) y = boundsY - 1;
        else if(y >= boundsY) y = 1;

        return (x, y, dir);
    }
}