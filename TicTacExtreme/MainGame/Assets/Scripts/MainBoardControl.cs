﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class MainBoardControl : MonoBehaviour {


    public int playerChk, sideChk, mainPlayer, turn, currentTexture, winner, pass;
    public float fieldOfView;
    public char player1, player2;
    public string textSelect,textSelectParent, nextPlay;
    public GameObject[,] MainArrayBoards = new GameObject[3, 3];
    public GameObject temp;
    string position_clicked = "";
    public int mainWinner;

    public Text winnerText;
    public Text nextMove;
    private const int KEYFRAMENUMLERP = 20;

    // Use this for initialization
    void Start () {
        MainArrayBoards[0, 0] = GameObject.Find("Board_TL");
        MainArrayBoards[0, 1] = GameObject.Find("Board_TM");
        MainArrayBoards[0, 2] = GameObject.Find("Board_TR");

        MainArrayBoards[1, 0] = GameObject.Find("Board_ML");
        MainArrayBoards[1, 1] = GameObject.Find("Board_MM");
        MainArrayBoards[1, 2] = GameObject.Find("Board_MR");

        MainArrayBoards[2, 0] = GameObject.Find("Board_BL");
        MainArrayBoards[2, 1] = GameObject.Find("Board_BM");
        MainArrayBoards[2, 2] = GameObject.Find("Board_BR");

        mainWinner = 0;
        pass = 0;

        winnerText.text = "";
        nextMove.text = "";
        nextPlay = "";
    }

    void Reset() {
        Debug.Log("Board needs to be reset.");
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

    void updateTurn()
    {
        switch (turn)
        {
            case 1:
                if (2 == mainPlayer)
                {
                    turn = 2;
                    print("Player " + turn + "'s turn.");
                }
                else
                {
                    print("Player " + turn + "'s turn.");
                    turn = 2;
                }
                break;
            case 2:
                if (2 == mainPlayer)
                {
                    turn = 1;
                    print("Player " + turn + "'s turn.");
                }
                else
                {
                    print("Player " + turn + "'s turn.");
                    turn = 1;
                }
                break;
        }
    }

    public void mainWinCheck()
    {
        //if (mainWinner!= 0)
        //{
        //    return;
        //}
        //else
        //{
        //Debug.Log("Inside mainWinCheck. mainWinner = " + mainWinner);
        //Debug.Log("[0,0]: " + MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn() +
        //        ", [0,1]:  " + MainArrayBoards[0, 1].GetComponentInChildren<BoardControl>().winReturn() +
        //        ", [0,2]:  " + MainArrayBoards[0, 2].GetComponentInChildren<BoardControl>().winReturn());

        //Debug.Log("[1,0]: " + MainArrayBoards[1, 0].GetComponentInChildren<BoardControl>().winReturn() +
        //        ", [1,1]: " + MainArrayBoards[1, 1].GetComponentInChildren<BoardControl>().winReturn() +
        //        ", [1,2]: " + MainArrayBoards[1, 2].GetComponentInChildren<BoardControl>().winReturn());

        //Debug.Log("[2,0]: " + MainArrayBoards[2, 0].GetComponentInChildren<BoardControl>().winReturn() +
        //        ", [2,1]: " + MainArrayBoards[2, 1].GetComponentInChildren<BoardControl>().winReturn() +
        //        ", [2,2]: " + MainArrayBoards[2, 2].GetComponentInChildren<BoardControl>().winReturn());

        //if (MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[0, 1].GetComponentInChildren<BoardControl>().winReturn() &&
        //        MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[0, 2].GetComponentInChildren<BoardControl>().winReturn())
        //{
        //    mainWinner = MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn();
        //}else if (MainArrayBoards[1, 0].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[1, 1].GetComponentInChildren<BoardControl>().winReturn() && 
        //            MainArrayBoards[1, 0].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[1, 2].GetComponentInChildren<BoardControl>().winReturn()) 
        //{
        //    mainWinner = MainArrayBoards[1, 0].GetComponentInChildren<BoardControl>().winReturn();
        //}else if (MainArrayBoards[2, 0].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[2, 1].GetComponentInChildren<BoardControl>().winReturn() && 
        //            MainArrayBoards[2, 0].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[2, 2].GetComponentInChildren<BoardControl>().winReturn()) 
        //{
        //    mainWinner = MainArrayBoards[2, 0].GetComponentInChildren<BoardControl>().winReturn();
        //}else if (MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[1, 0].GetComponentInChildren<BoardControl>().winReturn() && 
        //            MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[2, 0].GetComponentInChildren<BoardControl>().winReturn()) 
        //{
        //    mainWinner = MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn();
        //}else if (MainArrayBoards[0, 1].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[1, 1].GetComponentInChildren<BoardControl>().winReturn() && 
        //            MainArrayBoards[0, 1].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[2, 1].GetComponentInChildren<BoardControl>().winReturn()) 
        //{
        //    mainWinner = MainArrayBoards[0, 1].GetComponentInChildren<BoardControl>().winReturn();
        //}else if (MainArrayBoards[0, 2].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[1, 2].GetComponentInChildren<BoardControl>().winReturn() && 
        //            MainArrayBoards[0, 2].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[2, 2].GetComponentInChildren<BoardControl>().winReturn()) 
        //{
        //    mainWinner = MainArrayBoards[0, 2].GetComponentInChildren<BoardControl>().winReturn();
        //}else if (MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[1, 1].GetComponentInChildren<BoardControl>().winReturn() && 
        //            MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[2, 2].GetComponentInChildren<BoardControl>().winReturn()) 
        //{
        //    mainWinner = MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn();
        //}else if (MainArrayBoards[0, 2].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[1, 1].GetComponentInChildren<BoardControl>().winReturn() && 
        //            MainArrayBoards[0, 2].GetComponentInChildren<BoardControl>().winReturn() == MainArrayBoards[2, 0].GetComponentInChildren<BoardControl>().winReturn()) 
        //{
        //    mainWinner = MainArrayBoards[2, 0].GetComponentInChildren<BoardControl>().winReturn();
        //}
        //}

        //Check Row
        for (int i=0; i<3; i++) {
                if(MainArrayBoards[i, 0].GetComponentInChildren<BoardControl>().winReturn() != 0 && 
                    MainArrayBoards[i, 0].GetComponentInChildren<BoardControl>().winReturn() == 
                        MainArrayBoards[i, 1].GetComponentInChildren<BoardControl>().winReturn() && 
                            MainArrayBoards[i, 0].GetComponentInChildren<BoardControl>().winReturn() == 
                                MainArrayBoards[i, 2].GetComponentInChildren<BoardControl>().winReturn()) {
                    mainWinner = MainArrayBoards[i, 0].GetComponentInChildren<BoardControl>().winReturn();
                }
                
                //Check Diagonal
                if(MainArrayBoards[i, 0].GetComponentInChildren<BoardControl>().winReturn() != 0 && 
                    MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn() == 
                        MainArrayBoards[1, 1].GetComponentInChildren<BoardControl>().winReturn() && 
                            MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn() == 
                                MainArrayBoards[2, 2].GetComponentInChildren<BoardControl>().winReturn()) {
                    mainWinner = MainArrayBoards[0, 0].GetComponentInChildren<BoardControl>().winReturn();
                }
            }

            //Check Column
            for(int j=0; j<3; j++) {
                if(MainArrayBoards[0, j].GetComponentInChildren<BoardControl>().winReturn() != 0 && 
                    MainArrayBoards[0, j].GetComponentInChildren<BoardControl>().winReturn() == 
                        MainArrayBoards[1, j].GetComponentInChildren<BoardControl>().winReturn() && 
                            MainArrayBoards[0, j].GetComponentInChildren<BoardControl>().winReturn() == 
                                MainArrayBoards[2, j].GetComponentInChildren<BoardControl>().winReturn()) {
                    mainWinner = MainArrayBoards[0, j].GetComponentInChildren<BoardControl>().winReturn();
                }
                
                //Check Anti-Diagonal
                if(MainArrayBoards[0, j].GetComponentInChildren<BoardControl>().winReturn() != 0 &&
                    MainArrayBoards[2, 0].GetComponentInChildren<BoardControl>().winReturn() == 
                        MainArrayBoards[1, 1].GetComponentInChildren<BoardControl>().winReturn() &&
                            MainArrayBoards[2, 0].GetComponentInChildren<BoardControl>().winReturn() == 
                                MainArrayBoards[0, 2].GetComponentInChildren<BoardControl>().winReturn()) {
                    mainWinner = MainArrayBoards[2, 0].GetComponentInChildren<BoardControl>().winReturn();
                }
            }
            

            if(0 != mainWinner) {
                if (1 == mainWinner) {
                    winnerText.text = "O's the Winner!";
                }
                if (2 == mainWinner)
                    winnerText.text = "X's the Winner!";
                return;
            }
        
    }

    //linear interpolation function for smooth camera animation
    //change KEYFRAMENUMLERP for number of keyframes in animation
    //Start is the starting position, end is the ending position, FOV is the field of view
    void cameraLerp(float startPosX, float startPosY, float startPosZ, int startFOV, float endPosX, float endPosY, float endPosZ, float endFOV)
    {
        for (int keyframeCounter = 0; keyframeCounter <= KEYFRAMENUMLERP; keyframeCounter++)
        {
            //Camera.main.transform.position = new Vector3(startPosX / KEYFRAMENUMLERP * keyframeCounter + endPosX / KEYFRAMENUMLERP * (KEYFRAMENUMLERP - keyframeCounter),
            //startPosY / KEYFRAMENUMLERP * keyframeCounter + endPosY / KEYFRAMENUMLERP * (KEYFRAMENUMLERP - keyframeCounter),
            //startPosZ / KEYFRAMENUMLERP * keyframeCounter + endPosZ / KEYFRAMENUMLERP * (KEYFRAMENUMLERP - keyframeCounter));
            //Camera.main.fieldOfView = startFOV / KEYFRAMENUMLERP * keyframeCounter + endFOV / KEYFRAMENUMLERP * (KEYFRAMENUMLERP - keyframeCounter);

            Camera.main.transform.position = new Vector3(startPosX / KEYFRAMENUMLERP * (KEYFRAMENUMLERP - keyframeCounter) + endPosX / KEYFRAMENUMLERP * keyframeCounter,
            startPosY / KEYFRAMENUMLERP * (KEYFRAMENUMLERP - keyframeCounter) + endPosY / KEYFRAMENUMLERP * keyframeCounter,
            startPosZ / KEYFRAMENUMLERP * (KEYFRAMENUMLERP - keyframeCounter) + endPosZ / KEYFRAMENUMLERP * keyframeCounter);
            Camera.main.fieldOfView = startFOV / KEYFRAMENUMLERP * (KEYFRAMENUMLERP - keyframeCounter) + endFOV / KEYFRAMENUMLERP * keyframeCounter;
            System.Threading.Thread.Sleep(500/KEYFRAMENUMLERP);

            //Camera.main.transform.position = new Vector3(endPosX, endPosY, endPosZ);
            //Camera.main.fieldOfView = endFOV;
        }
    }

    GameObject returnObjectInPosition (string pos)
    {
        return GameObject.Find("Board_"+pos);
    }

    void cameraZoom(String board)
    {
        GameObject targetBoard = returnObjectInPosition(board);
        //Top Right - 50/50/0
        //Top Mid   - 0/50/0
        //Top Left  - -50/50/0
        //Mid Right - 50/0/0
        //Mid Mid   - 0/0/0
        //Mid Left  - -50/0/0
        //Bot Right - 50/-50/0
        //Bot Mid   - 0/-50/0
        //Bot Left  - -50/-50/0

        if (Camera.main.fieldOfView == 25)
        {
            cameraLerp(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z, 25, targetBoard.transform.position.x, 150, targetBoard.transform.position.z, 60);
        }
        else if (Camera.main.fieldOfView == 60)
        {
            cameraLerp(Camera.main.transform.position.x, Camera.main.transform.position.y, Camera.main.transform.position.z, 60, targetBoard.transform.position.x, 115, targetBoard.transform.position.z, 25);
        }
        /*
        switch (board) {
            case "TL":
                if (Camera.main.fieldOfView == 25) {
                    Camera.main.fieldOfView = 60;
                    Camera.main.transform.Translate(50f, -50f, 0f);
                } else if (Camera.main.fieldOfView == 60) {
                    Camera.main.fieldOfView = 25;
                    Camera.main.transform.Translate(-50f, 50f, 0f);
                }
                break;
            case "TM":
                if (Camera.main.fieldOfView == 25) {
                    Camera.main.fieldOfView = 60;
                    Camera.main.transform.Translate(0f, -50f, 0f);
                } else if (Camera.main.fieldOfView == 60) {
                    Camera.main.fieldOfView = 25;
                    Camera.main.transform.Translate(0f, 50f, 0f);
                }
                break;
            case "TR":
                if (Camera.main.fieldOfView == 25) {
                    Camera.main.fieldOfView = 60;
                    Camera.main.transform.Translate(-50f, -50f, 0f);
                } else if (Camera.main.fieldOfView == 60) {
                    Camera.main.fieldOfView = 25;
                    Camera.main.transform.Translate(50f, 50f, 0f);
                }
                break;
            case "ML":
                if (Camera.main.fieldOfView == 25) {
                    Camera.main.fieldOfView = 60;
                    Camera.main.transform.Translate(50f, -50f, 0f);
                } else if (Camera.main.fieldOfView == 60) {
                    Camera.main.fieldOfView = 25;
                    Camera.main.transform.Translate(-50f, 50f, 0f);
                }
                break;
            case "MM":
                if (Camera.main.fieldOfView == 25) {
                    Camera.main.fieldOfView = 60;
                } else if (Camera.main.fieldOfView == 60) {
                    Camera.main.fieldOfView = 25;
                }
                break;
            case "MR":
                if (Camera.main.fieldOfView == 25) {
                    Camera.main.fieldOfView = 60;
                    Camera.main.transform.Translate(-50f, 0f, 0f);
                } else if (Camera.main.fieldOfView == 60) {
                    Camera.main.fieldOfView = 25;
                    Camera.main.transform.Translate(50f, 0f, 0f);
                }
                break;
            case "BL":
                if (Camera.main.fieldOfView == 25) {
                    Camera.main.fieldOfView = 60;
                    Camera.main.transform.Translate(50f, 50f, 0f);
                } else if (Camera.main.fieldOfView == 60) {
                    Camera.main.fieldOfView = 25;
                    Camera.main.transform.Translate(-50f, -50f, 0f);
                }
                break;
            case "BM":
                if (Camera.main.fieldOfView == 25) {
                    Camera.main.fieldOfView = 60;
                    Camera.main.transform.Translate(0f, 50f, 0f);
                } else if (Camera.main.fieldOfView == 60) {
                    Camera.main.fieldOfView = 25;
                    Camera.main.transform.Translate(0f, -50f, 0f);
                }
                break;
            case "BR":
                if (Camera.main.fieldOfView == 25) {
                    Camera.main.fieldOfView = 60;
                    Camera.main.transform.Translate(-50f, 50f, 0f);
                } else if (Camera.main.fieldOfView == 60) {
                    Camera.main.fieldOfView = 25;
                    Camera.main.transform.Translate(50f, -50f, 0f);
                }
                break;
                
        }
        */

    }

    // Update is called once per frame
    void Update () {

        if (0 == playerChk)
        {
            selectPlayer();
        }
        if (0 == sideChk)
        {
            selectSide();
        }
        
        //MainArrayBoards[0,0].GetComponent<BoardControl>().winCheck();

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (1 == playerChk && 1 == sideChk)
        {
            if (Input.GetMouseButtonDown(0))
            {
                //print("Pressed left mouse click"); //---- Debug line
                if (Physics.Raycast(ray, out hit))
                {

                    textSelect = hit.collider.name;
                    if("Side" != textSelect && "Plane" != textSelect) {
                        try {
                            textSelectParent = hit.collider.transform.parent.parent.name;
                            //Debug.Log("textSelectParent: " + textSelectParent);
                        } catch (Exception e) {
                            print(e);
                        }

                        temp = GameObject.Find(textSelectParent);
                        position_clicked = temp.GetComponentInChildren<BoardControl>().returnBoardPosition();

                        //Debug.Log("nextPlay: " + nextPlay + ", textSelect: " + textSelect.Split('_')[0] +
                        //    ", position_clicked: " + position_clicked);
                        if (nextPlay == "" || position_clicked == nextPlay) {
                            pass = 1;
                            //Debug.Log("texture name: " + textSelect);
                            //Debug.Log("Board_" + textSelect.Split('_')[0]);
                            nextPlay = textSelect.Split('_')[0];
                            nextMove.text = "Board: " + nextPlay;
                        } else {
                            //Debug.Log("Keep repeating until it is equal");
                            pass = 0;
                        }
                    } else {
                        pass = 0;
                    }

                }
                if (1 == pass && "Side" != textSelect && "Plane" != textSelect)
                {
                    try {
                        textSelectParent = hit.collider.transform.parent.parent.name;
                        //Debug.Log("textSelectParent: " + textSelectParent);
                    } catch(Exception e) {
                        print(e);
                    }
                    

                    temp = GameObject.Find(textSelectParent);
                    position_clicked = temp.GetComponentInChildren<BoardControl>().returnBoardPosition();

                    Debug.Log("Board Position: " + temp.GetComponentInChildren<BoardControl>().returnBoardPosition());
                    //Debug.Log("NameFetch = " + temp.GetComponentInChildren<BoardControl>().winner);

                    switch (position_clicked)
                    {
                        case "TL":
                            //Debug.Log("in TL");
                            if(MainArrayBoards[0,0].GetComponent<BoardControl>().getMainArray(textSelect) == 0) {
                                MainArrayBoards[0, 0].GetComponent<BoardControl>().textureSwap(textSelect, turn);
                                MainArrayBoards[0, 0].GetComponent<BoardControl>().winCheck();
                                updateTurn();
                                //Debug.Log("Clicked!");
                            }
                            break;
                        case "TM":
                            //Debug.Log("in TM");
                            if(MainArrayBoards[0, 0].GetComponent<BoardControl>().getMainArray(textSelect) == 0) {
                                MainArrayBoards[0, 1].GetComponent<BoardControl>().textureSwap(textSelect, turn);
                                MainArrayBoards[0, 1].GetComponent<BoardControl>().winCheck();
                                updateTurn();
                            }
                            break;
                        case "TR":
                            //Debug.Log("in TR");
                            if (MainArrayBoards[0, 0].GetComponent<BoardControl>().getMainArray(textSelect) == 0) {
                                MainArrayBoards[0, 2].GetComponent<BoardControl>().textureSwap(textSelect, turn);
                                MainArrayBoards[0, 2].GetComponent<BoardControl>().winCheck();
                                updateTurn();
                            }
                            break;

                        case "ML":
                            //Debug.Log("in ML");
                            if (MainArrayBoards[0, 0].GetComponent<BoardControl>().getMainArray(textSelect) == 0) {
                                MainArrayBoards[1, 0].GetComponent<BoardControl>().textureSwap(textSelect, turn);
                                MainArrayBoards[1, 0].GetComponent<BoardControl>().winCheck();
                                updateTurn();
                            }
                            break;
                        case "MM":
                            //Debug.Log("in MM");
                            if (MainArrayBoards[0, 0].GetComponent<BoardControl>().getMainArray(textSelect) == 0) {
                                MainArrayBoards[1, 1].GetComponent<BoardControl>().textureSwap(textSelect, turn);
                                MainArrayBoards[1, 1].GetComponent<BoardControl>().winCheck();
                                updateTurn();
                            }
                            break;
                        case "MR":
                            //Debug.Log("in MR");
                            if (MainArrayBoards[0, 0].GetComponent<BoardControl>().getMainArray(textSelect) == 0) {
                                MainArrayBoards[1, 2].GetComponent<BoardControl>().textureSwap(textSelect, turn);
                                MainArrayBoards[1, 2].GetComponent<BoardControl>().winCheck();
                                updateTurn();
                            }
                            break;

                        case "BL":
                            //Debug.Log("in BL");
                            if (MainArrayBoards[0, 0].GetComponent<BoardControl>().getMainArray(textSelect) == 0) {
                                MainArrayBoards[2, 0].GetComponent<BoardControl>().textureSwap(textSelect, turn);
                                MainArrayBoards[2, 0].GetComponent<BoardControl>().winCheck();
                                updateTurn();
                            }
                            break;
                        case "BM":
                            //Debug.Log("in BM");
                            if (MainArrayBoards[0, 0].GetComponent<BoardControl>().getMainArray(textSelect) == 0) {
                                MainArrayBoards[2, 1].GetComponent<BoardControl>().textureSwap(textSelect, turn);
                                MainArrayBoards[2, 1].GetComponent<BoardControl>().winCheck();
                                updateTurn();
                            }
                            break;
                        case "BR":
                            //Debug.Log("in BR");
                            if (MainArrayBoards[0, 0].GetComponent<BoardControl>().getMainArray(textSelect) == 0) {
                                MainArrayBoards[2, 2].GetComponent<BoardControl>().textureSwap(textSelect, turn);
                                MainArrayBoards[2, 2].GetComponent<BoardControl>().winCheck();
                                updateTurn();
                            }
                            break;
                    }
                    mainWinCheck();
                }

            }
        }
        if (Input.GetKeyUp("r")) {
            Reset(); 
        }
        if (Input.GetKeyUp(KeyCode.Space)) {
            if (position_clicked != "")
                //cameraZoom(position_clicked);
                cameraZoom(nextPlay);
        }
    }
}
