class Cell{
    #posX;
    #posY;
    #type;

    #obj;

    #score;
    #scoreObj;

    constructor({posX, posY}, score, type){
        this.#posX = posX;
        this.#posY = posY;
        this.#score = score;
        this.#type = type;

        this.#obj = document.createElement("div");
        this.#obj.classList.add("cell");

        if(this.#type == 0){

        }
        else if(this.#type == 1){
            this.#obj.innerHTML =
            `<div class="cross">
                <span class="line1"></span>
                <span class="line2"></span>
            </div>`;
        }
        else if(this.#type == 2){
            this.#obj.innerHTML =
            `<div class="arrow">
                <span class="line1"></span>
                <span class="line2"></span>
                <span class="line3"></span>
            </div>`;
        }
        else{
            this.#obj.innerHTML = 
            `<span>incorrect type</span>`;
        }

        this.#scoreObj = document.createElement("span");
        this.#scoreObj.classList.add("score");
        this.#scoreObj.innerText = this.#score;
        this.#obj.append(this.#scoreObj);

        mazeObj.append(this.#obj);
    }

    changeScore(score){
        this.#score = score;
        this.#scoreObj.innerText = this.#score;
    }

    getObj(){
        return this.#obj;
    }
}
