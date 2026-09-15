<?php
// ============================================================
// list.php
// ============================================================
//
// READ bleibt bei list.php.
//
// JavaScript ruft:
//
// list.php?orderby=points
//
// auf und bekommt JSON zurück.
//
// ============================================================


// ============================================================
// DATENBANKVERBINDUNG
// ============================================================

// functions.php wird eingebunden.
//
// Dort wird die Datenbankverbindung hergestellt.
//
// Danach steht die PDO-Verbindung
// normalerweise in der Variable $dbh zur Verfügung.

require "functions.php";


// ============================================================
// CONTENT-TYPE
// ============================================================

// Wir teilen dem Browser mit:
// Die Antwort ist JSON.
//
// Dadurch weiß JavaScript, dass es eine JSON-Antwort
// vom Server bekommt.

header("Content-Type: application/json");


// ============================================================
// SORTIERUNG AUS URL
// ============================================================

// $_GET enthält Werte aus der URL.
//
// Bei:
//
// list.php?orderby=points
//
// ist:
//
// $_GET["orderby"] = "points"
//
// ?? bedeutet:
// Falls "orderby" nicht existiert,
// verwende "points" als Standardwert.

$orderby = $_GET["orderby"] ?? "points";


// ============================================================
// WHITELIST
// ============================================================

// Hier werden ALLE erlaubten Sortiermöglichkeiten
// definiert.
//
// Links:
// Wert aus der URL
//
// Rechts:
// Der tatsächlich erlaubte SQL-Teil.

$sort = [

    // Punkte:
    // höchste Punkte zuerst

    "points" => "points DESC",


    // Datum:
    // neuestes Datum zuerst

    "date" => "played_at DESC",


    // Player:
    // laut Aufgabenstellung nach ID aufsteigend

    "player" => "id ASC"

];


// ============================================================
// UNGÜLTIGE EINGABE
// ============================================================

// isset() prüft, ob ein bestimmter Array-Eintrag existiert.
//
// Wenn beispielsweise jemand aufruft:
//
// list.php?orderby=hacked
//
// gibt es keinen Eintrag "hacked" in $sort.
//
// Dann wird wieder "points" verwendet.

if (!isset($sort[$orderby])) {

    $orderby = "points";

}


// ============================================================
// SQL
// ============================================================

// Die SQL-Abfrage wird zusammengesetzt.
//
// Wichtig:
// $orderby wird NICHT direkt in SQL eingesetzt.
//
// Stattdessen wird:
//
// $sort[$orderby]
//
// verwendet.
//
// Dadurch kommt nur ein Wert aus unserer Whitelist
// in das SQL-Statement.
//
// Beispiel:
//
// $orderby = "points"
//
// ergibt:
//
// ORDER BY points DESC

$sql = "
    SELECT *
    FROM scores
    ORDER BY " . $sort[$orderby];


// ============================================================
// QUERY
// ============================================================

// query() führt die SQL-Abfrage aus.
//
// Hier gibt es keine normalen User-Werte,
// die als Parameter eingesetzt werden müssen,
// weil die Sortierung vorher über die Whitelist
// kontrolliert wurde.

$stmt = $dbh->query($sql);


// ============================================================
// DATEN HOLEN
// ============================================================

// fetchAll() holt alle Datensätze.
//
// PDO::FETCH_ASSOC bedeutet:
// Jeder Datensatz wird als assoziatives Array zurückgegeben.
//
// Beispiel:
//
// [
//     "id" => 1,
//     "player" => "Max",
//     "points" => 100
// ]

$scores = $stmt->fetchAll(PDO::FETCH_ASSOC);


// ============================================================
// JSON AUSGEBEN
// ============================================================

// json_encode() wandelt das PHP-Array in JSON um.
//
// JavaScript kann diese Antwort anschließend mit:
//
// response.json()
//
// wieder in JavaScript-Daten umwandeln.

echo json_encode($scores);

?>