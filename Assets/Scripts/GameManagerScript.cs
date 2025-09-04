using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private Transform gameTransform; // To house and scale our puzzle
    [SerializeField] private Transform piecePrefab; // Reference to the Transform as we'll want to position it

    private int emptyLocation;
    private int size;

    // Creates game setup with 3x3 pieces
    private void CreateGamePieces(float gapThickness)
    {
        // Width of each puzzle tile
        float width = 0.95f / (float)size;
        for (int row = 0; row < size; row++)
        {
            for (int col = 0; col < size; col++)
            {
                Transform piece = Instantiate(piecePrefab, gameTransform);
                // Centers the puzzle; pieces will be in the game board going from -1 to +1
                piece.localPosition = new Vector3(-1 + (2 * width * col) + width, // x cords
                                                  +1 - (2 * width * row) + width, // y cords
                                                  0); // z cords
                // Scales the puzzle
                piece.localScale = ((2.05f * width) - gapThickness) * Vector3.one;
                piece.name = $"{(row * size) + col}"; // Assigns a name to each quad (indexes them)
                                                      // May be used to make it easier for us to detect if the game is complete

         
                // Hopefully this will merge the whole picture, if not, fucking nuke the whole thing.
                float gap = gapThickness / 2;
                Mesh mesh = piece.GetComponent<MeshFilter>().mesh;
                Vector2[] uv = new Vector2[4]; // Array for UV coordinates of the puzzle's vertexes/corners
                                               // (four since the whole puzzle is a square)

                // Tutorial says UV coord order should be (0, 1) (1, 1) (0, 0) (1, 0)
                uv[0] = new Vector2((width * col) + gap, 1 - ((width * (row + 1)) - gap));
                uv[1] = new Vector2((width * (col + 1)) - gap, 1 - ((width * (row + 1)) - gap));
                uv[2] = new Vector2((width * col) + gap, 1 - ((width * row) + gap));
                uv[3] = new Vector2((width * (col + 1)) - gap, 1 - ((width * row) + gap));

                // Assign new UVs to the mesh
                mesh.uv = uv;
            }
        }
    }

    private void Start()
    {
        size = 3;
        CreateGamePieces(0.01f);
    }
}
