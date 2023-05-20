namespace webApp
{
    record PlayerJson(int posX, int posY);

    public class Player
    {
        private int posX;
        private int posY;
        private int startPosX;
        private int startPosY;
        private List<(int, int)> pastPosList;
        private double[,] scoreMatrix;
        private int numMazeRows;
        private int numMazeColumns;

        public int PosX
        {
            set { posX = value; }
            get { return posX; }
        }
        public int PosY
        {
            set { posY = value; }
            get { return posY; }
        }
        public int StartPosX
        {
            set { startPosX = value; }
            get { return startPosX; }
        }
        public int StartPosY
        {
            set { startPosY = value; }
            get { return startPosY; }
        }
        public List<(int, int)> PastPosList
        {
            set { pastPosList = value; }
            get { return pastPosList; }
        }
        public int NumPastPos
        {
            set { }
            get { return pastPosList.Count; }
        }
        public double [,] ScoreMatrix
        {
            set { scoreMatrix = value; }
            get { return scoreMatrix; }
        }
        public int NumMazeRows
        {
            set { numMazeRows = value; }
            get { return numMazeRows; }
        }
        public int NumMazeColumns
        {
            set { numMazeColumns = value; }
            get { return numMazeColumns; }
        }

        public Player()
        {
            pastPosList = new List<(int, int)>();
        }

        public void move()
        {
            string direction = getMoveDirection();

            switch (direction)
            {
                case "toUp":
                    posY -= 1;
                    break;
                case "toRight":
                    posX += 1;
                    break;
                case "toDown":
                    posY += 1;
                    break;
                case "toLeft":
                    posX -= 1;
                    break;
            }

            pastPosList.Add((posX, posY));
        }

        private string getMoveDirection()
        {
            List<string> directionsList = new List<string>();

            if(posY > 1)
            {
                bool alreadyVisited = false;
                foreach((int posX, int posY) in pastPosList)
                {
                    if (this.posX == posX && this.posY - 1 == posY)
                    {
                        alreadyVisited = true;
                        break;
                    }
                }

                if (!alreadyVisited)
                {
                    directionsList.Add("toUp");
                }
            }
            if(posX < numMazeColumns)
            {
                bool alreadyVisited = false;
                foreach ((int posX, int posY) in pastPosList)
                {
                    if(this.posX + 1 == posX && this.posY == posY)
                    {
                        alreadyVisited = true;
                        break;
                    }
                }

                if (!alreadyVisited)
                {
                    directionsList.Add("toRight");
                }
            }
            if (posY < numMazeRows)
            {
                bool alreadyVisited = false;
                foreach ((int posX, int posY) in pastPosList)
                {
                    if (this.posX == posX && this.posY + 1 == posY)
                    {
                        alreadyVisited = true;
                        break;
                    }
                }

                if (!alreadyVisited)
                {
                    directionsList.Add("toDown");
                }
            }
            if (posX > 1)
            {
                bool alreadyVisited = false;
                foreach ((int posX, int posY) in pastPosList)
                {
                    if (this.posX - 1 == posX && this.posY == posY)
                    {
                        alreadyVisited = true;
                        break;
                    }
                }

                if (!alreadyVisited)
                {
                    directionsList.Add("toLeft");
                }
            }

            var rand = new Random();
            double randSum = 0;

            List<(string, double)> randList = new List<(string, double)>();

            foreach(string direction in directionsList)
            {
                if(direction == "toUp")
                {
                    randList.Add(("toUp", randSum + scoreMatrix[posY - 2, posX - 1]));
                    randSum += scoreMatrix[posY - 2, posX - 1];
                }
                else if (direction == "toRight")
                {
                    randList.Add(("toRight", randSum + scoreMatrix[posY - 1, posX]));
                    randSum += scoreMatrix[posY - 1, posX];
                }
                else if (direction == "toDown")
                {
                    randList.Add(("toDown", randSum + scoreMatrix[posY, posX - 1]));
                    randSum += scoreMatrix[posY, posX - 1];
                }
                else if (direction == "toLeft")
                {
                    randList.Add(("toLeft", randSum + scoreMatrix[posY - 1, posX - 2]));
                    randSum += scoreMatrix[posY - 1, posX - 2];
                }
                else
                {

                }
            }

            directionsList.Clear();

            for(int i = 0; i < randList.Count(); i++)
            {
                for(int j = i; j < randList.Count(); j++)
                {
                    if (randList[i].Item2 > randList[j].Item2)
                    {
                        (string, double) temp = (randList[i].Item1, randList[i].Item2);
                        randList[i] = randList[j];
                        randList[j] = temp;
                    }
                }
            }

            int directionNum = rand.Next(0, Convert.ToInt32(randSum) + 1);
            string directionString;

            if (directionNum <= randList[0].Item2) 
                directionString = randList[0].Item1;
            else if(directionNum <= randList[1].Item2)
                directionString = randList[1].Item1;
            else if (directionNum <= randList[2].Item2)
                directionString = randList[2].Item1;
            else 
                directionString = randList[3].Item1;

            randList.Clear();

            return directionString;
        }
    }
}
