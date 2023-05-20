using System.Text.Json;

namespace webApp
{
    public record JsonString(int playerPosX, int playerPosY, 
        double[] matrixMas);

    public class MovementExecutor
    {
        private Matrix matrix;
        private Player player;
        private List<JsonString> jsonInstructs;

        public Matrix Matrix
        {
            set { matrix = value; }
            get { return matrix; }
        }
        public Player Player
        {
            set { player = value; }
            get { return player; }
        }
        public List<JsonString> JsonInstructs
        {
            set { }
            get { return jsonInstructs; }
        }

        public MovementExecutor()
        {
            jsonInstructs = new List<JsonString>();
        }

        public void execute()
        {
            jsonInstructs.Clear();

            player.PosX = player.StartPosX;
            player.PosY = player.StartPosY;
            player.PastPosList.Clear();

            matrix.increaseScoreCell(player.PosX, player.PosY);
            player.ScoreMatrix = matrix.ScoreMatrix;

            JsonString jsonString = new JsonString(
                player.PosX, player.PosY, matrix.getScoreMatrixMas());
            jsonInstructs.Add(jsonString);

            player.PastPosList.Add((player.PosX, player.PosY));

            while (true)
            {
                player.move();

                if(matrix.hasObstackle(player.PosX, player.PosY))
                {
                    matrix.decreaseScoreCell(player.PosX, player.PosY);
                    player.ScoreMatrix = matrix.ScoreMatrix;

                    jsonString = new JsonString(
                        player.PosX, player.PosY, matrix.getScoreMatrixMas());
                    jsonInstructs.Add(jsonString);

                    break;
                }
                else if (matrix.hasFinish(player.PosX, player.PosY))
                {
                    matrix.increaseScoreCell(player.PosX, player.PosY);
                    player.ScoreMatrix = matrix.ScoreMatrix;

                    jsonString = new JsonString(
                        player.PosX, player.PosY, matrix.getScoreMatrixMas());
                    jsonInstructs.Add(jsonString);

                    break;
                }
                else
                {
                    matrix.increaseScoreCell(player.PosX, player.PosY);
                    player.ScoreMatrix = matrix.ScoreMatrix;

                    jsonString = new JsonString(
                        player.PosX, player.PosY, matrix.getScoreMatrixMas());
                    jsonInstructs.Add(jsonString);
                }
            }
        }
    }
}
