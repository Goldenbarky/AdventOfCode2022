class Day25 {

    public static void Part1(StreamReader sr) {
        SNAFUConverter converter = new SNAFUConverter();
        List<Int128> numbers = new List<Int128>();

        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            numbers.Add(SNAFUConverter.SNAFUConvert(line));
        }

        //numbers.ForEach(x => Console.WriteLine(converter.DecimalConvert(x)));

        Int128 sum = 0;
        numbers.ForEach(x => sum += x);

        Console.WriteLine(converter.DecimalConvert(sum));
    }

    public static void Part2(StreamReader sr) {
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            
        }
    }
}

class SNAFUConverter {
    Dictionary<Int128, string> SNAFUTable;

    public SNAFUConverter() {
        SNAFUTable = new Dictionary<Int128, string>();

        SNAFUTable.Add(0, "0");
        SNAFUTable.Add(1, "1");
        SNAFUTable.Add(2, "2");
        SNAFUTable.Add(3, "1=");
        SNAFUTable.Add(4, "1-");
        SNAFUTable.Add(5, "10");
        SNAFUTable.Add(6, "11");
        SNAFUTable.Add(7, "12");
        SNAFUTable.Add(8, "2=");
        SNAFUTable.Add(9, "2-");
    }

    public string Invert(string number) {
        string newNum = "";
        for(int i = 0; i < number.Length; i++) {
            switch(number[i]) {
                case '1':
                    newNum += '-';
                    break;
                case '2':
                    newNum += '=';
                    break;
                case '-':
                    newNum += '1';
                    break;
                case '=':
                    newNum += '2';
                    break;
                case '0':
                    newNum += '0';
                    break;
            }
        }

        Int128 num = SNAFUConvert(newNum);
        SNAFUTable.TryAdd(num, newNum);

        return newNum;
    }

    public static Int128 SNAFUConvert(string number) {
        Int128 total = 0;
        for(int i = 0; i < number.Length; i++) {
            Int128 place = (Int128) Math.Pow(5, number.Length - 1 - i);

            switch(number[i]) {
                case '0':
                    break;
                case '1':
                    total += place;
                    break;
                case '2':
                    total += (place * 2);
                    break;
                case '-':
                    total -= place;
                    break;
                case '=':
                    total -= (place * 2);
                    break;
            }
        }

        return total;
    }

    public string DecimalConvert(Int128 number) {
        if(SNAFUTable.ContainsKey(number)) return SNAFUTable[number];

        int maxPow = 0;
        Int128 maxNum = 0;
        while(true) {
            Int128 nextInc = (Int128) Math.Pow(5, maxPow) * 2;
            if(maxNum + nextInc < number) {
                maxPow++;
                maxNum += nextInc;
            } else break;
        }

        Int128 placeNum = (Int128) Math.Pow(5, maxPow);

        char prepend;
        string append;
        if(placeNum > number) {
            prepend = '1';
            append = Invert(DecimalConvert(placeNum - number));
        } else if(placeNum + (placeNum / 2) < number) {
            prepend = '2';
            
            if(number > placeNum * 2) {
                append = DecimalConvert(number - (placeNum * 2));
            } else {
                append = Invert(DecimalConvert((placeNum * 2) - number));
            }
        } else {
            prepend = '1';
            append = DecimalConvert(number - placeNum);
        }

        while(append.Length < maxPow) {
            append = append.Insert(0, "0");
        }

        string final = string.Concat(prepend, append);
        SNAFUTable.TryAdd(number, final);

        return final;
    }
}