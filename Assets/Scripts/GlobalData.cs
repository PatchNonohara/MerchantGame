using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;

static public class GlobalData
{
   
    public static Dictionary<string, (int x, int y)> TileMapNew = new Dictionary<string, (int x, int y) > ();
    public static Dictionary<string, string> TileMapIDDirection = new Dictionary<string, string>();
    public static Dictionary<string, bool> IsRoot = new Dictionary<string, bool>();

    public static Dictionary<string, (int x, int y)> TileBranchMap = new Dictionary<string, (int x, int y)>();
    public static Dictionary<string, string> TileIDBranchMap = new Dictionary<string, string>();

    public static List<string> RoomKeyDict = new List<string>();
    public static List<string> BranchRoomKeyDict = new List<string>();

    public static List<string> TileMap = new List<string> ();
    public static List<string> Tiletype = new List<string> { "left", "forward","right"};
    public static List<string> TileNR = new List<string> {"left","forward" };
    public static List<string> TileNL = new List<string> {"right","forward" };
    public static List<Vector3> tilePositions = new List<Vector3>();
    public static List<Quaternion> tileRotations = new List<Quaternion>();
    public static int range = 5;
    public static int branchRange = 3;
    public static bool TileMapIsFull = false;
    public static int currentTileInc = 0;
    public static System.Random rand = new System.Random();
    public static string lastTile;
    public static Quaternion rotation;
    public static bool TiletoBeRoot = false;
    public static string rootTileKey;
    public static Vector3 rootTilePosition;
    public static Quaternion rootTileRotation;
    public static bool rootPlanted = false;
    public static bool MapisGenerated = false;
    public static bool RootExists = false;
    public static bool ToPlant = false;
    public static string RootTileOrientation;

}
