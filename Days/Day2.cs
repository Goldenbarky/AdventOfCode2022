class Day2 {

    public static void Part1(StreamReader sr) {
        int score = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] round = line.Split(" ");
            string opponent = round[0];
            string me = round[1];

            Round currRound = new Round(opponent, choice: me);
            score += currRound.score();
        }

        Console.WriteLine(score);
    }

    public static void Part2(StreamReader sr) {
        int score = 0;
        for(string line = sr.ReadLine(); line != null; line = sr.ReadLine()) {
            string[] round = line.Split(" ");
            string opponent = round[0];
            string me = round[1];

            Round currRound = new Round(opponent, outcome: me);
            score += currRound.score();
        }

        Console.WriteLine(score);
    }
}

class Round {
    int opponent;
    int choice;
    int outcome;
    public Round(string opponent, string choice = null, string outcome = null) {
        this.opponent = choiceToValue(opponent);

        if(choice != null) this.choice = choiceToValue(choice);

        if(outcome != null) this.choice = outcomeToChoice(outcome);
        
        calculateOutcome();
    }

    public int score() {
        return choice + outcome;
    }

    public static int choiceToValue(string choice) {
        if(choice == "A" || choice == "X") return 1;
        if(choice == "B" || choice == "Y") return 2;
        if(choice == "C" || choice == "Z") return 3;
        return 0;
    }

    public void calculateOutcome() {
        if(opponent == choice) outcome = 3;
        else if(choice - (opponent % 3) == 1) outcome = 6;
        else outcome = 0;
    }

    public int outcomeToChoice(string outcome) {
        if(outcome == "X") return Modulo(((opponent - 1) - 1), 3) + 1;
        if(outcome == "Y") return opponent;
        if(outcome == "Z") return Modulo(((opponent + 1) - 1), 3) + 1;
        return 0;
    }

    public static int Modulo(int a, int b) {
        return ((a % b) + b) % b;
    }
}