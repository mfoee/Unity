using UnityEngine;
using System.Collections;

public class BoardControl : MonoBehaviour {

    public int[,] MainArray = new int[3, 3];
    public KeyCode vKey;

    public bool wait, toggleAI;
    public int turn, currentTexture, winner;

    public Texture[] textures;
    public string textSelect;
    public string boardPositionInMain;

    public int numPlayer, playerChk;
    public int player, opponent;
    public int sideChk;

    private int endGame;



    private int currentScore;
    private int[] bestMove = { 0, 0 };

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
        
        toggleAI = false;

        endGame = 0;
        //delete this later; here for testing
        //selected = transform.FindChild("TL"); -- Debug Line

        currentScore = 0;

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
        //Debug.Log("winner: " + winner + ", countEmpty: " + countEmpty);
        if (countEmpty == 0 && winner == 0) {
            print("Game Tied");
            endGame = 1;
        }else {
            if (1 == winner) {
                print("O is the winner!");
                endGame = 1;
            }   
            if (2 == winner) {
                print("X is the winner!");
                endGame = 1;
            }  
        }
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

    //AI optimized for a single board using the Minimax algorithm
    public int MiniMaxAB(int[,] simArray, int depth, int alpha, int beta, int turn) {

        //string empty = "empty: ";
        //string board = "";

        //for (int i = 0; i < 3; i++) {
        //    for (int j = 0; j < 3; j++) {
        //        if (0 == simArray[i, j]) {
        //            empty += "[" + i + ", " + j + "] ";
        //        }
        //    }
        //}
        //Debug.Log(empty + "\nPlayer: " + turn + "\nDepth: " + depth + "\nWinner: " + winner);
        

        //winCheck();
        if (depth <= 0 || winner != 0) {
            currentScore = boardEvaluation(simArray);
            //Debug.Log("currentScore: " + currentScore + ", winner: " + winner);
            return currentScore;
        }

        if(turn == opponent) { //maximize - opponent (AI)
            
            for (int i =0; i< 3; i++) {
                for(int j=0; j< 3; j++) {
                    if(simArray[i, j] == 0) {
                        simArray[i, j] = opponent;

                        //for (int x = 0; x < 3; x++) {
                        //    for (int y = 0; y < 3; y++) {
                        //        board += "[" + simArray[x, y] + "], ";
                        //    }
                        //    board += "\n";
                        //}
                        //Debug.Log(board + "opponent");

                        currentScore = MiniMaxAB(simArray, depth - 1, alpha, beta, player);
                        //Debug.Log("currentScore: " + currentScore + ", alpha: " + alpha + ", beta: " + beta + "-------- opponent");
                        if (currentScore > alpha) {
                            alpha = currentScore;
                            //Debug.Log("bestMove(opponent): " + i + ", " +  j);
                            //best move for opponent
                            bestMove[0] = i;
                            bestMove[1] = j;
                        }
                        if (alpha >= beta) {
                            //Debug.Log("beta cut-off");
                            //board = ""; //Debug line
                            simArray[i, j] = 0;
                            break; //beta cut-off
                        }
                        simArray[i, j] = 0;

                        //board = "";
                        //for (int x = 0; x < 3; x++) {
                        //    for (int y = 0; y < 3; y++) {
                        //        board += "[" + simArray[x, y] + "], ";
                        //    }
                        //    board += "\n";
                        //}
                        //Debug.Log(board + "/. opponent");
                    }
                    //board = ""; //Debug line
                }
            }
            return alpha;
        }else { //minimize - player
            
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (simArray[i, j] == 0) {
                        simArray[i, j] = player;

                        //for (int x = 0; x < 3; x++) {
                        //    for (int y = 0; y < 3; y++) {
                        //        board += "[" + simArray[x, y] + "], ";
                        //    }
                        //    board += "\n";
                        //}
                        //Debug.Log(board + "player");

                        currentScore = MiniMaxAB(simArray, depth - 1, alpha, beta, opponent);
                        //Debug.Log("currentScore: " + currentScore + ", beta: " + beta + ", alpha: " + alpha + "-------- player");
                        if (currentScore < beta) {
                            beta = currentScore;
                            //Debug.Log("bestMove(player): " + i + ", " + j);
                            //best move for player
                        }
                        if (alpha >= beta) {
                            //Debug.Log("alpha cut-off");
                            //board = ""; //Debug line
                            simArray[i, j] = 0;
                            break; //alpha cut-off
                        }
                            
                        simArray[i, j] = 0;

                        //board = "";
                        //for (int x = 0; x < 3; x++) {
                        //    for (int y = 0; y < 3; y++) {
                        //        board += "[" + simArray[x, y] + "], ";
                        //    }
                        //    board += "\n";
                        //}
                        //Debug.Log(board + "/. player");
                    }
                    //board = ""; //Debug line
                }
            }
            return beta;
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
                if (0 == i && board[0, j] == player) { //subtract for player
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
                } else if (0 == i && board[0, j] == opponent) { //addition for opponent/ai
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
                if (1 == i && board[1, j] == player) { //subtract for player
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
                } else if (1 == i && board[1, j] == opponent) { //addition for opponent/ai
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
                if (2 == i && board[2, j] == player) { //subtract for player
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
                } else if (2 == i && board[2, j] == opponent) { //addition for opponent/ai
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
            if (player == board[i, i]) { //subtract for player
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
            } else if (opponent == board[i, i]) { //addition for opponent/ai
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
                if (0 == i && board[j, 0] == player) { //subtract for player
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
                } else if (0 == i && board[j, 0] == opponent) { //subtract for opponent/ai
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
                if (1 == i && board[j, 1] == player) { //subtract for player
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
                } else if (1 == i && board[j, 1] == opponent) { //subtract for opponent/ai
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
                if (2 == i && board[j, 2] == player) { //subtract for player
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
                } else if (2 == i && board[j, 2] == opponent) { //subtract for opponent/ai
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
            if (player == board[i, 2 - i]) { //subtract for player
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
            } else if (opponent == board[i, 2 - i]) { //addition for opponent/ai
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

        //Debug.Log("c0: " + count[0] + ", c1: " + count[1] + ", c2: " + count[2] +
        //            "\nc3: " + count[3] + ", c4: " + count[4] + ", c5: " + count[5] +
        //            "\nc6: " + count[6] + ", c7: " + count[7]);

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


    /*------------------------------------------------------------------------------------------/
    /------------------------------ RETURN FUNCTIONS -------------------------------------------/
    /------------------------------------------------------------------------------------------*/
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

    public int winReturn() {
        return winner;
    }

    public string returnBoardPosition() {
        return boardPositionInMain;
    }
    /*-----------------------------------------------------------------------------------------/
    /-----------------------------------------------------------------------------------------*/
    
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
        if (1 == playerChk && 1 == sideChk && endGame == 0) {
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
                    if (numPlayer == 1 && endGame == 0) {
                        toggleAI = true;
                    }     
                    //simulatedWincheck(MainArray, turn);
                    //print("" + boardEvaluation(MainArray, turn));
                }
            }
        }
        if(endGame == 1) {
            if (Input.GetKeyDown(KeyCode.R)) {
                Debug.Log("RESTART");
            }
        }
    }

    void LateUpdate() {
        if (toggleAI) {
            Debug.Log("run AI-----------------------------");
            //highest = 0; lowest = 0;
            Debug.Log("--: " + MiniMaxAB(MainArray, 2, int.MinValue, int.MaxValue, turn) + ", bestMove: " + bestMove[0] + ", " + bestMove[1]);
            textureSwap(getPosition(bestMove[0],bestMove[1]), turn);
            //AIMinimax(MainArray, turn);
            //MiniMax(MainArray, turn, 0, 0);
            //Debug.Log(high[0] + ", " + high[1] + "-----");
            //Debug.Log(countMove[0] + ", " + countMove[1] + ", " + countMove[2] +
            //        ", " + countMove[3] + ", " + countMove[4] + ", " + countMove[5] +
            //        ", " + countMove[6] + ", " + countMove[7] + ", " + countMove[8]);
            winCheck();
            toggleAI = false;
        }
    }
}


//public void resetBoard() {
//    print("Would you like to play another game? (y/n)");
//    if (Input.GetKeyDown(KeyCode.Y)) {

//    } else if (Input.GetKeyDown(KeyCode.N)) {
//        print("Good Game.");
//    }
//}

//public void AIMinimax(int[,] simArray, int turn) {
//    //int score = 0;
//    //string empty = "empty: ";

//    //for (int i = 0; i < 3; i++) {
//    //    for (int j = 0; j < 3; j++) {
//    //        if (0 == simArray[i, j]) {
//    //            empty += "[" + i + ", " + j + "] ";
//    //        }
//    //    }
//    //}
//    //Debug.Log(empty + "\nPlayer: " + turn + "\nDepth: " + depth);


//    //if (simArray[1, 1] == 0) {
//    //    textureSwap("MM", turn);
//    //} else {
//    //--------------------------------------------------------------------------
//    //Rule 1: If there's a winning move, take it
//    //Horizontal
//    for (int i = 0; i < 3; i++) {
//        if (simArray[i, 0] == opponent && simArray[i, 1] == opponent && simArray[i, 2] == 0) {
//            textureSwap(getPosition(i, 2), turn);
//            return;
//        } else if (simArray[i, 0] == opponent && simArray[i, 1] == 0 && simArray[i, 2] == opponent) {
//            textureSwap(getPosition(i, 1), turn);
//            return;
//        } else if (simArray[i, 0] == 0 && simArray[i, 1] == opponent && simArray[i, 2] == opponent) {
//            textureSwap(getPosition(i, 0), turn);
//            return;
//        }
//    }
//    //Vertical
//    for (int i = 0; i < 3; i++) {
//        if (simArray[0, i] == opponent && simArray[1, i] == opponent && simArray[2, i] == 0) {
//            textureSwap(getPosition(2, i), turn);
//            return;
//        } else if (simArray[0, i] == opponent && simArray[1, i] == 0 && simArray[2, i] == opponent) {
//            textureSwap(getPosition(1, i), turn);
//            return;
//        } else if (simArray[0, i] == 0 && simArray[1, i] == opponent && simArray[2, i] == opponent) {
//            textureSwap(getPosition(0, i), turn);
//            return;
//        }
//    }

//    //Diagonal
//    if (simArray[0, 0] == opponent && simArray[1, 1] == opponent && simArray[2, 2] == 0) {
//        textureSwap(getPosition(2, 2), turn);
//        return;
//    } else if (simArray[0, 0] == opponent && simArray[1, 1] == 0 && simArray[2, 2] == opponent) {
//        textureSwap(getPosition(1, 1), turn);
//        return;
//    } else if (simArray[0, 0] == 0 && simArray[1, 1] == opponent && simArray[2, 2] == opponent) {
//        textureSwap(getPosition(0, 0), turn);
//        return;
//    }

//    //Anti-Diagonal
//    if (simArray[2, 0] == opponent && simArray[1, 1] == opponent && simArray[0, 2] == 0) {
//        textureSwap(getPosition(0, 2), turn);
//        return;
//    } else if (simArray[2, 0] == opponent && simArray[1, 1] == 0 && simArray[0, 2] == opponent) {
//        textureSwap(getPosition(1, 1), turn);
//        return;
//    } else if (simArray[2, 0] == 0 && simArray[1, 1] == opponent && simArray[0, 2] == opponent) {
//        textureSwap(getPosition(2, 0), turn);
//        return;
//    }

//    //--------------------------------------------------------------------------
//    //Rule 2: If opponent has a winning move, block it
//    //Horizontal
//    for (int i = 0; i < 3; i++) {
//        if (simArray[i, 0] == player && simArray[i, 1] == player && simArray[i, 2] == 0) {
//            textureSwap(getPosition(i, 2), turn);
//            return;
//        } else if (simArray[i, 0] == player && simArray[i, 1] == 0 && simArray[i, 2] == player) {
//            textureSwap(getPosition(i, 1), turn);
//            return;
//        } else if (simArray[i, 0] == 0 && simArray[i, 1] == player && simArray[i, 2] == player) {
//            textureSwap(getPosition(i, 0), turn);
//            return;
//        }
//    }
//    //Vertical
//    for (int i = 0; i < 3; i++) {
//        if (simArray[0, i] == player && simArray[1, i] == player && simArray[2, i] == 0) {
//            textureSwap(getPosition(2, i), turn);
//            return;
//        } else if (simArray[0, i] == player && simArray[1, i] == 0 && simArray[2, i] == player) {
//            textureSwap(getPosition(1, i), turn);
//            return;
//        } else if (simArray[0, i] == 0 && simArray[1, i] == player && simArray[2, i] == player) {
//            textureSwap(getPosition(0, i), turn);
//            return;
//        }
//    }

//    //Diagonal
//    if (simArray[0, 0] == player && simArray[1, 1] == player && simArray[2, 2] == 0) {
//        textureSwap(getPosition(2, 2), turn);
//        return;
//    } else if (simArray[0, 0] == player && simArray[1, 1] == 0 && simArray[2, 2] == player) {
//        textureSwap(getPosition(1, 1), turn);
//        return;
//    } else if (simArray[0, 0] == 0 && simArray[1, 1] == player && simArray[2, 2] == player) {
//        textureSwap(getPosition(0, 0), turn);
//        return;
//    }

//    //Anti-Diagonal
//    if (simArray[2, 0] == player && simArray[1, 1] == player && simArray[0, 2] == 0) {
//        textureSwap(getPosition(0, 2), turn);
//        return;
//    } else if (simArray[2, 0] == player && simArray[1, 1] == 0 && simArray[0, 2] == player) {
//        textureSwap(getPosition(1, 1), turn);
//        return;
//    } else if (simArray[2, 0] == 0 && simArray[1, 1] == player && simArray[0, 2] == player) {
//        textureSwap(getPosition(2, 0), turn);
//        return;
//    }

//    int[] test = { 0, 0 };
//    //--------------------------------------------------------------------------
//    //Rule 3: If you can create a fork, do so (i.e. multiple winning moves)
//    MiniMax(MainArray, turn, 0, 0);
//    for (int i = 0; i < 9; i++) {
//        if (temp < countMove[i]) {
//            temp = countMove[i];
//            pos = i;
//        }
//    }
//    Debug.Log(pos);
//    textureSwap(AImove(pos), turn);
//    //textureSwap(getPosition(test[0], test[1]), turn);
//    //Debug.Log(test[0] + ", " + test[1] + "-----test");
//    //Debug.Log("end of AIMinimax---------");
//    //}
//}

//public void move(int i, int j) {

//    if (i == 0 && j == 0)
//        countMove[0]++;
//    if (i == 0 && j == 1)
//        countMove[1]++;
//    if (i == 0 && j == 2)
//        countMove[2]++;
//    if (i == 1 && j == 0)
//        countMove[3]++;
//    if (i == 1 && j == 1)
//        countMove[4]++;
//    if (i == 1 && j == 2)
//        countMove[5]++;
//    if (i == 2 && j == 0)
//        countMove[6]++;
//    if (i == 2 && j == 1)
//        countMove[7]++;
//    if (i == 2 && j == 2)
//        countMove[8]++;
//}

//public string AImove(int position) {
//    switch (position) {
//        case 0:
//            return "TL";
//        case 1:
//            return "TM";
//        case 2:
//            return "TR";
//        case 3:
//            return "ML";
//        case 4:
//            return "MM";
//        case 5:
//            return "MR";
//        case 6:
//            return "BL";
//        case 7:
//            return "BM";
//        case 8:
//            return "BR";
//        default:
//            return null;
//    }
//}

//public void MiniMax(int[,] simArray, int turn, int score, int depth) {

//    int currentScore = 0;
//    //string board = "";


//    for (int i = 0; i < 3; i++) {
//        for (int j = 0; j < 3; j++) {
//            //Debug.Log(i + ", " + j);
//            if (simArray[i, j] == 0) {//&& depth <= 6
//                                      //Debug.Log("inside if: " + i + ", " + j);
//                simArray[i, j] = turn;

//                //Debug - Portion-----------------------------------------------
//                ////for (int x = 0; x < 3; x++) {
//                ////    for (int y = 0; y < 3; y++) {
//                ////        board += "[" + simArray[x, y] + "], ";
//                ////    }
//                ////    board += "\n";
//                ////}
//                ////Debug.Log(board);
//                //---------------------------------------------------------------
//                currentScore = boardEvaluation(simArray);

//                depth++;


//                //Need to work on evaluation of the board and which moves to make.
//                if (highest < currentScore) {
//                    highest = currentScore;
//                    setFlag = 1;
//                    //Need to keep track of the position where 1 is being played.
//                    high[0] = i; high[1] = j;
//                    move(high[0], high[1]);
//                    Debug.Log(i + ", " + j);
//                } else if (setFlag == 0) {
//                    highest = currentScore;
//                    lowest = currentScore;
//                    setFlag = 1;
//                }
//                if (lowest > currentScore) {
//                    lowest = currentScore;
//                }
//                Debug.Log("high: " + highest + ", low: " + lowest + ", currentScore: " + currentScore + ", score: " + score);

//                if ((i + j) != 4 & depth < 6) {

//                    MiniMax(simArray, abs(3 - turn), currentScore + score, depth);
//                    score = 0;
//                    setFlag = 0;
//                }

//                simArray[i, j] = 0;
//                //Debug.Log("simArray[" + i + ", " + j + "]: " + simArray[i, j]);

//                //Debug - Portion-----------------------------------------------
//                //board = "";
//                //for (int x = 0; x < 3; x++) {
//                //    for (int y = 0; y < 3; y++) {
//                //        board += "[" + simArray[x, y] + "], ";
//                //    }
//                //    board += "\n";
//                //}
//                //Debug.Log(board + "/.");
//                //---------------------------------------------------------------
//            }
//            //board = ""; //Debug line
//        }

//    }
//    //Debug.Log(high[0] + ", " + high[1] + "-----");
//}