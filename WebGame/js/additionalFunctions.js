function pointerInteraction(){
    game.iso.unproject(game.input.activePointer.position, cursorPos);
    objectGroup.forEachAlive(function (tile) {
        let inBounds = tile.isoBounds.containsXY(cursorPos.x, cursorPos.y);
        if (!tile.selected && inBounds) {
            tile.selected = true;
            tile.tint = 0xCFECFA;
            game.add.tween(tile).to({ isoZ: 0 }, 200, Phaser.Easing.Quadratic.InOut, true);
        }
        else if (tile.selected && !inBounds) {
            tile.selected = false;
            tile.tint = 0xffffff;
            game.add.tween(tile).to({ isoZ: 0 }, 200, Phaser.Easing.Quadratic.InOut, true);
        }
    });
}

let sleep = (ms) => new Promise(resolve => setTimeout(resolve, ms));

let world_clear = () => objectGroup.removeAll();

let onDown = false;

let debug = () => onDown==false? onDown=true: reset();

function reset() {
    onDown = false;
    game.debug.reset();
}

function getAcceptableTiles(){
    let acceptTableTiles = [];
    for (let i = 10; i < 21; i++){
        acceptTableTiles.push(i);
    }
    return acceptTableTiles;
}

let gameObjects = [];

function drawTileIso(TileType, i, j){
    let tile;
    tile = game.add.isoSprite(i * tileWidth, j * tileWidth , 0, gameObjects[TileType], 0, objectGroup);
    tile.autoCull = true;
    tile.smoothed = false;
}

function initImages(){
    game.load.spritesheet('bus', 'assets/buss.png', 200, 100);
    for (let i = 1; i < 100; i++){
        if (i >= 5 && i <= 9) gameObjects[i] = undefined;
        else if (i >= 32 && i <= 50) gameObjects[i] = undefined;
        else if (i >= 55) gameObjects[i] = undefined;
        else{
            game.load.image(`${i}`, `assets/${i}.png`);
            gameObjects[i] = i;
        }
        if (i == 99){
            game.load.image(`${i}`, `assets/${i}.png`);
            gameObjects[i] = i;
        } 
    }
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

function dispayMap(){
    OffsetPen = new Phaser.Point(game.world.camera.x - (gameWidth / 2 - 500), game.world.camera.y);
    game.iso.unproject(OffsetPen, OffsetPen);

    OffsetPen.x = Math.round(OffsetPen.x / chunk_size / (tileWidth * 2));
    OffsetPen.y = Math.round(OffsetPen.y / chunk_size / (tileWidth * 2));

    if (tempPoint.x != OffsetPen.x || tempPoint.y != OffsetPen.y){ 
        tempPoint =  OffsetPen;
         setTimeout(drawChunks(tempPoint.x, tempPoint.y), 100);
    }
}

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


let cursorPos = new Phaser.Point(0,0);
let ox = 0;
let oy = 0;

function registerChunk(ox, oy){
    if (chunks[ox][oy] == undefined){
        let chunk = new Array(chunk_size).fill(1).map(() => Array(chunk_size).fill(1));
        chunks[ox][oy] = chunk;
    }
    else return;
}

function loadMap(){
    game_load_all();
    setTimeout( function (){
        drawChunks(0,0);
        createBuses(0,0,3);
    }, 5000);
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

function update_tile(){
    game.iso.unproject(game.input.activePointer.position, cursorPos);
    let pos = new Phaser.Point();
    let buildings = game.add.group();
    objectGroup.forEachAlive(function (tile) {
        let inBounds = tile.isoBounds.containsXY(cursorPos.x, cursorPos.y);
        if (inBounds) {
            pos.x = tile.isoX / tileWidth;
            pos.y = tile.isoY / tileWidth;
            let building = game.add.isoSprite(pos.x * tileWidth, pos.y * tileWidth , 0, 99, 0, buildings);

            ox = Math.floor(pos.x/chunk_size);
            oy = Math.floor(pos.y/chunk_size);
            if (pos.x < chunk_size)  ox = 0; else pos.x -= chunk_size * ox;
            if (pos.y < chunk_size) oy = 0; else pos.y -= chunk_size * oy;
            game.iso.simpleSort(objectGroup);
            setTimeout(function(){ building.destroy(); }, 3000);
        }
    });
    click_level(ox, oy, pos.x, pos.y);
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

function getAllRoads(ox, oy){
    let roads = [];

    for (let i = 0; i < chunk_size; i++){
        for (let j = 0; j < chunk_size; j++){
            if (getAcceptableTiles().includes(chunks[ox][oy][i][j])){
                roads.push(new Phaser.Point(j, i));
            }
        }
    }
    return roads;
}

function getRandomRoad(ox, oy){
    let tmp = getAllRoads(ox, oy);
    return tmp[Math.floor(Math.random() * tmp.length)];
}

function createBuses(ox, oy , count){
    let startPos;
    let finalPos;

    for (let i = 0; i < count; i++){

        startPos = getRandomRoad(ox, oy);
        finalPos = getRandomRoad(ox, oy);

        createBus(ox, oy, startPos.x, startPos.y, finalPos);
    }
}

function createBus(ox ,oy, x, y, finalPos){
    let b = new bus(ox, oy, x, y, finalPos);
    b.draw();
    b.findPath();
}

function decompressor(data){
    let chunks = data.split(';');
    let result = "";
    chunks.forEach(function(chunk){
        let parts = chunk.split(':');
        let content = parts[1].substr(1, parts[1].length - 2);
        
        let ids = content.split(',');

        result += parts[0] + ':' + '[';

        for (let i = 0; i < ids.length; i++)
        {
            let id = ids[i].split('>');

            if (id.length > 1)
            {
                for (let j = 0; j < Number.parseInt(id[1]); j++)
                {
                    result += id[0] + ',';
                }
            }
            else
            {
                result += id[0] + ',';
            }
        }
        result = result.substr(0, result.length - 1);
        result += "];";
    });
    result = result.substr(0, result.length - 1);
    return result;
}