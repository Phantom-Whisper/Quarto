using Microsoft.Maui.Layouts;

namespace QuartoApp.MyLayouts;

public class Matrix2d
{
    private static Random random = new Random();

    private static int[,] RandomInit(int nbRows, int nbColumns)
    {
        int[,] mat = new int[4, 4];
        for (int i = 0; i < 4; i++)
        {
            for (int j = 0; j < 4; j++)
            {
                mat[i, j] = random.Next(101);
            }
        }
        ;
        return mat;
    }

    public Matrix2d(int nbRows, int nbColumns)
    {
        matrix = RandomInit(nbRows, nbColumns);
    }

    private readonly int[,]? matrix;

    public int NbRows => matrix?.GetLength(0) ?? 0;
    public int NbColumns => matrix?.GetLength(1) ?? 0;

    public int[,] Matrix
    {
        get
        {
            if (matrix == null) return new int[,] { { } };

            int[,] mat = new int[NbRows, NbColumns];
            for (int numRow = 0; numRow < NbRows; numRow++)
            {
                for (int numCol = 0; numCol < NbColumns; numCol++)
                {
                    mat[numRow, numCol] = matrix[numRow, numCol];
                }
            }
            return matrix;
        }
    }

    public IEnumerable<int> FlatMatrix2d
    {
        get
        {
            List<int> flatMatrix = new();

            if (matrix == null) return flatMatrix;

            int[,] mat = new int[NbRows, NbColumns];
            for (int numRow = 0; numRow < NbRows; numRow++)
            {
                for (int numCol = 0; numCol < NbColumns; numCol++)
                {
                    flatMatrix.Add(matrix[numRow, numCol]);
                }
            }
            return flatMatrix;
        }
    }
}
