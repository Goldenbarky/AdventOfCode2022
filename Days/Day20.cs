class Day20 {

    public static void Part1(StreamReader sr) {
        Queue<(int, int)> coords = new Queue<(int, int)>();
        int index = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            coords.Enqueue((int.Parse(line), index++));
        }

        List<(int, int)> decrypted = coords.ToList();
        while(coords.Count > 0) {
            (int, int) num = coords.Dequeue();
            MoveNumOne(decrypted, num);
        }

        decrypted.ForEach(x=> Console.Write(x + ", "));

        index = FindZero(decrypted);

        int length = decrypted.Count;
        int score = decrypted[Modulo(index + 1000, length)].Item1 + decrypted[Modulo(index + 2000, length)].Item1 + decrypted[Modulo(index + 3000, length)].Item1;

        Console.WriteLine();
        Console.WriteLine(score);
    }

    public static void Part2(StreamReader sr) {
        Queue<(Int128, int)> coords = new Queue<(Int128, int)>();
        int index = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            Int128 number = (Int128) int.Parse(line);
            number *= 811589153;
            coords.Enqueue((number, index++));
        }

        List<(Int128, int)> decrypted = coords.ToList();
        for(int i = 0; i < coords.Count * 10; i++) {
            (Int128, int) num = coords.Dequeue();
            MoveNumTwo(decrypted, num);
            coords.Enqueue(num);
        }

        decrypted.ForEach(x=> Console.Write(x + ", "));

        index = FindZero(decrypted);

        int length = decrypted.Count;
        Int128 score = decrypted[Modulo(index + 1000, length)].Item1 + decrypted[Modulo(index + 2000, length)].Item1 + decrypted[Modulo(index + 3000, length)].Item1;

        Console.WriteLine();
        Console.WriteLine(score);
    }

    public static int Modulo(int a, int b) {
        return ((a % b) + b) % b;
    }
    public static int Modulo(Int128 a, int b) {
        return (int) ((a % b) + b) % b;
    }

    public static void MoveNumOne(List<(int, int)> coords, (int, int) node) {
        int index = coords.IndexOf(node);
        int number = Modulo(node.Item1, coords.Count - 1);

        coords.RemoveAt(index);
        int newIndex = Modulo(index + number, coords.Count);

        coords.Insert(newIndex, (node.Item1, index));
    }

    public static void MoveNumTwo(List<(Int128, int)> coords, (Int128, int) node) {
        int index = coords.IndexOf(node);
        int number = Modulo(node.Item1, coords.Count - 1);

        coords.RemoveAt(index);
        int newIndex = Modulo(index + number, coords.Count);

        coords.Insert(newIndex, node);
    }

    public static int FindZero(List<(int, int)> numbers) {
        for(int i = 0; i < numbers.Count; i++) {
            if(numbers[i].Item1 == 0) return i;
        }

        return -1;
    }

    public static int FindZero(List<(Int128, int)> numbers) {
        for(int i = 0; i < numbers.Count; i++) {
            if(numbers[i].Item1 == 0) return i;
        }

        return -1;
    }
}