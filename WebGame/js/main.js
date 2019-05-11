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

let OffsetPen = new Phaser.Point(0,0);

var game = new Phaser.Game(width, height, Phaser.Auto, 'Game', {

    preload: function (){
        initImages();

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
    	
        loadMap();	
    },

    update: function (){
        dispayMap();
        cameraMove();
        pointerInteraction();
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


