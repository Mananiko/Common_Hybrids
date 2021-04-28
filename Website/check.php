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

    
if(!isset($_POST["group"]) || empty($_POST["group"])){
    $msg = array();
    $msg['msg'] = "Group name is empty";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}

if (!isset($_POST["email"]) || !filter_var($_POST["email"], FILTER_VALIDATE_EMAIL)) {
  $msg = array();
  $msg['msg'] = "Invalid email format";
  $msg['status'] = 0;
  echo json_encode($msg);
  die;
}

if(!isset($_POST["password"]) || empty($_POST["password"])){
    $msg = array();
    $msg['msg'] = "Password is empty";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}


$group = $_POST["group"];
$emails =  $_POST["email"];
$password = $_POST["password"];


$sql = "SELECT b.id FROM users AS a INNER JOIN groups AS b ON a.group_id = b.id 
       WHERE b.name = '$group' AND  b.password = '$password'  AND  a.email = '$emails'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) == 0) {
    $msg = array();
    $msg['msg'] = "Group not found !!!";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}

$row = mysqli_fetch_assoc($result);
$row_id = $row['id'];

$sql = "UPDATE groups SET isActive = 0 WHERE id = $row_id";

if (mysqli_query($conn, $sql)) {
    
    mysqli_close($conn);
    $msg = array();
    $msg['msg'] = "Status changed successfully !!!";
    $msg['status'] = 1;
    $msg['password'] = $password;
    echo json_encode($msg);
    die;
} else {
    $msg = array();
    $msg['msg'] = "Status not changed !!!";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}



?>