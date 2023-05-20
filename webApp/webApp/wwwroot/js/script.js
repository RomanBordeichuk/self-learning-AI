const container = document.querySelector(".container");
const mazeObj = document.querySelector(".maze");
const mazeConfig = [
    [0, 1, 2, 0, 0],
    [0, 0, 1, 1, 0],
    [0, 1, 1, 0, 0],
    [0, 1, 0, 0, 1],
    [0, 0, 0, 1, 1]
];

let maze = new Maze(mazeConfig);
let player = new Player(maze, 1, 1);

//************* EXECUTING REQUESTS **************

start();

async function start(){
    await send("Maze config");
    await send("Player config");
    
    await movementExecution();
}

async function movementExecution(){
    let movementConfig = await movementConfigResponse();

    do{
        for(let i = 0; i < movementConfig.length; i++){
            player.changePos(
                movementConfig[i].playerPosX, movementConfig[i].playerPosY);
            
            maze.changeScoreMatrix(movementConfig[i].matrixMas);

            await new Promise(resolve => setTimeout(resolve, 50));
        }
    
        movementConfig = await movementConfigResponse();
    }
    while(true);
}

//************* SENDING DATA TO BACKEND **************

async function send(dataType){
    let response;

    switch(dataType){
        case "Maze config":
            response = await fetch("/mazeConfig", {
                method: "MazeConfig",
                headers: { "Accept":"application/json", "Content-Type":"application/json" },
                body: JSON.stringify({
                    matrixMas: maze.getCellMas(),
                    numRows: maze.getNumRows(),
                    numColumns: maze.getNumColumns()
                })
            });
        
            await response;
            
            break;
        case "Player config":
            response = await fetch("/playerConfig", {
                method: "PlayerConfig",
                headers: { "Accept":"application/json", "Content-Type":"application/json" },
                body: JSON.stringify({
                    posX: player.getPosX(),
                    posY: player.getPosY()
                })
            })

            await response;

            break;
    }
}

async function movementConfigResponse(){  
    const response = await fetch("/movementConfig", {
        method: "MovementConfig",
        headers: { "Accept":"application/json", "Content-Type":"application/json" },
        body: JSON.stringify()
    })

    return await response.json();
}
