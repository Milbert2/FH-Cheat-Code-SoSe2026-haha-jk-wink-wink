// ============================================================
// index.js
// ============================================================


// ============================================================
// WARTEN BIS HTML GELADEN IST
// ============================================================

// DOMContentLoaded bedeutet:
// Der Browser wartet, bis das komplette HTML-Dokument
// geladen und aufgebaut wurde.
//
// Erst danach greifen wir auf HTML-Elemente zu.
// Das ist wichtig, damit z.B. #cards-wrapper bereits existiert.

document.addEventListener("DOMContentLoaded", () => {

    // ========================================================
    // SCORES LADEN
    // ========================================================

    // Beim Start der Seite werden die Scores geladen.
    //
    // "points" wird als Standard-Sortierung übergeben.
    //
    // Dadurch wird später:
    // list.php?orderby=points
    // aufgerufen.

    loadScores("points");


    // ========================================================
    // RADIO BUTTONS
    // ========================================================

    // querySelectorAll() sucht ALLE HTML-Elemente,
    // die zum angegebenen CSS-Selektor passen.
    //
    // Hier:
    // input[name="orderby"]
    //
    // Das sind die drei Radio Buttons:
    //
    // Sort by Points
    // Sort by Date
    // Sort by Player

    document
        .querySelectorAll('input[name="orderby"]')
        .forEach(radio => {

            // Für jeden Radio Button wird ein Event Listener
            // registriert.
            //
            // "change" wird ausgelöst, wenn der Benutzer
            // einen anderen Radio Button auswählt.

            radio.addEventListener("change", () => {

                // radio.value enthält den Wert des
                // angeklickten Radio Buttons.
                //
                // Beispiel:
                // value="points" → "points"
                // value="date"   → "date"
                // value="player" → "player"
                //
                // Danach werden die Scores mit dieser
                // Sortierung erneut geladen.

                loadScores(radio.value);

            });

        });

});


// ============================================================
// SCORES LADEN
// ============================================================

// async bedeutet:
// Die Funktion arbeitet asynchron.
//
// Dadurch können wir innerhalb der Funktion "await"
// verwenden.
//
// await wartet auf das Ergebnis eines Promise,
// ohne dass der restliche JavaScript-Code blockiert wird.

async function loadScores(orderby) {

    // try/catch wird für Fehlerbehandlung verwendet.
    //
    // Falls z.B. die PHP-Datei nicht erreichbar ist
    // oder JSON nicht korrekt zurückkommt,
    // landen wir im catch-Block.

    try {

        // ====================================================
        // FETCH
        // ====================================================

        // fetch() macht einen HTTP-Request.
        //
        // Hier wird list.php aufgerufen.
        //
        // Der GET-Parameter orderby wird an die URL angehängt.
        //
        // Beispiel:
        //
        // list.php?orderby=points
        //
        // oder:
        //
        // list.php?orderby=date
        //
        // oder:
        //
        // list.php?orderby=player

        const response = await fetch(
            "list.php?orderby=" + orderby
        );


        // ====================================================
        // JSON AUSLESEN
        // ====================================================

        // response.json() liest die Antwort des Servers
        // und wandelt das JSON in JavaScript-Daten um.
        //
        // Das Ergebnis wird in "scores" gespeichert.

        const scores = await response.json();


        // ====================================================
        // DATEN ANZEIGEN
        // ====================================================

        // Die geladenen Scores werden an die Funktion
        // showScores() weitergegeben.

        showScores(scores);

    } catch (error) {

        // Wenn irgendwo im try-Block ein Fehler passiert,
        // wird dieser hier aufgefangen.
        //
        // console.log() gibt den Fehler in der
        // Browser-Konsole aus.

        console.log(error);

    }

}


// ============================================================
// SCORES ANZEIGEN
// ============================================================

// Diese Funktion bekommt das Array "scores".
//
// Beispiel:
//
// scores = [
//     {
//         id: 1,
//         player: "Max",
//         points: 100,
//         played_at: "2025-05-01",
//         avatar: "avatar.png"
//     }
// ]

function showScores(scores) {

    // ========================================================
    // CONTAINER FINDEN
    // ========================================================

    // querySelector() sucht ein einzelnes HTML-Element.
    //
    // "#cards-wrapper" bedeutet:
    // Suche das Element mit der ID "cards-wrapper".
    //
    // Dieses Element befindet sich in index.html.

    const wrapper =
        document.querySelector("#cards-wrapper");


    // ========================================================
    // ALTE LISTE LÖSCHEN
    // ========================================================

    // innerHTML = "" entfernt den bisherigen Inhalt.
    //
    // Das ist wichtig beim Sortieren:
    //
    // Alte Cards werden entfernt
    // und danach werden die Cards mit der neuen
    // Sortierung wieder erzeugt.

    wrapper.innerHTML = "";


    // ========================================================
    // JEDEN SCORE ANZEIGEN
    // ========================================================

    // scores ist ein Array.
    //
    // forEach() geht jedes Element des Arrays durch.
    //
    // "score" ist jeweils EIN Eintrag aus dem Array.

    scores.forEach(score => {

        // ====================================================
        // NEUES HTML-ELEMENT ERSTELLEN
        // ====================================================

        // createElement() erstellt ein neues HTML-Element.
        //
        // Hier erstellen wir:
        //
        // <div></div>

        const card =
            document.createElement("div");


        // ====================================================
        // CSS-KLASSE HINZUFÜGEN
        // ====================================================

        // classList.add() fügt dem Element eine CSS-Klasse hinzu.
        //
        // Ergebnis:
        //
        // <div class="card">

        card.classList.add("card");


        // ====================================================
        // INHALT DER CARD
        // ====================================================

        // innerHTML erlaubt es uns, HTML direkt
        // in die Card einzufügen.
        //
        // ${score.id}
        // ${score.avatar}
        // usw.
        //
        // werden durch die Werte des aktuellen Scores ersetzt.

       card.innerHTML = `

    <!-- ID des Scores -->
    <span>${score.id}</span>


    <!-- Avatar -->
    <img
        src="${score.avatar}"
        alt="${score.player}"
    >


    <!-- Spielername -->
    <span>${score.player}</span>


    <!-- Punkte -->
    <span>${score.points}</span>


    <!-- Datum -->
    <span>${score.played_at}</span>


    <!-- EDIT -->
    <a href="edit.php?id=${score.id}">
        Edit
    </a>


    <!-- DELETE -->
    <a href="delete.php?id=${score.id}">
        Delete
    </a>

`;


        // ====================================================
        // CARD IN DEN CONTAINER EINHÄNGEN
        // ====================================================

        // appendChild() hängt die neu erstellte Card
        // an den wrapper an.

        wrapper.appendChild(card);

    });


}