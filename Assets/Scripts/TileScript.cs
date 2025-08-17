using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class TileScript : MonoBehaviour
{
   
    public GameObject TileForward;

    public GameObject TileRight;
     
    public GameObject TileLeft;
    public GameObject TileRootFL;
    public GameObject TileRootRL;
    public float distance = 3f;
    public bool hasPlaced = false;
  


    public enum TileType { Forward, Left, Right }
    public TileType tileType;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void PlaceTile()
    {
        string roomKey = "room" + GlobalData.currentTileInc;
        GlobalData.rotation = transform.rotation;

        if (GlobalData.ToPlant == true)
        {
            GlobalData.TiletoBeRoot = true;

            GlobalData.rootTilePosition = transform.position;
            GlobalData.rootTileRotation = transform.rotation;
           

            GlobalData.rootPlanted = true;
            GlobalData.ToPlant = false;
        }
       
        float tileLength = GetComponent<Collider>().bounds.size.x;
        if (GlobalData.TileMapIDDirection[roomKey] == GlobalData.lastTile)
        {
            if (GlobalData.lastTile == "left")
            { GlobalData.TileMapIDDirection[roomKey] = GlobalData.TileNL[GlobalData.rand.Next(0,2)]; }
            else if (GlobalData.lastTile == "right")
            { GlobalData.TileMapIDDirection[roomKey] = GlobalData.TileNR[GlobalData.rand.Next(0,2)]; }

        }
        Vector3 spawnPosition = transform.position + (transform.forward) * distance;
        if (GlobalData.TileMapIDDirection[roomKey] == "forward" && GlobalData.IsRoot[roomKey] != true)
        {
            
            
            Instantiate(TileForward, spawnPosition, GlobalData.rotation); 
        }
        else if (GlobalData.TileMapIDDirection[roomKey] == "left" && GlobalData.lastTile != "left" && GlobalData.IsRoot[roomKey] != true)
        {

            GlobalData.rotation *= Quaternion.Euler(0, 90, 0);
            Instantiate(TileRight, spawnPosition, GlobalData.rotation);

            ;
        }
        else if (GlobalData.TileMapIDDirection[roomKey] == "right" && GlobalData.lastTile != "right" && GlobalData.IsRoot[roomKey] != true)
        {
            
            GlobalData.rotation *= Quaternion.Euler(0, -90, 0);
            Instantiate(TileLeft, spawnPosition, GlobalData.rotation);
       

        }
        else if (GlobalData.IsRoot[roomKey] == true)
        {

            if (GlobalData.TileMapIDDirection[roomKey] == "right")
            {
                GlobalData.rotation *= Quaternion.Euler(0, -90, 0);
                Instantiate(TileRootRL, spawnPosition, GlobalData.rotation);
                GlobalData.RootTileOrientation = "leftandright";
            }
            else if (GlobalData.TileMapIDDirection[roomKey] == "left")
            {
                GlobalData.rotation *= Quaternion.Euler(0, 90, 0);
                Instantiate(TileRootRL, spawnPosition, GlobalData.rotation);
                GlobalData.RootTileOrientation = "leftandright";
            }
            else if (GlobalData.TileMapIDDirection[roomKey] == "forward")
            {
                GlobalData.rotation *= Quaternion.Euler(0, 0, 0);
                Instantiate(TileRootFL, spawnPosition, GlobalData.rotation);
                GlobalData.RootTileOrientation = "forwardandleft";
            }

            GlobalData.ToPlant = true;

        }
  

        Vector3 currentPos = transform.position;
        Quaternion currentRot = transform.rotation;

        GlobalData.tileRotations.Add(currentRot);
        GlobalData.tilePositions.Add(currentPos);
        GlobalData.lastTile = GlobalData.TileMapIDDirection[roomKey];

    




    }

    public void PlaceBranch() 
    {
        if (GlobalData.TiletoBeRoot == true)
        {
            string roomKey = "room" + GlobalData.currentTileInc;
            if (GlobalData.rootPlanted == true)
            {
                transform.position = GlobalData.rootTilePosition;
                transform.rotation = GlobalData.rootTileRotation;
                GlobalData.rotation = GlobalData.rootTileRotation;
                

                GlobalData.rootPlanted = false;
                transform.position += transform.forward * distance;
            }
            GlobalData.rotation = transform.rotation;
          
            float tileLength = GetComponent<Collider>().bounds.size.x;
            if (GlobalData.TileIDBranchMap[roomKey] == GlobalData.lastTile)
            {
                if (GlobalData.lastTile == "left")
                { GlobalData.TileIDBranchMap[roomKey] = GlobalData.TileNL[GlobalData.rand.Next(0, 2)]; }
                else if (GlobalData.lastTile == "right")
                { GlobalData.TileIDBranchMap[roomKey] = GlobalData.TileNR[GlobalData.rand.Next(0, 2)]; }

            }
            Vector3 spawnPosition = transform.position + (transform.forward) * distance;
            if (GlobalData.TileIDBranchMap[roomKey] == "forward")
            {

                if (GlobalData.RootTileOrientation == "forwardandleft")
                {
                    spawnPosition = transform.position + (transform.forward) * distance; 
                    GlobalData.RootTileOrientation = "none";
                }
           
                 Instantiate(TileForward, spawnPosition, GlobalData.rotation); 
            }
            else if (GlobalData.TileIDBranchMap[roomKey] == "left" && GlobalData.lastTile != "left")
            {
                GlobalData.rotation *= Quaternion.Euler(0, 90, 0);
                if (GlobalData.RootTileOrientation == "leftandright")
                {
                
                    spawnPosition = transform.position + (-transform.right) * distance;
                    GlobalData.RootTileOrientation = "none";
                }
                 
                 Instantiate(TileRight, spawnPosition, GlobalData.rotation); 
              
            



        }
            else if (GlobalData.TileIDBranchMap[roomKey] == "right" && GlobalData.lastTile != "right")
            {
                GlobalData.rotation *= Quaternion.Euler(0, -90, 0);
                if (GlobalData.RootTileOrientation == "leftandright")
                {
                   
                    spawnPosition = transform.position + (transform.right) * distance;
                    GlobalData.RootTileOrientation = "none";
                }
             
           
                    Instantiate(TileLeft, spawnPosition, GlobalData.rotation);
                
        


            }

            Vector3 currentPos = transform.position;
            Quaternion currentRot = transform.rotation;

            GlobalData.tileRotations.Add(currentRot);
            GlobalData.tilePositions.Add(currentPos);
            GlobalData.lastTile = GlobalData.TileIDBranchMap[roomKey];
            Debug.Log("Total Rooms(MapNew&BranchMap): " + (GlobalData.TileMapNew.Count + GlobalData.TileBranchMap.Count));

        }



    }
    void Start()
    {
        MazeGenerationScript.GenerateTileMap();
        MazeGenerationScript.GenerateBranch();
        MazeGenerationScript.PlantRoot();
        GlobalData.currentTileInc++;

        if (GlobalData.currentTileInc <= GlobalData.range )
        {
          
            Debug.Log("CurrentTileInc: " + GlobalData.currentTileInc);
            Debug.Log("CurrentTile Root: " + GlobalData.IsRoot["room"+GlobalData.currentTileInc]);
            PlaceTile();
         


        }

        if (GlobalData.currentTileInc > GlobalData.range && GlobalData.currentTileInc < (GlobalData.branchRange + GlobalData.range))
        {
          
            Debug.Log("CurrentTileInc: " + GlobalData.currentTileInc);
            PlaceBranch();



        }




    }

    // Update is called once per frame
    void Update()
    {
        

    }
}
