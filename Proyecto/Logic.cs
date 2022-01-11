using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using System.IO;



namespace TwelveLogic
{   [Serializable]
    class Point
    {
        public int x;
        public int y;
        public Point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
    }
    [Serializable]
    public class Twelve
    {
        //public int this[int i, int j] {
        //    get { return board[i, j]; }
        //}
        // public int getValue(int x, int y) { return this.board[x, y]; }
        public int[,] board;
        public bool isFirstClick;
        Point click1;
        Point click2;
        Random random;
        public int score;
        public int count;
        public int best;
        public bool modo;
        public bool extended;
        //List<int> aviable;


        public Twelve(int width, int height, bool modo)
        {
            extended = false;
            this.modo = modo;
            isFirstClick = false;
            board = new int[height, width];
            random = new Random();
            click1 = new Point(0, 0);
            count = 0;
            //aviable = new List<int>(); //NUEVO**********************
            //for (int i = 0; i < width * height; i++) //NUEVO ************************
            //  aviable.Add(i); //NUEVO *********************
            Respawn(); Respawn(); Respawn();



        }
        public int heigth
        {
            get
            {
                return board.GetLength(0);
            }
        }
        public int width
        {
            get
            {
                return board.GetLength(1);
            }
        }


        //public void Respawn()//con esta funcion se agrega un numero en una posicion;
        //{
        //    if (aviable.Count == 0) return; //NUEVO***********************************************************
        //    int t = aviable[random.Next(0, aviable.Count())]; //escogo una de las casillas validas
        //    aviable.Remove(t); //la remuevo de las posibles casillas validas
        //    board[t / board.GetLength(0), t % board.GetLength(0)] = random.Next(1, 3); //le asigno un valor
        //}
        public void Respawn()
        {
            bool ok = false;
            while (!ok)
            {

                Point p = new Point(random.Next(0, (board.GetLength(0))), random.Next(0, (board.GetLength(1))));
                while (board[p.x, p.y] == 0)
                {
                    ok = true;
                    board[p.x, p.y] = random.Next(1, 4);
                }
            }
        }

        public void Click(int x, int y)
        {
            if (board[x, y] == 0 && !isFirstClick) return;


            isFirstClick = !isFirstClick;
            if (isFirstClick)
                click1 = new Point(x, y);
            else if (click1.x != x || click1.y != y)
            {
                click2 = new Point(x, y);

                if (!IsTour(click1, click2))
                {

                    return;
                }
                if (board[click2.x, click2.y] == board[click1.x, click1.y])

                {
                    count++;
                    Move();
                    board[click2.x, click2.y]++;

                    score += board[click2.x, click2.y];
                    Respawn();


                }
                if (board[click2.x, click2.y] == 0)
                {
                    if (Matrix() == 1)
                    {
                        Move();
                        Respawn();
                        count++;
                        return;
                    }
                    else
                    {
                        count++;
                        Move();
                        Respawn();
                        Respawn();


                    }
                }

            }
        }
        bool IsTour(Point a, Point b)
        {
            int[,] _board = new int[board.GetLength(0), board.GetLength(1)];
            bool[,] mark = new bool[board.GetLength(0), board.GetLength(1)];
            MarkBoard(mark);
            Infest(a, _board, mark);
            bool anyInfest = true;
            int i = 1;
            while (anyInfest)
            {
                anyInfest = false;

                for (int x = 0; x < board.GetLength(0); x++)
                {
                    for (int y = 0; y < board.GetLength(1); y++)
                    {
                        if (_board[x, y] == i)
                            if (b.x == x && b.y == y) return true;
                            else
                            {
                                Infest(new Point(x, y), _board, mark);
                                anyInfest = true;
                            }
                    }
                }
                i++;
            }
            return false;
        }
        void MarkBoard(bool[,] mark)
        {
            for (int x = 0; x < board.GetLength(0); x++)
            {
                for (int y = 0; y < board.GetLength(1); y++)
                {
                    if (board[x, y] != 0 && !(x == click1.x && y == click1.y || x == click2.x && y == click2.y))
                        mark[x, y] = true;
                }
            }

        }
        void Infest(Point point, int[,] _board, bool[,] mark)
        {
            Point[] dir = new Point[] { new Point(0, 1), new Point(0, -1), new Point(1, 0), new Point(-1, 0) };
            int value = _board[point.x, point.y] + 1;
            for (int i = 0; i < 4; i++)
            {
                Point current = new Point(point.x + dir[i].x, point.y + dir[i].y);
                if (current.x < _board.GetLength(0) && current.x >= 0 && current.y < _board.GetLength(1) && current.y >= 0 && !mark[current.x, current.y])
                {
                    _board[current.x, current.y] = value;
                    mark[current.x, current.y] = true;
                }
            }
        }
        //void Move()// sirve para mover una celda
        //{
        //    board[click2.x, click2.y] = board[click1.x, click1.y];
        //    board[click1.x, click1.y] = 0;
        //    aviable.Remove(click2.x * board.GetLength(0) + click2.y);
        //    aviable.Add(click1.x * board.GetLength(0) + click1.y); //vuelvo a poner
        //}
        void Move()
        {
            board[click2.x, click2.y] = board[click1.x, click1.y];
            board[click1.x, click1.y] = 0;
        }

        public void Agressive_Mode(int x, int y)
        {
            Click(x, y);
            if (count == 18)
            {

                count = 0;
                for (int i = 0; i < board.GetLength(0); i++)
                    for (int j = 0; j < board.GetLength(1); j++)
                    {
                        int temp = 0;
                        Point p = new Point(random.Next(0, board.GetLength(0)), random.Next(0, board.GetLength(1)));
                        if (board[i, j] != 0)
                        {
                            temp = board[p.x, p.y];
                            board[p.x, p.y] = board[i, j];
                            board[i, j] = temp;

                        }
                    }

            }
        }
        public int Matrix()
        {
            int a = 0;
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 0)
                        a++;
                }
            return a;
        }
        public bool gameover()
        {
            for (int i = 0; i < board.GetLength(0); i++)
            {
                for (int j = 0; j < board.GetLength(0); j++)
                {
                    if (board[i, j] == 0)
                        return false;
                    if (j < board.GetLength(0) - 1)
                    {
                        if (board[i, j] == board[i, j + 1])
                            return false;

                    }
                    if (i < board.GetLength(1) - 1)
                    {
                        if (board[i, j] == board[i + 1, j])
                            return false;
                    }
                }
            }
            return true;
        }
        public bool gamewon()
        {
            for (int i = 0; i < board.GetLength(0); i++)
                for (int j = 0; j < board.GetLength(1); j++)
                {
                    if (board[i, j] == 12)
                        return true;
                }
            return false;

        }
        public void SaveGame()
        {


            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("save.bin", FileMode.Create, FileAccess.Write, FileShare.None);

            formatter.Serialize(stream, this);
            stream.Close();


        }
        static public Twelve LoadGame()
        {
            //Se deserializa el juego salvado se vuelve a calcular el ancho y alto
            //se pinta el tablero y se escriben los puntos y la mejor puntuacion
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("save.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
            Twelve game = (Twelve)formatter.Deserialize(stream);
            stream.Close();
            return game;
        }




    }
}










