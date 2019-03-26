const width = window.innerWidth;
const height = window.innerHeight;

let objectGroup;

const tileWidth = 100;

const cameraSpeed = 20;

const chunk_size = 16;

let cursors, cursorPos;

let gameWidth = 1000000;
let gameHeiht = 1000000;

let chunks= new Array(chunk_size).fill(undefined).map(() => Array(chunk_size).fill(undefined));

var game = new Phaser.Game(width, height, Phaser.AUTO, 'test', {

    preload: function () {
        initimages();
               
        game.time.advancedTiming = true;
        game.debug.renderShadow = false;
        game.stage.disableVisibilityChange = true;

        game.plugins.add(new Phaser.Plugin.Isometric(game));

        game.world.setBounds(0, 0, gameWidth, gameHeiht);

       	game.iso.anchor.setTo(0.5, 0);
    },

    create: function () {

        cursorPos = new Phaser.Plugin.Isometric.Point3();

        cursors = game.input.keyboard.createCursorKeys();

        game.world.camera.roundPx = false;

        game.stage.backgroundColor = '#b1dcfc';
            
        objectGroup = game.add.group();

        objectGroup.enableBody = false;

        game.world.setBounds(0, 0, gameWidth, gameHeiht);
        game.camera.focusOnXY(gameWidth / 2, 0);
		
        

        game_load_all();

		MapScrolling(game.camera.x, game.camera.y);

        //game.input.activePointer.leftButton.onUp.add(update_tile);       
    },

    update: function () {
        cameraMove();
    },

    render: function () {
        game.debug.text('FPS: ' + game.time.fps || 'FPS: --', 40, 120, "#F5F5DC");
       	game.debug.cameraInfo(game.camera, 32, 32);
        game.debug.text('Tiles: ' + objectGroup.countLiving() || 'FPS: --', 40, 150, "#F5F5DC");

    },

});


function world_create() {

	for (let i = 0; i < chunks[0].length; i++){
	 	for (let j = 0; j < chunks[1].length; j++) {	 		
            if (chunks[i][j] === undefined){
                console.log(i, j);
            }
            else {
                drawChunk(i, j, chunks[i][j]);
            }
	 	}
	}
}

function chunk_clear(offsetX, offsetY) {
    chunks[offsetX][offsetY] = undefined;
    world_clear();
    world_create();
}

let chunkWidth = tileWidth * chunk_size;

function drawChunk(offsetX, offsetY, chunk) {
	offsetX = offsetX * chunk_size;
	offsetY = offsetY * chunk_size;

	for (let i = 0; i < chunk_size; i++) {
		for (let j = 0; j < chunk_size; j++) {
			drawTileIso(chunk[i][j], i + offsetX, j + offsetY);
		}
	}
}
async function tile_set(offsetX, offsetY, x , y, id){

	if (chunks[offsetX][offsetY] != undefined){
		if (chunks[offsetX][offsetY][x][y] == id){
			return;
		}
		else{
			chunks[offsetX][offsetY][x][y] = id;
			await change_chunk(offsetX, offsetY, x, y, id);
		}
	}
	else{
		await registerChunk(offsetX, offsetY)
		await change_chunk(offsetX, offsetY, x, y, id);
	}
}

function change_chunk(offsetX, offsetY, x, y, id){
	let pos = new Phaser.Point();
    objectGroup.forEachAlive(function (tile) {
    	pos.x = tile.isoX / tileWidth;
        pos.y = tile.isoY / tileWidth;

        let isExists = pos.x == offsetX * 16 + x &&
        			   pos.y == offsetY * 16 + y;
    	if (isExists) {
        	tile.destroy();
        	drawTileIso(id, offsetX * 16 + x, offsetY * 16 + y);
    		}
    	});
}

function registerChunk(ox, oy) {
	if (chunks[ox][oy] == undefined){
    	let chunk = new Array(chunk_size).fill(1).map(() => Array(chunk_size).fill(1));
    	chunks[ox][oy] = chunk;
    	drawChunk(ox, oy, chunk);
	}
	else{
		console.log(`chunk[${ox}][${oy}] already exist!`);
	}
}

let world_clear = () => objectGroup.removeAll();


let viewport = {};
let isoCam;

function MapScrolling(x, y) {
  	game.world.camera.setPosition(x,y);
    isoCam = game.world.camera.view;
   	viewport = {
    	left: isoCam.x - 170,
      	right: isoCam.x + width + 170,
      	top: isoCam.y - 85,
      	bottom: isoCam.y + height + 100
    };

    objectGroup.forEach(function (tile) {      
        if (intersectRect(tile, viewport) === true) {
            tile.visible = true;
        }
        else{
            tile.visible = false;
        } 
    });
}

async function cameraMove() {
    if (cursors.right.isDown){
     	await MapScrolling((game.world.camera.x + cameraSpeed), game.world.camera.y);
    }
    else if (cursors.left.isDown){
      	await MapScrolling((game.world.camera.x - cameraSpeed), game.world.camera.y);
   	}
    if (cursors.down.isDown){
      	await MapScrolling(game.world.camera.x, (game.world.camera.y + cameraSpeed));
    }
    else if (cursors.up.isDown){
      	await MapScrolling(game.world.camera.x, (game.world.camera.y - cameraSpeed));    }

}

let intersectRect = (r1, r2) => !(r2.left > r1.left ||
            					 r2.right < r1.right ||
             					 r2.top > r1.top ||
             					 r2.bottom < r1.bottom);

function selectTile() {
	game.iso.unproject(game.input.activePointer.position, cursorPos);

    objectGroup.forEach(function (tile) {
    
    });
}

let gameObjects = [];

function drawTileIso(TileType, i, j){
	let tile;
	tile = game.add.isoSprite(i * tileWidth, j * tileWidth , 0, gameObjects[TileType], 0, objectGroup);
	tile.smoothed = false;
	tile.visible = false;
}

function drawTown(TileType, i, j){
	var town;
	town = game.add.isoSprite(i * 200, j * 200, 0, gameObjects[TileType], 0, objectGroup);
}

function update_tile(){
    game.iso.unproject(game.input.activePointer.position, cursorPos);
    let pos = new Phaser.Point();
    objectGroup.forEachAlive(function (tile) {
    	let inBounds = tile.isoBounds.containsXY(cursorPos.x, cursorPos.y);
    	if (inBounds) {
        	pos.x = tile.isoX / tileWidth;
        	pos.y = tile.isoY / tileWidth;
        	tile.destroy();
        	drawTileIso(1, pos.x, pos.y);
    	}
    });
}


function initimages(){
	game.load.image('floorSprite', 'assets/floor.png');
	game.load.image('dustSprite', 'assets/dust.png');
	game.load.image('oceanSprite', 'assets/ocean.jpg')
    gameObjects[1] = 'floorSprite';
    gameObjects[2] = 'oceanSprite';
    gameObjects[3] = 'dustSprite';
    initforests();
    reserv();
    initraces();
    inithouses();
    reservhouses();
    initshops();
}

function initraces(){
    for (let i = 1; i < 12; i++) {
        game.load.image(`race${i}Sprite`, `assets/race${i}.png`);
        gameObjects.push(`race${i}Sprite`);
    }
}

function inithouses(){
    for (let i = 1; i < 12; i++){
        game.load.image(`house${i}Sprite`, `assets/house${i}.png`);
        gameObjects.push(`house${i}Sprite`);
    }
}

function initforests(){
    for (let i = 1; i < 2; i++){
        game.load.image(`forest${i}Sprite`, `assets/forest${i}.png`);
        gameObjects.push(`forest${i}Sprite`);
    }
}

function initshops(){
    for (let i = 1; i < 5; i++){
        game.load.image(`shop${i}Sprite`, `assets/shop${i}.png`);
        gameObjects.push(`shop${i}Sprite`);
    }
}

function reserv(){
	for (var i = 0; i < 5; i++) {
		gameObjects.push(undefined);
	}
}

function reservhouses(){
	for (var i = 0; i < 19; i++) {
		gameObjects.push(undefined);
	}
}




