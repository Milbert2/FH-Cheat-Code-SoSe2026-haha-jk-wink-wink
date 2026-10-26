<?php


ini_set('display_errors', true);

require "config.php";

if ( ! $DB_NAME )

    die (
        'please create config.php, define $DB_NAME, $DSN, $DB_USER, $DB_PASS there. See config_sample.php'
    );

try {

    $dbh = new PDO(
        $DSN,
        $DB_USER,
        $DB_PASS
    );

    $dbh->setAttribute(
        PDO::ATTR_ERRMODE,
        PDO::ERRMODE_EXCEPTION
    );

    $dbh->setAttribute(
        PDO::ATTR_DEFAULT_FETCH_MODE,
        PDO::FETCH_OBJ
    );

} catch (Exception $e) {

    die (
        "Problem connecting to database $DB_NAME as $DB_USER: "
        . $e->getMessage()
    );

}

?>
