// ============================================================
// index.js
// ============================================================


// Warten bis HTML geladen ist
document.addEventListener("DOMContentLoaded", () => {

    // Scores laden
    loadScores("points");


    // Radio Buttons
    document
        .querySelectorAll('input[name="orderby"]')
        .forEach(radio => {

            radio.addEventListener("change", () => {

                loadScores(radio.value);

            });

        });

});


// ============================================================
// SCORES LADEN
// ============================================================

async function loadScores(orderby) {

    try {

        const response = await fetch(
            "list.php?orderby=" + orderby
        );

        const scores = await response.json();

        showScores(scores);

    } catch (error) {

        console.log(error);

    }

}


// ============================================================
// SCORES ANZEIGEN
// ============================================================

function showScores(scores) {

    const wrapper =
        document.querySelector("#cards-wrapper");


    // Alte Liste löschen
    wrapper.innerHTML = "";


    // Jeden Score anzeigen
    scores.forEach(score => {

        const card =
            document.createElement("div");


        card.classList.add("card");


        card.innerHTML = `

            <span>${score.id}</span>

            <img
                src="${score.avatar}"
                alt="${score.player}"
            >

            <span>${score.player}</span>

            <span>${score.points}</span>

            <span>${score.played_at}</span>


            <!-- ==========================================
                 EDIT
                 ========================================== -->

            <a href="edit.php?id=${score.id}">
                Edit
            </a>


            <!-- ==========================================
                 DELETE
                 ========================================== -->

            <a
                href="delete.php?id=${score.id}"

            >
                Delete
            </a>

        `;


        wrapper.appendChild(card);

    });

}