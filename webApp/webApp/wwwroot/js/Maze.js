class Maze{
    #cellMatrix;
    #cellMas;

    #numMazeRows;
    #numMazeColumns;
    #cellObjMatrix;

    #scoreMatrix;

    constructor(cellMatrix){
        this.#cellMatrix = cellMatrix;

        this.#numMazeRows = this.#cellMatrix.length;
        this.#numMazeColumns = this.#cellMatrix[0].length;
        this.#cellMas = [];
        
        for(let i = 0; i < this.#numMazeRows; i++){
            for(let j = 0; j < this.#numMazeColumns; j++){
                this.#cellMas.push(this.#cellMatrix[i][j]);
            }
        }

        this.#cellObjMatrix = [];

        for(let i = 0; i < this.#numMazeRows; i++){
            this.#cellObjMatrix[i] = [];
        
            for(let j = 0; j < this.#numMazeColumns; j++){
                this.#cellObjMatrix[i][j] = 
                    new Cell({j, i}, 100, this.#cellMatrix[i][j]);
            }
        }
    }
    changeScoreMatrix(scoreMas){
        this.#scoreMatrix = [];

        for(let i = 0; i < this.#numMazeRows; i++){
            this.#scoreMatrix[i] = [];

            for(let j = 0; j < this.#numMazeColumns; j++){
                this.#scoreMatrix[i][j] = scoreMas[i * this.#numMazeColumns + j];
                this.#cellObjMatrix[i][j].changeScore(this.#scoreMatrix[i][j]);
            }
        }
    }

    getNumRows(){
        return this.#numMazeRows;
    }
    getNumColumns(){
        return this.#numMazeColumns;
    }
    getCellObj(x, y){
        return this.#cellObjMatrix[y - 1][x - 1].getObj();
    }
    getCellMas(){
        return this.#cellMas;
    }
}
