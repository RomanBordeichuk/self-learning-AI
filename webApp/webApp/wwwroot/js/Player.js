class Player{
    #maze;
    #posX;
    #posY;

    #obj;
    #cellObj;
    
    constructor(maze, posX, posY){
        this.#maze = maze;
        this.#posX = posX;
        this.#posY = posY;

        this.#obj = document.createElement("div");
        this.#obj.classList.add("player");
        
        this.#cellObj = this.#maze.getCellObj(this.#posY, this.#posX);
        this.#cellObj.append(this.#obj);
    }

    changePos(posX, posY){
        this.#posX = posX;
        this.#posY = posY;
        
        this.#obj.remove();

        this.#obj = document.createElement("div");
        this.#obj.classList.add("player");
        
        this.#cellObj = this.#maze.getCellObj(this.#posX, this.#posY);
        this.#cellObj.append(this.#obj);
    }

    getPosX(){
        return this.#posX;
    }
    getPosY(){
        return this.#posY;
    }
}
