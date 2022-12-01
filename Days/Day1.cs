class Day1 {

    public static void Part1(StreamReader sr) {
        string line = sr.ReadLine();
        List<int> elves = new List<int>();
        elves.Add(0);
        int elfIndex = 0;
        while(line != null){
            if(line != "") {
                elves[elfIndex] += int.Parse(line);
            } else {
                elfIndex++;
                elves.Add(0);
            }
            line = sr.ReadLine();
        }

        PriorityQueue<int, int> bigElves = new System.Collections.Generic.PriorityQueue<int, int>();
        int max = int.MinValue;
        foreach(int elf in elves) {
            if(elf > max) max = elf;
        }

        Console.WriteLine(max);
    }

    public static void Part2(StreamReader sr) {
        string line = sr.ReadLine();
        List<int> elves = new List<int>();
        elves.Add(0);
        int elfIndex = 0;
        while(line != null){
            if(line != "") {
                elves[elfIndex] += int.Parse(line);
            } else {
                elfIndex++;
                elves.Add(0);
            }
            line = sr.ReadLine();
        }

        elves.Sort();
        elves.Reverse();

        int total = 0;
        for( int i = 0; i < 3; i++) {
            total += elves[i];
        }

        Console.WriteLine(total);
    }
}