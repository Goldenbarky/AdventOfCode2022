class Day13 {

    public static void Part1(StreamReader sr) {
        List<(container, container)> pairs = new List<(container, container)>();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            container pair1 = createList(line, 1).Item1;
            line = sr.ReadLine();
            container pair2 = createList(line, 1).Item1;

            pairs.Add((pair1, pair2));
            sr.ReadLine(); //throw away newline
        }

        int sum = 0;
        for(int i = 0; i < pairs.Count; i++) {
            int val = compare(pairs[i].Item1, pairs[i].Item2);

            if(val == 1) sum += i + 1;
        }

        Console.WriteLine(sum);
    }

    //returns container and new index
    public static (container, int) createList(string line, int index) {
        container list = new container();
        while(index < line.Length) {
            string item;
            int endItem = line.IndexOf(',', index);
            if(endItem != -1) item = line.Substring(index, endItem - index);
            else item = line.Substring(index);
            if(item[0] == '[') {
                container newItem = new container();
                (newItem, index) = createList(line, index+1);
                list.addItem(newItem);
                if(line[index] == ',') index++;
                continue;
            }
            
            int endList = item.IndexOf(']');
            if(endList != -1) item = item.Substring(0, endList);

            if(item != "") list.addItem(int.Parse(item));

            index += item.Length;

            if(line[index] == ']') {
                return (list, index+1);
            }

            index++;
        }

        return (list, index);
    }

    //returns 1 if right order, -1 if wrong, 0 if continue
    public static int compare(object left, object right) {
        if(left is int && right is int) {
            if((int) left == (int) right) return 0;
            if((int) left < (int) right) return 1;
            else return -1;
        } else if(left is container && right is container) {
            container Left = (container) left;
            container Right = (container) right;
            for(int i = 0; true; i++) {
                if(Left.getLength() == Right.getLength()) {
                    if(i >= Left.getLength()) return 0;
                }

                if(i >= Left.getLength()) return 1;
                else if(i >= Right.getLength()) return -1;

                object leftItem = Left.getItem(i);
                object rightItem = Right.getItem(i);

                int val = compare(leftItem, rightItem);

                if(val == 0) continue;
                else return val;
            }
        } else if(left is container) {
            container temp = new container();
            temp.addItem(right);
            return compare(left, temp);
        } else if(right is container) {
            container temp = new container();
            temp.addItem(left);
            return compare(temp, right);
        }

        return -1;
    }

    public static void Part2(StreamReader sr) {
        List<container> packets = new List<container>();

        container sub1 = new container();
        container decoder1 = new container();

        sub1.addItem(2);
        decoder1.addItem(sub1);
        packets.Add(decoder1);

        container sub2 = new container();
        container decoder2 = new container();

        sub2.addItem(6);
        decoder2.addItem(sub2);
        packets.Add(decoder2);
        
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            if(line == "") continue;
            container packet = createList(line, 1).Item1;
            packets.Add(packet);
            //packets = binarySort(packet, packets);
        }

        packets.Sort(new comparer());

        int score = (packets.IndexOf(decoder1) + 1) * (packets.IndexOf(decoder2) + 1);
        Console.WriteLine(score);
    }

    public static List<container> binarySort(container packet, List<container> packets) {
        int window = packets.Count;
        int index = (int) packets.Count / 2;

        while(window > 0) {
            int val = compare(packet, packets[index]);

            window = (int) Math.Floor((double) window / 2);

            if(val == 1) {
                if(window == 0) break;

                if(window == 1) index -= 1;
                else index -= (int) Math.Floor((double) window / 2);
            } else if(val == -1) {
                if(window == 0) break;
                if(window == 1) index += 1;
                else index += (int) Math.Floor((double) window / 2);
            }
        }

        packets.Insert(index, packet);

        return packets;
    }
}

class comparer : IComparer<container> {
    public int Compare(container x, container y)
    {
        return -Day13.compare(x, y);
        throw new NotImplementedException();
    }
}

class container{
    List<object> items;

    public container() {
        items = new List<object>();
    }

    public void addItem(object item) {
        items.Add(item);
    }

    public int getLength() {
        return items.Count;
    }

    public object getItem(int index) {
        return items[index];
    }

    public List<object> GetList() {
        return items;
    }
}