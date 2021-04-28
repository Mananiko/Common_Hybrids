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


if(!isset($_GET["id"]) || empty($_GET["id"])){
    $msg = array();
    $msg['msg'] = "Group is empty";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}

if(!isset($_GET["user_id"]) || empty($_GET["user_id"])){
    $msg = array();
    $msg['msg'] = "User is empty";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}

if(!isset($_GET["password"]) || empty($_GET["password"])){
    $msg = array();
    $msg['msg'] = "Password is empty";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}

$group = $_GET["id"];
$password = $_GET["password"];
$user_id = $_GET["user_id"];

$sql = "SELECT * FROM groups WHERE id = '$group' AND password = '$password'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) == 0) {
    $msg = array();
    $msg['msg'] = "login Failed !!!";
    $msg['status'] = 0;
    echo json_encode($msg);
    die;
}

$sql = "SELECT * FROM content WHERE group_id = '$group'";
$result = mysqli_query($conn, $sql);

if (mysqli_num_rows($result) == 0) {
    $msg = array();
    $msg['msg'] = "";
    $msg['status'] = 2;
    echo json_encode($msg);
    die;
}

$resData = mysqli_fetch_assoc($result);
$dataObj = new stdClass;
$dataObj->objects = ($resData['objects']) ? json_decode($resData['objects']) : '';
$dataObj->cubes = ($resData['cubes']) ? json_decode($resData['cubes']) : '';;
$dataObj->camera = ($resData['camera']) ? json_decode($resData['camera']) : '';;
$dataObj->comments = ($resData['comments']) ? json_decode($resData['comments']) : '';;


$sql = "SELECT * FROM user_moves WHERE group_id = '$group' AND user_id = '$user_id'";
$result = mysqli_query($conn, $sql);

$moves = 0;

if (mysqli_num_rows($result) > 0) {
    $resData = mysqli_fetch_assoc($result);
    $moves = ($resData['moves']) ? $resData['moves'] : 0;
}

$dataObj->moves = $moves;

mysqli_close($conn);

$msg = array();
$msg['msg'] = "";
$msg['status'] = 1;
$msg['data'] = $dataObj;
echo json_encode($msg);
die;

?>