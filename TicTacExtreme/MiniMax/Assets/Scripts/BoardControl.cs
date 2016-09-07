using UnityEngine;
using System.Collections;

public class BoardControl : MonoBehaviour
{

    public int[,] MainArray = new int[3, 3];
    public KeyCode vKey;
    char entered, pressed;
    public bool wait;
    public int turn, currentTexture, winner;
    public char player1, player2;
    public Texture[] textures;
    public string textSelect;
    public string boardPositionInMain;
    public int mainPlayer;
    public int playerChk;
    public int sideChk;

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
        player1 = '\0';
        player2 = '\0';
        currentTexture = 0;
        renderers = transform.GetComponentsInChildren<Renderer>();
        winner = 0;

        //delete this later; here for testing
        //selected = transform.FindChild("TL"); -- Debug Line
    }

    void input() {

    }
    void display() {

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
                player1 = 'x';
                player2 = 'o';
            }
            else if (2 == mainPlayer)
            {
                player2 = 'x';
                player1 = 'o';
            }
            turn = 2;
            sideChk = 1;
        }
        else if (Input.GetKeyDown("o"))
        {
            print("Player " + mainPlayer + ", you are O.");
            if (1 == mainPlayer)
            {
                player1 = 'o';
                player2 = 'x';
            }
            else if (2 == mainPlayer)
            {
                player2 = 'o';
                player1 = 'x';
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
            return subject;
        }
        else
        {
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
        //if (winner != 0)
        //{
        //    return;
        //}
        //else
        //{
        //    //Debug.Log("inside winCheck of BoardControl of " + boardPositionInMain);
        //    if (MainArray[0, 0] == MainArray[0, 1] && MainArray[0, 0] == MainArray[0, 2])
        //    {
        //        winner = MainArray[0, 0];
        //    }
        //    else if (MainArray[1, 0] == MainArray[1, 1] && MainArray[1, 0] == MainArray[1, 2])
        //    {
        //        winner = MainArray[1, 0];
        //    }
        //    else if (MainArray[2, 0] == MainArray[2, 1] && MainArray[2, 0] == MainArray[2, 2])
        //    {
        //        winner = MainArray[2, 0];
        //    }
        //    else if (MainArray[0, 0] == MainArray[1, 0] && MainArray[0, 0] == MainArray[2, 0])
        //    {
        //        winner = MainArray[0, 0];
        //    }
        //    else if (MainArray[0, 1] == MainArray[1, 1] && MainArray[0, 1] == MainArray[2, 1])
        //    {
        //        winner = MainArray[0, 1];
        //    }
        //    else if (MainArray[0, 2] == MainArray[1, 2] && MainArray[0, 2] == MainArray[2, 2])
        //    {
        //        winner = MainArray[0, 2];
        //    }
        //    else if (MainArray[0, 0] == MainArray[1, 1] && MainArray[0, 0] == MainArray[2, 2])
        //    {
        //        winner = MainArray[0, 0];
        //    }
        //    else if (MainArray[0, 2] == MainArray[1, 1] && MainArray[0, 2] == MainArray[2, 0])
        //    {
        //        winner = MainArray[2, 0];
        //    }
        //}

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

        if(posTextSelect == "TL") {
            return MainArray[0, 0];
        }else if(posTextSelect == "TM") {
            return MainArray[0, 1];
        }else if (posTextSelect == "TR") {
            return MainArray[0, 2];
        } else if (posTextSelect == "ML") {
            return MainArray[1, 0];
        } else if (posTextSelect == "MM") {
            return MainArray[1, 1];
        } else if (posTextSelect == "MR") {
            return MainArray[1, 2];
        } else if (posTextSelect == "BL") {
            return MainArray[2, 0];
        } else if (posTextSelect == "BM") {
            return MainArray[2, 1];
        } else if (posTextSelect == "BR") {
            return MainArray[2, 2];
        } else {
            return 0;
        }
    }

    //AI optimized for a single board using the Minimax algorithm

    public int AIMinimax(int[,] simArray, int player)
    {
        int[,] simulatedArray = simArray;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (simulatedArray [i,j] ==0)
                {
                    AIMinimax(simulatedArray, abs(3 - player));
                }
            }
        }
    }

    public int sumulatedWincheck(int[,] simArray, int player)
    {
        for (int i = 0; i < 3; i++)
        {
            //Debug.Log("i=" + i + "; MainArray[0," + i + "]=" + MainArray[0, i]);
            if (simArray[i, 0] != 0 && simArray[i, 0] == simArray[i, 1] && simArray[i, 0] == simArray[i, 2])
            {
                //Debug.Log("Winner Row");
                winner = simArray[i, 0];
            }
            //Check Diagonal
            if (simArray[i, 0] != 0 && simArray[0, 0] == simArray[1, 1] && simArray[0, 0] == simArray[2, 2])
            {
                //Debug.Log("Winner Diagonal");
                winner = simArray[0, 0];
            }
        }

        //Check Column
        for (int j = 0; j < 3; j++)
        {
            //Debug.Log("j=" + j + "; MainArray[" + j + ",0]=" + MainArray[j, 0]);
            if (simArray[0, j] != 0 && simArray[0, j] == simArray[1, j] && simArray[0, j] == simArray[2, j])
            {
                //Debug.Log("Winner Column");
                winner = simArray[0, j];
            }
            //Check Anti-Diagonal
            if (simArray[0, j] != 0 && simArray[2, 0] == simArray[1, 1] && simArray[2, 0] == simArray[0, 2])
            {
                //Debug.Log("Winner Anti-Diagonal");
                winner = simArray[2, 0];
            }
        }
        return winner;
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
                    //print("texture name: " + textSelect); ---- Debug line
                }
                if ("Side" != textSelect)
                {
                    textureSwap(textSelect, turn);
                    winCheck();
                }

            }
        }
    }
}