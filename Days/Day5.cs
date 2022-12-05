class Day5 {

    public static void Part1(StreamReader sr) {
        List<string> crates = new List<string>();
        Stack<char>[] stacks = new Stack<char>[9];

        stacks[0] = new Stack<char>();
        stacks[1] = new Stack<char>();
        stacks[2] = new Stack<char>();
        stacks[3] = new Stack<char>();
        stacks[4] = new Stack<char>();
        stacks[5] = new Stack<char>();
        stacks[6] = new Stack<char>();
        stacks[7] = new Stack<char>();
        stacks[8] = new Stack<char>();


        string line = "";
        for(line = sr.ReadLine(); line.Contains('['); line = sr.ReadLine()) {
            crates.Add(line);
        }

        foreach(string crate in crates){

            for(int i = 0; i < crate.Length; i++) {
                if(char.IsLetter(crate[i])) {
                    stacks[int.Parse(line[i].ToString()) - 1].Push(crate[i]);
                }
            }
        }

        for(int i = 0; i < stacks.Length; i++){
            Stack<char> newStack = new Stack<char>();
            while(stacks[i].Count > 0){
                newStack.Push(stacks[i].Pop());
            }
            stacks[i] = newStack;
        }

        line = sr.ReadLine();

        for(line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string instructions = "";
            string[] groups = line.Split(" ");
            int index = 0;
            int numOfCrates = 0;
            foreach(string group in groups) {
                    if(group.All(char.IsNumber)){
                        if(index == 0){
                            numOfCrates = int.Parse(group);
                            index++;
                        } else {
                            instructions += group;
                        }
                    }
                }

            while(numOfCrates > 0) {
                char letter = stacks[int.Parse(instructions[0].ToString()) - 1].Pop();
                stacks[int.Parse(instructions[1].ToString()) - 1].Push(letter);
                numOfCrates -= 1;
            }
        }

        foreach(Stack<char> stack in stacks) {
            Console.WriteLine(stack.Peek());
        }
    }

    public static void Part2(StreamReader sr) {
        List<string> crates = new List<string>();
        Stack<char>[] stacks = new Stack<char>[9];

        stacks[0] = new Stack<char>();
        stacks[1] = new Stack<char>();
        stacks[2] = new Stack<char>();
        stacks[3] = new Stack<char>();
        stacks[4] = new Stack<char>();
        stacks[5] = new Stack<char>();
        stacks[6] = new Stack<char>();
        stacks[7] = new Stack<char>();
        stacks[8] = new Stack<char>();


        string line = "";
        for(line = sr.ReadLine(); line.Contains('['); line = sr.ReadLine()) {
            crates.Add(line);
        }

        foreach(string crate in crates){

            for(int i = 0; i < crate.Length; i++) {
                if(char.IsLetter(crate[i])) {
                    stacks[int.Parse(line[i].ToString()) - 1].Push(crate[i]);
                }
            }
        }

        for(int i = 0; i < stacks.Length; i++){
            Stack<char> newStack = new Stack<char>();
            while(stacks[i].Count > 0){
                newStack.Push(stacks[i].Pop());
            }
            stacks[i] = newStack;
        }

        line = sr.ReadLine();

        for(line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string instructions = "";
            string[] groups = line.Split(" ");
            int index = 0;
            int numOfCrates = 0;
            foreach(string group in groups) {
                    if(group.All(char.IsNumber)){
                        if(index == 0){
                            numOfCrates = int.Parse(group);
                            index++;
                        } else {
                            instructions += group;
                        }
                    }
                }

            Stack<char> tempStack = new Stack<char>();
            int cratesToGrab = numOfCrates;
            while(cratesToGrab > 0) {
                char letter = stacks[int.Parse(instructions[0].ToString()) - 1].Pop();
                tempStack.Push(letter);
                cratesToGrab -= 1;
            }

            while(numOfCrates > 0) {
                char letter = tempStack.Pop();
                stacks[int.Parse(instructions[1].ToString()) - 1].Push(letter);
                numOfCrates -= 1;
            }
        }

        foreach(Stack<char> stack in stacks) {
            Console.Write(stack.Peek());
        }
    }
}