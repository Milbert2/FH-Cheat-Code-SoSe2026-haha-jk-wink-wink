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

require "functions.php";

header("Content-Type: application/json");


// Sortierung aus URL
$orderby = $_GET["orderby"] ?? "points";


// Whitelist
$sort = [

    "points" => "points DESC",

    "date" => "played_at DESC",

    "player" => "id ASC"

];


// Ungültige Eingabe
if (!isset($sort[$orderby])) {

    $orderby = "points";

}


// SQL
$sql = "
    SELECT *
    FROM scores
    ORDER BY " . $sort[$orderby];


// Query
$stmt = $dbh->query($sql);


// Daten holen
$scores = $stmt->fetchAll(PDO::FETCH_ASSOC);


// JSON ausgeben
echo json_encode($scores);

?>