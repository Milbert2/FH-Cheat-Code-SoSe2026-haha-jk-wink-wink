<?php

// ============================================================
// add.php
// ============================================================
//
// Wird vom Formular in index.html aufgerufen.
//
// POST
// -> INSERT
//
// Danach zurück zu index.html.
//
// ============================================================


// ============================================================
// DATENBANKVERBINDUNG
// ============================================================

require "functions.php";


// ============================================================
// NUR POST ERLAUBEN
// ============================================================

// $_SERVER["REQUEST_METHOD"] sagt,
// mit welcher HTTP-Methode die Seite aufgerufen wurde.
//
// Das Formular verwendet:
//
// method="post"
//
// Deshalb wird der INSERT nur bei POST ausgeführt.

if ($_SERVER["REQUEST_METHOD"] === "POST") {


    // ========================================================
    // INSERT VORBEREITEN
    // ========================================================

    // prepare() bereitet das SQL-Statement vor.
//
// Die ? sind Platzhalter.
//
// Dadurch werden die Werte später sicher eingesetzt.
//
// Das schützt vor SQL-Injection.

    $stmt = $dbh->prepare(
        "INSERT INTO scores
        (player, points, played_at, avatar)
        VALUES (?, ?, ?, ?)"
    );


    // ========================================================
    // WERTE EINSETZEN
    // ========================================================

    // execute() führt das vorbereitete SQL aus.
//
// Die Werte werden in derselben Reihenfolge
// wie die ? eingesetzt.
//
// ? 1 → player
// ? 2 → points
// ? 3 → played_at
// ? 4 → avatar

    $stmt->execute([

        $_POST["player"],

        $_POST["points"],

        $_POST["played_at"],

        $_POST["avatar"]

    ]);
}


// ============================================================
// ZURÜCK ZUR LISTE
// ============================================================

// Nach dem INSERT wird der Benutzer zurück
// auf index.html geschickt.

header("Location: index.html");


// Wichtig:
// Nach header() sollte die PHP-Ausführung beendet werden.
//
// Sonst würde PHP noch weiteren Code ausführen.

exit;

?>