let timeInterval;
let timeLeft;

export function startTimer(seconds, onTimeUp, onTick) {
    console.log("starting timer for", seconds, "seconds");
    clearInterval(timeInterval);
    timeLeft = seconds;
    onTick(timeLeft);

    timeInterval = setInterval(() => {
        timeLeft--;
        onTick(timeLeft);
        if (timeLeft <= 0) {
            clearInterval(timeInterval);
            onTimeUp();
        }
    }, 1000);
    // 1000 == 1s Takt
}

export function stopTimer() {
    clearInterval(timeInterval);
}
