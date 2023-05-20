using webApp;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

MovementExecutor executor = new MovementExecutor();
executor.Matrix = new Matrix();
executor.Player = new Player();

app.Map("/mazeConfig", mazeConstructor =>
{
    mazeConstructor.Run(async (context) =>
    {
        var request = context.Request;
        var jsonMatrix = await request.ReadFromJsonAsync<MatrixJson>();

        executor.Matrix.MatrixMas = jsonMatrix.matrixMas;
        executor.Matrix.NumRows = jsonMatrix.numRows;
        executor.Matrix.NumColumns = jsonMatrix.numColumns;
        executor.Matrix.calculate();
    });
});

app.Map("/playerConfig", playerConfigConstructor =>
{
    playerConfigConstructor.Run(async (context) =>
    {
        var request = context.Request;

        var playerConfigJson = await request.ReadFromJsonAsync<PlayerJson>();

        executor.Player.StartPosX = playerConfigJson.posX;
        executor.Player.StartPosY = playerConfigJson.posY;
        executor.Player.PosX = playerConfigJson.posX;
        executor.Player.PosY = playerConfigJson.posY;

        executor.Player.NumMazeRows = executor.Matrix.NumRows;
        executor.Player.NumMazeColumns = executor.Matrix.NumColumns;
    });
});

app.Map("/movementConfig", movementConfigConstructor =>
{
    movementConfigConstructor.Run(async (context) =>
    {
        var response = context.Response;

        executor.execute();

        await response.WriteAsJsonAsync(executor.JsonInstructs);
    });
});

app.Run();
