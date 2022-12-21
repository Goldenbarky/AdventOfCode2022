class Day21 {

    public static void Part1(StreamReader sr) {
        Dictionary<string, RiddleMonkey> mathMonkeys = new Dictionary<string, RiddleMonkey>();
        Dictionary<string, Int128> simpleMonkeys = new Dictionary<string, Int128>();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] split = line.Split(":");

            string name = split[0];

            split = line.Substring(name.Length + 2).Split(" ");
            if(split.Length > 1) {
                string one = split[0];
                string two = split[2];

                char sign = split[1][0];

                mathMonkeys.Add(name, new RiddleMonkey(name, one, two, sign));
            } else {
                simpleMonkeys.Add(name, int.Parse(split[0]));
            }
        }

        Stack<RiddleMonkey[]> toSolve = new Stack<RiddleMonkey[]>();
        List<RiddleMonkey> monkeys = new List<RiddleMonkey>() {mathMonkeys["root"]};
        toSolve.Push(monkeys.ToArray());

        bool solving = true;
        while(solving) {
            RiddleMonkey[] math = toSolve.Peek();
            monkeys = new List<RiddleMonkey>();

            foreach(RiddleMonkey monkey in math) {
                (string, string) reference = monkey.reference;

                if(!simpleMonkeys.ContainsKey(reference.Item1)) {
                    monkeys.Add(mathMonkeys[reference.Item1]);
                }

                if(!simpleMonkeys.ContainsKey(reference.Item2)) {
                    monkeys.Add(mathMonkeys[reference.Item2]);
                }
            }

            if(monkeys.Count == 0) solving = false;
            else toSolve.Push(monkeys.ToArray());
        }

        while(toSolve.Count > 0) {
            monkeys = toSolve.Pop().ToList();

            foreach(RiddleMonkey monkey in monkeys) {
                mathMonkeys.Remove(monkey.name);

                (string, string) reference = monkey.reference;

                Int128 one = simpleMonkeys[reference.Item1];
                Int128 two = simpleMonkeys[reference.Item2];

                Int128 value = 0;
                switch(monkey.sign) {
                    case '+':
                        value = one + two;
                        break;
                    case '-':
                        value = one - two;
                        break;
                    case '*':
                        value = one * two;
                        break;
                    case '/':
                        value = one / two;
                        break;
                }

                simpleMonkeys.Add(monkey.name, value);
            }
        }

        Console.WriteLine(simpleMonkeys["root"]);
    }

    public static void Part2(StreamReader sr) {
        Dictionary<string, RiddleMonkey> mathMonkeys = new Dictionary<string, RiddleMonkey>();
        Dictionary<string, Int128> simpleMonkeys = new Dictionary<string, Int128>();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] split = line.Split(":");

            string name = split[0];

            split = line.Substring(name.Length + 2).Split(" ");
            if(split.Length > 1) {
                string one = split[0];
                string two = split[2];

                char sign = split[1][0];

                mathMonkeys.Add(name, new RiddleMonkey(name, one, two, sign));
            } else {
                simpleMonkeys.Add(name, int.Parse(split[0]));
            }
        }

        simpleMonkeys["humn"] = 0;

        Stack<RiddleMonkey[]> toSolve = new Stack<RiddleMonkey[]>();
        List<RiddleMonkey> monkeys = new List<RiddleMonkey>() {mathMonkeys["root"]};
        toSolve.Push(monkeys.ToArray());

        bool solving = true;
        while(solving) {
            RiddleMonkey[] math = toSolve.Peek();
            monkeys = new List<RiddleMonkey>();

            foreach(RiddleMonkey monkey in math) {
                (string, string) reference = monkey.reference;

                if(!simpleMonkeys.ContainsKey(reference.Item1)) {
                    monkeys.Add(mathMonkeys[reference.Item1]);
                }

                if(!simpleMonkeys.ContainsKey(reference.Item2)) {
                    monkeys.Add(mathMonkeys[reference.Item2]);
                }
            }

            if(monkeys.Count == 0) solving = false;
            else toSolve.Push(monkeys.ToArray());
        }

        while(toSolve.Count > 0) {
            monkeys = toSolve.Pop().ToList();

            foreach(RiddleMonkey monkey in monkeys) {
                mathMonkeys.Remove(monkey.name);

                (string, string) reference = monkey.reference;

                Int128 one = simpleMonkeys[reference.Item1];
                Int128 two = simpleMonkeys[reference.Item2];

                Int128 value = 0;
                if(one != 0 && two != 0) {
                    switch(monkey.sign) {
                        case '+':
                            value = one + two;
                            break;
                        case '-':
                            value = one - two;
                            break;
                        case '*':
                            value = one * two;
                            break;
                        case '/':
                            value = one / two;
                            break;
                    }
                } else {
                    mathMonkeys.Add(monkey.name, monkey);
                }

                simpleMonkeys.Add(monkey.name, value);
            }
        }

        RiddleMonkey monkey1 = mathMonkeys["root"];
        Int128 number = 0;
        if(mathMonkeys.ContainsKey(monkey1.reference.Item1)) {
            number = simpleMonkeys[monkey1.reference.Item2];
            monkey1 = mathMonkeys[monkey1.reference.Item1];
        } else {
            number = simpleMonkeys[monkey1.reference.Item1];
            monkey1 = mathMonkeys[monkey1.reference.Item2];
        }

        while(true) {
            (string, string) reference = monkey1.reference;

            string one = reference.Item1;
            string two = reference.Item2;

            if(mathMonkeys.ContainsKey(one) || one == "humn") {
                Int128 num = simpleMonkeys[two];

                switch(monkey1.sign) {
                    case '+':
                        number = number - num;
                        break;
                    case '-':
                        number = number + num;
                        break;
                    case '*':
                        number = number / num;
                        break;
                    case '/':
                        number = number * num;
                        break;
                }
                if(one == "humn") break;
                monkey1 = mathMonkeys[one];
            } else {
                Int128 num = simpleMonkeys[one];

                switch(monkey1.sign) {
                    case '+':
                        number = number - num;
                        break;
                    case '-':
                        number = num - number;
                        break;
                    case '*':
                        number = number / num;
                        break;
                    case '/':
                        number = num / number;
                        break;
                }
                if(two == "humn") break;
                monkey1 = mathMonkeys[two];
            }
        }

        Console.WriteLine(number);
    }
}

class RiddleMonkey {
    public string name;
    public (string, string) reference;
    public char sign;

    public RiddleMonkey(string name, string one, string two, char sign) {
        this.name = name;
        this.reference = (one, two);
        this.sign = sign;
    }
}