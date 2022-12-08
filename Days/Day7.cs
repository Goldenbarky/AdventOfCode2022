class Day7 {

    const int maxSize = 40000000;
    static int currSize = 0;

    public static void Part1(StreamReader sr) {
        directory root = new directory("/");
        directory currDir = root;

        Stack<directory> depth = new Stack<directory>();
        depth.Push(root);
        sr.ReadLine();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            if(line[0] == '$') {

                line = line.Trim();
                string[] commands = line.Split(" ");

                if(commands[1] == "ls") {
                    continue;
                } else if(commands[1] == "cd") {
                    string name = commands[2];

                    if(name == ".."){
                        depth.Pop();
                        currDir = depth.Peek();
                        continue;
                    }

                    foreach(storageObject thing in currDir.files){
                        if(thing is directory && thing.name == name) {
                            currDir = (directory) thing;
                            depth.Push(currDir);
                            continue;
                        }
                    }
                }
            } else {
                string[] info = line.Split(" ");

                if(info[0] == "dir") {
                    directory newDir = new directory(info[1]);
                    currDir.addObject(newDir);
                    continue;
                } else {
                    elfFile newFile = new elfFile(info[1], int.Parse(info[0]));
                    currDir.addObject(newFile);
                    continue;
                }
            }
        }

        sumFiles(root);

        Console.WriteLine(calcBloat(root, 0));
    }

    public static int sumFiles(directory currDir) {
        foreach(storageObject thing in currDir.files) {
            if(thing is directory) {
                currDir.size += sumFiles((directory)thing);
            }
        }

        return currDir.size;
    }

    public static int calcBloat(directory currDir, int currSum) {
        if(currDir.size <= 100000) currSum += currDir.size;

        foreach(storageObject thing in currDir.files) {
            if(thing is directory) {
                currSum += calcBloat((directory)thing, 0);
            }
        }

        return currSum;
    }

    public static void Part2(StreamReader sr) {
        directory root = new directory("/");
        directory currDir = root;

        Stack<directory> depth = new Stack<directory>();
        depth.Push(root);
        bool readMode = false;
        sr.ReadLine();
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            if(line[0] == '$') {
                readMode = false;

                line = line.Trim();
                string[] commands = line.Split(" ");

                if(commands[1] == "ls") {
                    readMode = true;
                    continue;
                } else if(commands[1] == "cd") {
                    string name = commands[2];

                    if(name == ".."){
                        depth.Pop();
                        currDir = depth.Peek();
                        continue;
                    }

                    foreach(storageObject thing in currDir.files){
                        if(thing is directory && thing.name == name) {
                            currDir = (directory) thing;
                            depth.Push(currDir);
                            continue;
                        }
                    }
                }
            } else {
                string[] info = line.Split(" ");

                if(info[0] == "dir") {
                    directory newDir = new directory(info[1]);
                    currDir.addObject(newDir);
                    continue;
                } else {
                    elfFile newFile = new elfFile(info[1], int.Parse(info[0]));
                    currDir.addObject(newFile);
                    continue;
                }
            }
        }

        sumFiles(root);

        currSize = root.size;

        List<directory> prospects = new List<directory>();
        foldersToDelete(root, prospects);

        int smallest = int.MaxValue;
        foreach(directory dir in prospects) {
            if(dir.size < smallest) smallest = dir.size;
        }

        Console.WriteLine(smallest);
    }

    public static void foldersToDelete(directory currDir, List<directory> prospects) {
        if(currSize - currDir.size < maxSize) {
            prospects.Add(currDir);
        }

        foreach(storageObject thing in currDir.files){
            if(thing is directory) {
                foldersToDelete((directory)thing, prospects);
            }
        }
    }
    
}


class elfFile : storageObject{
    public elfFile(string name, int size) : base(name){
        this.name = name;
        this.size = size;
    }
}

class directory : storageObject{
    public List<storageObject> files = new List<storageObject>();

    public directory(string name) : base(name) {
        this.name = name;
    }

    public void addObject(storageObject newObject){
        files.Add(newObject);
        size += newObject.size;
    }
}

class storageObject {
    public string name;
    public int size;

    public storageObject(string name){
        this.name = name;
    }
}