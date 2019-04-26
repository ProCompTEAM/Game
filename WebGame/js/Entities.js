var direction;
var tile;
var currentPos = new Phaser.Point(0, 0);

class bus{
    consturcor(){

        this.bus = game.add.group();
    }

    draw(ox, oy, x, y){
        tile = game.add.isoSprite(x * 100 + 70, y * 100 - 10, 1, 'bus', 0, this.bus);

        tile.animations.add('S', [0], 1, true);
        tile.animations.add('E', [3], 1, true);
        tile.animations.add('W', [2], 1, true);
        tile.animations.add('N', [3], 1, true);
        game.physics.isoArcade.enable(tile);
    }

    findPath(){
        var easystar = new EasyStar.js();
        var temp = new Phaser.Point(0,0);

        easystar.setAcceptableTiles([10]);
        easystar.setGrid(chunks[ox][oy]);
        console.log(chunks[ox][oy]);
        setInterval(function (){
            easystar.findPath(currentPos.x, currentPos.y, 0, 5, function(path){
                if (path === null){
                    console.log("Path = null");
                    direction = 'STOP';
                }
				else if (path[1] != undefined){
					temp.x = currentPos.x;
					temp.y = currentPos.y;
					currentPos.x = path[1].x;
					currentPos.y = path[1].y;
					console.log(currentPos.x, currentPos.y);
				}
				
				if (currentPos.y > temp.y){
					direction = 'E';
				}
				
				else if (currentPos.x > temp.x){
					direction = 'S'
				}
				
				if (path[1] == undefined){
					direction = 'STOP';
				}
				
                if (direction != 'STOP') tile.animations.play(direction);
            });
            easystar.calculate();
        }, 2000);
    }

    move(){
        if (direction == 'N'){
            tile.body.velocity.x = 50;
            tile.body.velocity.y = 0;
        }
        else if (direction == 'S'){
            tile.body.velocity.y = 50;
            tile.body.velocity.x = 0;
        }
        else if (direction == 'N'){
            tile.body.velocity.y = -50;
            tile.body.velocity.x = 0;
        }
        else if (direction == 'STOP'){
            tile.body.velocity.x = 0;
            tile.body.velocity.y = 0;
        }
    }
}