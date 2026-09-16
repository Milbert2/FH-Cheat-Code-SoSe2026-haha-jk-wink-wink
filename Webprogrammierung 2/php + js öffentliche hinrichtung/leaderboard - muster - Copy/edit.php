<?php

// ============================================================
// edit.php
// ============================================================
//
// GET:
//
//     edit.php?id=5
//
// -> Score laden
// -> Formular anzeigen
//
// POST:
//
// -> Score aktualisieren
// -> zurück zu index.html
//
// ============================================================


// ============================================================
// DATENBANKVERBINDUNG
// ============================================================

require "functions.php";


// ============================================================
// ID HOLEN
// ============================================================

// Die ID kann entweder aus GET oder POST kommen.
//
// GET:
//
// edit.php?id=5
//
// POST:
//
// <input type="hidden" name="id" value="5">
//
// ?? bedeutet:
// Wenn GET["id"] nicht vorhanden ist,
// wird POST["id"] verwendet.
//
// Wenn beides nicht vorhanden ist,
// wird null verwendet.

$id = $_GET["id"] ?? $_POST["id"] ?? null;


// ============================================================
// KEINE ID
// ============================================================

if (!$id) {

    // Ohne ID können wir keinen Score bearbeiten.

    header("Location: index.html");

    exit;
}


// ============================================================
// UPDATE
// ============================================================

// Wenn das Formular abgeschickt wurde,
// ist die HTTP-Methode POST.

if ($_SERVER["REQUEST_METHOD"] === "POST") {


    // ========================================================
    // UPDATE VORBEREITEN
    // ========================================================

    // Wieder werden ?-Platzhalter verwendet.
//
// Dadurch werden die User-Werte sicher eingesetzt.

    $stmt = $dbh->prepare(
        "UPDATE scores
         SET player = ?,
             points = ?,
             played_at = ?,
             avatar = ?
         WHERE id = ?"
    );


    // ========================================================
    // UPDATE AUSFÜHREN
    // ========================================================

    $stmt->execute([

        $_POST["player"],

        $_POST["points"],

        $_POST["played_at"],

        $_POST["avatar"],

        $id

    ]);


    // ========================================================
    // ZURÜCK ZUR LISTE
    // ========================================================

    header("Location: index.html");

    exit;
}


// ============================================================
// SCORE LADEN
// ============================================================

// Jetzt wird der Datensatz mit der ID geladen.
//
// prepare() verhindert SQL-Injection.
//
// ? wird später durch $id ersetzt.

$stmt = $dbh->prepare(
    "SELECT *
     FROM scores
     WHERE id = ?"
);


// SQL ausführen und ID einsetzen.

$stmt->execute([$id]);


// ============================================================
// SCORE HOLEN
// ============================================================

// fetch() holt EINEN Datensatz.
//
// Weil in functions.php
//
// PDO::FETCH_OBJ
//
// eingestellt wurde,
// bekommen wir ein Objekt.
//
// Deshalb können wir später schreiben:
//
// $score->player
// $score->points
// $score->id

$score = $stmt->fetch();


// ============================================================
// SCORE NICHT GEFUNDEN
// ============================================================

// Wenn fetch() nichts gefunden hat,
// ist $score false.

if (!$score) {

    header("Location: index.html");

    exit;
}

?>

<!DOCTYPE html>

<html lang="en">

<head>

    <!-- Zeichencodierung -->

    <meta charset="UTF-8">


    <!-- Responsive Darstellung -->

    <meta
        name="viewport"
        content="width=device-width, initial-scale=1.0"
    >


    <!-- Seitentitel -->

    <title>Edit Score</title>


    <!-- CSS -->

    <link rel="stylesheet" href="./styles.css">

</head>

<body>

<main>

    <!-- ======================================================
         DIESE SEITE EXISTIERT NUR NACH KLICK AUF EDIT
         ====================================================== -->


    <!-- Überschrift -->

    <h1>Edit Score</h1>


    <!-- ======================================================
         FORMULAR
         ====================================================== -->

    <!--
        method="post":
        Beim Absenden werden die Werte per POST
        an dieselbe Datei geschickt.

        Da kein action angegeben wurde,
        wird die aktuelle URL verwendet.
    -->

    <form method="post">


        <!-- ==================================================
             ID
             ================================================== -->

        <!--
            Hidden bedeutet:
            Das Feld wird nicht sichtbar angezeigt.

            Die ID wird aber beim POST mitgeschickt.

            PHP kann sie dann über
            $_POST["id"]
            auslesen.
        -->

        <input
            type="hidden"
            name="id"
            value="<?= $score->id ?>"
        >


        <!-- ==================================================
             PLAYER
             ================================================== -->

        <input
            type="text"
            name="player"
            value="<?= htmlspecialchars($score->player) ?>"
            required
        >


        <!-- ==================================================
             POINTS
             ================================================== -->

        <input
            type="number"
            name="points"
            value="<?= $score->points ?>"
            required
        >


        <!-- ==================================================
             DATE
             ================================================== -->

        <input
            type="date"
            name="played_at"
            value="<?= $score->played_at ?>"
            required
        >


        <!-- ==================================================
             AVATAR
             ================================================== -->

        <input
            type="text"
            name="avatar"
            value="<?= htmlspecialchars($score->avatar ?? '') ?>"
        >


        <!-- Formular absenden -->

        <button type="submit">
            Speichern
        </button>

    </form>


    <!-- ======================================================
         ABBRECHEN
         ====================================================== -->

    <!--
        Einfacher Link zurück zur Liste.
    -->

    <a href="index.html">
        Abbrechen
    </a>

</main>

</body>

</html>