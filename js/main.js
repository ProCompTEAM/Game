const width = window.innerWidth;
const height = window.innerHeight;

let objectGroup;

const tileWidth = 100;

const cameraSpeed = 20;

const chunk_size = 16;

let cursors, cursorPos;

let chunks= new Array(chunk_size).fill(undefined).map(() => Array(chunk_size).fill(undefined));

var game = new Phaser.Game(width, height, Phaser.AUTO, 'test', {

    preload: function () {
        initimages();
               
        game.time.advancedTiming = true;
        game.debug.renderShadow = false;
        game.stage.disableVisibilityChange = true;

        game.plugins.add(new Phaser.Plugin.Isometric(game));

        //game.world.setBounds(0, 0, 3200 * 10, 1500 * 10);

        game.world.setBounds(0, 0, 3200, 1500);

       	game.iso.anchor.setTo(0.5, 0);
    },

    create: function () {

        cursorPos = new Phaser.Plugin.Isometric.Point3();

        cursors = game.input.keyboard.createCursorKeys();

        game.camera.focusOnXY(game.world.centerX, game.world.centerY);

        game.stage.backgroundColor = '#b1dcfc';
            
        objectGroup = game.add.group();

        objectGroup.enableBody = false;

        //game_load_all();
        registerChunk(0,0);
        tile_set(0,0,4,4,12);
        //game.input.activePointer.leftButton.onUp.add(update_tile);       
    },

    update: function () {
        cameraMove();
        selectTile();
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

function drawChunk(offsetX, offsetY, chunk) {
	offsetX = offsetX * chunk_size;
	offsetY = offsetY * chunk_size;

	for (let i = 0; i < chunk_size; i++) {
		for (let j = 0; j < chunk_size; j++) {
			drawTileIso(chunk[i][j], i + offsetX, j + offsetY);			
		}
	}
}

function tile_set(offsetX, offsetY, x , y, id){
	if (chunks[offsetX][offsetY] != undefined){
		chunks[offsetX][offsetY][x][y] = id;
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
}

let world_clear = () => objectGroup.removeAll();

// Other Function

function MapScrolling(x, y) {
  	game.world.camera.setPosition(x,y);
    let isoCam = game.world.camera.view;
    let viewport = {
      	left: isoCam.x - 170,
      	right: isoCam.x + width + 150,
      	top: isoCam.y - 85,
      	bottom: isoCam.y + height + 65
    };

    objectGroup.forEach(function (tile) {
        
        if (intersectRect(tile, viewport) === true) {
            tile.revive();
        }
        else{
            tile.kill();
        } 
    });
}

function cameraMove() {
    if (cursors.right.isDown){
     	MapScrolling((game.world.camera.x + cameraSpeed), game.world.camera.y);
    }
    else if (cursors.left.isDown){
      	MapScrolling((game.world.camera.x - cameraSpeed), game.world.camera.y);
   	}
    if (cursors.down.isDown){
      	MapScrolling(game.world.camera.x, (game.world.camera.y + cameraSpeed));
    }
    else if (cursors.up.isDown){
      	MapScrolling(game.world.camera.x, (game.world.camera.y - cameraSpeed));
    }
}

let intersectRect = (r1, r2) => !(r2.left > r1.left ||
            					 r2.right < r1.right ||
             					 r2.top > r1.top ||
             					 r2.bottom < r1.bottom);

function selectTile() {
	game.iso.unproject(game.input.activePointer.position, cursorPos);

    objectGroup.forEachAlive(function (tile) {
    let inBounds = tile.isoBounds.containsXY(cursorPos.x, cursorPos.y);
    if (!tile.selected && inBounds) {
        tile.selected = true;
        tile.tint = 0x86bfda;
        game.add.tween(tile).to({ isoZ: 4 }, 200, Phaser.Easing.Quadratic.InOut, true);
    }
    else if (tile.selected && !inBounds) {
            tile.selected = false;
            tile.tint = 0xffffff;
            game.add.tween(tile).to({ isoZ: 0 }, 200, Phaser.Easing.Quadratic.InOut, true);
        }
    });
}

let gameObjects = [];

function drawTileIso(TileType, i, j) {
	let tile;
    tile = game.add.isoSprite(i * tileWidth, j * tileWidth , 0, gameObjects[TileType], 0, objectGroup);
    tile.smoothed = false;
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

function registerChunk(ox, oy) {
	if (chunks[ox][oy] == undefined){
    let chunk = new Array(chunk_size).fill(0).map(() => Array(chunk_size).fill(0));
    chunks[ox][oy] = chunk;
    drawChunk(ox, oy, chunk);
	}
	else{
		console.log(`chunk[${ox}][${oy}] already exist!`);
	}
}


function initimages(){
	game.load.image('floorSprite', 'assets/floor.png');
    gameObjects[0] = 'floorSprite';
    initraces();
    inithouses();
    initforests();
    initshops();
    game.load.image('buildingSprite', 'assets/building.png');
    gameObjects[28] = 'buildingSprite';
}

function initraces(){
    for (let i = 1; i < 12; i++) {
        game.load.image(`race${i}Sprite`, `assets/race${i}.png`);
        gameObjects.push(`race${i}Sprite`);
    }
}

function inithouses(){
    for (let i = 1; i < 11; i++){
        game.load.image(`house${i}Sprite`, `assets/house${i}.png`);
        gameObjects.push(`house${i}Sprite`);
    }
}

function initforests(){
    for (let i = 1; i < 3; i++){
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




