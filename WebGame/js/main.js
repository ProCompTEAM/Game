
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
var wallGraphicHeight=100;
var floorGraphicWidth=200;
var floorGraphicHeight=100;
var heroGraphicWidth=60;
var heroGraphicHeight=120;
var wallHeight=wallGraphicHeight-floorGraphicHeight; 
var heroHeight=(floorGraphicHeight/2)+(heroGraphicHeight-floorGraphicHeight)-20;//adjustments to make the legs hit the middle of the tile for initial load
var heroWidth= (floorGraphicWidth/2)-(heroGraphicWidth/2);//for placing hero at the middle of the tile
var facing='south';//direction the character faces
var sorcerer;//hero
var normText;//text to display hero coordinates
var hero;
var heroMapSprite;//hero marker sprite in the minimap
var gameScene;//this is the render texture onto which we draw depth sorted scene
var floorSprite;
var wallSprite;
var heroMapTile=new Phaser.Point(1,1);//hero tile values in array
var heroMapPos;//2D coordinates of hero map marker sprite in minimap, assume this is mid point of graphic
var heroSpeed=2.5;//well, speed of our hero 
var tapPos=new Phaser.Point(0,0);
var easystar;
var isFindingPath=false;
var path=[];
var destination=heroMapTile;
var stepsTillTurn=20;//20 works best but thats for full frame rate
var stepsTaken=0;
var isWalking;
var halfSpeed=0.8;//changed from 0.5 for smooth diagonal walks
var cursors;


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

function preload() {
        game.load.crossOrigin='Anonymous';
    game.load.image('floor', 'assets/floor.png');
    game.load.atlasJSONArray('hero', 'https://dl.dropboxusercontent.com/s/hradzhl7mok1q25/hero_8_4_41_62.png?dl=0', 'https://dl.dropboxusercontent.com/s/95vb0e8zscc4k54/hero_8_4_41_62.json?dl=0');

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
}

function create() {

    game.world.setBounds(0, -100, 8100, 4100);
    gameScene = game.add.renderTexture(7900, 7900);
    game.stage.backgroundColor = '#b1dcfc';
    //we draw the depth sorted scene into this render texture
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
    floorSprite= game.make.sprite(0, 0, 'floor');

    isWalking=false;
    initGameObjects();
    cursors = game.input.keyboard.createCursorKeys();
    game.camera.focusOnXY(4000, 0);
    addHero();
    game.scale.fullScreenScaleMode = Phaser.ScaleManager.NO_SCALE;
    easystar = new EasyStar.js();
    easystar.setGrid(levelData);
    easystar.setAcceptableTiles([1,2,3,4,5,6,11,12,13,14]);
    //easystar.enableDiagonals();// we want path to have diagonals
    easystar.disableCornerCutting();// no diagonal path when walking at wall corners
    
    game.input.activePointer.leftButton.onUp.add(findPath)

    game_load_all();
}

function update(){
    //follow the path
    aiWalk();
    //if no key is pressed then stop else play walking animation
    if (dY == 0 && dX == 0)
    {
        sorcerer.animations.stop();
        sorcerer.animations.currentAnim.frame=0;
    }else{
        if(sorcerer.animations.currentAnim!=facing){
            sorcerer.animations.play(facing);
        }
    }
    //check if we are walking into a wall else move hero in 2D
    
    heroMapPos.x +=  heroSpeed * dX;
    heroMapPos.y +=  heroSpeed * dY;
    heroMapSprite.x=heroMapPos.x-heroMapSprite.width/2;
    heroMapSprite.y=heroMapPos.y-heroMapSprite.height/2;
    //get the new hero map tile
    heroMapTile=getTileCoordinates(heroMapPos,tileWidth);
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
    //depthsort & draw new scene
    renderScene();
}

function addHero(){
    hero= game.add.group();
    heroMapSprite=hero.create(heroMapTile.y * tileWidth, heroMapTile.x * tileWidth, null);
    heroMapSprite.x+=(tileWidth/2)-(heroMapSprite.width/2);
    heroMapSprite.y+=(tileWidth/2)-(heroMapSprite.height/2);
    heroMapPos=new Phaser.Point(heroMapSprite.x+heroMapSprite.width/2,heroMapSprite.y+heroMapSprite.height/2);
    heroMapTile=getTileCoordinates(heroMapPos,tileWidth);

    sorcerer = game.add.sprite(-50, 0, 'hero', '1.png');
    // animation
    sorcerer.animations.add('southeast', ['1.png','2.png','3.png','4.png'], 6, true);
    sorcerer.animations.add('south', ['5.png','6.png','7.png','8.png'], 6, true);
    sorcerer.animations.add('southwest', ['9.png','10.png','11.png','12.png'], 6, true);
    sorcerer.animations.add('west', ['13.png','14.png','15.png','16.png'], 6, true);
    sorcerer.animations.add('northwest', ['17.png','18.png','19.png','20.png'], 6, true);
    sorcerer.animations.add('north', ['21.png','22.png','23.png','24.png'], 6, true);
    sorcerer.animations.add('northeast', ['25.png','26.png','27.png','28.png'], 6, true);
    sorcerer.animations.add('east', ['29.png','30.png','31.png','32.png'], 6, true);

}

function renderScene(){
    gameScene.clear();//clear the previous frame then draw again
    
    for (var i = 0; i < levelData.length; i++)
    {
        for (var j = 0; j < levelData[0].length; j++)
        {
            tileType=levelData[i][j];
            drawTileIso(tileType,i,j);
            if(i==heroMapTile.y&&j==heroMapTile.x){
                drawHeroIso();
            }
        }
    }
}

function drawHeroIso(){
    var isoPt= new Phaser.Point();
    var heroCornerPt=new Phaser.Point(heroMapPos.x-heroMapSprite.width/2,heroMapPos.y-heroMapSprite.height/2);
    isoPt=cartesianToIsometric(heroCornerPt);
    gameScene.renderXY(sorcerer,isoPt.x+borderOffset.x+heroWidth, isoPt.y+borderOffset.y-heroHeight, false);
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
    gameObjects[7] = floorSprite;//Empty Tile
    gameObjects[8] = floorSprite;//Empty Tile
    gameObjects[9] = floorSprite;//Empty Tile
    gameObjects[10] = floorSprite;//Empty Tile
    gameObjects[11] = race9Sprite;
    gameObjects[12] = race8Sprite;
    gameObjects[13] = race7Sprite;
    gameObjects[14] = race10Sprite;
    gameObjects[15] = floorSprite;// Empty Tile
    gameObjects[16] = floorSprite;// Empty Tile
    gameObjects[17] = floorSprite;// Empty Tile
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
    var isoPt= new Phaser.Point();//It is not advisable to create point in update loop
    var cartPt=new Phaser.Point();//This is here for better code readability.
    cartPt.x=j*tileWidth;
    cartPt.y=i*tileWidth;
    isoPt=cartesianToIsometric(cartPt);
    gameScene.renderXY(gameObjects[tileType], isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
}

function findPath(){
    if(isFindingPath || isWalking)return;
    var pos=game.input.activePointer.position;
    var isoPt= new Phaser.Point(pos.x-borderOffset.x+game.camera.x,pos.y-borderOffset.y+game.camera.y);
    tapPos=isometricToCartesian(isoPt);
    tapPos.x-=tileWidth/2;
    tapPos.y+=tileWidth/2;
    tapPos=getTileCoordinates(tapPos,tileWidth);
    if(tapPos.x>-1&&tapPos.y>-1&&tapPos.x<40&&tapPos.y<40){
        if(levelData[tapPos.y][tapPos.x]!=0 && 
            levelData[tapPos.y][tapPos.x]!=20 &&
            levelData[tapPos.y][tapPos.x]!=21 &&
            levelData[tapPos.y][tapPos.x]!=22 &&
            levelData[tapPos.y][tapPos.x]!=23 &&
            levelData[tapPos.y][tapPos.x]!=24 &&
            levelData[tapPos.y][tapPos.x]!=25 &&
            levelData[tapPos.y][tapPos.x]!=26 &&
            levelData[tapPos.y][tapPos.x]!=27 &&
            levelData[tapPos.y][tapPos.x]!=28 &&
            levelData[tapPos.y][tapPos.x]!=29){
            isFindingPath=true;
            
            easystar.findPath(heroMapTile.x, heroMapTile.y, tapPos.x, tapPos.y, plotAndMove);
            easystar.calculate();

        }
    }
}

function plotAndMove(newPath){
    destination=heroMapTile;
    path=newPath;
    isFindingPath=false;
    if (path === null) {
        console.log("No Path was found.");
    }else{
        path.push(tapPos);
        path.reverse();
        path.pop();
    }
}

function aiWalk(){
    if(path.length==0){//path has ended
        if(heroMapTile.x==destination.x&&heroMapTile.y==destination.y){
            dX=0;
            dY=0;
            //console.log("ret "+destination.x+" ; "+destination.y+"-"+heroMapTile.x+" ; "+heroMapTile.y);
            isWalking=false;
            return;
        }
    }
    isWalking=true;
    if(heroMapTile.x==destination.x&&heroMapTile.y==destination.y){//reached current destination, set new, change direction
        //wait till we are few steps into the tile before we turn
        stepsTaken++;
        if(stepsTaken<stepsTillTurn){
            return;
        }
        console.log("at "+heroMapTile.x+" ; "+heroMapTile.y);
        //centralise the hero on the tile    
        heroMapSprite.x=(heroMapTile.x * tileWidth)+(tileWidth/2)-(heroMapSprite.width/2);
        heroMapSprite.y=(heroMapTile.y * tileWidth)+(tileWidth/2)-(heroMapSprite.height/2);
        heroMapPos.x=heroMapSprite.x+heroMapSprite.width/2;
        heroMapPos.y=heroMapSprite.y+heroMapSprite.height/2;
        
        stepsTaken=0;
        destination=path.pop();//whats next tile in path
        if(heroMapTile.x<destination.x){
            dX = 1;
        }else if(heroMapTile.x>destination.x){
            dX = -1;
        }else {
            dX=0;
        }
        if(heroMapTile.y<destination.y){
            dY = 1;
        }else if(heroMapTile.y>destination.y){
            dY = -1;
        }else {
            dY=0;
        }
        if(heroMapTile.x==destination.x){//top or bottom
            dX=0;
        }else if(heroMapTile.y==destination.y){//left or right
            dY=0;
        }
        //figure out which direction to face
        if (dX==1)
        {
            if (dY == 0)
            {
                facing = "east";
            }
            else if (dY==1)
            {
                facing = "southeast";
                dX = dY=halfSpeed;
            }
            else
            {
                facing = "northeast";
                dX=halfSpeed;
                dY=-1*halfSpeed;
            }
        }
        else if (dX==-1)
        {
            dX = -1;
            if (dY == 0)
            {
                facing = "west";
            }
            else if (dY==1)
            {
                facing = "southwest";
                dY=halfSpeed;
                dX=-1*halfSpeed;
            }
            else
            {
                facing = "northwest";
                dX = dY=-1*halfSpeed;
            }
        }
        else
        {
            dX = 0;
            if (dY == 0)
            {
                //facing="west";
            }
            else if (dY==1)
            {
                facing = "south";
            }
            else
            {
                facing = "north";
            }
        }
    }
    //console.log(facing);
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