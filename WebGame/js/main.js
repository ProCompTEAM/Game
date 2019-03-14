
var winH = document.documentElement.clientHeight;
var winW = document.documentElement.clientWidth;
var game = new Phaser.Game(winW, winH, Phaser.AUTO, 'TutContainer', { preload: preload,   create: create, update:update });

//level array
var levelData=
[[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,]]

//x & y values of the direction vector for character movement
var tileType=0;
var dX=0;
var dY=0;
var tileWidth=95;// the width of a tile
var borderOffset = new Phaser.Point(4000,0);//to centralise the isometric level display
var floorGraphicWidth=200;
var floorGraphicHeight=100;
var heroGraphicWidth=60;
var heroGraphicHeight=120;
var heroHeight=(floorGraphicHeight/2)+(heroGraphicHeight-floorGraphicHeight)-40;//adjustments to make the legs hit the middle of the tile for initial load
var heroWidth= (floorGraphicWidth/2)-(heroGraphicWidth/2);//for placing hero at the middle of the tile
var facing='south';//direction the character faces

var hero;
var heroMapSprite;//hero marker sprite in the minimap
var gameScene;//this is the render texture onto which we draw depth sorted scene

var heroMapTile=new Phaser.Point(0,0);//hero tile values in array
var heroMapPos;//2D coordinates of hero map marker sprite in minimap, assume this is mid point of graphic
var heroSpeed=2.5;//well, speed of our hero 
var tapPos=new Phaser.Point(0,0);
var easystar;
var isFindingPath=false;
var path=[];
var destination=heroMapTile;
var stepsTillTurn=20;//20 works best but thats for full frame rate
var stepsTaken=0;
var cursors;
var bus;
var currentHeroPos = new Phaser.Point(0,0);
var startHeroPos;

var floorSprite;
var race1Sprite;
var race2Sprite;
var race3Sprite;
var race4Sprite;
var race5Sprite;
var race6Sprite;
var race7Sprite;
var race8Sprite;
var race9Spirte;
var race10Sprite;
var race11Sprite;

var house1Sprite;
var house2Sprite;
var house3Sprite;
var house4Sprite;
var house5Sprite
var house6Sprite;
var house7Sprite;
var house8Sprite;
var house9Sprite;
var house10Sprite;

var forest1Sprite;
var forest2Sprite;

var shop1Sprite;
var shop2Sprite;
var shop3Sprite;
var shop4Sprite;

var buildingSprite;

function preload() {
    game.time.advancedTiming = true;
    game.load.crossOrigin='Anonymous';
    game.load.image('floor', 'assets/floor.png');
    game.load.image('hero', 'assets/bus1.png');

    game.load.image('shop1', 'assets/shop1.png');
    game.load.image('shop2', 'assets/shop2.png');
    game.load.image('shop3', 'assets/shop3.png');
    game.load.image('shop4', 'assets/shop4.png');

    game.load.image('race1', 'assets/race1.png');
    game.load.image('race2', 'assets/race2.png');
    game.load.image('race3', 'assets/race3.png');
    game.load.image('race4', 'assets/race4.png');
    game.load.image('race5', 'assets/race5.png');
    game.load.image('race6', 'assets/race6.png');
    game.load.image('race7', 'assets/race7.png');
    game.load.image('race8', 'assets/race8.png');
    game.load.image('race9', 'assets/race9.png');
    game.load.image('race10', 'assets/race10.png');
    game.load.image('race11', 'assets/race11.png');

    game.load.image('house1', 'assets/house1.png');
    game.load.image('house2', 'assets/house2.png');
    game.load.image('house3', 'assets/house3.png');
    game.load.image('house4', 'assets/house4.png');
    game.load.image('house5', 'assets/house5.png');
    game.load.image('house6', 'assets/house6.png');
    game.load.image('house7', 'assets/house7.png');
    game.load.image('house8', 'assets/house8.png');
    game.load.image('house9', 'assets/house9.png');
    game.load.image('house10', 'assets/house10.png');

    game.load.image('forest1', 'assets/forest1.png');
    game.load.image('forest2', 'assets/forest2.png');
	
	game.load.image('building', 'assets/building.png');
}

function create() {
    game.world.setBounds(0, -100, 8100, 4100);
    gameScene = game.add.renderTexture(7900, 7900);
    game.stage.backgroundColor = '#b1dcfc';
    game.add.sprite(0, 0, gameScene);

    race1Sprite = game.make.sprite(0, 0, 'race1');
    race2Sprite = game.make.sprite(0, 0, 'race2');
    race3Sprite = game.make.sprite(0, 0, 'race3');
    race4Sprite = game.make.sprite(0, 0, 'race4');
    race5Sprite = game.make.sprite(0, 0, 'race5');
    race6Sprite = game.make.sprite(0, 0, 'race6');
    race7Sprite = game.make.sprite(0, 0, 'race7');
    race8Sprite = game.make.sprite(0, 0, 'race8');
    race9Sprite = game.make.sprite(0, 0, 'race9');
    race10Sprite = game.make.sprite(0, 0, 'race10');
    race11Sprite = game.make.sprite(0, 0, 'race11');

    house1Sprite = game.make.sprite(0, 0, 'house1');
    house2Sprite = game.make.sprite(0, 0, 'house2');
    house3Sprite = game.make.sprite(0, 0, 'house3');
    house4Sprite = game.make.sprite(0, 0, 'house4');
    house5Sprite = game.make.sprite(0, 0, 'house5');
    house6Sprite = game.make.sprite(0, 0, 'house6');
    house7Sprite = game.make.sprite(0, 0, 'house7');
    house8Sprite = game.make.sprite(0, 0, 'house8');
    house9Sprite = game.make.sprite(0, 0, 'house9');
    house10Sprite = game.make.sprite(0, 0, 'house10');

    forest1Sprite = game.make.sprite(0, 0, 'forest1');
    forest2Sprite = game.make.sprite(0, 0, 'forest2');

    shop1Sprite = game.make.sprite(0, 0, 'shop1');
    shop2Sprite = game.make.sprite(0, 0, 'shop2');
    shop3Sprite = game.make.sprite(0, 0, 'shop3');
    shop4Sprite = game.make.sprite(0, 0, 'shop4');

    floorSprite= game.make.sprite(0, 0, 'floor');
	
	buildingSprite= game.make.sprite(0, 0, 'building');

    initGameObjects();
    cursors = game.input.keyboard.createCursorKeys();
    game.camera.focusOnXY(4000, 0);
    game.scale.fullScreenScaleMode = Phaser.ScaleManager.NO_SCALE;
    easystar = new EasyStar.js();
    easystar.setGrid(levelData);
    easystar.setAcceptableTiles([1,2,3,4,5,6,11,12,13,14,15]);
    easystar.setIterationsPerCalculation(1000);
    easystar.disableCornerCutting();

	game.input.activePointer.leftButton.onUp.add(clicker);
    game.input.activePointer.leftButton.onUp.add(gameResume);


	game_load_all();
    renderScene();
    addHero();
    findPath();
}

function update(){
    aiWalk();
    cameraMove();
    heroMapPos.x +=  heroSpeed * dX;
    heroMapPos.y +=  heroSpeed * dY;
    heroMapSprite.x=heroMapPos.x-heroMapSprite.width/2;
    heroMapSprite.y=heroMapPos.y-heroMapSprite.height/2;
    //get the new hero map tile
    heroMapTile=getTileCoordinates(heroMapPos,tileWidth);
    drawHeroIso();
    game.debug.text('FPS: ' + game.time.fps || 'FPS: --', 40, 80, "#00ff00");
}

function clicker(obj)
{	
	var gPos= new Phaser.Point();
	var pos=game.input.activePointer.position;
    var isoPt= new Phaser.Point(pos.x-borderOffset.x+game.camera.x,pos.y-borderOffset.y+game.camera.y);
    gPos=isometricToCartesian(isoPt);
    gPos.x-=tileWidth/2;
    gPos.y+=tileWidth/2;
	gPos=getTileCoordinates(gPos,tileWidth);
	
	drawBuilding(gPos.y, gPos.x);
	
	click_level(gPos.x, gPos.y, levelData[gPos.y][gPos.x]);
}

function gameResume(){
    game.paused = false;
}

function cameraMove(){
    if (cursors.up.isDown) {
        game.camera.y -= 20;
    }
    else if (cursors.down.isDown) {
        game.camera.y += 20;
    }
    if (cursors.left.isDown) {
        game.camera.x -= 20;
    }
    else if (cursors.right.isDown) {
        game.camera.x += 20;
    }
}

function addHero(){
    hero= game.add.group();
    heroMapSprite=hero.create(heroMapTile.x * tileWidth, heroMapTile.y * tileWidth, null);
    heroMapSprite.x+=(tileWidth/2)-(heroMapSprite.width/2);
    heroMapSprite.y+=(tileWidth/2)-(heroMapSprite.height/2);
    heroMapPos=new Phaser.Point(heroMapSprite.x+heroMapSprite.width/2,heroMapSprite.y+heroMapSprite.height/2);
    heroMapTile=getTileCoordinates(heroMapPos,tileWidth);
    var isoPt= new Phaser.Point();
    var heroCornerPt=new Phaser.Point(heroMapPos.x-heroMapSprite.width/2,heroMapPos.y-heroMapSprite.height/2);
    isoPt=cartesianToIsometric(heroCornerPt);
    bus = game.add.sprite(isoPt.x+borderOffset.x+heroWidth, isoPt.y+borderOffset.y-heroHeight, 'hero');
}

function renderScene(){
    gameScene.clear();
    
    for (var i = 0; i < 40; i++)
    {
        for (var j = 0; j < 40; j++)
        {
            tileType=levelData[i][j];
            drawTileIso(tileType,i,j);
			
            if (tileType == 1){
                tapPos.x = j;
                tapPos.y = i;
				//alert(i + " " + j);
            }
            if (tileType == 2){
                heroMapTile.x = j;
                heroMapTile.y = i;
                startHeroPos = heroMapTile;
				//alert(i + " " + j);
            }

        }
    }
}

function drawHeroIso(){
    bus.destroy();
    var isoPt= new Phaser.Point();
    var heroCornerPt=new Phaser.Point(heroMapPos.x-heroMapSprite.width/2,heroMapPos.y-heroMapSprite.height/2);
    isoPt=cartesianToIsometric(heroCornerPt);
    bus = game.add.sprite(isoPt.x+borderOffset.x+heroWidth, isoPt.y+borderOffset.y-heroHeight, 'hero');
}

function drawTile(id, x, y)
{
    levelData[x][y] = id;
    drawTileIso(id, x, y);
}

var gameObjects = [];

function initGameObjects()
{
    gameObjects[0] = floorSprite;
    gameObjects[1] = race2Sprite;
    gameObjects[2] = race1Sprite;
    gameObjects[3] = race6Sprite;
    gameObjects[4] = race4Sprite;
    gameObjects[5] = race5Sprite;
    gameObjects[6] = race3Sprite;
    gameObjects[7] = shop1Sprite;
    gameObjects[8] = shop2Sprite;
    gameObjects[9] = shop3Sprite;
    gameObjects[10] = shop4Sprite;
    gameObjects[11] = race9Sprite;
    gameObjects[12] = race8Sprite;
    gameObjects[13] = race7Sprite;
    gameObjects[14] = race10Sprite;
    gameObjects[15] = race11Sprite;
    gameObjects[16] = forest1Sprite;
    gameObjects[17] = forest2Sprite;
    gameObjects[18] = floorSprite;// Empty Tile
    gameObjects[19] = floorSprite;// Empty Tile
    gameObjects[20] = house1Sprite;
    gameObjects[21] = house2Sprite;
    gameObjects[22] = house2Sprite;
    gameObjects[23] = house3Sprite;
    gameObjects[24] = house4Sprite;
    gameObjects[25] = house5Sprite;
    gameObjects[26] = house6Sprite;
    gameObjects[27] = house7Sprite;
    gameObjects[28] = house8Sprite;
    gameObjects[29] = house9Sprite;
    gameObjects[30] = house10Sprite;
}

function drawTileIso(tileType,i,j){//place isometric level tiles
    var isoPt= new Phaser.Point();
    var cartPt=new Phaser.Point();
    cartPt.x=j*tileWidth;
    cartPt.y=i*tileWidth;
    isoPt=cartesianToIsometric(cartPt);
    gameScene.renderXY(gameObjects[tileType], isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
}

function drawBuilding(i, j)
{
	var isoPt= new Phaser.Point();
    var cartPt=new Phaser.Point();
    cartPt.x=j*tileWidth;
    cartPt.y=i*tileWidth;
    isoPt=cartesianToIsometric(cartPt);
    gameScene.renderXY(buildingSprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
}

function findPath(){
    if(tapPos.x>-1&&tapPos.y>-1&&tapPos.x<40&&tapPos.y<40){            
        easystar.findPath(heroMapTile.x, heroMapTile.y, tapPos.x, tapPos.y, plotAndMove);
        easystar.calculate();
    }
}

function plotAndMove(newPath){
    destination=heroMapTile;
    path=newPath;
    if (path === null) {
        console.log("No Path was found.");
    }else{
        path.push(tapPos);
        path.reverse();
        path.pop();
    }
}

function aiWalk(){

    if(path != null && path.length==0){//path has ended
        if(heroMapTile.x==destination.x&&heroMapTile.y==destination.y){
            dX=0;
            dY=0;
            return;
        }
    }
    if(path != null && heroMapTile.x==destination.x && heroMapTile.y==destination.y){//reached current destination, set new, change direction
        //wait till we are few steps into the tile before we turn
        stepsTaken++;
        if(stepsTaken<stepsTillTurn){
            return;
        }
        //centralise the hero on the tile    
        heroMapSprite.x=(heroMapTile.x * tileWidth)+(tileWidth/2)-(heroMapSprite.width/2);
        heroMapSprite.y=(heroMapTile.y * tileWidth)+(tileWidth/2)-(heroMapSprite.height/2);
        heroMapPos.x=heroMapSprite.x+heroMapSprite.width/2;
        heroMapPos.y=heroMapSprite.y+heroMapSprite.height/2;
        
        stepsTaken=0;
        destination=path.pop();//whats next tile in path
        if (heroMapTile.x == destination.x && heroMapTile.y < destination.y){
            dX = 0;
            dY = 1;
            facing = "N";
        }
        else if (heroMapTile.x < destination.x && heroMapTile.y == destination.y ){
            dX = 1;
            dY =0;
            facing = "E"
        }
        else if (heroMapTile.x > destination.x && heroMapTile.y == destination.y){
            dX = -1;
            dY = 0;
            facing = "W"
        }
        else if (heroMapTile.x == destination.x && heroMapTile.y > destination.y){
            dX = 0;
            dY = -1;
            facing = "S";
        }
        else {
            dX = 0;
            dY = 0;
            facing = "STOP";
            isWalking = false;
            currentHeroPos.x = heroMapTile.x;
            currentHeroPos.y = heroMapTile.y;
            easystar.findPath(currentHeroPos.x, currentHeroPos.y, startHeroPos.x, startHeroPos.y, plotAndMove);
            easystar.calculate();
            if (currentHeroPos.x == startHeroPos.x && currentHeroPos.y == startHeroPos.y)
            {
                easystar.findPath(currentHeroPos.x, currentHeroPos.y, tapPos.x, tapPos.y, plotAndMove);
                easystar.calculate();
            }
        }
    }
}

function cartesianToIsometric(cartPt){
    var tempPt=new Phaser.Point();
    tempPt.x=cartPt.x-cartPt.y;
    tempPt.y=(cartPt.x+cartPt.y)/2;
    return (tempPt);
}
function isometricToCartesian(isoPt){
    var tempPt=new Phaser.Point();
    tempPt.x=(2*isoPt.y+isoPt.x)/2;
    tempPt.y=(2*isoPt.y-isoPt.x)/2;
    return (tempPt);
}
function getTileCoordinates(cartPt, tileHeight){
    var tempPt=new Phaser.Point();
    tempPt.x=Math.floor(cartPt.x/tileHeight);
    tempPt.y=Math.floor(cartPt.y/tileHeight);
    return(tempPt);
}
function getCartesianFromTileCoordinates(tilePt, tileHeight){
    var tempPt=new Phaser.Point();
    tempPt.x=tilePt.x*tileHeight;
    tempPt.y=tilePt.y*tileHeight;
    return(tempPt);
}