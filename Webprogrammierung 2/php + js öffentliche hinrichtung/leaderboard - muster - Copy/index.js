import { getScores } from "./getScores.js";
import { Leaderboard } from "./Leaderboard.js";


document.addEventListener("DOMContentLoaded", async () => {

    const wrapper =
        document.querySelector("#cards-wrapper");

    const scores =
        await getScores("points");

    new Leaderboard(scores, wrapper);


    document
        .querySelectorAll('input[name="orderby"]')
        .forEach(radio => {

            radio.addEventListener("change", async () => {

                const scores =
                    await getScores(radio.value);

                new Leaderboard(scores, wrapper);

            });

        });

});