using UnityEngine;
using System.Collections;

public class BoardControl : MonoBehaviour {

    public int[,] MainArray = new int[3, 3];
    public KeyCode vKey;
    char entered, pressed;
    public bool wait, toggleAI;
    public int turn, currentTexture, winner;

    public Texture[] textures;
    public string textSelect;
    public string boardPositionInMain;

    public int numPlayer, playerChk;
    public int player, opponent;
    public int sideChk;

    private int user, simWin;

    private int highest, lowest;
    private int[] high = { 0, 0 };

    Transform selected;
    Renderer[] renderers;

    // Use this for initialization
    void Start() {
        MainArray = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        //foreach (int element in MainArray) {
        //    //print(element); -- Debug line
        //}

        //Initialization
        turn = 0;
        player = 0;
        opponent = 0;
        currentTexture = 0;
        renderers = transform.GetComponentsInChildren<Renderer>();
        winner = 0;

        highest = 0; lowest = 0;
        toggleAI = false;

        //delete this later; here for testing
        //selected = transform.FindChild("TL"); -- Debug Line

        print("1 Player or 2 Player?");
    }


    void players() {
        /**
         * This function is to set the number of players
         * for this game. 1 would be vs. AI; 2 would be
         * human vs human.
        */
        //print("1 Player or 2 Player?");
        if (Input.GetKeyDown("1")) {
            print("1 Player Selected");
            numPlayer = 1;
            playerChk = 1;
        } else if (Input.GetKeyDown("2")) {
            print("2 Player Selected");
            numPlayer = 2;
            playerChk = 1;
        }

    }


    void selectSide() {
        /** 
         * This function will be used to determine whether the player
         * wants to play as X or O
        */

        //if (Input.GetKeyDown("x")) {
        //    print("Player " + numPlayer + ", you are X.");
        //    if (1 == numPlayer) {
        //        player1 = 2; //-- 'x';
        //        player2 = 1; //-- 'o';
        //    } else if (2 == numPlayer) {
        //        player2 = 2; //-- 'x';
        //        player1 = 1; //-- 'o';
        //    }
        //    turn = 2;
        //    sideChk = 1;
        //} else if (Input.GetKeyDown("o")) {
        //    print("Player " + numPlayer + ", you are O.");
        //    if (1 == numPlayer) {
        //        player1 = 1; //--'o';
        //        player2 = 2; //-- 'x';
        //    } else if (2 == numPlayer) {
        //        player2 = 1; //-- 'o';
        //        player1 = 2; //-- 'x';
        //    }
        //    turn = 1;
        //    sideChk = 1;
        //}

        if (Input.GetKeyUp(KeyCode.X)) {
            player = 2; //main player
            opponent = 1; //AI or 2nd player
            print("Player 1 is X");
            turn = 2;
            sideChk = 1;
        }else if (Input.GetKeyUp(KeyCode.O)) {
            player = 1; //main player
            opponent = 2; // AI or 2nd player
            print("Player 1 is O");
            turn = 1;
            sideChk = 1;
        }
    }


    void rendererTextureChange(string toSwap, int swapTo) {
        foreach (Renderer r in renderers) {
            if (r.name == toSwap) {
                r.material.mainTexture = textures[swapTo];
            }
        }

    }

    private int abs(int subject) {
        if (subject > 0) {
            //Debug.Log("subject: " + subject);
            return subject;
        } else {
            //Debug.Log("subject: -" + subject);
            return -subject;
        }
    }

    //swaps the texture of toSwap into swapTo; 0 for transparent, 1 for O, 2 for X
    public void textureSwap(string toSwap, int swapTo) {
        if (toSwap == "TL" && MainArray[0, 0] == 0) {
            MainArray[0, 0] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);

            //Testing
            //Debug.Log("TM MainArray: " + getMainArray("TM"));
            //if (0 == getMainArray("TM")) {
            //    textureSwap("TM",turn);
            //}

        } else if (toSwap == "TM" && MainArray[0, 1] == 0) {
            MainArray[0, 1] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        } else if (toSwap == "TR" && MainArray[0, 2] == 0) {
            MainArray[0, 2] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        } else if (toSwap == "ML" && MainArray[1, 0] == 0) {
            MainArray[1, 0] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        } else if (toSwap == "MM" && MainArray[1, 1] == 0) {
            MainArray[1, 1] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        } else if (toSwap == "MR" && MainArray[1, 2] == 0) {
            MainArray[1, 2] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        } else if (toSwap == "BL" && MainArray[2, 0] == 0) {
            MainArray[2, 0] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        } else if (toSwap == "BM" && MainArray[2, 1] == 0) {
            MainArray[2, 1] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        } else if (toSwap == "BR" && MainArray[2, 2] == 0) {
            MainArray[2, 2] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        }

    }

    public void winCheck() {

        int countEmpty = 0;

        //Check Row
        for (int i = 0; i < 3; i++) {
            //Debug.Log("i=" + i + "; MainArray[0," + i + "]=" + MainArray[0, i]);
            if (MainArray[i, 0] != 0 && MainArray[i, 0] == MainArray[i, 1] && MainArray[i, 0] == MainArray[i, 2]) {
                //Debug.Log("Winner Row");
                winner = MainArray[i, 0];
            }
            //Check Diagonal
            if (MainArray[i, 0] != 0 && MainArray[0, 0] == MainArray[1, 1] && MainArray[0, 0] == MainArray[2, 2]) {
                //Debug.Log("Winner Diagonal");
                winner = MainArray[0, 0];
            }
        }

        //Check Column
        for (int j = 0; j < 3; j++) {
            //Debug.Log("j=" + j + "; MainArray[" + j + ",0]=" + MainArray[j, 0]);
            if (MainArray[0, j] != 0 && MainArray[0, j] == MainArray[1, j] && MainArray[0, j] == MainArray[2, j]) {
                //Debug.Log("Winner Column");
                winner = MainArray[0, j];
            }
            //Check Anti-Diagonal
            if (MainArray[0, j] != 0 && MainArray[2, 0] == MainArray[1, 1] && MainArray[2, 0] == MainArray[0, 2]) {
                //Debug.Log("Winner Anti-Diagonal");
                winner = MainArray[2, 0];
            }
        }

        for(int x = 0; x < 3; x++) {
            for (int y = 0; y < 3; y++) {
                if (MainArray[x, y] == 0)
                    countEmpty++;
            }
        }

        if (countEmpty == 0) {
            print("Game Tied");
        }

    }

    public int winReturn() {
        return winner;
    }

    public string returnBoardPosition() {
        return boardPositionInMain;
    }

    public int getMainArray(string posTextSelect) {

        switch (posTextSelect) {
            case "TL":
                return MainArray[0, 0];
            case "TM":
                return MainArray[0, 1];
            case "TR":
                return MainArray[0, 2];
            case "ML":
                return MainArray[1, 0];
            case "MM":
                return MainArray[1, 1];
            case "MR":
                return MainArray[1, 2];
            case "BL":
                return MainArray[2, 0];
            case "BM":
                return MainArray[2, 1];
            case "BR":
                return MainArray[2, 2];
            default:
                return 0;
        }
    }

    public string getPosition(int x, int y) {
        if (0 == x && 0 == y) {
            return "TL";
        } else if (0 == x && 1 == y) {
            return "TM";
        } else if (0 == x && 2 == y) {
            return "TR";
        } else if (1 == x && 0 == y) {
            return "ML";
        } else if (1 == x && 1 == y) {
            return "MM";
        } else if (1 == x && 2 == y) {
            return "MR";
        } else if (2 == x && 0 == y) {
            return "BL";
        } else if (2 == x && 1 == y) {
            return "BM";
        } else if (2 == x && 2 == y) {
            return "BR";
        } else {
            return "";
        }
    }

    //AI optimized for a single board using the Minimax algorithm

    public void AIMinimax(int[,] simArray, int turn) {
        int score = 0;
        int depth = 0;
        string empty = "empty: ";

        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (0 == simArray[i, j]) {
                    empty += "[" + i + ", " + j + "] ";
                }
            }
        }
        //Debug.Log(empty + "\nPlayer: " + turn + "\nDepth: " + depth);


        if (simArray[1, 1] == 0) {
            textureSwap("MM", turn);
        } else {
            //--------------------------------------------------------------------------
            //Rule 1: If there's a winning move, take it
            //Horizontal
            for (int i = 0; i < 3; i++) {
                if (simArray[i, 0] == player && simArray[i, 1] == player && simArray[i, 2] == 0) {
                    textureSwap(getPosition(i, 2), turn);
                    //return;
                }else if (simArray[i, 0] == player && simArray[i, 1] == 0 && simArray[i, 2] == player) {
                    textureSwap(getPosition(i, 1), turn);
                    //return;
                }else if (simArray[i, 0] == 0 && simArray[i, 1] == player && simArray[i, 2] == player) {
                    textureSwap(getPosition(i, 0), turn);
                    //return;
                }
            }
            //Vertical
            for (int i = 0; i < 3; i++) {
                if (simArray[0, i] == player && simArray[1, i] == player && simArray[2, i] == 0) {
                    textureSwap(getPosition(2, i), turn);
                    //return;
                }else if (simArray[0, i] == player && simArray[1, i] == 0 && simArray[2, i] == player) {
                    textureSwap(getPosition(1, i), turn);
                    //return;
                }else if (simArray[0, i] == 0 && simArray[1, i] == player && simArray[2, i] == player) {
                    textureSwap(getPosition(0, i), turn);
                    //return;
                }
            }

            //Diagonal
            if (simArray[0, 0] == player && simArray[1, 1] == player && simArray[2, 2] == 0) {
                textureSwap(getPosition(2, 2), turn);
                //return;
            }else if (simArray[0, 0] == player && simArray[1, 1] == 0 && simArray[2, 2] == player) {
                textureSwap(getPosition(1, 1), turn);
                //return;
            }else if (simArray[0, 0] == 0 && simArray[1, 1] == player && simArray[2, 2] == player) {
                textureSwap(getPosition(0, 0), turn);
                //return;
            }

            //Anti-Diagonal
            if (simArray[2, 0] == 1 && simArray[1, 1] == 1 && simArray[0, 2] == 0) {
                textureSwap(getPosition(0, 2), turn);
                //return;
            }else if (simArray[2, 0] == 1 && simArray[1, 1] == 0 && simArray[0, 2] == 1) {
                textureSwap(getPosition(1, 1), turn);
                //return;
            }else if (simArray[2, 0] == 0 && simArray[1, 1] == 1 && simArray[0, 2] == 1) {
                textureSwap(getPosition(2, 0), turn);
                //return;
            }

            //--------------------------------------------------------------------------
            //Rule 2: If opponent has a winning move, block it
            //Horizontal
            for (int i = 0; i < 3; i++) {
                if (simArray[i, 0] == opponent && simArray[i, 1] == opponent && simArray[i, 2] == 0) {
                    textureSwap(getPosition(i, 2), turn);
                    //return;
                }else if (simArray[i, 0] == opponent && simArray[i, 1] == 0 && simArray[i, 2] == opponent) {
                    textureSwap(getPosition(i, 1), turn);
                    //return;
                }else if (simArray[i, 0] == 0 && simArray[i, 1] == opponent && simArray[i, 2] == opponent) {
                    textureSwap(getPosition(i, 0), turn);
                    //return;
                }
            }
            //Vertical
            for (int i = 0; i < 3; i++) {
                if (simArray[0, i] == opponent && simArray[1, i] == opponent && simArray[2, i] == 0) {
                    textureSwap(getPosition(2, i), turn);
                    //return;
                }else if (simArray[0, i] == opponent && simArray[1, i] == 0 && simArray[2, i] == opponent) {
                    textureSwap(getPosition(1, i), turn);
                    //return;
                }else if (simArray[0, i] == 0 && simArray[1, i] == opponent && simArray[2, i] == opponent) {
                    textureSwap(getPosition(0, i), turn);
                    //return;
                }
            }

            //Diagonal
            if (simArray[0, 0] == opponent && simArray[1, 1] == opponent && simArray[2, 2] == 0) {
                textureSwap(getPosition(2, 2), turn);
                //return;
            }else if (simArray[0, 0] == opponent && simArray[1, 1] == 0 && simArray[2, 2] == opponent) {
                textureSwap(getPosition(1, 1), turn);
                //return;
            }else if (simArray[0, 0] == 0 && simArray[1, 1] == opponent && simArray[2, 2] == opponent) {
                textureSwap(getPosition(0, 0), turn);
                //return;
            }

            //Anti-Diagonal
            if (simArray[2, 0] == opponent && simArray[1, 1] == opponent && simArray[0, 2] == 0) {
                textureSwap(getPosition(0, 2), turn);
                //return;
            }else if (simArray[2, 0] == opponent && simArray[1, 1] == 0 && simArray[0, 2] == opponent) {
                textureSwap(getPosition(1, 1), turn);
                //return;
            }else if (simArray[2, 0] == 0 && simArray[1, 1] == opponent && simArray[0, 2] == opponent) {
                textureSwap(getPosition(2, 0), turn);
                //return;
            }

            int[] test = { 0, 0 };
            //--------------------------------------------------------------------------
            //Rule 3: If you can create a fork, do so (i.e. multiple winning moves)
            test = MiniMax(simArray, turn, score, 0);
            textureSwap(getPosition(test[0], test[1]), turn);
            //Debug.Log(test[0] + ", " + test[1] + "-----test");
            //Debug.Log("end of AIMinimax---------");
        }
}

    public int boardEvaluation(int[,] board) {
        //This function is to evaluate the board and provide a score

        int[] count = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] countX = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] countO = { 0, 0, 0, 0, 0, 0, 0, 0 };
        int sum = 0;

        //Heuristic Score check:
        //Horiziontal - Row
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (0 == i && board[0, j] == opponent) { //subtract for opponent
                    if (1 == countX[i]) {
                        count[i] -= 10;
                        countX[i]++;
                    } else if (2 == countX[i]) {
                        count[i] -= 100;
                        countX[i] = 0;
                    } else {
                        count[i] -= 1;
                        countX[i]++;
                    }
                } else if (0 == i && board[0, j] == player) { //addition for player
                    if (1 == countO[i]) {
                        count[i] += 10;
                        countO[i]++;
                    } else if (2 == countO[i]) {
                        count[i] += 100;
                        countO[i] = 0;
                    } else {
                        count[i] += 1;
                        countO[i]++;
                    }
                }
                if (1 == i && board[1, j] == opponent) { //subtract for opponent
                    if (1 == countX[i]) {
                        count[i] -= 10;
                        countX[i]++;
                    } else if (2 == countX[i]) {
                        count[i] -= 100;
                        countX[i] = 0;
                    } else {
                        count[i] -= 1;
                        countX[i]++;
                    }
                } else if (1 == i && board[1, j] == player) { //addition for player
                    if (1 == countO[i]) {
                        count[i] += 10;
                        countO[i]++;
                    } else if (2 == countO[i]) {
                        count[i] += 100;
                        countO[i] = 0;
                    } else {
                        count[i] += 1;
                        countO[i]++;
                    }
                }
                if (2 == i && board[2, j] == opponent) { //subtract for opponent
                    if (1 == countX[i]) {
                        count[i] -= 10;
                        countX[i]++;
                    } else if (2 == countX[i]) {
                        count[i] -= 100;
                        countX[i] = 0;
                    } else {
                        count[i] -= 1;
                        countX[i]++;
                    }
                } else if (2 == i && board[2, j] == player) { //addition for player
                    if (1 == countO[i]) {
                        count[i] += 10;
                        countO[i]++;
                    } else if (2 == countO[i]) {
                        count[i] += 100;
                        countO[i] = 0;
                    } else {
                        count[i] += 1;
                        countO[i]++;
                    }
                }
            }

            //Check diagonal
            if (opponent == board[i, i]) { //subtract for opponent
                if (1 == countX[6]) {
                    count[6] -= 10;
                    countX[6]++;
                } else if (2 == countX[6]) { 
                    count[6] -= 100;
                    countX[6] = 0;
                } else {
                    count[6] -= 1;
                    countX[6]++;
                }
            } else if (player == board[i, i]) { //addition for player
                if (1 == countO[6]) {
                    count[6] += 10;
                    countO[6]++;
                } else if (2 == countO[6]) {
                    count[6] += 100;
                    countO[6] = 0;
                } else {
                    count[6] += 1;
                    countO[6]++;
                }
            }
        }

        //Vertical - Column
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (0 == i && board[j, 0] == opponent) { //subtract for opponent
                    if (1 == countX[3 + i]) {
                        count[3 + i] -= 10;
                        countX[3 + i]++;
                    } else if (2 == countX[3 + i]) {
                        count[3 + i] -= 100;
                        countX[3 + i] = 0;
                    } else {
                        count[3 + i] -= 1;
                        countX[3 + i]++;
                    }
                } else if (0 == i && board[j, 0] == player) { //addition for player
                    if (1 == countO[3 + i]) {
                        count[3 + i] += 10;
                        countO[3 + i]++;
                    } else if (2 == countO[3 + i]) {
                        count[3 + i] += 100;
                        countO[3 + i] = 0;
                    } else {
                        count[3 + i] += 1;
                        countO[3 + i]++;
                    }
                }
                if (1 == i && board[j, 1] == opponent) { //subtract for opponent
                    if (1 == countX[3 + i]) {
                        count[3 + i] -= 10;
                        countX[3 + i]++;
                    } else if (2 == countX[3 + i]) {
                        count[3 + i] -= 100;
                        countX[3 + i] = 0;
                    } else {
                        count[3 + i] -= 1;
                        countX[3 + i]++;
                    }
                } else if (1 == i && board[j, 1] == player) { //addition for player
                    if (1 == countO[3 + i]) {
                        count[3 + i] += 10;
                        countO[3 + i]++;
                    } else if (2 == countO[3 + i]) {
                        count[3 + i] += 100;
                        countO[3 + i] = 0;
                    } else {
                        count[3 + i] += 1;
                        countO[3 + i]++;
                    }
                }
                if (2 == i && board[j, 2] == opponent) { //subtract for opponent
                    if (1 == countX[3 + i]) {
                        count[3 + i] -= 10;
                        countX[3 + i]++;
                    } else if (2 == countX[3 + i]) {
                        count[3 + i] -= 100;
                        countX[3 + i] = 0;
                    } else {
                        count[3 + i] -= 1;
                        countX[3 + i]++;
                    }
                } else if (2 == i && board[j, 2] == player) { //addition for player
                    if (1 == countO[3 + i]) {
                        count[3 + i] += 10;
                        countO[3 + i]++;
                    } else if (2 == countO[3 + i]) {
                        count[3 + i] += 100;
                        countO[3 + i] = 0;
                    } else {
                        count[3 + i] += 1;
                        countO[3 + i]++;
                    }
                }
            }
            //Check anti-diagonal
            if (opponent == board[i, 2 - i]) { //subtract for opponent
                if (1 == countX[7]) {
                    count[7] -= 10;
                    countX[7]++;
                } else if (2 == countX[7]) {
                    count[7] -= 100;
                    countX[7] = 0;
                } else {
                    count[7] -= 1;
                    countX[7]++;
                }
            } else if (player == board[i, 2 - i]) { //addition for player
                if (1 == countO[7]) {
                    count[7] += 10;
                    countO[7]++;
                } else if (2 == countO[7]) {
                    count[7] += 100;
                    countO[7] = 0;
                } else {
                    count[7] += 1;
                    countO[7]++;
                }
            }
        }


        for (int i = 0; i < 8; i++) {
            sum += count[i];
        }

        //for (int i = 0; i < 8; i++) {
        //    if (-20 == count[i]) {
        //        //Rule 2: Block opponent's winning move
        //        //Debug.Log("Need to block count[" + i + "].");
        //        //AImove(i, player);
        //        //Debug.Log("--------------------return1");
        //        //return 100000;
        //    }else if (20 == count[i]) {
        //        //Rule 1: Take the winning move
        //        //Debug.Log("Take winning move count[" + i + "].");
        //        //AImove(i, player);
        //        //Debug.Log("--------------------return2");
        //        //return -100000;
        //    }

        //}

        //if (depth < 4) {
        //    Debug.Log("Need to recursive");
        //}

        //Debug.Log("sum: " + sum);
        return sum;
    }

    public int[] MiniMax(int[,] simArray, int turn, int score, int depth) {

        int currentScore = 0;
        string board = "";
        //int[] high = { 1, 10 };
        
        
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                //Debug.Log(i + ", " + j);
                if (simArray[i, j] == 0 && depth <= 5) {
                    //Debug.Log("inside if: " + i + ", " + j);
                    simArray[i, j] = turn;
                    //Debug - Portion
                    //for (int x = 0; x < 3; x++) {
                    //    for (int y = 0; y < 3; y++) {
                    //        board += "[" + simArray[x, y] + "], ";
                    //    }
                    //    board += "\n";
                    //}
                    //Debug.Log(board);
                    currentScore = boardEvaluation(simArray);
                    //Debug.Log("iteration: " + countLoop + "; currentScore: " + currentScore + "; score: " + score );
                    depth++;
                    Debug.Log("high: " + highest + ", low: " + lowest);
                    if ((i + j) != 4) {
                        MiniMax(simArray, abs(3 - turn), currentScore + score, depth);
                        if (highest < currentScore + score) {
                            highest = currentScore + score;
                            //Need to keep track of the position where 1 is being played.
                            high[0] = i; high[1] = j;
                        } else if (lowest > currentScore + score) {
                            lowest = currentScore + score;
                        }
                    }
                    //will not execute until Minimax has reached all the end (i.e. simArray[i, j] != 0)
                    //Debug.Log("before: -------- simArray[" + i + ", " + j + "]: " + simArray[i, j]);
                    simArray[i, j] = 0;
                    //Debug.Log("simArray[" + i + ", " + j + "]: " + simArray[i, j]);

                    //Debug - Portion
                    //board = "";
                    //for (int x = 0; x < 3; x++) {
                    //    for (int y = 0; y < 3; y++) {
                    //        board += "[" + simArray[x, y] + "], ";
                    //    }
                    //    board += "\n";
                    //}
                    //Debug.Log(board + "/.");
                }
                //Debug - line
                //board = "";
                
            }

        }
        //Debug.Log(high[0] + ", " + high[1] + "-----");
        return high;
    }


    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (0 == playerChk) {
            players();
        }
        if (0 == sideChk && playerChk == 1) {
            selectSide();
        }
        if (1 == playerChk && 1 == sideChk) {
            if (Input.GetMouseButtonDown(0)) {
                //print("Pressed left mouse click"); -- Debug line
                if (Physics.Raycast(ray, out hit)) {
                    textSelect = hit.collider.name;
                    //Debug.Log("texture name: " + textSelect); //---- Debug line
                }
                if ("Side" != textSelect && "Plane" != textSelect && getMainArray(textSelect) == 0) {
                    textureSwap(textSelect, turn);
                    winCheck();
                    //if AI, then AI move
                    //MiniMax(MainArray, turn, 0, 0);
                    if (numPlayer == 1)
                        toggleAI = true;
                    //simulatedWincheck(MainArray, turn);
                    //print("" + boardEvaluation(MainArray, turn));
                }
            }
        }
        
    }

    void LateUpdate() {
        if (toggleAI) {
            AIMinimax(MainArray, turn);
            toggleAI = false;
        }
    }
}

//public void AImove(int n_element, int player) {
//    switch (n_element) {
//        case 0:
//            if (0 == MainArray[0, 0])
//                textureSwap("TL", player);
//            if (0 == MainArray[0, 1])
//                textureSwap("TM", player);
//            if (0 == MainArray[0, 2])
//                textureSwap("TR", player);
//            break;
//        case 1:
//            if (0 == MainArray[1, 0])
//                textureSwap("ML", player);
//            if (0 == MainArray[1, 1])
//                textureSwap("MM", player);
//            if (0 == MainArray[1, 2])
//                textureSwap("MR", player);
//            break;
//        case 2:
//            if (0 == MainArray[2, 0])
//                textureSwap("BL", player);
//            if (0 == MainArray[2, 1])
//                textureSwap("BM", player);
//            if (0 == MainArray[2, 2])
//                textureSwap("BR", player);
//            break;
//        case 3:
//            if (0 == MainArray[0, 0])
//                textureSwap("TL", player);
//            if (0 == MainArray[1, 0])
//                textureSwap("ML", player);
//            if (0 == MainArray[2, 0])
//                textureSwap("BL", player);
//            break;
//        case 4:
//            if (0 == MainArray[0, 1])
//                textureSwap("TM", player);
//            if (0 == MainArray[1, 1])
//                textureSwap("MM", player);
//            if (0 == MainArray[2, 1])
//                textureSwap("BM", player);
//            break;
//        case 5:
//            if (0 == MainArray[0, 2])
//                textureSwap("TR", player);
//            if (0 == MainArray[1, 2])
//                textureSwap("MR", player);
//            if (0 == MainArray[2, 2])
//                textureSwap("BR", player);
//            break;
//        case 6:
//            if (0 == MainArray[0, 0])
//                textureSwap("TL", player);
//            if (0 == MainArray[1, 1])
//                textureSwap("MM", player);
//            if (0 == MainArray[2, 2])
//                textureSwap("BR", player);
//            break;
//        case 7:
//            if (0 == MainArray[2, 0])
//                textureSwap("BL", player);
//            if (0 == MainArray[1, 1])
//                textureSwap("MM", player);
//            if (0 == MainArray[0, 2])
//                textureSwap("TR", player);
//            break;
//    }
//}


////for (int i = 0; i < 3; i++) {
//    for (int j = 0; j < 3; j++) {
//        if (4 > depth && 0 == simArray[i, j]) {
//            //Debug.Log("Empty @["+i+", "+j+"], need to board check");
//            simArray[i, j] = player;
//            score = boardEvaluation(simArray, player);
//            Debug.Log("Player: " + player + " @ [" + i + ", " + j + "] w/ Score: " + score);

//            depth++;
//            //Debug.Log("EndOf---------------------------------------");
//            if (4 > depth && 1 == player) {
//                AIMinimax(simArray, 2, depth);
//                return;
//            } else if(4 > depth && 2 == player){
//                AIMinimax(simArray, 1, depth);
//                return;
//            }

//            //simArray[i, j] = 0;

//        }else if(4 <= depth) {
//            Debug.Log("return");
//            return;
//        }
//    }
//}
//Debug.Log("EndOfAI---------------------------------------");


//public void simulatedWincheck(int[,] simArray, int player) {

//    int[] count = { 0, 0, 0, 0, 0, 0, 0, 0 };
//    int sum = 0;

//    //Debug.Log("Player: " + player);

//    //Heuristic Score check:
//    //Horiziontal - Row
//    for (int i = 0; i < 3; i++) {
//        for (int j = 0; j < 3; j++) {
//            if (0 == i && simArray[0, j] == 2) {
//                count[i] -= 10;
//            } else if (0 == i && simArray[0, j] == 1) {
//                count[i] += 10;
//            }
//            if (1 == i && simArray[1, j] == 2) {
//                count[i] -= 10;
//            } else if (1 == i && simArray[1, j] == 1) {
//                count[i] += 10;
//            }
//            if (2 == i && simArray[2, j] == 2) {
//                count[i] -= 10;
//            } else if (2 == i && simArray[2, j] == 1) {
//                count[i] += 10;
//            }
//        }
//        //Check diagonal
//        if (2 == simArray[i, i]) {
//            count[6] -= 10;
//        } else if (1 == simArray[i, i]) {
//            count[6] += 10;
//        }
//    }

//    //Vertical - Column
//    for (int i = 0; i < 3; i++) {
//        for (int j = 0; j < 3; j++) {
//            if (0 == i && simArray[j, 0] == 2) {
//                count[3 + i] -= 10;
//            } else if (0 == i && simArray[j, 0] == 1) {
//                count[3 + i] += 10;
//            }
//            if (1 == i && simArray[j, 1] == 2) {
//                count[3 + i] -= 10;
//            } else if (1 == i && simArray[j, 1] == 1) {
//                count[3 + i] += 10;
//            }
//            if (2 == i && simArray[j, 2] == 2) {
//                count[3 + i] -= 10;
//            } else if (2 == i && simArray[j, 2] == 1) {
//                count[3 + i] += 10;
//            }
//        }
//        //Check anti-diagonal
//        if (2 == simArray[i, 2 - i]) {
//            count[7] -= 10;
//        } else if (1 == simArray[i, 2 - i]) {
//            count[7] += 10;
//        }
//    }

//    for (int i = 0; i < 8; i++) {
//        if (-20 == count[i]) {
//            //Rule 2: Block opponent's winning move
//            Debug.Log("Need to block count[" + i + "].");
//            AImove(i, player);
//            Debug.Log("--------------------return1");
//            return;
//        } else if (20 == count[i]) {
//            //Rule 1: Take the winning move
//            Debug.Log("Take winning move count[" + i + "].");
//            AImove(i, player);
//            Debug.Log("--------------------return2");
//            return;
//        }
//        sum += count[i];
//    }
//    Debug.Log("   _ | _ | _ => " + count[0] +
//              "\n   _ | _ | _ => " + count[1] +
//              "\n   _ | _ | _ => " + count[2] +
//              "\n   :    :    :" +
//              "\n" + count[3] + " | " + count[4] + " | " + count[5] +
//              "\n-> Diagonal: " + count[6] +
//              "\n-> Anti-Diag: " + count[7]);
//    Debug.Log("Sum: " + sum);
//}



//AIMiniMax
//Preferred moves in the order of left to right: 
//{1,1}, {0,0}, {0,2}, {2,0}, {2,2}, {0,1}, {1,0}, {1,2}, {2,1};

//if (simArray[1, 1] == 0) {
//    textureSwap("MM", player);
//}else if(simArray[0, 0]== 0) {
//    textureSwap("TL", player);
//} else if (simArray[0, 2] == 0) {
//    textureSwap("TR", player);
//} else if (simArray[2, 0] == 0) {
//    textureSwap("BL", player);
//} else if (simArray[2, 2] == 0) {
//    textureSwap("BR", player);
//} else if (simArray[0, 1] == 0) {
//    textureSwap("TM", player);
//} else if (simArray[1, 0] == 0) {
//    textureSwap("ML", player);
//} else if (simArray[1, 2] == 0) {
//    textureSwap("MR", player);
//} else if (simArray[2, 1] == 0) {
//    textureSwap("BM", player);
//}
//for (int i = 0; i < 3; i++)
//{
//    for (int j = 0; j < 3; j++)
//    {
//        //if (0 == simulatedArray[i,j]){
//        //    simulatedArray[i, j] = player;
//        //    AIMinimax(simulatedArray, abs(3 - player));
//        //}
//        //Debug.Log("turn: " + player + ", simulatedArray[" + i + "], [" + j + "]: " + simulatedArray[i, j]);
//    }
//}

//for (int i = 0; i < 3; i++) {
//    for (int j = 0; j < 3; j++) {
//        if (0 == simulatedArray[i, j]) {
//            //Debug.Log("next---------------------------------");
//            simulatedArray[i, j] = player;
//            AIMinimax(simulatedArray, abs(3 - player));
//        }

//        if(0 != simulatedWincheck(simulatedArray, player)) {
//            //Debug.Log("-------WinCheck: " + simulatedWincheck(simulatedArray, player));
//            return;
//        }
//    }
//}

//switch (i) {
//    case 0:
//        if (0 == simArray[0, 0])
//            textureSwap("TL", player);
//        if (0 == simArray[0, 1])
//            textureSwap("TM", player);
//        if (0 == simArray[0, 2])
//            textureSwap("TR", player);
//        break;
//    case 1:
//        if (0 == simArray[1, 0])
//            textureSwap("ML", player);
//        if (0 == simArray[1, 1])
//            textureSwap("MM", player);
//        if (0 == simArray[1, 2])
//            textureSwap("MR", player);
//        break;
//    case 2:
//        if (0 == simArray[2, 0])
//            textureSwap("BL", player);
//        if (0 == simArray[2, 1])
//            textureSwap("BM", player);
//        if (0 == simArray[2, 2])
//            textureSwap("BR", player);
//        break;
//    case 3:
//        if (0 == simArray[0, 0])
//            textureSwap("TL", player);
//        if (0 == simArray[1, 0])
//            textureSwap("ML", player);
//        if (0 == simArray[2, 0])
//            textureSwap("BL", player);
//        break;
//    case 4:
//        if (0 == simArray[0, 1])
//            textureSwap("TM", player);
//        if (0 == simArray[1, 1])
//            textureSwap("MM", player);
//        if (0 == simArray[2, 1])
//            textureSwap("BM", player);
//        break;
//    case 5:
//        if (0 == simArray[0, 2])
//            textureSwap("TR", player);
//        if (0 == simArray[1, 2])
//            textureSwap("MR", player);
//        if (0 == simArray[2, 2])
//            textureSwap("BR", player);
//        break;
//    case 6:
//        if (0 == simArray[0, 0])
//            textureSwap("TL", player);
//        if (0 == simArray[1, 1])
//            textureSwap("MM", player);
//        if (0 == simArray[2, 2])
//            textureSwap("BR", player);
//        break;
//    case 7:
//        if (0 == simArray[2, 0])
//            textureSwap("BL", player);
//        if (0 == simArray[1, 1])
//            textureSwap("MM", player);
//        if (0 == simArray[0, 2])
//            textureSwap("TR", player);
//        break;
//}