const width = window.innerWidth;
const height = window.innerHeight;

let objectGroup;

const tileWidth = 100;

const cameraSpeed = 10;

const chunk_size = 16;

let cursors;

let gameWidth = 3200 * 20;
let gameHeight = 1600 * 20;

let chunks= new Array(100).fill(undefined).map(() => Array(100).fill(undefined));

let onDown = false;

let OffsetPen = new Phaser.Point(0,0);

var game = new Phaser.Game(width, height, Phaser.Auto, 'Game', {

    preload: function (){
        initimages();
		game.load.spritesheet('bus', 'assets/bus.png', 60 , 40);

        game.time.advancedTiming = true;

        game.debug.renderShadow = false;

        game.plugins.add(new Phaser.Plugin.Isometric(game));

        game.world.setBounds(0, 0, gameWidth, gameHeight);

       	game.iso.anchor.setTo(0.5, 0);
    },

    create: function (){
        cursors = game.input.keyboard.createCursorKeys();
        game.physics.startSystem(Phaser.Plugin.Isometric.ISOARCADE);

        game.world.camera.roundPx = false;

        game.stage.backgroundColor = '#b1dcfc';
            
        objectGroup = game.add.group();

        game.world.camera.setPosition(gameWidth / 2 - 500, 0);

        key = game.input.keyboard.addKey(Phaser.Keyboard.F1);

        key.onDown.add(debug, this);

        game.input.activePointer.leftButton.onUp.add(update_tile); 
    
		bluebus = new bus();
		bluebus.draw(0,0,1,0);
		
        loadMap();
		

    },

    update: function (){
        dispayMap();
        cameraMove();
		bluebus.move();
    },

    render: function (){
        if(onDown){
            game.debug.text('FPS: ' + game.time.fps || 'FPS: --', 40, 115, "#F5F5DC");
            game.debug.cameraInfo(game.camera, 40, 32);
            game.debug.text('Tiles: ' + objectGroup.countLiving() || 'Tiles: --', 40, 135, "#F5F5DC");
            game.debug.text(`chunk[${OffsetPen.x}][${OffsetPen.y}]`|| 'Chunk: --', 40, 155, "#F5F5DC");   
        }
    },
});


function drawChunk(offsetX, offsetY){
	if (offsetX < 0 || offsetY < 0) return;

	if (chunks[offsetX][offsetY] != undefined){
		let X = offsetX * chunk_size;
		let Y = offsetY * chunk_size;
		for (let i = 0; i < chunk_size; i++) {
			for (let j = 0; j < chunk_size; j++) {
				drawTileIso(chunks[offsetX][offsetY][i][j], i + X, j + Y);
			}
		}
	}
	else return;
}

function tile_set(offsetX, offsetY, x , y, id){
	if (chunks[offsetX][offsetY] != undefined){
		if (chunks[offsetX][offsetY][x][y] == id) return;
		else{
			chunks[offsetX][offsetY][x][y] = id;
			change_tile(offsetX, offsetY, x, y, id);
		}
	}
	else{
		registerChunk(offsetX, offsetY);
		change_tile(offsetX, offsetY, x, y, id);
	}
	game.iso.simpleSort(objectGroup);
}

let cursorPos = new Phaser.Point(0,0);
let ox = 0;
let oy = 0;

function update_tile(){
    game.iso.unproject(game.input.activePointer.position, cursorPos);
    let pos = new Phaser.Point();
    objectGroup.forEachAlive(function (tile) {
        let inBounds = tile.isoBounds.containsXY(cursorPos.x, cursorPos.y);
        if (inBounds) {
            pos.x = tile.isoX / tileWidth;
            pos.y = tile.isoY / tileWidth;
            tile.destroy();
            drawTileIso(99, pos.x, pos.y);
            ox = Math.floor(pos.x/chunk_size);
            oy = Math.floor(pos.y/chunk_size);
            if (pos.x < chunk_size)  ox = 0; else pos.x -= chunk_size * ox;
            if (pos.y < chunk_size) oy = 0; else pos.y -= chunk_size * oy;
            game.iso.simpleSort(objectGroup);
        }
    });
	click_level(ox, oy, pos.x, pos.y);
}

function cameraMove() {
    if (cursors.right.isDown) game.world.camera.x += cameraSpeed;
    else if (cursors.left.isDown) game.world.camera.x -= cameraSpeed;		
    if (cursors.down.isDown) game.world.camera.y += cameraSpeed;
    else if (cursors.up.isDown)game.world.camera.y -= cameraSpeed;
}


function drawChunks(offsetX, offsetY){
	world_clear();
	for (var x = offsetX - 1; x < offsetX + 2 ; x++){
		for (var y = offsetY - 1 ; y < offsetY + 2 ; y++) {
			drawChunk(x, y); 
		}
	}
}

let tempPoint = new Phaser.Point(0,0);

async function dispayMap(){
    OffsetPen = await new Phaser.Point(game.world.camera.x - (gameWidth / 2 - 500), game.world.camera.y);
    await game.iso.unproject(OffsetPen, OffsetPen);

    OffsetPen.x =  await Math.round(OffsetPen.x / chunk_size / (tileWidth * 2));
    OffsetPen.y =  await Math.round(OffsetPen.y / chunk_size / (tileWidth * 2));

    if (tempPoint.x != OffsetPen.x || tempPoint.y != OffsetPen.y){ 
        tempPoint =  await OffsetPen;
        await sleep(100);
        await drawChunks(tempPoint.x, tempPoint.y);
    }
}

async function loadMap(){
	await game_load_all();
	await sleep(2000);
	await drawChunks(0,0);
	await bluebus.findPath();
}

let gameObjects = [];

function drawTileIso(TileType, i, j){
	let tile;
	tile = game.add.isoSprite(i * tileWidth, j * tileWidth , 0, gameObjects[TileType], 0, objectGroup);
    tile.autoCull = true;
    tile.smoothed = false;
}

function initimages(){
    for (let i = 1; i < 100; i++){
        if (i >= 5 && i <= 9) gameObjects[i] = undefined;
        else if (i >= 32 && i <= 50) gameObjects[i] = undefined;
        else if (i >= 55) gameObjects[i] = undefined;
        else{
            game.load.image(`${i}`, `assets/${i}.png`);
            gameObjects[i] = i;
        } 
    }
	game.load.image(`99`, `assets/99.png`);
	gameObjects[99] = 99;
}

function change_tile(offsetX, offsetY, x, y, id){
	let pos = new Phaser.Point();
    objectGroup.forEach(function (tile) {
    	pos.x = tile.isoX / tileWidth;
        pos.y = tile.isoY / tileWidth;
        
        let isExists = pos.x == x + offsetX * chunk_size &&
        			   pos.y == y + offsetY * chunk_size;
    	if (isExists) {
        	tile.destroy();
        	drawTileIso(id, pos.x, pos.y);
    	}
    });
}

function registerChunk(ox, oy){
	if (chunks[ox][oy] == undefined){
    	let chunk = new Array(chunk_size).fill(1).map(() => Array(chunk_size).fill(1));
    	chunks[ox][oy] = chunk;
	}
	else return;
}

let sleep = (ms) => new Promise(resolve => setTimeout(resolve, ms));

let world_clear = () => objectGroup.removeAll();

let debug = () => onDown==false? onDown=true: reset();

function reset() {
    onDown = false;
    game.debug.reset();
}


