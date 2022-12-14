using System.Collections;

class Day16 {
    public static void Part1(StreamReader sr) {
        Dictionary<string, Room> rooms = new Dictionary<string, Room>();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            (string name, int flow, List<string> connected) = parseInput(line);

            List<Room> connectedRooms = new List<Room>();
            foreach(string room in connected) {
                if(!rooms.Keys.Contains(room)) {
                    rooms.Add(room, new global::Room(room));
                }

                connectedRooms.Add(rooms[room]);
            }

            if(!rooms.Keys.Contains(name)) {
                rooms.Add(name, new global::Room(name));
            }

            rooms[name].flow = flow;
            rooms[name].addConnected(connectedRooms);
        }

        Stack path = new Stack();
        path.Push(rooms["AA"]);

        int best = BruteForce(rooms, new PressureCache(), rooms["AA"], 30, new List<string>());

        //(int timeLeft, int flowRate, int pressure) = CalculateOptimal(rooms["AA"], rooms, 30, 0, 0);

        //pressure += timeLeft * flowRate;
        Console.WriteLine(best);
    } 

    public static void Part2(StreamReader sr) {
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            
        }
    }

    public static (string, int, List<string>) parseInput(string line) {
        string name = line.Substring(6, 2);
        int flow = int.Parse(line.Substring(line.IndexOf('=')+1).Split(";")[0]);

        string valves = line.Split(";")[1];
        valves = valves.Substring(valves.IndexOf("valve"));

        List<string> connected = new List<string>();
        foreach(string valve in valves.Split(" ")) {
            string nextValve = valve;
            if(valve[0] == 'v') {
                continue;
            }

            nextValve.Trim();
            nextValve = nextValve.Replace(",", "");
            connected.Add(nextValve);
        }

        return (name, flow, connected);
    }

    public static int BruteForce(Dictionary<string, Room> rooms, PressureCache pressureCache, Room current, int timeLeft, List<string> openValves) {
        if(timeLeft <= 0) {
            return 0;
        }

        //Check cached
        string[] openValvesArray = openValves.ToArray();
        if(pressureCache.bestScores.ContainsKey((openValvesArray, current, timeLeft))) return pressureCache.bestScores[(openValvesArray, current, timeLeft)];

        int score = 0;
        //Open Valve
        if(current.flow > 0 && !current.open) {
            //Choose
            timeLeft--;
            score = timeLeft * current.Open();
            openValves.Add(current.name);
        }

        Queue<Room> valves = SortRooms(current, rooms, pressureCache);

        int bestScore = 0;
        //Try every other room
        while(valves.Count > 0) {
            Room room = valves.Dequeue();

            int distance = pressureCache.Distance(current.name, room.name, rooms);

            if(distance > timeLeft) continue;

            //Choose
            timeLeft -= distance;

            //Explore
            bestScore = Math.Max(BruteForce(rooms, pressureCache, room, timeLeft, openValves), bestScore);

            //Unchoose
            timeLeft += distance;
        }

        score += bestScore;
        pressureCache.bestScores.Add((openValvesArray, current, timeLeft), bestScore);

        //Unchoose
        current.Close();

        return score;
    }

    public static List<Room> ConvertQueue(Queue path) {
        List<Room> queue = new List<Room>();
        Queue temp = (Queue)path.Clone();

        while(temp.Count > 0) {
            queue.Add((Room)temp.Dequeue());
        }

        return queue;
    }

    public static Queue<Room> SortRooms(Room current, Dictionary<string, Room> rooms, PressureCache distanceCounter) {
        PriorityQueue<Room, double> priorityQueue = new PriorityQueue<Room, double>();

        foreach(Room room in rooms.Values) {
            if(current == room || room.flow == 0 || room.open) continue;

            int distance = distanceCounter.Distance(current.name, room.name, rooms);
            double distanceHeuristic = room.flow / distance;

            priorityQueue.Enqueue(room, distanceHeuristic);
        }

        Queue<Room> queue = new Queue<Room>();
        while(priorityQueue.Count > 0) {
            queue.Enqueue(priorityQueue.Dequeue());
        }

        return new Queue<Room>(queue.Reverse());

    }
}

class Room {
    public string name;
    public int flow;
    List<Room> connectedRooms; 
    public bool open;

    public Room(string name) {
        this.name = name;
        this.flow = 0;
        connectedRooms = new List<Room>();
        this.open = false;
    }

    public void setFlow(int flow) {
        this.flow = flow;
    }

    public int Open() {
        open = true;
        return flow;
    }

    public int Close() {
        open = false;
        return flow;
    }

    public void addConnected(List<Room> connected) {
        connectedRooms = connected;
    }

    public List<Room> GetRooms() {
        return connectedRooms;
    }
}

class PressureCache {
    Dictionary<(string, string), int> distances;
    public Dictionary<(string[], Room, int), int> bestScores;

    public PressureCache() {
        distances = new Dictionary<(string, string), int>();
        bestScores = new Dictionary<(string[], Room, int), int>();
    }

    public int Distance(string room1, string room2, Dictionary<string, Room> rooms) {
        if(distances.ContainsKey((room1, room2))) {
            return distances[(room1, room2)];
        }

        return Calculate(room1, room2, rooms);
    }

    private int Calculate(string room1, string room2, Dictionary<string, Room> rooms) {

        if(room1 == room2) return 0;

        Queue<Room> searchQueue = new Queue<Room>();
        Dictionary<Room, int> roomDistances = new Dictionary<Room, int>();
        searchQueue.Enqueue(rooms[room1]);
        roomDistances.Add(rooms[room1], 0);

        while(searchQueue.Count > 0) {
            Room current = searchQueue.Dequeue();
            int distance = roomDistances[current];

            if(current == rooms[room2]) {
                distances.Add((room1, room2), distance);
                distances.Add((room2, room1), distance);
                return distance;
            }

            List<Room> nearby = current.GetRooms();

            foreach(Room room in nearby) {
                searchQueue.Enqueue(room);
                if(!roomDistances.ContainsKey(room)) {
                    roomDistances.Add(room, roomDistances[current] + 1);
                }
            }
        }

        return -1;
    }
}