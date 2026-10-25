
import { startTimer, stopTimer } from './timer.js';

const QuizQuestionTime = 8*3;


const QuizState = Object.freeze({
    BEFORE: 'before start',
    ASK: 'ask',
    ANSWERED: 'answered',
    DONE: 'done'
});

export class GameApp {

    constructor(setup, parent) {
        this.grid = setup.grid;
        this.questions = setup.questions;
        this.totalQuestions = this.questions.length;
        this.state = QuizState.BEFORE;

        this.gridWrapper = parent.querySelector(".grid");
        this.questionWrapper = parent.querySelector(".question");
        this.scoreDisplay = parent.querySelector(".score");
        this.timerElement = parent.querySelector(".timer");

        if (!this.gridWrapper || !this.questionWrapper || !this.scoreDisplay || !this.timerElement) {
            throw new Error(`Missing required elements in${parent}`);
        }

        this.startButton = parent.querySelector(".start-btn");
        if (!this.startButton) {
            console.error("Missing start button in", parent);
            return;
        }
        this.startButton.addEventListener("click", () => {
            this.startGame();
        });

        this.initGrid();
    }

    initGrid() {
        for (const item of this.grid) {
        const div = document.createElement("div");
        div.classList.add("show");

            const img = document.createElement("img");
            img.src = item.image;

            const button = document.createElement("button");
            button.dataset.name = item.name;

            button.addEventListener("click", (event) => {
                this.handleClick(event, item);
            });

            div.appendChild(img);
            div.appendChild(button);

            this.gridWrapper.appendChild(div);
        }
    }

    startGame() {
        this.currentQuestionIndex = 0;
        this.score = [];
        this.state = QuizState.ASK;
        this.displayGrid(false);
        this.showQuestion();

        startTimer(
            QuizQuestionTime,
            () => this.endGame("Time is up!"),
            (seconds) => {
                this.timerElement.textContent = seconds + " seconds";
            }
        );

        this.startButton.style.display="none";
    }

    endGame(message) {
        stopTimer();
        this.state = QuizState.DONE;
        this.questionWrapper.textContent = message;
    }

    displayGrid(visible) {
        for( const child of this.gridWrapper.children ) {
            if (visible) {
                child.classList.add("show");
            } else {
                child.classList.remove("show");
            }
        }
    }

    showQuestion() {
        const question = this.questions[this.currentQuestionIndex];

        this.questionWrapper.innerHTML = "";

        const item = this.grid.find(item => item.name === question);

        const img = document.createElement("img");
        img.src = item.image;
        img.alt = item.name;

        this.questionWrapper.appendChild(img);
    }

    handleClick(event, item) {
        const grid_item = event.currentTarget.parentElement;
        console.log("clicked on", item.name, "in state", this.state," clicked on", grid_item);

        if( this.state !== QuizState.ASK) {
            console.log("ignoring click because we are not in ASK state");
            return;
        }

        this.state = QuizState.ANSWERED;
        grid_item.classList.add("show");


        if (item.name === this.questions[this.currentQuestionIndex]) {
        this.score.push(1);
        } else {
            this.score.push(-2);
        }

        this.displayScore();

        if( this.currentQuestionIndex < this.totalQuestions-1) {
            setTimeout(() => grid_item.classList.remove('show'), 500);
            setTimeout(() => this.nextQuestion(), 550);
        } else {
            this.timerElement.textContent = "you made it in time!";
            this.endGame("Game over!");
        }
    }

    displayScore() {
        let total = 0;
        let result = "";

        for (const points of this.score) {
            total += points;

            if (points === 1) {
                result += "✓ ";
            } else {
                result += "X ";
            }
        }

        this.scoreDisplay.textContent = result + " " + total;
    }

    nextQuestion() {
        this.currentQuestionIndex++;
        this.state = QuizState.ASK;
        this.showQuestion();
    }




}
