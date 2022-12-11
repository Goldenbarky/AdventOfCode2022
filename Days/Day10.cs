class Day10 {

    public static void Part1(StreamReader sr) {
        int x = 1;
        Queue<int> values = new Queue<int>();
        List<int> result = new List<int>();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            if(line == "noop") values.Enqueue(0);
            else values.Enqueue(int.Parse(line.Split(" ")[1]));
        }

        int cycle = 1;
        int delay = 1;
        while(values.Count > 0) {
            delay--;
            cycle++;

            if(delay == 0) {
                x += values.Dequeue();
                if(values.Count == 0) break;
                int instruction = values.Peek();

                if(instruction == 0) delay = 1;
                else delay = 2;
            }

            if(cycle < 40) {
                if(cycle == 20) {
                    result.Add(x * cycle);
                    Console.WriteLine(x * cycle);
                }
            } else if((cycle - 20) % 40 == 0) {
                result.Add(x * cycle);
                Console.WriteLine(x * cycle);
            }

        }

        Console.WriteLine(result.Sum());
    }

    public static void Part2(StreamReader sr) {
        int x = 1;
        Queue<int> values = new Queue<int>();
        List<int> picture = new List<int>();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            if(line == "noop") values.Enqueue(0);
            else values.Enqueue(int.Parse(line.Split(" ")[1]));
        }
        
        int cycle = 0;
        int delay = 1;
        while(values.Count > 0) {
            delay--;
            cycle++;

            if(x == ((cycle - 1) % 40) || x - 1 == ((cycle - 1) % 40) || x + 1 == ((cycle - 1) % 40)) picture.Add(1);
            else picture.Add(0);
            
            if(delay == 0) {
                x += values.Dequeue();
                if(values.Count == 0) break;
                int instruction = values.Peek();

                if(instruction == 0) delay = 1;
                else delay = 2;
            }
        }

        for(int i = 0; i < picture.Count; i++) {
            if(i % 40 == 0 && i != 0) Console.WriteLine();

            if(picture[i] == 1) Console.Write("#");
            else Console.Write(".");
        }
    }
}