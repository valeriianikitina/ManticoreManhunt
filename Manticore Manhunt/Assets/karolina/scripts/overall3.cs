using UnityEngine;
using UnityEngine.Tilemaps;

public class overall3 : MonoBehaviour
{
    public Tilemap tilemap; // reference to the tilemap
    public UnityEngine.Tilemaps.Tile coverTile; // reference to the cover tile
    public int specialAbilityUses = 3; // number of times the special ability can be used
    public bool gameOver = false; // flag to check if the game is over
    public Color highlightColor = Color.yellow; // color to use for highlighting
    public Color defaultColor = Color.white; // default color of the tiles
    private Vector3Int currentTilePos; // position of the current tile
    private Vector3Int selectedTilePos; // position of the selected tile
    private int Dogcounter1 = 0; //how many turns the dog took
    private int Dogcounter2 = 0; //how many turns the dog took


    // Use this for initialization
    void Start()
    {
        // Cover all the predefined tiles with the coverTile
        BoundsInt bounds = tilemap.cellBounds;
        TileBase[] allTiles = tilemap.GetTilesBlock(bounds);
        currentTilePos = tilemap.WorldToCell(transform.position);
        selectedTilePos = currentTilePos;
        HighlightSelectedTile();

        for (int x = 0; x < bounds.size.x; x++)
        {
            for (int y = 0; y < bounds.size.y; y++)
            {
                Vector3Int localPlace = new Vector3Int(x, y, 0);
                Vector3Int place = localPlace + bounds.position;
                tilemap.SetTile(place, coverTile);
                //coverTile = coverTile.AddComponent<BoxCollider2D>();
            }
        }
    }

    void Update()
    {


        // check for arrow input and highlight selected tiles
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            selectedTilePos.y += 1;
            HighlightSelectedTile();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            selectedTilePos.y -= 1;
            HighlightSelectedTile();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selectedTilePos.x -= 1;
            HighlightSelectedTile();
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            selectedTilePos.x += 1;
            HighlightSelectedTile();
        }

        if (!gameOver)
        {
            // check for input from the player to reveal the tile in front of them
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Vector3Int currentPos = tilemap.WorldToCell(transform.position);
                Vector3Int revealPos = currentPos + new Vector3Int(0, 1, 0);
                RevealTile(revealPos);
            }

            // check for input from the player to use the special ability
            if (Input.GetKeyDown(KeyCode.S) && specialAbilityUses > 0)
            {
                Vector3Int currentPos = tilemap.WorldToCell(transform.position);
                RevealAdjacentTiles(currentPos);
                specialAbilityUses--;
            }
        }
    }

    public void RevealTile(Vector3Int pos)
    {
        // check if there is a tile at the position
        if (tilemap.GetTile(pos) != null)
        {
            // reveal the tile at the specified position
            tilemap.SetTile(pos, null);

            // check if the revealed tile is an "Enemy" tile
            if (tilemap.GetTile(pos).name == "Enemy")
            {
                gameOver = true;
                Debug.Log("Game Over!");
            }
        }
    }

    private void HighlightSelectedTile()
    {
        // check if selected tile is within the bounds of the tilemap
        if (tilemap.HasTile(selectedTilePos))
        {
            // reset the color of the current tile
            tilemap.SetTileFlags(currentTilePos, TileFlags.None);
            tilemap.SetColor(currentTilePos, defaultColor);

            // set the color of the selected tile
            tilemap.SetTileFlags(selectedTilePos, TileFlags.None);
            tilemap.SetColor(selectedTilePos, highlightColor);

            currentTilePos = selectedTilePos;
        }
        else
        {
            selectedTilePos = currentTilePos;
        }
    }

    public void RevealAdjacentTiles(Vector3Int pos)
    {
        // reveal the tiles to the left, right, top and bottom of the current position
        Vector3Int revealPos = pos + new Vector3Int(-1, 0, 0);
        RevealTile(revealPos);
        revealPos = pos + new Vector3Int(1, 0, 0);
        RevealTile(revealPos);
        revealPos = pos + new Vector3Int(0, 1, 0);
        RevealTile(revealPos);
        revealPos = pos + new Vector3Int(0, -1, 0);
        RevealTile(revealPos);
    }
}
