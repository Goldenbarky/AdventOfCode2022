class Day3 {

    public static void Part1(StreamReader sr) {
        
        int sum = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            int length = line.Length;
            string compartment1 = line.Substring(0, length/2);
            string compartment2 = line.Substring(length/2);

            int score = 0;
            foreach(char one in compartment1){
                foreach(char two in compartment2){
                    if(one == two) {
                        if(char.IsUpper(one)) score += 26;
                        score += (int)char.ToLower(one) - 96;
                        goto end;
                    }
                }
            }
            end:

            sum += score;
            Console.WriteLine(score);
        }
        Console.WriteLine(sum);

    }

    public static void Part2(StreamReader sr) {
        int sum = 0;
        while(true) {
            string[] elves = new string[3];

            for(int i = 0; i < 3; i++) {
                string line = sr.ReadLine();
                if(line == null) goto realEnd;
                elves[i] = line;
            }

            char badge = '0';
            foreach(char char1 in elves[0]) {
                if(elves[1].Contains(char1) && elves[2].Contains(char1)){
                    badge = char1;
                    goto end;
                } 
            }
            foreach(char char2 in elves[1]) {
                if(elves[0].Contains(char2) && elves[2].Contains(char2)){
                    badge = char2;
                    goto end;
                } 
            }
            foreach(char char3 in elves[2]) {
                if(elves[0].Contains(char3) && elves[1].Contains(char3)){
                    badge = char3;
                    goto end;
                } 
            }
            Console.WriteLine("uhhhhhh this shouldnt be possible. oops.");
            
            end:

            int score = 0;
            if(char.IsUpper(badge)) score += 26;
            score += (int)char.ToLower(badge) - 96;
            sum += score;
            Console.WriteLine(score);
        }
        realEnd:
        Console.WriteLine(sum);
    }
}