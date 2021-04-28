<?php
$servername = "localhost";
$username = "mananikoDB";
$password = "mananikoDB1@3";
$dbname = "commonhybrid";


// Create connection
$conn = mysqli_connect($servername, $username, $password, $dbname);

// Check connection
if (!$conn) {
  die("Connection failed: " . mysqli_connect_error());
}

if(!isset($_POST["id"]) || empty($_POST["id"])){
    die;
}
$row_id = $_POST["id"];
mysqli_query($conn, "UPDATE groups SET isActive = 0 WHERE id = '$row_id'");

mysqli_close($conn);


die;

?>