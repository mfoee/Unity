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
        string board = "";

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
            Debug.Log("currentScore: " + currentScore + ", winner: " + winner + "depth 0");
            return currentScore;
        }

        if(turn == opponent) { //maximize - opponent (AI)
            
            for (int i =0; i< 3; i++) {
                for(int j=0; j< 3; j++) {
                    if(simArray[i, j] == 0) {
                        simArray[i, j] = opponent;

                        for (int x = 0; x < 3; x++) {
                            for (int y = 0; y < 3; y++) {
                                board += "[" + simArray[x, y] + "], ";
                            }
                            board += "\n";
                        }
                        Debug.Log(board + "opponent");

                        currentScore = MiniMaxAB(simArray, depth - 1, alpha, beta, player);
                        Debug.Log("currentScore: " + currentScore + ", alpha: " + alpha + ", beta: " + beta + "-------- opponent");
                        if (currentScore > alpha) {
                            alpha = currentScore;
                            Debug.Log("bestMove(opponent): " + i + ", " +  j);
                            //best move for opponent
                            bestMove[0] = i;
                            bestMove[1] = j;
                        }
                        if (alpha >= beta) {
                            //Debug.Log("beta cut-off");
                            board = ""; //Debug line
                            simArray[i, j] = 0;
                            break; //beta cut-off
                        }
                        simArray[i, j] = 0;

                        board = "";
                        for (int x = 0; x < 3; x++) {
                            for (int y = 0; y < 3; y++) {
                                board += "[" + simArray[x, y] + "], ";
                            }
                            board += "\n";
                        }
                        Debug.Log(board + "/. opponent");
                    }
                    board = ""; //Debug line
                }
            }
            return alpha;
        }else { //minimize - player
            
            for (int i = 0; i < 3; i++) {
                for (int j = 0; j < 3; j++) {
                    if (simArray[i, j] == 0) {
                        simArray[i, j] = player;

                        for (int x = 0; x < 3; x++) {
                            for (int y = 0; y < 3; y++) {
                                board += "[" + simArray[x, y] + "], ";
                            }
                            board += "\n";
                        }
                        Debug.Log(board + "player");

                        currentScore = MiniMaxAB(simArray, depth - 1, alpha, beta, opponent);
                        Debug.Log("currentScore: " + currentScore + ", beta: " + beta + ", alpha: " + alpha + "-------- player");
                        if (currentScore < beta) {
                            beta = currentScore;
                            //Debug.Log("bestMove(player): " + i + ", " + j);
                            //best move for player
                        }
                        if (alpha >= beta) {
                            //Debug.Log("alpha cut-off");
                            board = ""; //Debug line
                            simArray[i, j] = 0;
                            break; //alpha cut-off
                        }
                            
                        simArray[i, j] = 0;

                        board = "";
                        for (int x = 0; x < 3; x++) {
                            for (int y = 0; y < 3; y++) {
                                board += "[" + simArray[x, y] + "], ";
                            }
                            board += "\n";
                        }
                        Debug.Log(board + "/. player");
                    }
                    board = ""; //Debug line
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

        //Heuristic Scorring
        //for Player: -- (subtract)
        //for Opponent: -- (addition)
        for (int i = 0; i < 3; i++) {
            for(int j = 0; j < 3; j++) {
                //Horizontal
                if (board[i, j] == player) {
                    countX[i]++; //player count
                }else if(board[i, j] == opponent) {
                    countO[i]++; //opponent count
                }
                //Vertical
                if (board[j, i] == player) {
                    countX[i + 3]++; //player count
                }else if(board[j, i] == opponent) {
                    countO[i + 3]++; //opponent count
                }
            }
            //Diagonal
            if(board[i, i] == player) {
                countX[6]++; //player count
            }else if(board[i, i] == opponent) {
                countO[6]++; //opponent count
            }
            //Anti-Diagonal
            if (board[i, 2 - i] == player) {
                countX[7]++; //player count
            }else if(board[i, 2 - i] == opponent) {
                countO[7]++; //opponent count
            }
        }
        Debug.Log("countX: " + countX[0] + ", " + countX[1] + ", " + countX[2] + ", " + countX[3] + ", " + countX[4] + ", " + countX[5] + ", " + countX[6] + ", " + countX[7] + ".");
        Debug.Log("countO: " + countO[0] + ", " + countO[1] + ", " + countO[2] + ", " + countO[3] + ", " + countO[4] + ", " + countO[5] + ", " + countO[6] + ", " + countO[7] + ".");
        //POINTs
        for (int i = 0; i < 8; i++) {
            //Player scoring
            if(countX[i] == 1) {
                count[i] -= 1;
            }else if(countX[i] == 2) {
                count[i] -= 10;
            }else if(countX[i] == 3) {
                count[i] -= 100;
            }

            //Opponent scoring
            if(countO[i] == 1) {
                count[i] += 1;
            }else if(countO[i] == 2) {
                count[i] += 10;
            }else if(countO[i] == 3) {
                count[i] += 100;
            }
        }

        Debug.Log("count: " + count[0] + ", " + count[1] + ", " + count[2] + ", " + count[3] + ", " + count[4] + ", " + count[5] + ", " + count[6] + ", " + count[7] + ".");

        for (int i = 0; i < 8; i++) {
            sum += count[i];
        }

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
                    if (numPlayer == 1 && endGame == 0) {
                        toggleAI = true;
                    }     
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
            //Debug.Log("run AI-----------------------------");
            Debug.Log("--: " + MiniMaxAB(MainArray, 2, int.MinValue, int.MaxValue, turn) + ", bestMove: " + bestMove[0] + ", " + bestMove[1]);
            //MiniMaxAB(MainArray, 2, int.MinValue, int.MaxValue, turn);
            textureSwap(getPosition(bestMove[0],bestMove[1]), turn);
            winCheck();
            toggleAI = false;
        }
    }
}