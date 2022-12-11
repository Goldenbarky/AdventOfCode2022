using System.Numerics;

class Day11 {
    public static void Part1(StreamReader sr) {
        List<Monkey> monkeys = new List<Monkey>();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            if(line == "") continue;

            line = line.Trim();
            string[] things = line.Split(" ");

            if(things[0] == "Monkey") {
                monkeys.Add(new Monkey());
                continue;
            }

            Monkey currMonkey = monkeys[monkeys.Count - 1];

            if(things[0] == "Starting") {
                for(int i = 2; i < things.Length; i++) {
                    currMonkey.items.Add(int.Parse(things[i].Replace(",", " ")));
                }
            }

            if(things[0] == "Operation:") {
                string operand = things[4];

                string value = things[5];
                if(value == "old") value = "-1";

                currMonkey.operation = (operand, int.Parse(value));
            }

            if(things[0] == "Test:") {
                currMonkey.test = int.Parse(things[3]);
            }

            if(things[1] == "true:") {
                currMonkey.outTrue = int.Parse(things[5]);
            }

            if(things[1] == "false:") {
                currMonkey.outFalse = int.Parse(things[5]);
            }
        }

        for(int i = 0; i < 20; i++) {
            foreach(Monkey monkey in monkeys) {
                int itemCount = monkey.items.Count;
                for(int j = 0; j < itemCount; j++) {
                    int nextMonkey = monkey.inspect1();
                    Int64 item = monkey.items[0];
                    monkey.items.RemoveAt(0);

                    monkeys[nextMonkey].items.Add(item);
                }
            }
        }

        List<int> interactions = new List<int>();
        foreach(Monkey monkey in monkeys) {
            interactions.Add(monkey.inspections);
        }

        interactions.Sort();
        interactions.Reverse();

        Console.WriteLine(interactions[0] * interactions[1]);
    }

    public static void Part2(StreamReader sr) {
        List<Monkey> monkeys = new List<Monkey>();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            if(line == "") continue;

            line = line.Trim();
            string[] things = line.Split(" ");

            if(things[0] == "Monkey") {
                monkeys.Add(new Monkey());
                continue;
            }

            Monkey currMonkey = monkeys[^1];

            if(things[0] == "Starting") {
                for(int i = 2; i < things.Length; i++) {
                    currMonkey.items.Add(int.Parse(things[i].Replace(",", " ")));
                }
            }

            if(things[0] == "Operation:") {
                string operand = things[4];

                string value = things[5];
                if(value == "old") value = "-1";

                currMonkey.operation = (operand, int.Parse(value));
            }

            if(things[0] == "Test:") {
                currMonkey.test = int.Parse(things[3]);
            }

            if(things[1] == "true:") {
                currMonkey.outTrue = int.Parse(things[5]);
            }

            if(things[1] == "false:") {
                currMonkey.outFalse = int.Parse(things[5]);
            }
        }

        int globalMod = 1;
        foreach(Monkey monkey in monkeys) {
            globalMod *= monkey.test;
        }

        for(int i = 0; i < 10000; i++) {
            foreach(Monkey monkey in monkeys) {
                int itemCount = monkey.items.Count;
                for(int j = 0; j < itemCount; j++) {
                    int nextMonkey = monkey.inspect2(globalMod);
                    Int64 item = monkey.items[0];
                    monkey.items.RemoveAt(0);

                    monkeys[nextMonkey].items.Add(item);
                }
            }
        }

        List<int> interactions = new List<int>();
        foreach(Monkey monkey in monkeys) {
            interactions.Add(monkey.inspections);
        }

        interactions.Sort();
        interactions.Reverse();

        Console.WriteLine((BigInteger) interactions[0] * (BigInteger) interactions[1]);
    }
}

class Monkey {
    public (string, int) operation;
    public List<Int64> items;
    public int test;
    public int outTrue;
    public int outFalse;
    public int inspections;
    public Monkey() {
        items = new List<Int64>();
        operation = (null, 0);
        test = 0;
        outTrue = 0;
        outFalse = 0;
        inspections = 0;
    }

    public int inspect1() {
        inspections++;
        Int64 worry = items[0];

        if(operation.Item1 == "*") {
            if(operation.Item2 == -1) worry *= worry;
            else worry *= operation.Item2;
        } else {
            if(operation.Item2 == -1) worry += worry;
            else worry += operation.Item2;
        }

        worry = (int) Math.Floor((double)(worry / 3));

        items[0] = worry;

        if(worry % test == 0) return outTrue;
        else return outFalse;
    }

    public int inspect2(int mod) {
        inspections++;
        Int64 worry = items[0];

        worry %= mod;

        if (operation.Item1 == "*") {
            if(operation.Item2 == -1) worry *= worry;
            else worry *= operation.Item2;
        } else {
            if(operation.Item2 == -1) worry += worry;
            else worry += operation.Item2;
        }

        items[0] = worry;

        if(worry % test == 0) return outTrue;
        else return outFalse;
    }
}