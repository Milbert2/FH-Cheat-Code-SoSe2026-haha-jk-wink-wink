<?php

ini_set('display_errors', true);

$lines = $_GET['lines'] ?? 1;



function db_data($lines) {

    require_once 'config.php';

    if (!$DB_NAME) {
        exit('please create config.php, define $DB_NAME, $DSN, $DB_USER, $DB_PASS there. See config_sample.php');
    }

    try {
        $dbh = new PDO($DSN, $DB_USER, $DB_PASS);
        $dbh->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
        $dbh->setAttribute(PDO::ATTR_DEFAULT_FETCH_MODE, PDO::FETCH_OBJ);

        error_log("finished setting up dbh");
    } catch (Exception $e) {
        exit("Problem connecting to database $DB_NAME as $DB_USER: " . $e->getMessage());
    }

    try {
        $no_images = $lines * 4;

        $sql = "SELECT name, image_id
                FROM game_pictures
                ORDER BY RANDOM()
                LIMIT :amount";

        $stmt = $dbh->prepare($sql);
        $stmt->bindValue(':amount', (int)$no_images, PDO::PARAM_INT);
        $stmt->execute();

        $rows = $stmt->fetchAll();

        $grid = [];

        foreach ($rows as $row) {
            $grid[] = [
                "name" => $row->name,
                "image" => "https://picsum.photos/id/" . $row->image_id . "/200/150"
            ];
        }

        return $grid;

    } catch(Exception  $error ) {
        return [];
    }

}

// from the grid, pick $max random questions, but not the same question twice in a row
function pick_questions($grid, $max) {
    $questions = [];
    $last_question = "";

    for ($i = 0; $i < $max; $i++) {
        do {
            $index = array_rand($grid);
            $new_question = $grid[$index]['name'];
        } while ($new_question == $last_question);

        $questions[] = $new_question;
        $last_question = $new_question;
    }

    return $questions;
}

function output_json($grid, $questions) {
    $data = [
        "grid" => $grid,
        "questions" => $questions
    ];
    header('Content-Type: application/json');
    echo json_encode($data, JSON_UNESCAPED_SLASHES);
    exit();
}


$grid = db_data($lines);
$questions = pick_questions($grid, 8);

output_json($grid, $questions);
