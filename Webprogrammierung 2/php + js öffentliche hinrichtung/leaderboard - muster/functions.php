<?php


// ============================================================
// FEHLERANZEIGE
// ============================================================

// PHP soll Fehler anzeigen.
//
// true bedeutet:
// Fehler werden angezeigt.
//
// Während Entwicklung / Live-Coding praktisch.
// In einer echten Produktionsumgebung sollte man
// Fehler normalerweise nicht direkt ausgeben.

ini_set('display_errors', true);


// ============================================================
// CONFIG EINBINDEN
// ============================================================

// config.php enthält die Datenbank-Konfiguration.
//
// Zum Beispiel:
//
// $DSN
// $DB_USER
// $DB_PASS
// $DB_NAME

require "config.php";


// ============================================================
// PRÜFEN, OB DATENBANKNAME VORHANDEN IST
// ============================================================

// Falls $DB_NAME nicht existiert bzw. leer ist,
// wird die Ausführung mit die() beendet.

if ( ! $DB_NAME )

    die (
        'please create config.php, define $DB_NAME, $DSN, $DB_USER, $DB_PASS there. See config_sample.php'
    );


// ============================================================
// DATENBANKVERBINDUNG
// ============================================================

// try/catch wird verwendet,
// damit Fehler bei der Datenbankverbindung
// abgefangen werden können.

try {


    // ========================================================
    // PDO
    // ========================================================

    // PDO stellt die Verbindung zur Datenbank her.

    $dbh = new PDO(
        $DSN,
        $DB_USER,
        $DB_PASS
    );


    // ========================================================
    // FEHLERMODUS
    // ========================================================

    // PDO soll bei Datenbankfehlern Exceptions werfen.

    $dbh->setAttribute(
        PDO::ATTR_ERRMODE,
        PDO::ERRMODE_EXCEPTION
    );


    // ========================================================
    // DEFAULT FETCH MODE
    // ========================================================

    // Standardmäßig sollen Ergebnisse als Objekt
    // zurückgegeben werden.

    //
    // Dadurch funktioniert später z.B.:
    //
    // $score->player
    //
    // $score->points

    $dbh->setAttribute(
        PDO::ATTR_DEFAULT_FETCH_MODE,
        PDO::FETCH_OBJ
    );


} catch (Exception $e) {


    // ========================================================
    // FEHLER BEI DB-VERBINDUNG
    // ========================================================

    // Falls die Verbindung nicht funktioniert,
    // wird eine Fehlermeldung ausgegeben.

    die (
        "Problem connecting to database $DB_NAME as $DB_USER: "
        . $e->getMessage()
    );

}

?>