class Day9 {

    public enum Direction { up, down, left, right }

    public static void Part1(StreamReader sr) {
        int[] headPos = new int[2];
        int[] tailPos = new int[2];
        HashSet<(int, int)> cells = new HashSet<(int, int)>();

        headPos[0] = 0;
        headPos[1] = 0;
        tailPos[0] = 0;
        tailPos[1] = 0;

        cells.Add((0, 0));

        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] split = line.Split(" ");

            string dir = split[0];
            int distance = int.Parse(split[1]);

            Direction direction;

            switch(dir) {
                case "R": direction = Direction.right; break;
                case "L": direction = Direction.left; break;
                case "U": direction = Direction.up; break;
                case "D": direction = Direction.down; break;
                default: Console.WriteLine("uh oh"); return;
            }

            for(int i = distance; i > 0; i--) {
                headPos = moveHead(headPos, direction);
                if(!areAdjacent(headPos, tailPos)) tailPos = moveTail(headPos, tailPos);
                cells.Add((tailPos[0], tailPos[1]));
            }

        }

        Console.WriteLine(cells.Count);
    }

    public static bool areAdjacent(int[] headPos, int[] tailPos){
        if(headPos.Equals(tailPos)) {
            return true;
        }

        int vertDist = Math.Abs(headPos[1] - tailPos[1]);
        int horDist = Math.Abs(headPos[0] - tailPos[0]);

        return vertDist <= 1 && horDist <= 1;
    }

    public static int[] moveHead(int[] headPos, Direction dir){
        switch(dir){
            case Direction.up: 
                headPos[1]++;
                break;
            case Direction.down:
                headPos[1]--;
                break;
            case Direction.left:
                headPos[0]--;
                break;
            case Direction.right:
                headPos[0]++;
                break;
            }

        return headPos;
    }

    public static int[] moveTail(int[] headPos, int[] tailPos) {
        int vertDist = headPos[1] - tailPos[1];
        int horDist = headPos[0] - tailPos[0];

        int moveVert = 0;
        int moveHor = 0;

        //same row or col
        if(vertDist != 0) {
            if(vertDist < 0) {
                moveVert--;
            } else {
                moveVert++;
            }
        }
        
        if (horDist != 0) {
            if(horDist < 0) {
                moveHor--;
            } else {
                moveHor++;
            }
        }

        tailPos[0] += moveHor;
        tailPos[1] += moveVert;

        return tailPos;
    }

    public static void Part2(StreamReader sr) {
        List<int[]> knots = new List<int[]>();
        HashSet<(int, int)> cells = new HashSet<(int, int)>();

        for(int i = 0; i < 10; i++) {
            knots.Add(new int[2]);
        }

        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] split = line.Split(" ");

            string dir = split[0];
            int distance = int.Parse(split[1]);

            Direction direction;

            switch(dir) {
                case "R": direction = Direction.right; break;
                case "L": direction = Direction.left; break;
                case "U": direction = Direction.up; break;
                case "D": direction = Direction.down; break;
                default: Console.WriteLine("uh oh"); return;
            }

            for(int i = distance; i > 0; i--) {
                for(int knotNum = 0; knotNum < knots.Count - 1; knotNum++) {
                    int[] headPos = knots[knotNum];
                    int[] tailPos = knots[knotNum + 1];

                    if(knotNum == 0) headPos = moveHead(headPos, direction);
                    
                    if(!areAdjacent(headPos, tailPos)) tailPos = moveTail(headPos, tailPos);

                    if(knotNum + 1 == 9) cells.Add((tailPos[0], tailPos[1]));
                }
            }
        }

        Console.WriteLine(cells.Count);
    }
}