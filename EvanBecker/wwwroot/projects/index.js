var ct = 0;
var rubiks;

function windowResized() {
    resizeCanvas(windowWidth, windowHeight);
}

function setup() {
    CreateCanvas();
    rubiks = new Rubiks(75);
}

function draw() {
    background(166);
    tint(255, 127);
    //pointLight(0,0,255, 0, -500, 0)
    //ambientLight(255);
    
    rubiks.DrawUpdate();
}

function mouseDragged() {
    if (rubiks != null)
        rubiks.MouseDragged();
}

function mouseReleased() {
    if (rubiks != null)
        rubiks.MouseReleased();
}

// Helper
function CreateCanvas() {
    if (ct++ == 1)
        canv.remove(); // workaround for double setup call
    canv = createCanvas(windowWidth, windowHeight / 2, WEBGL);
    canv.parent('sketch-holder');
    //canv.style('z-index', '-1');
    //canv.position(windowWidth, windowHeight);
}

class Rubiks {
    constructor() {
        this._isDragging = false;
        this._rotateTick = 0;
        this._size = 75;
        this._paddedSize = this._size-0.5;
        this._iterations = 3;
    }

    DrawUpdate() {
        this.IdleRotate();

        if(second()%3==0){
            for(let i = -1; i < this._iterations-1; i++){
                for(let j = -1; j < this._iterations-1; j++){
                    for(let k = -1; k < this._iterations-1; k++){
                        this.DrawBox(this.BuildPoint(i, j, k, this._size), this._paddedSize);
                    }
                }
                if (i==0)
                    rotateX(-this._rotateTick * 0.05);
            }
        }

        else if(second()%3==1){
            for(let i = -1; i < this._iterations-1; i++){
                for(let j = -1; j < this._iterations-1; j++){
                    for(let k = -1; k < this._iterations-1; k++){
                        this.DrawBox(this.BuildPoint(k, j, i, this._size), this._paddedSize);
                    }
                }
                if (i==0)
                    rotateZ(-this._rotateTick * 0.05);
            }
        }

        else if(second()%3==2){
            for(let i = -1; i < this._iterations-1; i++){
                for(let j = -1; j < this._iterations-1; j++){
                    for(let k = -1; k < this._iterations-1; k++){
                        this.DrawBox(this.BuildPoint(j, i, k, this._size), this._paddedSize);
                    }
                }
                if (i==0)
                    rotateY(-this._rotateTick * 0.05);
            }
        }
    }

    DrawBox(pos, size) {
        push();
            translate(pos.x, pos.y, pos.z);
            //noFill();
            //ambientMaterial(255)
             this.CreateBox(size);
        pop();
    }

    IdleRotate() {
        if (!this._isDragging)
            this._rotateTick++;
        rotateX(this._rotateTick * 0.002);
        rotateY(this._rotateTick * 0.002);
        rotateZ(this._rotateTick * 0.002);
    }

    BuildPoint(i, j, k, grid=1) {
        return { x: (i * grid), y: (j * grid), z: (k * grid) };
    }

    MouseDragged() {
        this._isDragging = true;
    }

    MouseReleased() {
        this._isDragging = false;
    }

    CreateBox(size) {
        push();
            //scale(size)
            translate(-size/2,-size/2,-size/2);
        
            push();
                beginShape(); //bottom
                fill("green");
                vertex(0, 0, 0);
                vertex(0, size, 0);
                vertex(size, size, 0);
                vertex(size, 0, 0);
                endShape(CLOSE);

                translate(0,0,size);

                beginShape();
                fill("yellow"); // top
                vertex(0, 0, 0);
                vertex(0, size, 0);
                vertex(size, size, 0);
                vertex(size, 0, 0);
                endShape(CLOSE);
            pop();

            push();
                rotateX(Math.PI/2);
                beginShape();
                fill("blue"); // left
                vertex(0, 0, 0);
                vertex(0, size, 0);
                vertex(size, size, 0);
                vertex(size, 0, 0);
                endShape(CLOSE);

                translate(0,0,-size)

                beginShape();
                fill("white"); // right
                vertex(0, 0, 0);
                vertex(0, size, 0);
                vertex(size, size, 0);
                vertex(size, 0, 0);
                endShape(CLOSE);
            pop();

            push();
                rotateY(-Math.PI/2);
                beginShape();
                fill("red"); // back
                vertex(0, 0, 0);
                vertex(0, size, 0);
                vertex(size, size, 0);
                vertex(size, 0, 0);
                endShape(CLOSE);

                translate(0,0,-size)

                beginShape(); // front
                fill("orange");
                vertex(0, 0, 0);
                vertex(0, size, 0);
                vertex(size, size, 0);
                vertex(size, 0, 0);
                endShape(CLOSE);
            pop();
        pop()
    }
}