using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

class GRAPH
{
    private int _numberOfVertexes;
    private int[,] _matrix;

    public int numberOfVertexes
    {
        get { return _numberOfVertexes; }
        set
        {
            if (value > 2)
                _numberOfVertexes = value;
        }
    }

    public int[,] matrix
    {
        get { return _matrix; }
        set { _matrix = value; }
    }
}
