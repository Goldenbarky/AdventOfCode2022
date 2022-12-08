class Day8 {

    public static void Part1(StreamReader sr) {
        string line = sr.ReadLine();
        int[,] treeMap = new int[line.Length, line.Length];

        for(int i = 0; i < line.Length; i++) {
            for(int j = 0; j < line.Length; j++) {
                treeMap[i, j] = int.Parse(line[j].ToString());
            }

            line = sr.ReadLine();
            if(line == null) break;
        }

        int count = 0;
        int edgeIndex = treeMap.GetLength(0) - 1;
        for(int i = 0; i < treeMap.GetLength(0); i++) {
            for(int j = 0; j < treeMap.GetLength(0); j++) {
                if(i == 0 || j == 0 || i == edgeIndex || j == edgeIndex) {
                    count++;
                    continue;
                }

                bool visible = true;
                //up
                int height = treeMap[i,j];
                for(int y = i - 1; y >= 0; y--) {
                    if(treeMap[y, j] >= height) visible = false;
                }

                if(visible == true) {
                    count++;
                    continue;
                }

                //down
                visible = true;
                height = treeMap[i,j];
                for(int y = i + 1; y < treeMap.GetLength(0); y++) {
                    if(treeMap[y, j] >= height) visible = false;
                }

                if(visible == true) {
                    count++;
                    continue;
                }

                //left
                visible = true;
                height = treeMap[i,j];
                for(int x = j - 1; x >= 0; x--) {
                    if(treeMap[i, x] >= height) visible = false;
                }

                if(visible == true) {
                    count++;
                    continue;
                }

                //right
                visible = true;
                height = treeMap[i,j];
                for(int x = j + 1; x < treeMap.GetLength(0); x++) {
                    if(treeMap[i, x] >= height) visible = false;
                }

                if(visible == true) {
                    count++;
                    continue;
                }
            }
        }

        Console.WriteLine(count);
    }

    public static void Part2(StreamReader sr) {
        string line = sr.ReadLine();
        int[,] treeMap = new int[line.Length, line.Length];

        for(int i = 0; i < line.Length; i++) {
            for(int j = 0; j < line.Length; j++) {
                treeMap[i, j] = int.Parse(line[j].ToString());
            }

            line = sr.ReadLine();
            if(line == null) break;
        }

        int maxScenic = int.MinValue;
        int edgeIndex = treeMap.GetLength(0) - 1;
        for(int i = 0; i < treeMap.GetLength(0); i++) {
            for(int j = 0; j < treeMap.GetLength(0); j++) {

                int[] scenicArray = new int[4];
                //up
                int height = treeMap[i,j];
                for(int y = i - 1; y >= 0; y--) {
                    scenicArray[0]++;

                    if(treeMap[y, j] >= height) break;
                }

                //down
                height = treeMap[i,j];
                for(int y = i + 1; y < treeMap.GetLength(0); y++) {
                    scenicArray[1]++;

                    if(treeMap[y, j] >= height) break;
                }

                //left
                height = treeMap[i,j];
                for(int x = j - 1; x >= 0; x--) {
                    scenicArray[2]++;

                    if(treeMap[i, x] >= height) break;
                }

                //right
                height = treeMap[i,j];
                for(int x = j + 1; x < treeMap.GetLength(0); x++) {
                    scenicArray[3]++;

                    if(treeMap[i, x] >= height) break;
                }

                int scenic = 1;
                foreach(int score in scenicArray) {
                    scenic *= score;
                }

                if(scenic > maxScenic) maxScenic = scenic;
            }
        }

        Console.WriteLine(maxScenic);
    }
}