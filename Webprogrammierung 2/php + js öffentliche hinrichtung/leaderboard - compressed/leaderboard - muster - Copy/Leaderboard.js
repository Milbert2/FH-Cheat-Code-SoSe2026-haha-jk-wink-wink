export class Leaderboard {

    constructor(scores, parentElement) {

        this.scores = scores;
        this.parentElement = parentElement;
        this.displayScores();
    }

    displayScores() {

        this.parentElement.innerHTML = "";
        this.scores.forEach(score => {

            const card = document.createElement("div");
            card.classList.add("card");

            card.innerHTML = `
                <span>${score.id}</span>
                <img src="${score.avatar}" alt="${score.player}">
                <span>${score.player}</span>
                <span>${score.points}</span>
                <span>${score.played_at}</span>
                <a href="edit.php?id=${score.id}">Edit</a>
                <a href="delete.php?id=${score.id}">Delete</a>
            `;

            this.parentElement.appendChild(card);

        });
    }
}