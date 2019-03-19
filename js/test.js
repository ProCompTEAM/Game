var width = window.innerWidth;
var height = window.innerHeight;

var objectGroup;

const tileWidth = 100;

const cameraSpeed = 20;

const chunk_size = 4;

var cursors, cursorPos;

var chunk1 = new Array(chunk_size).fill(0).map(() => Array(chunk_size).fill(0));
var chunk2 = new Array(chunk_size).fill(0).map(() => Array(chunk_size).fill(0));
var chunk3 = new Array(chunk_size).fill(0).map(() => Array(chunk_size).fill(0));
var chunk4 = new Array(chunk_size).fill(0).map(() => Array(chunk_size).fill(0));

var chunks = [[chunk1, chunk2],
			  [chunk3, chunk4]];

var gameObjects = [];

var game = new Phaser.Game(width, height, Phaser.AUTO, 'test', {

    preload: function () {
       	game.load.image('tile', 'assets/floor.png');
        game.load.image('empty', 'assets/empty.png');
            
         
        game.time.advancedTiming = true;
        game.debug.renderShadow = false;
        game.stage.disableVisibilityChange = true;

        game.plugins.add(new Phaser.Plugin.Isometric(game));

        game.world.setBounds(0, 0, 2000, 2000);

       	game.iso.anchor.setTo(0.5, 0);
    },

    create: function () {

        initGameObjects();

        var TileType;

        cursorPos = new Phaser.Plugin.Isometric.Point3();

        cursors = game.input.keyboard.createCursorKeys();

        game.camera.focusOnXY(4160, 0);


        game.stage.backgroundColor = '#b1dcfc';
            
        objectGroup = game.add.group();

        objectGroup.enableBody = false;

        //cchunks = chunkArray(levelData, chunk_size);

    	//while(cchunks.length) chunks.push(cchunks.splice(0,chunk_size));

       	world_create();
       
    },

    update: function () {

        cameraMove();
        selectTile();
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

	chunks[offsetX][offsetY][x][y] = id;
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

    gameObjects[0] = 'tile';
    gameObjects[1] = 'empty';
}

var tile;

function drawTileIso(TileType, i, j) {

    tile = game.add.isoSprite(i * tileWidth, j * tileWidth , 0, gameObjects[TileType], 0, objectGroup);
    tile.smoothed = false;
}





