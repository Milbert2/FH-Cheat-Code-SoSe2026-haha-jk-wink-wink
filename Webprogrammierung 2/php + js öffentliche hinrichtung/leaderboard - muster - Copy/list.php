<?php

require "functions.php";

header("Content-Type: application/json");

$orderby = $_GET["orderby"] ?? "points";

$sort = [

    "points" => "points DESC",
    "date" => "played_at DESC",
    "player" => "id ASC"

];

if (!isset($sort[$orderby])) {

    $orderby = "points";

}

$sql = "
    SELECT *
    FROM scores
    ORDER BY " . $sort[$orderby];

$stmt = $dbh->query($sql);

$scores = $stmt->fetchAll(PDO::FETCH_ASSOC);

echo json_encode($scores);

?>
