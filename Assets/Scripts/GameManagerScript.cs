using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private Transform gameTransform; // To house and scale our puzzle
    [SerializeField] private Transform piecePrefab; // Reference to the Transform as we'll want to position it

    private int size;
    private List<Transform> pieces;
    private bool shuffling = false;

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
                pieces.Add(piece);
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

                // Identifying Blocks Outside Of Class
                int[] blockCoords = { row, col };
                DataManager.Instance.blocks.Add((row,col), piece.gameObject);

                GameObject temp;
                if (DataManager.Instance.blocks.TryGetValue((row,col), out temp))
                {
                    Debug.Log("this item is in fact added: " + temp.name);
                    Debug.Log("The key for this item is: " + blockCoords[0] + "," + blockCoords[1]);
                }
            }
        }
    }

    private void Start()
    {
        pieces = new List<Transform>();
        size = 3;
        CreateGamePieces(0.01f);
        StartCoroutine(WaitShuffle(0.5f)); // Will not make the shuffling process instantaneous,
                                           // Also stops the thing from shuffling endlessly because of the loop
    }

    // Name each puzzle piece in order so we can check completion
    private bool CheckCompletion()
    {
        for (int i = 0; i < pieces.Count; i++)
        {
            if (pieces[i].name != $"{i}")
            {
                return false;
            }
        }
        return true;
    }

    private IEnumerator WaitShuffle(float duration)
    {
        yield return new WaitForSeconds(duration);
        Shuffle();
        shuffling = false;
    }

    // Brute force the fuckass shuffling method.
    private void Shuffle()
    {
        // Store the current positions of all pieces
        List<Vector3> positions = new List<Vector3>(); // Create new list for each position for the tiles
        foreach (Transform piece in pieces)
        {
            positions.Add(piece.localPosition);
        }

        // Shuffle the list of positions
        for (int i = positions.Count - 1; i > 0; i--)
        {
            int j = Random.Range(0, i + 1);
            (positions[i], positions[j]) = (positions[j], positions[i]);
        }

        // Assign the shuffled positions back to the pieces
        for (int i = 0; i < pieces.Count; i++)
        {
            pieces[i].localPosition = positions[i];
        }
    }
}
