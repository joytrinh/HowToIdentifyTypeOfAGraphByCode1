using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace LTDT_BTTuan00_2
{
    class Program
    {   
        static void Main(string[] args)
        {
            GRAPH g = new GRAPH();
            string input_filename = args[0];
            inputValue(input_filename, g);
            Console.ReadLine();
        }
        private static void inputValue(string input_filename, GRAPH g)
        {
            try
            {
                using (StreamReader reader = new StreamReader(input_filename))
                {
                    string line = reader.ReadLine();
                    g.numberOfVertexes = int.Parse(line);
                    if (g.numberOfVertexes > 2)
                    {
                        //Declare variables
                        g.matrix = new int[g.numberOfVertexes, g.numberOfVertexes];
                        int rowIndex, colIndex;
                        int sIndex = 0;
                        bool directedGraph = false;
                        int numberOfEdges = 0;
                        int numberOfVertexesHaveMultiples = 0;
                        int numberOfLoops = 0;

                        //Process
                        line = reader.ReadToEnd().Replace("\r\n", " ");
                        string[] s = line.Split(' ');
                        
                        while (sIndex < g.numberOfVertexes * 2)
                        {
                            for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                                for (colIndex = 0; colIndex < g.numberOfVertexes; colIndex++)
                                {
                                    g.matrix[rowIndex, colIndex] = int.Parse(s[sIndex]);
                                    numberOfEdges += g.matrix[rowIndex, colIndex];
                                    sIndex++;
                                }
                        }
                        reader.Close();
                        Console.WriteLine(g.numberOfVertexes);
                        
                        //a. Ma tr?n k? c?a d? th?
                        for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                        {
                            for (colIndex = 0; colIndex < g.numberOfVertexes; colIndex++)
                            {
                                Console.Write(g.matrix[rowIndex, colIndex] + " ");
                                if (g.matrix[rowIndex, colIndex] != g.matrix[colIndex, rowIndex])
                                    directedGraph = true;
                                else if ((g.matrix[rowIndex, colIndex] == g.matrix[colIndex, rowIndex]) && directedGraph == true)
                                    directedGraph = true;
                                else if ((g.matrix[rowIndex, colIndex] == g.matrix[colIndex, rowIndex]) && directedGraph == false)
                                    directedGraph = false;
                            }
                            Console.WriteLine();
                        }
                        
                        if (directedGraph)
                        {
                            //b. X�c d?nh t�nh c� hu?ng c?a d? th?
                            Console.WriteLine("Do thi co huong");

                            //c. S? d?nh c?a d? th? (k? c? d?nh d?c bi?t)
                            Console.WriteLine("So dinh cua do thi: " + g.numberOfVertexes);

                            //d. S? c?nh c?a d? th? (k? c? c?nh d?c bi?t)
                            Console.WriteLine("So canh cua do thi: " + numberOfEdges);

                            //e. S? lu?ng c?p d?nh xu?t hi?n c?nh b?i, s? c?nh khuy�n
                            for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                                for (colIndex = 0; colIndex < g.numberOfVertexes; colIndex++)
                                {
                                    if (g.matrix[rowIndex, colIndex] != 0 && g.matrix[colIndex, rowIndex] != 0 && colIndex != rowIndex)
                                        numberOfVertexesHaveMultiples++;
                                    if (g.matrix[rowIndex, colIndex] != 0 && colIndex == rowIndex)
                                        numberOfLoops++;
                                }

                            Console.WriteLine("So cap dinh xuat hien canh boi: " + numberOfVertexesHaveMultiples / 2);//Chia 2 vi khi cong se double                            
                            Console.WriteLine("So canh khuyen: " + numberOfLoops);

                            int[] inDegree = new int[g.numberOfVertexes];
                            int[] outDegree = new int[g.numberOfVertexes];

                            //xac dinh bac vao theo rowIndex
                            for (colIndex = 0; colIndex < g.numberOfVertexes; colIndex++)
                                for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                                    inDegree[colIndex] += g.matrix[rowIndex, colIndex];

                            //xac dinh bac ra theo colIndex
                            for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                                for (colIndex = 0; colIndex < g.numberOfVertexes; colIndex++)
                                    outDegree[rowIndex] += g.matrix[rowIndex, colIndex];

                            //f. S? d?nh treo, s? d?nh c� l?p
                            int pendantVertex = 0;
                            int isolatedVertex = 0;
                            for (int i = 0; i < g.numberOfVertexes; i++)
                            {
                                if (inDegree[i] == 0 && outDegree[i] == 0)
                                    isolatedVertex++;
                                else if ((inDegree[i] == 1 && outDegree[i] == 0) || (inDegree[i] == 0 && outDegree[i] == 1))
                                    pendantVertex++;
                            }
                            Console.WriteLine("So dinh treo: " + pendantVertex);
                            Console.WriteLine("So dinh co lap: " + isolatedVertex);

                            //g. X�c d?nh b?c v�o � b?c ra (n?u l� d? th? c� hu?ng) c?a t?ng d?nh trong d? th?
                            Console.WriteLine("(Bac vao - Bac ra) cua tung dinh:");
                            for (int i = 0; i < g.numberOfVertexes; i++)
                                Console.Write(i + "(" + inDegree[i] + "-" + outDegree[i] + ") ");
                            Console.WriteLine();

                            //h. X�c d?nh lo?i d? th? co b?n
                            if (numberOfVertexesHaveMultiples / 2 > 0)
                                Console.WriteLine("Da do thi co huong");
                            else
                                Console.WriteLine("Do thi co huong");
                        }
                            
                        else if (!directedGraph)
                        {
                            Console.WriteLine("Do thi vo huong");

                            //c. S? d?nh c?a d? th? (k? c? d?nh d?c bi?t)
                            Console.WriteLine("So dinh cua do thi: " + g.numberOfVertexes);

                            //d. S? c?nh c?a d? th? (k? c? c?nh d?c bi?t)
                            //=> Ch? t�nh tam gi�c tr�n c?a h�nh vu�ng. S? c?nh th?c t? l� t?ng s? c?nh/2.
                            //Tuy nhi�n T?t C? nh?ng gi� tr? tr�n du?ng ch�o kh�ng du?c chia 2.
                            //Do d� sau khi ta chia d�i xong r?i ta l?i c?ng v�o 1/2 gi� tr? c?a du?ng ch�o
                            numberOfEdges = 0;
                            for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                                for (colIndex = rowIndex; colIndex < g.numberOfVertexes; colIndex++)
                                    numberOfEdges += g.matrix[rowIndex, colIndex];
                                    
                            Console.WriteLine("So canh cua do thi: " + numberOfEdges);

                            //e. S? lu?ng c?p d?nh xu?t hi?n c?nh b?i, s? c?nh khuy�n
                            for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                                for (colIndex = 0; colIndex < g.numberOfVertexes; colIndex++)
                                {
                                    if (g.matrix[rowIndex, colIndex] > 1)
                                        numberOfVertexesHaveMultiples++;
                                    if (g.matrix[rowIndex, colIndex] != 0 && colIndex == rowIndex)
                                        numberOfLoops++;
                                }
                            Console.WriteLine("So cap dinh xuat hien canh boi: " + numberOfVertexesHaveMultiples/2);                     
                            Console.WriteLine("So canh khuyen: " + numberOfLoops);

                            int[] degree = new int[g.numberOfVertexes];
                            for (rowIndex = 0; rowIndex < g.numberOfVertexes; rowIndex++)
                                for (colIndex = 0; colIndex < g.numberOfVertexes; colIndex++)
                                {
                                    if (rowIndex == colIndex)
                                        degree[rowIndex] += g.matrix[rowIndex, colIndex] * 2;
                                    else
                                        degree[rowIndex] += g.matrix[rowIndex, colIndex];
                                }
                            //f. S? d?nh treo, s? d?nh c� l?p
                            int pendantVertex = 0;
                            int isolatedVertex = 0;
                            for (int i = 0; i < g.numberOfVertexes; i++)
                            {
                                if (degree[i] == 0)
                                    isolatedVertex++;
                                else if (degree[i] == 1)
                                    pendantVertex++;
                            }
                            Console.WriteLine("So dinh treo: " + pendantVertex);
                            Console.WriteLine("So dinh co lap: " + isolatedVertex);

                            //g. X�c d?nh b?c(n?u l� d? th? v� hu?ng) c?a t?ng d?nh trong d? th?
                            Console.WriteLine("Bac cua tung dinh:");
                            for (int i = 0; i < g.numberOfVertexes; i++)
                                Console.Write(i + "(" + degree[i] + ") ");
                            Console.WriteLine();

                            //h. X�c d?nh lo?i d? th? co b?n
                            if (numberOfLoops > 0)
                                Console.WriteLine("Gia do thi");
                            else if (numberOfVertexesHaveMultiples / 2 > 0)
                                Console.WriteLine("Da do thi");
                            else
                                Console.WriteLine("Don do thi");
                        }
                    }
                    else
                        Console.WriteLine("The number of Vertexes must be greater than 2.");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read: ");
                Console.WriteLine(e.Message);
            }
        }
    }

	class GRAPH
    {
        private int _numberOfVertexes;
        private int[,] _matrix;

        public int numberOfVertexes {
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
}
