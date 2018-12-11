/* activity uses path finding to lead the character to mouse click position
using https://github.com/prettymuchbryce/easystarjs for pathfinding
*/

var game = new Phaser.Game(1280, 720, Phaser.AUTO, 'TutContainer', { preload: preload, create: create, update:update });

//level array
var levelData=
[[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[0,7,7,7,7,7,7,7,10,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[0,0,0,0,20,0,16,13,8,16,0,0,13,0,0,17,14,0,0,0,0,0,0,0,0,0,0],
[0,0,0,12,7,7,7,7,7,9,7,12,7,7,7,7,7,10,0,0,0,0,0,0,0,0,0],
[0,0,0,8,0,0,0,0,0,0,0,8,0,0,0,0,0,8,0,0,0,0,0,0,0,0,0],
[0,0,3,8,0,0,18,17,16,13,14,8,0,0,0,0,3,8,0,0,0,0,0,0,0,0,0],
[0,0,3,8,0,0,3,12,7,7,7,7,10,0,0,0,3,8,0,0,0,0,0,0,0,0,0],
[0,0,0,8,0,0,0,8,0,0,0,0,8,0,16,16,0,8,0,0,0,0,0,0,0,0,0],
[0,0,0,11,7,7,7,8,0,0,0,0,8,7,7,7,7,10,0,0,0,0,0,0,0,0,0],
[0,0,0,0,0,0,3,8,0,16,20,13,8,0,0,0,0,8,0,0,0,0,0,0,0,0,0],
[0,0,0,0,0,0,0,11,7,7,7,7,9,0,0,0,0,8,0,0,0,0,0,0,0,0,0],
[0,0,0,0,0,0,0,5,0,0,8,0,0,0,0,0,3,8,0,0,0,0,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,0,8,0,0,0,0,0,3,8,0,0,0,0,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,3,8,0,19,0,0,0,0,8,0,0,0,0,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,0,11,7,7,10,0,0,3,8,0,0,0,13,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,0,0,0,0,8,0,19,13,8,0,0,3,8,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,0,0,0,0,11,7,7,7,7,10,0,0,8,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,8,20,0,8,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,11,7,7,9,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0],
[0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0]]

//x & y values of the direction vector for character movement
var dX=0;
var dY=0;
var tileWidth=95;// the width of a tile
var borderOffset = new Phaser.Point(450,50);//to centralise the isometric level display
var wallGraphicHeight=100;
var floorGraphicWidth=200;
var floorGraphicHeight=100;
var heroGraphicWidth=60;
var heroGraphicHeight=120;
var wallHeight=wallGraphicHeight-floorGraphicHeight; 
var heroHeight=(floorGraphicHeight/2)+(heroGraphicHeight-floorGraphicHeight)-12;//adjustments to make the legs hit the middle of the tile for initial load
var heroWidth= (floorGraphicWidth/2)-(heroGraphicWidth/2);//for placing hero at the middle of the tile
var facing='south';//direction the character faces
var sorcerer;//hero
var sorcererShadow;//duh
var shadowOffset=new Phaser.Point(heroWidth+7,11);
var bmpText;//title text
var normText;//text to display hero coordinates
var minimap;//minimap holder group
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

var house1Sprite;
var race2Sprite;
var race3Sprite;
var race4Sprite;
var race5Sprite;
var race6Sprite;
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
    game.load.bitmapFont('font', 'https://dl.dropboxusercontent.com/s/z4riz6hymsiimam/font.png?dl=0', 'https://dl.dropboxusercontent.com/s/7caqsovjw5xelp0/font.xml?dl=0');
    game.load.image('greenTile', 'https://dl.dropboxusercontent.com/s/nxs4ptbuhrgzptx/green_tile.png?dl=0');
    game.load.image('redTile', 'https://dl.dropboxusercontent.com/s/zhk68fq5z0c70db/red_tile.png?dl=0');
    game.load.image('heroTile', 'https://dl.dropboxusercontent.com/s/8b5zkz9nhhx3a2i/hero_tile.png?dl=0');
    game.load.image('heroShadow', 'https://dl.dropboxusercontent.com/s/sq6deec9ddm2635/ball_shadow.png?dl=0');
    game.load.image('floor', 'assets/floor.png');
    game.load.image('wall', 'https://dl.dropboxusercontent.com/s/uhugfdq1xcwbm91/block.png?dl=0');
    game.load.image('ball', 'https://dl.dropboxusercontent.com/s/pf574jtx7tlmkj6/ball.png?dl=0');
    game.load.atlasJSONArray('hero', 'https://dl.dropboxusercontent.com/s/hradzhl7mok1q25/hero_8_4_41_62.png?dl=0', 'https://dl.dropboxusercontent.com/s/95vb0e8zscc4k54/hero_8_4_41_62.json?dl=0');

    game.load.image('race1', 'assets/race1.png');
    game.load.image('race2', 'assets/race2.png');
    game.load.image('race3', 'assets/race3.png');
    game.load.image('race4', 'assets/race4.png');
    game.load.image('race5', 'assets/race5.png');
    game.load.image('race6', 'assets/race6.png');


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
    game.world.setBounds(0, 0, 2000, 2000);
    gameScene = game.add.renderTexture(4000, 4000);
    bmpText = game.add.bitmapText(10, 10, 'font', 'PathFinding\nTap Green Tile', 18);
    normText=game.add.text(50,10,"hi");
    game.stage.backgroundColor = '#cccccc';
    //we draw the depth sorted scene into this render texture
    game.add.sprite(0, 0, gameScene);

    race1Sprite = game.make.sprite(0, 0, 'race1');
    race2Sprite = game.make.sprite(0, 0, 'race2');
    race3Sprite = game.make.sprite(0, 0, 'race3');
    race4Sprite = game.make.sprite(0, 0, 'race4');
    race5Sprite = game.make.sprite(0, 0, 'race5');
    race6Sprite = game.make.sprite(0, 0, 'race6');

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
    wallSprite= game.make.sprite(0, 0, 'wall');
    sorcererShadow=game.make.sprite(0,0,'heroShadow');
    sorcererShadow.scale= new Phaser.Point(0.5,0.6);
    sorcererShadow.alpha=0.4;
    isWalking=false;

    cursors = game.input.keyboard.createCursorKeys();
    game.camera.focusOnXY(700, 900);
    createLevel();
    
    easystar = new EasyStar.js();
    easystar.setGrid(levelData);
    easystar.setAcceptableTiles([7,8,9,10,11,12]);
    //easystar.enableDiagonals();// we want path to have diagonals
    easystar.disableCornerCutting();// no diagonal path when walking at wall corners
    
    game.input.activePointer.leftButton.onUp.add(findPath)
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
        game.camera.y -= 6;
    }
    else if (cursors.down.isDown) {
        game.camera.y += 6;
    }
    if (cursors.left.isDown) {
        game.camera.x -= 6;
    }
    else if (cursors.right.isDown) {
        game.camera.x += 6;
    }
    //depthsort & draw new scene
    renderScene();
}

function createLevel(){//create minimap
    minimap= game.add.group();
    var tileType=0;
    for (var i = 0; i < levelData.length; i++)
    {
        for (var j = 0; j < levelData[0].length; j++)
        {
            tileType=levelData[i][j];
            placeTile(tileType,i,j);
            if(tileType==2){//save hero map tile
               heroMapTile=new Phaser.Point(i,j);
            }
        }
    }
    addHero();
    heroMapSprite=minimap.create(heroMapTile.y * tileWidth, heroMapTile.x * tileWidth, 'heroTile');
    heroMapSprite.x+=(tileWidth/2)-(heroMapSprite.width/2);
    heroMapSprite.y+=(tileWidth/2)-(heroMapSprite.height/2);
    heroMapPos=new Phaser.Point(heroMapSprite.x+heroMapSprite.width/2,heroMapSprite.y+heroMapSprite.height/2);
    heroMapTile=getTileCoordinates(heroMapPos,tileWidth);
    minimap.scale= new Phaser.Point(0,0);
    minimap.x=500;
    minimap.y=10;
    renderScene();//draw once the initial state
}

function addHero(){
    // sprite
    sorcerer = game.add.sprite(-50, 0, 'hero', '1.png');// keep him out side screen area
   
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
function placeTile(tileType,i,j){//place minimap
    var tile='greenTile';
    if(tileType==1){
        tile='redTile';
    }
    var tmpSpr=minimap.create(j * tileWidth, i * tileWidth, tile);
    tmpSpr.name="tile"+i+"_"+j;
}
function renderScene(){
    gameScene.clear();//clear the previous frame then draw again
    var tileType=0;
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
    normText.text='Tap on x,y: '+tapPos.x +','+tapPos.y;
}
function drawHeroIso(){
    var isoPt= new Phaser.Point();//It is not advisable to create points in update loop
    var heroCornerPt=new Phaser.Point(heroMapPos.x-heroMapSprite.width/2,heroMapPos.y-heroMapSprite.height/2);
    isoPt=cartesianToIsometric(heroCornerPt);//find new isometric position for hero from 2D map position
    gameScene.renderXY(sorcererShadow,isoPt.x+borderOffset.x+shadowOffset.x, isoPt.y+borderOffset.y+shadowOffset.y, false);//draw shadow to render texture
    gameScene.renderXY(sorcerer,isoPt.x+borderOffset.x+heroWidth, isoPt.y+borderOffset.y-heroHeight, false);//draw hero to render texture
}
function drawTileIso(tileType,i,j){//place isometric level tiles
    var isoPt= new Phaser.Point();//It is not advisable to create point in update loop
    var cartPt=new Phaser.Point();//This is here for better code readability.
    cartPt.x=j*tileWidth;
    cartPt.y=i*tileWidth;
    isoPt=cartesianToIsometric(cartPt);
    if(tileType==1){
        gameScene.renderXY(wallSprite, isoPt.x+borderOffset.x, isoPt.y+borderOffset.y-wallHeight, false);
    }

    else if(tileType == 7) {
        gameScene.renderXY(race1Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 3) {
        gameScene.renderXY(house1Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 4) {
        gameScene.renderXY(house2Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 8) {
        gameScene.renderXY(race2Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 9) {
        gameScene.renderXY(race3Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 10) {
        gameScene.renderXY(race4Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 11) {
        gameScene.renderXY(race5Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 12) {
        gameScene.renderXY(race6Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 13) {
        gameScene.renderXY(house3Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 14) {
        gameScene.renderXY(house4Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 15) {
        gameScene.renderXY(house5Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 16) {
        gameScene.renderXY(house6Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 17) {
        gameScene.renderXY(house7Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 18) {
        gameScene.renderXY(house8Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 19) {
        gameScene.renderXY(house9Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }

    else if (tileType == 20) {
        gameScene.renderXY(house10Sprite, isoPt.x + borderOffset.x, isoPt.y + borderOffset.y, false);
    }
    else{
        gameScene.renderXY(floorSprite, isoPt.x+borderOffset.x, isoPt.y+borderOffset.y, false);
    }
}

function findPath(){
    if(isFindingPath || isWalking)return;
    var pos=game.input.activePointer.position;
    var isoPt= new Phaser.Point(pos.x-borderOffset.x+game.camera.x,pos.y-borderOffset.y+game.camera.y);
    tapPos=isometricToCartesian(isoPt);
    tapPos.x-=tileWidth/2;//adjustment to find the right tile for error due to rounding off
    tapPos.y+=tileWidth/2;
    tapPos=getTileCoordinates(tapPos,tileWidth);
    if(tapPos.x>-1&&tapPos.y>-1&&tapPos.x<52&&tapPos.y<22){//tapped within grid
        if(levelData[tapPos.y][tapPos.x]!=0){//not wall tile
            isFindingPath=true;
            //let the algorithm do the magic
            easystar.findPath(heroMapTile.x, heroMapTile.y, tapPos.x, tapPos.y, plotAndMove);
            easystar.calculate();
        }
    }
}
function plotAndMove(newPath){
    destination=heroMapTile;
    path=newPath;
    isFindingPath=false;
    repaintMinimap();
    if (path === null) {
        console.log("No Path was found.");
    }else{
        path.push(tapPos);
        path.reverse();
        path.pop();
        for (var i = 0; i < path.length; i++)
        {
            var tmpSpr=minimap.getByName("tile"+path[i].y+"_"+path[i].x);
            tmpSpr.tint=0x0000ff;
            //console.log("p "+path[i].x+":"+path[i].y);
        }
        
    }
}
function repaintMinimap(){
    for (var i = 0; i < levelData.length; i++)
    {
        for (var j = 0; j < levelData[0].length; j++)
        {
            var tmpSpr=minimap.getByName("tile"+i+"_"+j);
            tmpSpr.tint=0xffffff;
        }
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
