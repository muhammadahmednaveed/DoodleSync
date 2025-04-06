let context;
let canvas;

export function initCanvas(canvasElement) {
    canvas = canvasElement;
    context = canvas.getContext('2d');
    
    // Set canvas size to match container
    resizeCanvas();
    window.addEventListener('resize', resizeCanvas);

    // Set drawing style
    context.strokeStyle = '#000000';
    context.lineWidth = 2;
    context.lineCap = 'round';
    context.lineJoin = 'round';
}

function resizeCanvas() {
    const container = canvas.parentElement;
    canvas.width = container.clientWidth;
    canvas.height = window.innerHeight * 0.7;
}

export function startDrawing(x, y) {
    context.beginPath();
    context.moveTo(x, y);
}

export function draw(x, y) {
    context.lineTo(x, y);
    context.stroke();
}

export function stopDrawing() {
    context.closePath();
}

export function clearCanvas() {
    context.clearRect(0, 0, canvas.width, canvas.height);
}
