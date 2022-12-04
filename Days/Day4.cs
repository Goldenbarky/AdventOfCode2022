class Day4 {

    public static void Part1(StreamReader sr) {
        int num = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] elves = line.Split(',');

            string[] elf1 = elves[0].Split('-');
            string[] elf2 = elves[1].Split('-');

            if(int.Parse(elf1[0]) <= int.Parse(elf2[0]) && int.Parse(elf1[1]) >= int.Parse(elf2[1]) ||
            int.Parse(elf2[0]) <= int.Parse(elf1[0]) && int.Parse(elf2[1]) >= int.Parse(elf1[1])) {
                num++;
            }
        }

        Console.WriteLine(num);
    }

    public static void Part2(StreamReader sr) {
        int num = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] elves = line.Split(',');

            string[] elf1 = elves[0].Split('-');
            string[] elf2 = elves[1].Split('-');

            List<int> elfone = new List<int>();
            List<int> elftwo = new List<int>();

            for(int i = int.Parse(elf1[0]); i <= int.Parse(elf1[1]); i++) {
                elfone.Add(i);
            }
            for(int i = int.Parse(elf2[0]); i <= int.Parse(elf2[1]); i++) {
                elftwo.Add(i);
            }

            foreach(int area in elfone){
                if(elftwo.Contains(area)) {
                    num++;
                    break;
                }
            }
        }
        Console.WriteLine(num);
    }
}