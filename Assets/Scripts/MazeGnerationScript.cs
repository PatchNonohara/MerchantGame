using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering.Universal;
using static UnityEngine.Rendering.DebugUI;

public class MazeGenerationScript : MonoBehaviour
{
    //REMINDER !!! MAKE NEW DICT OR LIST TO ASSIGN VALUES OF EACH ROOM INSTANCE AND ONE SEPERATE FOR DIRECTIONS N,W,E,S USE ROOM KEY"NAMES" AS CALLER FOR DIRECTIONS WHEN PLACING ROOMS/TILES

    //CHECK YO KEYS AGAIN SINCE I CHANGED IT GOTTA USE VALUES NOW NO MORE KEY
    static public void GenerateTileMap()
    {
        int currentfacingdirection = 1;
        int roomNum = 0;
        string tiledirection;
   

        List<string> Directions = new List<string> { "west", "north", "east", "south", };
        
        Dictionary<string, (int dx, int dy)> Compass = new Dictionary<string, (int dx, int dy)>
         {
        { "north", (0, 1) },
        { "south", (0, -1) },
        { "east",  (-1, 0) },
        { "west",  (1, 0) }
          };
        while (GlobalData.TileMapNew.Count < GlobalData.range)
        {
                
            roomNum++;
            GlobalData.RoomKeyDict.Add("room" + roomNum);
            GlobalData.TileMapNew.Add("room" + roomNum, (0, 0));
            GlobalData.TileMapIDDirection.Add("room" + roomNum, "null");
            GlobalData.IsRoot.Add("room" +roomNum, false);


        }
        int RoomKeyDictCount = GlobalData.RoomKeyDict.Count;


            foreach (var Tile in GlobalData.RoomKeyDict)
            {
            string randomTileOrientation = Directions[GlobalData.rand.Next(0, 4)];
                tiledirection = Directions[((currentfacingdirection % 4) + 4) % 4];
                var previousPosition = GlobalData.TileMapNew[Tile];
                int prevX = previousPosition.x;
                int prevY = previousPosition.y;

                var value = GlobalData.TileMapNew[Tile];
                int x = value.x += Compass[tiledirection].dx;
                int y = value.y += Compass[tiledirection].dy;
                GlobalData.TileMapNew[Tile] = (x, y);



                if (randomTileOrientation == "north")
                {
                    GlobalData.TileMapIDDirection[Tile] = "forward";
                }
                if (randomTileOrientation == "east")
                {
                    GlobalData.TileMapIDDirection[Tile] = "left";
                    currentfacingdirection -= 1;
                }
                if (randomTileOrientation == "west")
                {
                    GlobalData.TileMapIDDirection[Tile] = "right";
                    currentfacingdirection += 1;
                }
                if (randomTileOrientation == "south")      // PLACED AS "FORWARD" MAY SUBJECT TO CHANGE <<<<<<--------
                {
                    GlobalData.TileMapIDDirection[Tile] = "forward";
                }

           
        }
       
         
 
    }

    static public void PlantRoot()
    {
        if (GlobalData.RootExists == false)
        {
            string rootTile = GlobalData.RoomKeyDict[GlobalData.rand.Next(GlobalData.RoomKeyDict.Count)];
            GlobalData.IsRoot[rootTile] = true;
            GlobalData.RootExists = true;
        }
    }

    static public void GenerateBranch() 
        {
        int currentfacingdirection = 1;
        int roomNumforBranch = GlobalData.TileMapNew.Count;
        string tiledirection;

        List<string> Directions = new List<string> { "west", "north", "east", "south", };
       
        Dictionary<string, (int dx, int dy)> Compass = new Dictionary<string, (int dx, int dy)>
         {
        { "north", (0, 1) },
        { "south", (0, -1) },
        { "east",  (-1, 0) },
        { "west",  (1, 0) }
          };
        while (GlobalData.TileBranchMap.Count <= GlobalData.range)
        {
         
            roomNumforBranch++;
            GlobalData.TileBranchMap.Add("room" + roomNumforBranch, (0, 0));
            GlobalData.TileIDBranchMap.Add("room" + roomNumforBranch, "null");

           
            GlobalData.BranchRoomKeyDict.Add("room" + roomNumforBranch);

        }
     


            foreach (var Tile in GlobalData.BranchRoomKeyDict)
            {
                string randomTileOrientation = Directions[GlobalData.rand.Next(0, 4)];
                tiledirection = Directions[((currentfacingdirection % 4) + 4) % 4];
                var previousPosition = GlobalData.TileBranchMap[Tile];
                int prevX = previousPosition.x;
                int prevY = previousPosition.y;

                var value = GlobalData.TileBranchMap[Tile];
                int x = value.x += Compass[tiledirection].dx;
                int y = value.y += Compass[tiledirection].dy;
                GlobalData.TileBranchMap[Tile] = (x, y);



                if (randomTileOrientation == "north")
                {
                    GlobalData.TileIDBranchMap[Tile] = "forward";
                }
                if (randomTileOrientation == "east")
                {
                    GlobalData.TileIDBranchMap[Tile] = "left";
                    currentfacingdirection -= 1;
                }
                if (randomTileOrientation == "west")
                {
                    GlobalData.TileIDBranchMap[Tile] = "right";
                    currentfacingdirection += 1;
                }
                if (randomTileOrientation == "south")      // PLACED AS "FORWARD" MAY SUBJECT TO CHANGE <<<<<<--------
                {
                    GlobalData.TileIDBranchMap[Tile] = "forward";
                }


            }

        }
        

    
        // Start is called once before the first execution of Update after the MonoBehaviour is created

        void Start()
        {
       
    }

        // Update is called once per frame
        void Update()
        {

        }
    
}
