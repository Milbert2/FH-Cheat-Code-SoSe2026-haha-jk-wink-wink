<?php

// ============================================================
// edit.php
// ============================================================
//
// GET:
//     edit.php?id=5
//
// -> Score laden
// -> Formular anzeigen
//
// POST:
// -> Score aktualisieren
// -> zurück zu index.html
//
// ============================================================

require "functions.php";


// ============================================================
// ID holen
// ============================================================

$id = $_GET["id"] ?? $_POST["id"] ?? null;


// Keine ID
if (!$id) {

    header("Location: index.html");

    exit;
}


// ============================================================
// UPDATE
// ============================================================

if ($_SERVER["REQUEST_METHOD"] === "POST") {

    $stmt = $dbh->prepare(
        "UPDATE scores
         SET player = ?,
             points = ?,
             played_at = ?,
             avatar = ?
         WHERE id = ?"
    );


    $stmt->execute([

        $_POST["player"],

        $_POST["points"],

        $_POST["played_at"],

        $_POST["avatar"],

        $id

    ]);


    // Zurück zur Liste

    header("Location: index.html");

    exit;
}


// ============================================================
// SCORE LADEN
// ============================================================

$stmt = $dbh->prepare(
    "SELECT *
     FROM scores
     WHERE id = ?"
);


$stmt->execute([$id]);


$score = $stmt->fetch();


// Score nicht gefunden

if (!$score) {

    header("Location: index.html");

    exit;
}

?>

<!DOCTYPE html>

<html lang="en">

<head>

    <meta charset="UTF-8">

    <meta
        name="viewport"
        content="width=device-width, initial-scale=1.0"
    >

    <title>Edit Score</title>

    <link rel="stylesheet" href="./styles.css">

</head>

<body>

<main>

    <!-- ======================================================
         DIESE SEITE EXISTIERT NUR NACH KLICK AUF EDIT
         ====================================================== -->

    <h1>Edit Score</h1>


    <form method="post">

        <!-- ID -->
        <input
            type="hidden"
            name="id"
            value="<?= $score->id ?>"
        >


        <input
            type="text"
            name="player"
            value="<?= htmlspecialchars($score->player) ?>"
            required
        >


        <input
            type="number"
            name="points"
            value="<?= $score->points ?>"
            required
        >


        <input
            type="date"
            name="played_at"
            value="<?= $score->played_at ?>"
            required
        >


        <input
            type="text"
            name="avatar"
            value="<?= htmlspecialchars($score->avatar ?? '') ?>"
        >


        <button type="submit">
            Speichern
        </button>

    </form>


    <a href="index.html">
        Abbrechen
    </a>

</main>

</body>

</html>