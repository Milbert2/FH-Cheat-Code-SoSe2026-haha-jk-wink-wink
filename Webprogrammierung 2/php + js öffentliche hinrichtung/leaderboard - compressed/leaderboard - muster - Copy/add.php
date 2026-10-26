<?php

require "functions.php";

if ($_SERVER["REQUEST_METHOD"] === "POST") {

    $stmt = $dbh->prepare("INSERT INTO scores (player, points, played_at, avatar) VALUES (?, ?, ?, ?)");
    $stmt->execute([$_POST["player"], $_POST["points"], $_POST["played_at"], $_POST["avatar"]]);
}

header("Location: index.html");
exit;

?>
