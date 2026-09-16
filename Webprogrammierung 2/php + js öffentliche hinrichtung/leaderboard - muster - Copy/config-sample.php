<?php
    if ($_SERVER['HTTP_HOST'] == 'users.ct.fh-salzburg.ac.at') {
        $DB_NAME = "leaderboard";
        $DB_USER = "db1_reader";
        $DB_PASS = "geheim!";
        $DSN     = "pgsql:dbname=$DB_NAME;host=localhost";
    } else {
        $DB_NAME = "leaderboard";
        $DB_USER = "👉"; // fill in your local db username here!!
        $DB_PASS = "👉"; // fill in your locals db password here!!
        $DSN     = "pgsql:dbname=$DB_NAME;host=localhost";
    }
?>
