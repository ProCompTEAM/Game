var width = window.innerWidth;
var height = window.innerHeight;

var objectGroup;

const tileWidth = 100;

const cameraSpeed = 20;

const chunk_size = 16;

var cursors, cursorPos;

var chunks = [];
			  
function registerChunk(ox, oy)
{
	var ch = new Array(chunk_size).fill(0).map(() => Array(chunk_size).fill(0));
	
	drawChunk(ox, oy, ch);
}

var gameObjects = [];

var game = new Phaser.Game(width, height, Phaser.AUTO, 'test', {

    preload: function () {
		
    game.load.image('floorSprite', 'assets/floor.png');
    game.load.image('heroSprite', 'assets/bus1.png');

    game.load.image('shop1Sprite', 'assets/shop1.png');
    game.load.image('shop2Sprite', 'assets/shop2.png');
    game.load.image('shop3Sprite', 'assets/shop3.png');
    game.load.image('shop4Sprite', 'assets/shop4.png');

    game.load.image('race1Sprite', 'assets/race1.png');
    game.load.image('race2Sprite', 'assets/race2.png');
    game.load.image('race3Sprite', 'assets/race3.png');
    game.load.image('race4Sprite', 'assets/race4.png');
    game.load.image('race5Sprite', 'assets/race5.png');
    game.load.image('race6Sprite', 'assets/race6.png');
    game.load.image('race7Sprite', 'assets/race7.png');
    game.load.image('race8Sprite', 'assets/race8.png');
    game.load.image('race9Sprite', 'assets/race9.png');
    game.load.image('race10Sprite', 'assets/race10.png');
    game.load.image('race11Sprite', 'assets/race11.png');

    game.load.image('house1Sprite', 'assets/house1.png');
    game.load.image('house2Sprite', 'assets/house2.png');
    game.load.image('house3Sprite', 'assets/house3.png');
    game.load.image('house4Sprite', 'assets/house4.png');
    game.load.image('house5Sprite', 'assets/house5.png');
    game.load.image('house6Sprite', 'assets/house6.png');
    game.load.image('house7Sprite', 'assets/house7.png');
    game.load.image('house8Sprite', 'assets/house8.png');
    game.load.image('house9Sprite', 'assets/house9.png');
    game.load.image('house10Sprite', 'assets/house10.png');

    game.load.image('forest1Sprite', 'assets/forest1.png');
    game.load.image('forest2Sprite', 'assets/forest2.png');
	
	game.load.image('buildingSprite', 'assets/building.png');
            
         
        game.time.advancedTiming = true;
        game.debug.renderShadow = false;
        game.stage.disableVisibilityChange = true;

        game.plugins.add(new Phaser.Plugin.Isometric(game));

       	game.iso.anchor.setTo(0.5, 0);
    },

    create: function () {

        initGameObjects();

        var TileType;

        cursorPos = new Phaser.Plugin.Isometric.Point3();

        cursors = game.input.keyboard.createCursorKeys();
		
		game.world.setBounds(0, 0, 3200 * 10, 1500 * 10);
		//game.world.setBounds(0, 0, 1000, 1000);
		
		game.camera.focusOnXY(game.world.centerX, game.world.centerY);

        game.stage.backgroundColor = '#b1dcfc';
            
        objectGroup = game.add.group();

        objectGroup.enableBody = false;

        //cchunks = chunkArray(levelData, chunk_size);

    	//while(cchunks.length) chunks.push(cchunks.splice(0,chunk_size));

       	//world_create();
		
		
		//drawChunk(0,0, chunk1);
       
	   game_load_all();
    },

    update: function () {

        cameraMove();
       // selectTile();
    },

    render: function () {
        game.debug.text('FPS: ' + game.time.fps || 'FPS: --', 40, 120, "#F5F5DC");
       	game.debug.cameraInfo(game.camera, 32, 32);
    },

});


function world_create() {
	 for (var i = 0; i < chunks.length; i++){
	 	for (var j = 0; j < chunks.length; j++) {
	 		console.log(i, j);
	 		drawChunk(i, j, chunks[i][j]);
	 	}
	 }
}

function drawChunk(offsetX, offsetY, chunk) {

	offsetX = offsetX * chunk_size;
	offsetY = offsetY * chunk_size;

	for (var i = 0; i < chunk_size; i++) {
		for (var j = 0; j < chunk_size; j++) {
			drawTileIso(chunk[i][j], i + offsetX, j + offsetY);			
		}
	}
}

function tile_set(offsetX, offsetY, x, y, id) {

		drawTileIso(id, offsetX * 16 + x, offsetY * 16 + y);
		
		//console.log(id, offsetX, offsetY, y, x);
}





// Other Function
function MapScrolling(x, y) {
  	game.world.camera.setPosition(x,y);
    var isoCam = game.world.camera.view;
    var viewport = {
      	left: isoCam.x,
      	right: isoCam.x + width,
      	top: isoCam.y,
      	bottom: isoCam.y + height
    };

    objectGroup.forEach(function (tile) {
        var inBounds = tile.isoBounds.containsXY(viewport.right, viewport.bottom);
        
            // If it does, do a little animation and tint change.
        if (intersectRect(tile, viewport) === true) {

            tile.visible = true;
        }
        else{
        	tile.visible = false;
            // If not, revert back to how it was.
        }
   
    });
}

function cameraMove() {

    if (cursors.right.isDown){
     	MapScrolling((game.world.camera.x + cameraSpeed), game.world.camera.y);
    }
    if (cursors.left.isDown){
      	MapScrolling((game.world.camera.x - cameraSpeed), game.world.camera.y);
   	}
    if (cursors.down.isDown){
      	MapScrolling(game.world.camera.x, (game.world.camera.y + cameraSpeed));
    }
    if (cursors.up.isDown){
      	MapScrolling(game.world.camera.x, (game.world.camera.y - cameraSpeed));
    }
}

function intersectRect(r1, r2) {
    return !(r2.left > r1.right ||
             r2.right < r1.left ||
             r2.top > r1.bottom ||
             r2.bottom < r1.top);
}

function chunkArray(myArray, chunk_size) {
    var index = 0;
    var arrayLength = myArray.length;
    var tempArray = [];
    
    for (index = 0; index < arrayLength; index += chunk_size) {
        myChunk = myArray.slice(index, index+chunk_size);
        tempArray.push(myChunk);
        
    }
    return tempArray;
}

function selectTile() {

	game.iso.unproject(game.input.activePointer.position, cursorPos);

        // Loop through all tiles and test to see if the 3D position from above intersects with the automatically generated IsoSprite tile bounds.
    objectGroup.forEach(function (tile) {
    var inBounds = tile.isoBounds.containsXY(cursorPos.x, cursorPos.y);
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

function initGameObjects() {

    gameObjects[0] = 'floorSprite';
    gameObjects[1] = 'race2Sprite';
    gameObjects[2] = 'race1Sprite';
    gameObjects[3] = 'race6Sprite';
    gameObjects[4] = 'race4Sprite';
    gameObjects[5] = 'race5Sprite';
    gameObjects[6] = 'race3Sprite';
    gameObjects[7] = 'shop1Sprite';
    gameObjects[8] = 'shop2Sprite';
    gameObjects[9] = 'shop3Sprite';
    gameObjects[10] = 'shop4Sprite';
    gameObjects[11] = 'race9Sprite';
    gameObjects[12] = 'race8Sprite';
    gameObjects[13] = 'race7Sprite';
    gameObjects[14] = 'race10Sprite';
    gameObjects[15] = 'race11Sprite';
    gameObjects[16] = 'forest1Sprite';
    gameObjects[17] = 'forest2Sprite';
    gameObjects[18] = 'floorSprite';// Empty Tile
    gameObjects[19] = 'floorSprite';// Empty Tile
    gameObjects[20] = 'house1Sprite';
    gameObjects[21] = 'house2Sprite';
    gameObjects[22] = 'house2Sprite';
    gameObjects[23] = 'house3Sprite';
    gameObjects[24] = 'house4Sprite';
    gameObjects[25] = 'house5Sprite';
    gameObjects[26] = 'house6Sprite';
    gameObjects[27] = 'house7Sprite';
    gameObjects[28] = 'house8Sprite';
    gameObjects[29] = 'house9Sprite';
	gameObjects[30] = 'house10Sprite';
}

var tile;

function drawTileIso(TileType, i, j) {

    tile = game.add.isoSprite(i * tileWidth, j * tileWidth , 0, gameObjects[TileType], 0, objectGroup);
    tile.smoothed = false;
}

