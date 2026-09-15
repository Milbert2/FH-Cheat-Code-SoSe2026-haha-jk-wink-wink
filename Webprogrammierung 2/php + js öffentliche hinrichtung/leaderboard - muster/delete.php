<?php

// ============================================================
// delete.php
// ============================================================
//
// Einfachste Variante:
//
// Die ID wird über GET übergeben:
//
// delete.php?id=5
//
// PHP löscht den Datensatz und geht danach zurück.
//
// ============================================================


// ============================================================
// DATENBANKVERBINDUNG
// ============================================================

require "functions.php";


// ============================================================
// ID AUS DER URL HOLEN
// ============================================================

// $_GET enthält Werte aus der URL.
//
// Bei:
//
// delete.php?id=5
//
// ist:
//
// $_GET["id"] = 5
//
// ?? null bedeutet:
// Wenn keine ID vorhanden ist,
// wird null verwendet.

$id = $_GET["id"] ?? null;


// ============================================================
// KEINE ID
// ============================================================

// Wenn keine ID vorhanden ist,
// macht das Script keinen DELETE.

if (!$id) {

    // Zurück zur Startseite

    header("Location: index.html");

    // Script beenden

    exit;
}


// ============================================================
// DELETE AUSFÜHREN
// ============================================================

// prepare() erstellt ein vorbereitetes SQL-Statement.
//
// ? ist ein Platzhalter für die ID.

$stmt = $dbh->prepare(
    "DELETE FROM scores WHERE id = ?"
);


// Die tatsächliche ID wird sicher eingesetzt.

$stmt->execute([
    $id
]);


// ============================================================
// ZURÜCK ZUR LISTE
// ============================================================

header("Location: index.html");

exit;

?>