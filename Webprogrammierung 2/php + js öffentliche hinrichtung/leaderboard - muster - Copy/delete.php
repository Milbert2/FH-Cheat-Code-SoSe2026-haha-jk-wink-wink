<?php

require "functions.php";

$id = $_GET["id"] ?? null;

if (!$id) {

    header("Location: index.html");
    exit;
}

$stmt = $dbh->prepare(
    "DELETE FROM scores WHERE id = ?"
);

$stmt->execute([
    $id
]);

header("Location: index.html");
exit;

?>
