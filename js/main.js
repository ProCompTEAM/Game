var width = window.innerWidth;
var height = window.innerHeight;

var objectGroup;

const tileWidth = 100;

const cameraSpeed = 20;

const chunk_size = 16;

var cursors, cursorPos;

var chunks = [];

var gameObjects = [];

var images = [];

var game = new Phaser.Game(width, height, Phaser.AUTO, 'test', {

    preload: function () {
        initimages();
               
        game.time.advancedTiming = true;
        game.debug.renderShadow = false;
        game.stage.disableVisibilityChange = true;

        game.plugins.add(new Phaser.Plugin.Isometric(game));

        //game.world.setBounds(0, 0, 3200 * 10, 1500 * 10);

        game.world.setBounds(0, 0, 5000, 5000);

       	game.iso.anchor.setTo(0.5, 0);
    },

    create: function () {

        //initGameObjects();

        var TileType;

        cursorPos = new Phaser.Plugin.Isometric.Point3();

        cursors = game.input.keyboard.createCursorKeys();

        game.camera.focusOnXY(game.world.centerX, game.world.centerY);

        game.stage.backgroundColor = '#b1dcfc';
            
        objectGroup = game.add.group();

        objectGroup.enableBody = false;

        game_load_all();

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
	for (var i = 0; i < chunks[0].length; i++){
	 	for (var j = 0; j < chunks[1].length; j++) {
	 		
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

	for (var i = 0; i < chunk_size; i++) {
		for (var j = 0; j < chunk_size; j++) {
			drawTileIso(chunk[i][j], i + offsetX, j + offsetY);			
		}
	}
}

function tile_set(offsetX, offsetY, x, y, id) {
    drawTileIso(id, offsetX * 16 + x, offsetY * 16 + y);
}

function world_clear(){
    objectGroup.removeAll();
}

// Other Function

function MapScrolling(x, y) {
  	game.world.camera.setPosition(x,y);
    var isoCam = game.world.camera.view;
    var viewport = {
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

function intersectRect(r1, r2) {
    return !(r2.left > r1.left ||
             r2.right < r1.right ||
             r2.top > r1.top ||
             r2.bottom < r1.bottom);
}

function selectTile() {

	game.iso.unproject(game.input.activePointer.position, cursorPos);

    objectGroup.forEachAlive(function (tile) {
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

var tile;

function drawTileIso(TileType, i, j) {

    tile = game.add.isoSprite(i * tileWidth, j * tileWidth , 0, gameObjects[TileType], 0, objectGroup);
    tile.smoothed = false;
}

function update_tile(){
    game.iso.unproject(game.input.activePointer.position, cursorPos);
    var pos = new Phaser.Point();
    objectGroup.forEachAlive(function (tile) {
    var inBounds = tile.isoBounds.containsXY(cursorPos.x, cursorPos.y);
    if (inBounds) {
        pos.x = tile.isoX / tileWidth;
        pos.y = tile.isoY / tileWidth;
        tile.destroy();
        drawTileIso(1, pos.x, pos.y);
    }
    });
}

function registerChunk(ox, oy) {
    var ch = new Array(chunk_size).fill(0).map(() => Array(chunk_size).fill(0));
    
    drawChunk(ox, oy, ch);
}

function initimages(){

    images.push(game.load.image('floorSprite', 'assets/floor.png'));
    gameObjects[0] = 'floorSprite';
    initraces();
    inithouses();
    initforests();
    initshops();
    images.push(game.load.image('buildingSprite', 'assets/building.png'));
    gameObjects[30] = 'buildingSprite';
}

function initraces(){
    for (var i = 1; i < 12; i++) {
        images.push(game.load.image('race'+i+'Sprite', 'assets/race'+i+'.png'));
        gameObjects.push('race'+i+'Sprite');
    }
}

function inithouses(){
    for (var i = 1; i < 11; i++){
        images.push(game.load.image('house'+i+'Sprite', 'assets/house'+i+'.png'));
        gameObjects.push('house'+i+'Sprite');
    }
}

function initforests(){
    for (var i = 1; i < 3; i++){
        images.push(game.load.image('forest'+i+'Sprite', 'assets/forest'+i+'.png'));
        gameObjects.push('forest'+i+'Sprite');
    }
}

function initshops(){
    for (var i = 1; i < 5; i++){
        images.push(game.load.image('shop'+i+'Sprite', 'assets/shop'+i+'.png'));
        gameObjects.push('shop'+i+'Sprite');
    }
}
