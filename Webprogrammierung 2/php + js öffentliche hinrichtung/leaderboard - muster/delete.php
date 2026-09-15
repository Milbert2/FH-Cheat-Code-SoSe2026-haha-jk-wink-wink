<?php

// ============================================================
// delete.php
// ============================================================
//
// Einfachste Variante:
// Der ID wird über GET übergeben:
//
// delete.php?id=5
//
// PHP löscht den Datensatz und geht danach zurück.
//
// ============================================================

require "functions.php";


// ID aus der URL holen
$id = $_GET["id"] ?? null;


// Wenn keine ID vorhanden ist -> zurück
if (!$id) {

    header("Location: index.html");
    exit;
}


// DELETE ausführen
$stmt = $dbh->prepare(
    "DELETE FROM scores WHERE id = ?"
);

$stmt->execute([
    $id
]);


// Zurück zur Liste
header("Location: index.html");
exit;

?>