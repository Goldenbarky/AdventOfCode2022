class Day6 {

    public static void Part1(StreamReader sr) {
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            Queue<char> packet = new Queue<char>();

            for(int index = 0; index < line.Length; index++){
                if(index < 3){
                    packet.Enqueue(line[index]);
                    continue;
                }

                
                packet.Enqueue(line[index]);

                if(areAllDifferentOne(packet)){
                    Console.WriteLine(index + 1);
                    break;
                }

                packet.Dequeue();
            }
        }
    }

    public static bool areAllDifferentOne(Queue<char> packet) {
        HashSet<char> set = new HashSet<char>();
        foreach(char character in packet){
            set.Add(character);
        }

        return set.Count == 4;
    }

    public static void Part2(StreamReader sr) {
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            Queue<char> packet = new Queue<char>();

            for(int index = 0; index < line.Length; index++){
                if(index < 13){
                    packet.Enqueue(line[index]);
                    continue;
                }

                
                packet.Enqueue(line[index]);

                if(areAllDifferentTwo(packet)){
                    Console.WriteLine(index + 1);
                    break;
                }

                packet.Dequeue();
            }
        }
    }

    public static bool areAllDifferentTwo(Queue<char> packet) {
        HashSet<char> set = new HashSet<char>();
        foreach(char character in packet){
            set.Add(character);
        }

        return set.Count == 14;
    }
}