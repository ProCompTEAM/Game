var easystar = new EasyStar.js();
easystar.setIterationsPerCalculation(1000); 

class bus{
    constructor(ox, oy, x, y, finalPos){
        this.ox = ox;
        this.oy = oy;
        this.x = x;
        this.y = y;       
        this.tile;
        this.speed = 75;
        this.direction;
        this.finalPos = finalPos;
        this.currentPos = new Phaser.Point(this.x, this.y);
        this.startPos = Object.assign({}, this.currentPos);
        this.delay = Math.random() * (5000 - 1000) + 1000;
    }

    draw(){					
		this.tile = game.add.isoSprite((this.y + this.ox * chunk_size) * tileWidth, (this.x + this.oy * chunk_size) * tileWidth, 1, 'bus', 0, busGroup);
		this.tile.animations.add('W', [0], 1, true);
		this.tile.animations.add('E', [1], 1, true);
		this.tile.animations.add('S', [2], 1, true);
		this.tile.animations.add('N', [3], 1, true);
		game.physics.isoArcade.enable(this.tile);
		
		return;
    }

    findPath(){
        easystar.setAcceptableTiles(getAcceptableTiles()); 
        easystar.setGrid(chunks[this.ox][this.oy]);
        this.checkPath();
    }

    checkPath(){
        let that = this;
        let isFinished;
        let temp = new Phaser.Point(0,0);
        let timer = setTimeout(function tick(){
        easystar.setGrid(chunks[that.ox][that.oy]);
        easystar.findPath(that.currentPos.x, that.currentPos.y, 
            that.finalPos.x, that.finalPos.y, function(path){

            isFinished = that.currentPos.x == that.finalPos.x && that.currentPos.y == that.finalPos.y;

            if (isFinished){
                that.finalPos.x = that.startPos.x;
                that.finalPos.y = that.startPos.y;

                that.startPos.x = that.currentPos.x;
                that.startPos.y = that.currentPos.y;
            }

            if (path === null || path[1] == undefined){
                that.direction = 'STOP';
                that.move();
            }

            else if (path[1] != undefined){
                if (Math.abs(Math.floor(that.tile.isoY) - path[0].x * tileWidth) < 10 && 
                        Math.abs(Math.floor(that.tile.isoX) - path[0].y * tileWidth) < 10){

                    temp.x = that.currentPos.x;
                    temp.y = that.currentPos.y;

                    that.currentPos.x = path[1].x;
                    that.currentPos.y = path[1].y;
                
                    path.splice(0,1);    
                }
                that.direction = checkDirection(that, temp);
                that.tile.animations.play(that.direction);
                that.move();
            }

        });
        
        easystar.calculate();
        timer = setTimeout(tick, 10);
        }, that.delay);
    }

    move(){
        if (this.direction == 'E'){
            this.tile.body.velocity.x = this.speed;
            this.tile.body.velocity.y = 0;
        }
        else if (this.direction == 'W'){
            this.tile.body.velocity.x = -this.speed;
            this.tile.body.velocity.y = 0;
        }
        else if (this.direction == 'S'){
            this.tile.body.velocity.y = this.speed;
            this.tile.body.velocity.x = 0;
        }   
        else if (this.direction == 'N'){
            this.tile.body.velocity.y = -this.speed;
            this.tile.body.velocity.x = 0;
        }
        else{
            this.tile.body.velocity.x = 0;
            this.tile.body.velocity.y = 0;
        }
    }
}

function checkDirection(that, temp){
    let direction;

    if (that.currentPos.y > temp.y) direction = 'E';
    else if (that.currentPos.x > temp.x) direction = 'S';
    else if (that.currentPos.y < temp.y) direction = 'W';
    else if (that.currentPos.x < temp.x) direction = 'N';
    else if (that.currentPos.x == temp.x && that.currentPos.y == temp.y) direction = 'STOP';

    return direction;
}