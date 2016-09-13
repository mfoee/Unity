using UnityEngine;
using System.Collections;

public class BoardControl : MonoBehaviour
{

    public int[,] MainArray = new int[3, 3];
    public int[,] aiArray = new int[3, 3];
    public KeyCode vKey;
    char entered, pressed;
    public bool wait;
    public int turn, currentTexture, winner;
    public int player1, player2;
    public Texture[] textures;
    public string textSelect;
    public string boardPositionInMain;
    public int mainPlayer;
    public int playerChk;
    public int sideChk;

    private int user, simWin;

    Transform selected;
    Renderer[] renderers;

    // Use this for initialization
    void Start() {
        MainArray = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
        aiArray = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };

        //foreach (int element in MainArray) {
        //    //print(element); -- Debug line
        //}

        //Initialization
        turn = 0;
        player1 = 0;
        player2 = 0;
        currentTexture = 0;
        renderers = transform.GetComponentsInChildren<Renderer>();
        winner = 0;

        //delete this later; here for testing
        //selected = transform.FindChild("TL"); -- Debug Line
    }


    void selectPlayer()
    {
        /**
         * This function is to let player select which 
         * player they want to play, it is either 
         * player 1 or player 2
        */
        if (Input.GetKeyDown("1"))
        {
            print("You are Player 1.");
            mainPlayer = 1;
            playerChk = 1;
        }
        else if (Input.GetKeyDown("2"))
        {
            print("You are Player 2.");
            mainPlayer = 2;
            playerChk = 1;
        }

    }


    void selectSide()
    {
        /** 
         * This function will be used to determine whether the player
         * wants to play as X or O
        */

        if (Input.GetKeyDown("x"))
        {
            print("Player " + mainPlayer + ", you are X.");
            if (1 == mainPlayer)
            {
                player1 = 2; //-- 'x';
                player2 = 1; //-- 'o';
            }
            else if (2 == mainPlayer)
            {
                player2 = 2; //-- 'x';
                player1 = 1; //-- 'o';
            }
            turn = 2;
            sideChk = 1;
        }
        else if (Input.GetKeyDown("o"))
        {
            print("Player " + mainPlayer + ", you are O.");
            if (1 == mainPlayer)
            {
                player1 = 1; //--'o';
                player2 = 2; //-- 'x';
            }
            else if (2 == mainPlayer)
            {
                player2 = 1; //-- 'o';
                player1 = 2; //-- 'x';
            }
            turn = 1;
            sideChk = 1;
        }
    }


    void rendererTextureChange (string toSwap, int swapTo) {
        foreach (Renderer r in renderers) {
            if (r.name == toSwap) { 
                r.material.mainTexture = textures[swapTo];
            }
        }

    }

    private int abs (int subject)
    {
        if (subject>0)
        {
            //Debug.Log("subject: " + subject);
            return subject;
        }
        else
        {
            //Debug.Log("subject: -" + subject);
            return -subject;
        }
    }

    //swaps the texture of toSwap into swapTo; 0 for transparent, 1 for O, 2 for X
    public void textureSwap(string toSwap, int swapTo)
    {
        if (toSwap == "TL"&& MainArray[0,0]==0)
        {
            MainArray[0, 0] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);

            //Testing
            //Debug.Log("TM MainArray: " + getMainArray("TM"));
            //if (0 == getMainArray("TM")) {
            //    textureSwap("TM",turn);
            //}
            
        }
         else if (toSwap == "TM"&& MainArray[0, 1] == 0)
        {
            MainArray[0, 1] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        }
        else if (toSwap == "TR"&& MainArray[0, 2] == 0)
        {
            MainArray[0, 2] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        }
        else if (toSwap == "ML" && MainArray[1, 0] == 0)
        {
            MainArray[1, 0] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        }
        else if (toSwap == "MM" && MainArray[1, 1] == 0)
        {
            MainArray[1, 1] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        }
        else if (toSwap == "MR" && MainArray[1, 2] == 0)
        {
            MainArray[1, 2] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        }
        else if (toSwap == "BL" && MainArray[2, 0] == 0)
        {
            MainArray[2, 0] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        }
        else if (toSwap == "BM" && MainArray[2, 1] == 0)
        {
            MainArray[2, 1] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        }
        else if (toSwap == "BR" && MainArray[2, 2] == 0)
        {
            MainArray[2, 2] = swapTo;
            rendererTextureChange(toSwap, swapTo);
            turn = abs(3 - swapTo);
        }

    }

    public void winCheck()
    {

        //Check Row
        for(int i=0; i<3; i++) {
            //Debug.Log("i=" + i + "; MainArray[0," + i + "]=" + MainArray[0, i]);
            if (MainArray[i,0] != 0 && MainArray[i, 0] == MainArray[i, 1] && MainArray[i, 0] == MainArray[i, 2]) {
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
        for(int j=0; j<3; j++) {
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

    }

    public int winReturn()
    {
        return winner;
    }

    public string returnBoardPosition()
    {
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

    public void AIMinimax(int[,] simArray, int player)
    {
        int[,] simulatedArray = simArray;

        //Preferred moves in the order of left to right: 
        //{1,1}, {0,0}, {0,2}, {2,0}, {2,2}, {0,1}, {1,0}, {1,2}, {2,1};

        if (simArray[1, 1] == 0) {
            textureSwap("MM", player);
        }else if(simArray[0,0]== 0) {
            textureSwap("TL", player);
        } else if (simArray[0, 2] == 0) {
            textureSwap("TR", player);
        } else if (simArray[2, 0] == 0) {
            textureSwap("BL", player);
        } else if (simArray[2, 2] == 0) {
            textureSwap("BR", player);
        } else if (simArray[0, 1] == 0) {
            textureSwap("TM", player);
        } else if (simArray[1, 0] == 0) {
            textureSwap("ML", player);
        } else if (simArray[1, 2] == 0) {
            textureSwap("MR", player);
        } else if (simArray[2, 1] == 0) {
            textureSwap("BM", player);
        }

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
    }

    public int simulatedWincheck(int[,] simArray, int player)
    {
        
        int count0 = 0, count1 = 0, count2 = 0;
        int count3 = 0, count4 = 0, count5 = 0;
        int count6 = 0, count7 = 0;

        Debug.Log("Player: " + player);

        //Heuristic Score check:
        //Horiziontal - Row
        for(int i=0; i<3; i++) {
            for(int j=0; j<3; j++) {
                if(0 == i && simArray[0,j] == 2) {
                    count0 -= 10;
                }else if(0 == i && simArray[0,j] == 1) {
                    count0 += 10;
                }
                if (1 == i && simArray[1, j] == 2) {
                    count1 -= 10;
                } else if (1 == i && simArray[1, j] == 1) {
                    count1 += 10;
                }
                if (2 == i && simArray[2, j] == 2) {
                    count2 -= 10;
                } else if (2 == i && simArray[2, j] == 1) {
                    count2 += 10;
                }
            }

            //Check diagonal
            if (2 == simArray[i, i]) {
                count6 -= 10;
            } else if(1 == simArray[i,i]){
                count6 += 10;
            }
        }

        //Vertical - Column
        for (int i = 0; i < 3; i++) {
            for (int j = 0; j < 3; j++) {
                if (0 == i && simArray[j, 0] == 2) {
                    count3 -= 10;
                } else if (0 == i && simArray[j, 0] == 1) {
                    count3 += 10;
                }
                if (1 == i && simArray[j, 1] == 2) {
                    count4 -= 10;
                } else if (1 == i && simArray[j, 1] == 1) {
                    count4 += 10;
                }
                if (2 == i && simArray[j, 2] == 2) {
                    count5 -= 10;
                } else if (2 == i && simArray[j, 2] == 1) {
                    count5 += 10;
                }
            }

            //Check anti-diagonal
            if (2 == simArray[i, 2 - i]) {
                count7 -= 10;
            }else if(1 == simArray[i, 2 - i]) {
                count7 += 10;
            }
        }

        Debug.Log("|" + count0 + "|" + count1 + "|" + count2);
        Debug.Log("|" + count3 + "|" + count4 + "|" + count5);
        Debug.Log("|" + count6 + "|" + count7);
        return 0;
    }


    // Update is called once per frame
    void Update() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (0 == playerChk)
        {
            selectPlayer();
        }
        if (0 == sideChk)
        {
            selectSide();
        }
        if (1 == playerChk && 1 == sideChk)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //print("Pressed left mouse click"); -- Debug line
                if (Physics.Raycast(ray, out hit))
                {
                    textSelect = hit.collider.name;
                    //Debug.Log("texture name: " + textSelect); //---- Debug line
                }
                if ("Side" != textSelect && "Plane" != textSelect)
                {
                    textureSwap(textSelect, turn);
                    //winCheck();
                    //if AI, then AI move
                    //MiniMax(turn);
                    AIMinimax(MainArray, turn);
                    //simulatedWincheck(MainArray, turn);

                }

            }
        }
    }
}