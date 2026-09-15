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

require "functions.php";


// Nur POST erlauben

if ($_SERVER["REQUEST_METHOD"] === "POST") {

    $stmt = $dbh->prepare(
        "INSERT INTO scores
        (player, points, played_at, avatar)
        VALUES (?, ?, ?, ?)"
    );


    $stmt->execute([

        $_POST["player"],

        $_POST["points"],

        $_POST["played_at"],

        $_POST["avatar"]

    ]);
}


// Zurück zur Liste

header("Location: index.html");

exit;

?>