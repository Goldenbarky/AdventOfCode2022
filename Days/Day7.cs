class Day7 {

    const int maxSize = 40000000;
    static int currSize = 0;

    public static void Part1(StreamReader sr) {
        Directory root = new Directory("/");
        Directory currDir = root;

        Stack<Directory> depth = new Stack<Directory>();
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
                        if(thing is Directory && thing.name == name) {
                            currDir = (Directory) thing;
                            depth.Push(currDir);
                            continue;
                        }
                    }
                }
            } else {
                string[] info = line.Split(" ");

                if(info[0] == "dir") {
                    Directory newDir = new Directory(info[1]);
                    currDir.addObject(newDir);
                    continue;
                } else {
                    ElfFile newFile = new ElfFile(info[1], int.Parse(info[0]));
                    currDir.addObject(newFile);
                    continue;
                }
            }
        }

        sumFiles(root);

        Console.WriteLine(calcBloat(root, 0));
    }

    public static int sumFiles(Directory currDir) {
        foreach(storageObject thing in currDir.files) {
            if(thing is Directory) {
                currDir.size += sumFiles((Directory)thing);
            }
        }

        return currDir.size;
    }

    public static int calcBloat(Directory currDir, int currSum) {
        if(currDir.size <= 100000) currSum += currDir.size;

        foreach(storageObject thing in currDir.files) {
            if(thing is Directory) {
                currSum += calcBloat((Directory)thing, 0);
            }
        }

        return currSum;
    }

    public static void Part2(StreamReader sr) {
        Directory root = new Directory("/");
        Directory currDir = root;

        Stack<Directory> depth = new Stack<Directory>();
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
                        if(thing is Directory && thing.name == name) {
                            currDir = (Directory) thing;
                            depth.Push(currDir);
                            continue;
                        }
                    }
                }
            } else {
                string[] info = line.Split(" ");

                if(info[0] == "dir") {
                    Directory newDir = new Directory(info[1]);
                    currDir.addObject(newDir);
                    continue;
                } else {
                    ElfFile newFile = new ElfFile(info[1], int.Parse(info[0]));
                    currDir.addObject(newFile);
                    continue;
                }
            }
        }

        sumFiles(root);

        currSize = root.size;

        List<Directory> prospects = new List<Directory>();
        foldersToDelete(root, prospects);

        int smallest = int.MaxValue;
        foreach(Directory dir in prospects) {
            if(dir.size < smallest) smallest = dir.size;
        }

        Console.WriteLine(smallest);
    }

    public static void foldersToDelete(Directory currDir, List<Directory> prospects) {
        if(currSize - currDir.size < maxSize) {
            prospects.Add(currDir);
        }

        foreach(storageObject thing in currDir.files){
            if(thing is Directory) {
                foldersToDelete((Directory)thing, prospects);
            }
        }
    }
    
}


class ElfFile : storageObject{
    public ElfFile(string name, int size) : base(name){
        this.name = name;
        this.size = size;
    }
}

class Directory : storageObject{
    public List<storageObject> files = new List<storageObject>();

    public Directory(string name) : base(name) {
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